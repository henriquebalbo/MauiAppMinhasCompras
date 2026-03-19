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




        // MÉTODO PARA ATUALIZAR UM PRODUTO 


        public Task<List<Produto>> Update(Produto p) // Retorna uma tarefa contendo a lista do produto atualizada 
        {
            
            string sql = "UPDATE Produto SET Descricao=?, Quantidade=?, Preco=? WHERE Id=?";  // String com o comando SQL padrão para atualização e 
                                                                                              // o uso de interrogação para indicar espaços reservados                                                                                  // Executamos a query de forma assíncrona. 
                                                                                              
            return _db.QueryAsync<Produto>(sql, p.Descricao, p.Quantidade, p.Preco, p.Id);// os valores que devem substituir cada interrogação
                                                                                          // 1 Descrição, 2 Quantidade, 3 Preço e 4 o ID para saber qual linha altera
        }

        // MÉTODO PARA EXCLUIR UM PRODUTO 
        
        public Task<int> Delete(int id) // Task<int> indica o número de registros removidos 
        {

            return _db.Table<Produto>().DeleteAsync(i => i.Id == id); // Acessar a tabela Produto e deletar o item onde o ID do banco seja igual ao ID que foi oferecido por parâmetro
        }

        
    }

}
