namespace ConsultaViaCepWS;

public class Program
{
    public static async Task Main(string[] args)
    {
        var cvc = new ConsultaViaCEP();

        Console.Write("Entre os dados do endereço desejado:\nCEP: ");
        var cep = Console.ReadLine()?.Trim();

        try
        {

            if (!string.IsNullOrWhiteSpace(cep))
            {
                var pesquisa = new MontaPesquisaCEP(cep);

                var consulta = pesquisa.ConsultaPorCEP();

                var response = await cvc.SubmeteConsultaCEPAsync(consulta);

                if (response == null)
                {
                    throw new Exception("Não foi possível realizar a consulta");
                }

                Console.WriteLine(response.ToString());
            }
            else
            {
                Console.Write("Estado: ");
                var estado = Console.ReadLine()?.Trim();

                Console.Write("Cidade: ");
                var cidade = Console.ReadLine()?.Trim();

                Console.Write("Logradouro: ");
                var logradouro = Console.ReadLine()?.Trim();

                var pesquisa = new MontaPesquisaCEP(estado, cidade, logradouro);

                var consulta = pesquisa.ConsultaPorLogradouro();
                var response = await cvc.SubmeteConsultaLogradouroAsync(consulta);

                if (response == null || !response.Any())
                {
                    throw new Exception("Não foi possível realizar a consulta");
                }

                foreach (var obj in response)
                {
                    Console.WriteLine(obj.ToString());
                }
            }
        }
        catch(Exception ex)
        {
            Console.Error.WriteLine(ex.Message);
        }
    }
}
