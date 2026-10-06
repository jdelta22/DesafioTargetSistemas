namespace Desafio1;

using System.Diagnostics.Contracts;
using System.Dynamic;
using System.IO;
using System.Text.Json;


public class Produto
{
    public int codigoProduto{get; set;}
    public string descricaoProduto{get; set;} = "";
    public int estoque{get; set;}
}
public class EstoqueData
{
    public List<Produto> estoque {get; set;} = new();
}

public class JsonHelper
{
    private readonly string _caminho = "estoque.json";

    public EstoqueData Ler()
    {
        if(!File.Exists(_caminho))
            return new EstoqueData();

        string json = File.ReadAllText(_caminho);

        return JsonSerializer.Deserialize<EstoqueData>(json) ?? new EstoqueData(); 
    }

    public void Salvar(EstoqueData dados)
    {
        string json = JsonSerializer.Serialize(dados, new JsonSerializerOptions
        {
            WriteIndented = true
        });

        File.WriteAllText(_caminho, json);
    }
}

public class EstoqueService
{
    private readonly JsonHelper __json;

    public EstoqueService()
    {
        __json = new JsonHelper();
    }
    
    public List<Produto> ListarProdutos()
    {
        var dados = __json.Ler();
        return dados.estoque;
    }
    
    public void CadastrarProduto(Produto produto)
    {
        var dados = __json.Ler();
        if (dados.estoque.Any(p => p.codigoProduto == produto.codigoProduto))
        {
            Console.WriteLine("Produto já cadastrado");
            return;
        }

        dados.estoque.Add(produto);

        __json.Salvar(dados);

        Console.WriteLine($"Produto {produto.descricaoProduto} cadastrado com sucesso.");
    }

    public void AdicionarEstoque(int codigo, int quantidade)
    {
        var dados = __json.Ler();
        var produto = dados.estoque.FirstOrDefault(p => p.codigoProduto == codigo);

        if (produto == null)
        {
            Console.WriteLine("Produto não encontrado.");
            return;
        }

        produto.estoque += quantidade;

        __json.Salvar(dados);
        Console.WriteLine($"Produto {produto.descricaoProduto}, quantidade atual: {produto.estoque}");
    }

    public void RemoverEstoque(int codigo, int quantidade)
    {
        var dados = __json.Ler();
        var produto = dados.estoque.FirstOrDefault(p => p.codigoProduto == codigo);
        
        if (produto == null)
        {
            Console.WriteLine("Produto não encontrado.");
            return;
        }

            produto.estoque -= quantidade;

        __json.Salvar(dados);
        Console.WriteLine($"Produto {produto.descricaoProduto}, quantidade atual: {produto.estoque}");
    }
}
class Program
{
    public static void Main()
    {
        var estoque = new EstoqueService();

        while (true)
        {
            Console.WriteLine("\n=== CONTROLE DE ESTOQUE ===");
            Console.WriteLine("1 - Cadastrar produto");
            Console.WriteLine("2 - Adicionar estoque");
            Console.WriteLine("3 - Remover estoque");
            Console.WriteLine("4 - Mostrar produtos");
            Console.WriteLine("5 - Sair");


            Console.Write("Escolha: ");
            string? opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1": // Cadastrar Produto
                    Console.WriteLine("Cadastrar novo produto");
                    Console.Write("Código: ");
                    int codigo = int.Parse(Console.ReadLine()!);

                    Console.Write("Descrição: ");
                    string descricao = Console.ReadLine()!;

                    Console.Write("Quantidade: ");
                    int quantidade = int.Parse(Console.ReadLine()!);

                    estoque.CadastrarProduto(new Produto
                    {
                        codigoProduto = codigo,
                        descricaoProduto = descricao,
                        estoque = quantidade
                    });
                    break;

                case "2": // Adicionando estoque
                    Console.WriteLine("Adicionar estoque a produto");
                    Console.Write("Código do produto: ");
                    int codigoAdd = int.Parse(Console.ReadLine()!);

                    Console.Write("Quantidade a Adicionar: ");
                    int quantidadeAdd = int.Parse(Console.ReadLine()!);

                    estoque.AdicionarEstoque(
                        codigoAdd,
                        quantidadeAdd
                    );
                    break;
                
                case "3": // Removendo estoque
                    Console.WriteLine("Adicionar estoque a produto");
                    Console.Write("Código do produto: ");
                    int codigoRm = int.Parse(Console.ReadLine()!);

                    Console.Write("Quantidade a Remover: ");
                    int quantidadeRm = int.Parse(Console.ReadLine()!);

                    estoque.RemoverEstoque(
                        codigoRm,
                        quantidadeRm
                    );
                    break;

                case "4":
                    var produtos = estoque.ListarProdutos();
                    foreach(var p in produtos)
                    {
                        Console.WriteLine(
                            $"Código:{p.codigoProduto} |" +
                            $"Descrição:{p.descricaoProduto} |" +
                            $"Estoque:{p.estoque}"
                        );
                    }
                    break;

                case "5":
                    return;
                
                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }
        }
    }
}