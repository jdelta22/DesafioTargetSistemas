namespace Desafio3;

class Program
{
    public static void Main()
    {
        DateTime hoje = DateTime.Today;

        Console.Write("Digite o valor base: ");
        decimal valor = decimal.Parse(Console.ReadLine()!);

        Console.Write("Digite a Data de Vencimento: DD/MM/AAAA ");
        DateTime vencimento = DateTime.Parse(Console.ReadLine()!);

        TimeSpan dias_vencidos = hoje - vencimento ;
        
        decimal juros = 0.025m * dias_vencidos.Days * valor;

        Console.WriteLine(
            $"Valor Original = {valor}\n" +
            $"Data de vencimento: {vencimento}\n" +
            $"Dias vencidos: {dias_vencidos.Days}\n" +
            $"Juros referentes a {dias_vencidos.Days} dias a 2,5% de multa= {juros}\n" +
            $"Valor Final = {valor + juros}"
            );
    }
}
