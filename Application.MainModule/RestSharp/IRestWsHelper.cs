namespace Application.MainModule.RestSharp;

public interface IRestWsHelper
{
    Task<T> GetMethod<T>(string baseUrl, string pathApi, Dictionary<string, string> parameters = null);
    Task<T> PostMethod<T>(string baseUrl, string pathApi, object body);
}