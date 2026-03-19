using MauiAppMinhasCompras.Helpers;

namespace MauiAppMinhasCompras
{
   
    public partial class App : Application
    {
        //Início da Agenda 3

        static SQLiteDatabaseHelper _banco; //declara um campo estático

        public static SQLiteDatabaseHelper Banco //propriedade (somente leitura) estática para acessar o banco de dados
        {
            get //método
            {
                if (_banco == null) //verifica se o campo for nulo, se não tiver objeto no campo
                {

                    string caminho = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "banco_sqlite_compras.db3");

                    // caminho de onde os arquivos do meu aplicativo são armazenados, usando o nome banco_sqlite_compras.db3

                    _banco = new SQLiteDatabaseHelper(caminho); // instancia o objeto
                }
                return _banco; //retorna objeto o campo _banco
            }
        }

        public App()
        {
            
            InitializeComponent(); // Método que inicia o arquivo XAML os componentes da interface
                                  
                        
            MainPage = new NavigationPage(new Views.ListaProduto()); // Define a tela principal do app como uma NavigationPage
                                                                     // carrega a tela de ListaProduto, pasta Views
        }
    }
}