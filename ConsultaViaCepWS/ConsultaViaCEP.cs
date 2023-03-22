using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConsultaViaCepWS;

public class ConsultaViaCEP {
    public static readonly string urlbase = "https://viacep.com.br/ws";

    private HttpRequestMessage? httpRequest;
    private HttpResponseMessage? httpResponse;

    public ConsultaViaCEP(){
        this.httpRequest = null;
        this.httpResponse = null;
    }

    public HttpRequestMessage? GetHttpRequest() {
        return this.httpRequest;
    }

    public HttpResponseMessage? GetHttpResponse() {
        return this.httpResponse;
    }

    public static string MontaConsultaHTTP(string urlString) {
        var sb = new StringBuilder();

        sb.Append(urlbase);
        sb.Append(urlString);
        sb.Append("/json");

        return sb.ToString();
    }

    public async Task<ResultadoPesquisaCEP?> SubmeteConsultaCEPAsync(string urlString) {
        var url = ConsultaViaCEP.MontaConsultaHTTP(urlString);

        try 
        {
            var httpClient = new HttpClient();
            httpClient.Timeout = TimeSpan.FromMinutes(1);

            httpRequest = new HttpRequestMessage(HttpMethod.Get, url);
            httpResponse = await httpClient.SendAsync(httpRequest);

            var json = await httpResponse.Content.ReadAsStringAsync();
            var objResp = JsonSerializer.Deserialize<ResultadoPesquisaCEP>(json);

            return objResp;
        } 
        catch (IOException e) 
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<List<ResultadoPesquisaCEP>?> SubmeteConsultaLogradouroAsync(string urlString) {
        var url = ConsultaViaCEP.MontaConsultaHTTP(urlString);

        try 
        {
            var httpClient = new HttpClient();
            httpClient.Timeout = TimeSpan.FromMinutes(1);

            httpRequest = new HttpRequestMessage(HttpMethod.Get, url);
            httpResponse = await httpClient.SendAsync(httpRequest);

            var json = await httpResponse.Content.ReadAsStringAsync();
            var listResponse = JsonSerializer.Deserialize<List<ResultadoPesquisaCEP>>(json);

            return listResponse;
        } 
        catch (IOException e) 
        {
            throw new Exception(e.Message);
        }
    }
}
