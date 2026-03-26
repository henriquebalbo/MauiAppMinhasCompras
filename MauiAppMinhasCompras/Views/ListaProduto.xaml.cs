using MauiAppMinhasCompras.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MauiAppMinhasCompras.Views;

public partial class ListaProduto : ContentPage
{
    ObservableCollection<Produto> lista = new ObservableCollection<Produto>(); //Tem tudo que a lista generica tem, mas interage melhor com a interface gráfica,
                                                                               //pois é notificada quando há alterações na coleção, permitindo que a UI seja atualizada automaticamente.

    public ListaProduto()
    {
        InitializeComponent();

        lst_produtos.ItemsSource = lista; // Define a fonte de dados da ListView como a coleção "lista"

    }

    protected async override void OnAppearing() // sempre é chamado quando a tela aparece
    {
        try
        {
            lista.Clear(); // Limpa a coleção "lista" para remover os itens anteriores antes de adicionar os produtos atualizados do banco de dados.
            
            List<Produto> tmp = await App.Banco.GetAll(); // Obtém uma lista de produtos do banco de dados usando um método assíncrono "GetAll" definido na classe "Banco" do aplicativo.

            tmp.ForEach(i => lista.Add(i)); // Adiciona cada item da lista temporária à coleção "lista", o que atualizará automaticamente a ListView na interface gráfica.

        }

        catch (Exception ex)
        {

            await DisplayAlert("Erro", ex.Message, "OK");
        }


    }

    //Agenda 6 - Relatório por Categoria
    private async void ExibirRelatorioPorCategoria()
    {
        List<Produto> todos = await App.Banco.GetAll();

        // Agrupa por categoria e soma o Total de cada grupo
        var relatorio = todos.GroupBy(p => p.Categoria)
                             .Select(g => new {
                                 Categoria = g.Key ?? "Sem Categoria",
                                 TotalGasto = g.Sum(p => p.Total)
                             });

        string mensagem = "Gasto por Categoria:\n";
        foreach (var item in relatorio)
        {
            mensagem += $"{item.Categoria}: {item.TotalGasto:C}\n";
        }

        await DisplayAlert("Relatório", mensagem, "OK");
    }

    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            Navigation.PushAsync(new Views.NovoProduto());

        }
        catch (Exception ex)
        {
            DisplayAlert("Problema", ex.Message, "OK");
        }
    }

    private async void txt_search_TextChanged(object sender, TextChangedEventArgs e)
    {
        try
        {

            string q = e.NewTextValue; // adicona o novo valor de texto à variável q

            lista.Clear(); // Limpa a coleção "lista" para remover os itens anteriores antes de adicionar os resultados da nova pesquisa

            List<Produto> tmp = await App.Banco.Search(q); // Obtém uma lista de produtos do banco de dados usando um método assíncrono search definido na classe "Banco" do aplicativo.

            tmp.ForEach(i => lista.Add(i)); //busca todo o resultado da pesquisa e adiciona à coleção "lista", o que atualizará automaticamente a ListView na interface gráfica.
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", ex.Message, "OK");

        }
    }
    private void ToolbarItem_Clicked_1(object sender, EventArgs e)
    {
        double soma = lista.Sum(i => i.Total); //declara variavel soma para cada item na lista, quero somar através do total

        string msg = $"O valor total é {soma:C}"; // formata a string para exibir o valor total em formato de moeda, para determinar o símbolo monetário e a formatação adequada.

        DisplayAlert("Total dos produtos", msg, "OK");
    }

    // inicio da Agenda 5 - Botão Excluir
    private async void MenuItem_Clicked(object sender, EventArgs e)
    {
        try
        {

            MenuItem selecionado = sender as MenuItem; // Declara uma variável selecionado do tipo MenuItem e
                                                       // atribui ao objeto sender, que é o elemento que acionou o evento de clique.

            Produto p = selecionado.BindingContext as Produto;// Declara uma variável p do tipo Produto, que é o produto associado ao item de menu clicado

            // O BindingContext é uma propriedade que contém o contexto de dados associado a um elemento da interface do usuário.
            // Neste caso, o produto associado ao item de menu é obtido a partir do BindingContext do MenuItem selecionado.

            bool confirmacao = await DisplayAlert(
                "Confirmação", $"Deseja excluir o produto {p.Descricao}?", "Sim", "Não"); // Exibe um alerta de confirmação para o usuário, perguntando se ele deseja excluir o produto selecionado. O resultado da escolha do usuário é armazenado na variável confirmacao.
                                                                                          //variavel confirmacao é do tipobool, ou seja, recebe true se o usuário clicar em "Sim" e false se clicar em "Não".

            if (confirmacao) // Verifica se a variável confirmacao é verdadeira, ou seja, se o usuário confirmou a exclusão do produto
            {
                await App.Banco.Delete(p.Id);
                lista.Remove(p); // Remove o produto da coleção lista, o que atualizará automaticamente a ListView na interface gráfica

            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", ex.Message, "OK");
        }

    }

    private void lst_produtos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        try
        {
            Produto p = e.SelectedItem as Produto; // Declara uma variável p do tipo Produto e a atribui ao item selecionado na ListView,
                                                   // que é obtido a partir da propriedade SelectedItem do evento.

            Navigation.PushAsync(new Views.EditarProduto // Navega para a página de edição de produto,
                                                         // passando o produto selecionado como parâmetro para a nova página
            {
                BindingContext = p, // Define o BindingContext da nova página como o produto selecionado,
                                    // permitindo que os dados do produto sejam exibidos e editados na interface gráfica da página de edição

            });

        }
        catch (Exception ex)
        {
            DisplayAlert("Erro", ex.Message, "OK");
        }
    }

    // Agenda 6 - Botão Relatório por Categoria
    private void ToolbarItem_Clicked_2(object sender, EventArgs e)
    {
        ExibirRelatorioPorCategoria();
    }

    // criar o evento que é disparado quando o usuário muda a categoria no seletor de categoria
    private async void pck_filtro_categoria_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string categoriaSelecionada = pck_filtro_categoria.SelectedItem.ToString();

            lista.Clear();
            List<Produto> tmp;

            if (categoriaSelecionada == "Todos")
            {
                // Se escolher "Todos", busca tudo do banco
                tmp = await App.Banco.GetAll();
            }
            else
            {
                // Caso contrário, usa o novo método de busca por categoria
                tmp = await App.Banco.SearchByCategoria(categoriaSelecionada);
            }

            // Atualiza a ObservableCollection que está ligada à ListView
            tmp.ForEach(i => lista.Add(i));
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", ex.Message, "OK");
        }
    }


}