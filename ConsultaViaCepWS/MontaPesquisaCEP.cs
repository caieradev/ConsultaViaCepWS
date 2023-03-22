using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ConsultaViaCepWS;
public class MontaPesquisaCEP
{
    public string? logradouro { get; set; } = "";
    public string? cidade { get; set; } = "";
    public string? estado { get; set; } = "";
    public string cep { get; set; } = "";

    public MontaPesquisaCEP(string cep) { this.cep = cep; }
    public MontaPesquisaCEP(string? estado, string? cidade, string? logradouro)
    {
        this.estado = estado;
        this.cidade = cidade;
        this.logradouro = logradouro;
    }

    public static void ValidaCep(String cep){
        if (string.IsNullOrEmpty(cep.Trim())) 
            throw new ArgumentException("O cep informado não pode ser nulo ou vazio");

        cep = RemoveSpecialAndNonDecimalChars(cep);

        if (cep.Length > 8)
            throw new ArgumentException("CEP fora do formato");

        if (cep.Length < 8)
            throw new ArgumentException("CEP faltando numeros");
    }

    public string ConsultaPorCEP()
    {
        StringBuilder sb = new StringBuilder();

        MontaPesquisaCEP.ValidaCep(this.cep);

        sb.Append("/");
        sb.Append(HttpUtility.UrlEncode(this.cep, Encoding.UTF8));
        
        return sb.ToString();
    }

    public string ConsultaPorLogradouro()
    {
        var sb = new StringBuilder();

        if (!string.IsNullOrEmpty(estado))
            sb.Append("/" + HttpUtility
                .UrlEncode(estado, Encoding.UTF8));

        if (!string.IsNullOrEmpty(cidade))
        {
            cidade = HttpUtility.UrlEncode(cidade, Encoding.UTF8);
            cidade = cidade.Replace("+", "%20");
            sb.Append("/" + cidade);
        }

        if (!string.IsNullOrEmpty(logradouro))
        {
            logradouro = HttpUtility.UrlEncode(logradouro, Encoding.UTF8);
            logradouro = logradouro.Replace("+", "%20");
            sb.Append("/" + logradouro);
        }

        return sb.ToString();
    }

    public static string RemoveSpecialAndNonDecimalChars(string input)
    {
        var normalized = new string(input.Where(c => char.IsDigit(c) || char.IsWhiteSpace(c)).ToArray());

        var result = new StringBuilder();

        foreach (char c in normalized)
            if (char.IsDigit(c) || c == '.')
                result.Append(c);
                
        return result.ToString();
    }

}