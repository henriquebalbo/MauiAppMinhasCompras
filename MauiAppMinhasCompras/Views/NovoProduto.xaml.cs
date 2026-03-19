using MauiAppMinhasCompras.Models;
using System.Threading.Tasks;

namespace MauiAppMinhasCompras.Views;

public partial class NovoProduto : ContentPage
{
	public NovoProduto()
	{
		InitializeComponent();
	}

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            Produto p = new Produto
            {
                Descricao = txt_descricao.Text,
                Quantidade = Convert.ToDouble(txt_quantidade.Text),
                Preco = Convert.ToDouble(txt_preco.Text)

            };

            await App.Banco.Insert(p);
            await DisplayAlert("Agenda 3", "Registro Inserido com Sucesso!", "OK");

        }
        catch (Exception ex)
        {
            DisplayAlert("Problema", ex.Message, "OK");
        }


    }

    private void ToolbarItem_Clicked_1(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Views.ListaProduto());
    }
}