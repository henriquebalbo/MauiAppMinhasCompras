using MauiAppMinhasCompras.Models;

namespace MauiAppMinhasCompras.Views;

public partial class EditarProduto : ContentPage
{
	public EditarProduto()
	{
		InitializeComponent();
	}

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            Produto produto_anexo = BindingContext as Produto; // Declara uma variável produto_anexo do tipo Produto
                                                               // e a atribui ao contexto de vinculação atual da página,
                                                               // que é o produto que está sendo editado
            Produto p = new Produto
            {
                Id = produto_anexo.Id, // Atribui o ID do produto_anexo ao novo objeto p para
                                       // garantir que o produto existente seja atualizado corretamente no banco de dados
                Descricao = txt_descricao.Text,
                Quantidade = Convert.ToDouble(txt_quantidade.Text),
                Preco = Convert.ToDouble(txt_preco.Text),
                Categoria = pck_categoria.SelectedItem.ToString() // Agenda 6 - Captura a categoria

            };

            await App.Banco.Update(p); //Atualiza o produto no banco de dados usando um método update
                                       //definido na classe Banco do aplicativo

            await DisplayAlert("Agenda 5", "Registro Atualizado com Sucesso!", "OK");
           

        }
        catch (Exception ex)
        {
           await DisplayAlert("Problema", ex.Message, "OK");
        }

    }

    private void ToolbarItem_Clicked_1(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Views.ListaProduto()); //  Volta para a página anterior (ListaProduto) após a atualização do produto
    }
}