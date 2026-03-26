using MauiAppMinhasCompras.Models;
using SQLite;

namespace MauiAppMinhasCompras.Helpers
{
    
    public class SQLiteDatabaseHelper // Classe para auxiliar todas as operações com o BD
    {
        
        readonly SQLiteAsyncConnection _db; // Variável apenas de leitura que mantém a conexão com o arquivo do banco

        
        public SQLiteDatabaseHelper(string dbPath)
        {
            
            _db = new SQLiteAsyncConnection(dbPath); // Abre ou cria o arquivo do BD no caminho (dbPath)

            
            _db.CreateTableAsync<Produto>().Wait(); // Cria a tabela Produto e o .Wait() força o programa a esperar antes de prosseguir
          
        }

        // MÉTODO PARA INSERIR UM PRODUTO
       
        public Task<int> Insert(Produto p)  // Task<int> é uma tarefa
        {
            
            return _db.InsertAsync(p); // Executa o comando Isert usando o produto (p)

        }

        
        // MÉTODO PARA LISTAR OS ITENS


        public Task<List<Produto>> GetAll()// Retorna uma tarefa com a Lista Produto 
        {
           
            return _db.Table<Produto>().ToListAsync();  // Faz um select na tabela Produto e retorna uma Lista
        }


        public Task<List<Produto>> Search(string q) // Retorna uma tarefa com a Lista Produto 
        {

            string sql = "SELECT * FROM Produto WHERE Descricao LIKE '%" + q + "%'";        

            return _db.QueryAsync<Produto>(sql);
        }

        //Agenda 06 - Criar novo método para o filtro de pesquisa por categoria
        public Task<List<Produto>> SearchByCategoria(string categoria)
        {
            // Retorna apenas os produtos onde a categoria coincide com a selecionada
            return _db.Table<Produto>().Where(i => i.Categoria == categoria).ToListAsync();
        }


        // MÉTODO PARA ATUALIZAR UM PRODUTO 

        //Alerado Agenda - método Update foi modificado para retornar uma lista atualizada do produto, em vez de um inteiro indicando o número de registros afetados.


        public Task<List<Produto>> Update(Produto p) // Retorna uma tarefa contendo a lista do produto atualizada 
        {
            
            string sql = "UPDATE Produto SET Descricao=?, Quantidade=?, Preco=?, Categoria=? WHERE Id=?";  // String com o comando SQL padrão para atualização e 
                                                                                              // o uso de interrogação para indicar espaços reservados                                                                                  // Executamos a query de forma assíncrona. 
                                                                                              
            return _db.QueryAsync<Produto>(sql, p.Descricao, p.Quantidade, p.Preco,p.Categoria, p.Id);// os valores que devem substituir cada interrogação
                                                                                          // 1 Descrição, 2 Quantidade, 3 Preço e 4 o ID para saber qual linha altera
        }

        // MÉTODO PARA EXCLUIR UM PRODUTO 
        
        public Task<int> Delete(int id) // Task<int> indica o número de registros removidos 
        {

            return _db.Table<Produto>().DeleteAsync(i => i.Id == id); // Acessar a tabela Produto e deletar o item onde o ID do banco seja igual ao ID que foi oferecido por parâmetro
        }

        // Agenda 06 - Criar novo metodo para o filtro de pesquisa por categoria
        public Task<List<Produto>> GetByCategoria(string categoria)
        {
            return _db.Table<Produto>().Where(i => i.Categoria == categoria).ToListAsync();



        }        }

}
