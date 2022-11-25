using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RestSharp;
using Serilog;

namespace Application.MainModule.RestSharp;

public class RestWsHelper : IRestWsHelper
{
    private readonly ILogger _logger;
    private readonly string _token;

    public RestWsHelper(IServiceProvider serviceProvider)
    {
        _logger = Log.Logger;
        var httpContextAccessor = serviceProvider.GetService<IHttpContextAccessor>();
        var headers = httpContextAccessor?.HttpContext?.Request.Headers;
        if (headers != null) _token = headers["Authorization"];
    }

    public async Task<T> GetMethod<T>(string baseUrl, string pathApi, Dictionary<string, string> parameters = null)
    {
        _logger.Information($"Service URL => {baseUrl}{pathApi}");
        _logger.Information($"Parameter => {JsonConvert.SerializeObject(parameters)}");

        var (client, request) = BuildRestRequest(baseUrl, pathApi, (int) Method.Get);
        ParametersMap(request, parameters);
        var restResponse = await client.ExecuteAsync<T>(request);
        return restResponse.Data;
    }

    public async Task<T> PostMethod<T>(string baseUrl, string pathApi, object body)
    {
        var (client, request) = BuildRestRequest(baseUrl, pathApi, (int) Method.Post);
        request.AddBody(body);
        var restResponse = await client.ExecuteAsync<T>(request);
        return restResponse.Data;
    }

    private static void ParametersMap(RestRequest request, Dictionary<string, string> parameters)
    {
        if (parameters is null) return;

        foreach (var (key, value) in parameters)
            request.AddQueryParameter(key, value);
    }

    private (RestClient client, RestRequest request) BuildRestRequest(string baseurl, string action, int method)
    {
        RestClient client = new RestClient(baseurl);
        RestRequest request = new RestRequest(action, (Method) method);

        if (!string.IsNullOrEmpty(_token))
            request.AddHeader("authorization", _token);

        request.RequestFormat = DataFormat.Json;

        return (client, request);
    }
}