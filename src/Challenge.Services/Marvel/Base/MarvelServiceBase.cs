using System.Net.Http;
using System.Threading.Tasks;
using Challenge.Services.Marvel.Exceptions;
using Challenge.Services.Marvel.Model;

namespace Challenge.Services.Marvel.Base
{
    public abstract class MarvelServiceBase<T> where T : class, new()
    {
        private const string BaseUrl = "https://gateway.marvel.com:443/v1/public";
        private readonly HttpClientHandler _httpClientHandler;
        private readonly IMarvelAuthenticatorService _authenticator;

        protected MarvelServiceBase(HttpClientHandler httpClientHandler, IMarvelAuthenticatorService authenticator)
        {
            _httpClientHandler = httpClientHandler;
            _authenticator = authenticator;
        }

        protected async Task<MarvelResult<T>> HttpCall(string endpoint, string queryString)
        {
            using var client = _httpClientHandler == null ? new HttpClient() : new HttpClient(_httpClientHandler);
            var authInfo = _authenticator.GetAuthInfo();
            var requestUri = $"{BaseUrl}/{endpoint}?{queryString}&{authInfo}";

            var response = await client.GetAsync(requestUri);
            var json = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<MarvelResult<T>>(json) ?? new MarvelResult<T>();
            }

            throw new MarvelException($"Response {response.StatusCode} - {response.ReasonPhrase}");
        }

        protected virtual string GetQueryString(int limit, int offset)
        {
            var queryString = System.Web.HttpUtility.ParseQueryString(string.Empty);

            if (limit > 0)
            {
                queryString.Add("limit", limit.ToString());
            }

            if (offset > 0)
            {
                queryString.Add("offset", offset.ToString());
            }

            return queryString.ToString();
        }
    }
}