namespace Desafio1;

using System.Diagnostics.Contracts;
using System.IO;
using System.Text.Json;

class Program
{
    public class Venda
    {
        public string vendedor{get; set; }
        public decimal valor{get; set; }
    }
    
    public class Dados
    {
        public List<Venda> vendas {get; set; }       
    }



    public static void Main()
    {
        string json = File.ReadAllText("vendas.json");
        Dados dados = JsonSerializer.Deserialize<Dados>(json);

        var venda_por_vendedor = dados.vendas.GroupBy(venda => venda.vendedor);

        Console.WriteLine("Calculando o total de comissões para os vendedores:");
        Console.WriteLine($"--------------------------------------------");
        foreach (var grupo in venda_por_vendedor)
        {
            Console.WriteLine($"Vendedor: {grupo.Key}");
            decimal Totalcomissoes = grupo.Sum(venda =>
            {
                if (venda.valor >= 100m)
                {
                    if (venda.valor < 500m)
                    {
                        return venda.valor * 0.01m;
                    } else 
                    {
                        return venda.valor * 0.05m;
                    }
                }
                else
                {
                    return venda.valor * 0m;
                }
            });
            Console.WriteLine($"Total de Comissões = R$ {Totalcomissoes:F2}");
            Console.WriteLine($"--------------------------------------------");

        }
    }
}