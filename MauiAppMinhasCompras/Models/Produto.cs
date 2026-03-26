
using SQLite; // Importa a biblioteca SQLite 

namespace MauiAppMinhasCompras.Models
{

    public class Produto  // classe que será convertid em uma tabela no BD
    {

        //Fazendo a validação da Model - Agenda 5

        string _descricao; // Variável privada para armazenar a descrição do produto

        double _quantidade; // Variável privada para armazenar a quantidade do produto

        double _preco; // Variável privada para armazenar o preço do produto


        [PrimaryKey, AutoIncrement] // PrimaryKey define chave única e AutoIncrement gera o número do ID automaticamente
        public int Id { get; set; }


        public string Descricao { // propriedade que armazena o nome do produto 

            get => _descricao; // Retorna o valor da  _descricao
            set
            {
                if (value == null)
                {
                    throw new Exception("A descrição deve ser preenchida!"); // Lança uma exceção se o valor for nulo
                }
                _descricao = value;
            }
        }


        public double Preco { // propriedade que armazena o valor unitário

            get => _preco; // Retorna o valor do preço
            set
            {
                if (value <= 0) //Preço nõ pode ser zero ou negativo
                {
                    throw new Exception("O preço deve ser maior que zero"); // Lança uma exceção se o valor menor ou igual a zero
                }
                _preco = value;
            }
        }
        public double Quantidade { // propriedade que armazena a quantidade

            get => _quantidade; // Retorna o valor da quantidade
            set
            {
                if (value <= 0) //Quantidade naõ pode ser menor ou igual a zero

                    throw new Exception("A quantidade deve ser maior que zero"); // Lança uma exceção se o valor menor ou igual a zero

                _quantidade = value;
            }
        }

        //Agenda 6 - Adicionando a categoria do produto
        public string Categoria { get; set; } // propriedade que armazena a categoria do produto
        public double Total => Quantidade * Preco; // propriedade calculada que retorna o valor total do produto (preço multiplicado pela quantidade)
    }
    
}
    

        
    
