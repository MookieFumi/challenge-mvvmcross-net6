using System.Net.Http;
using System.Threading.Tasks;
using Challenge.Services.Marvel.Base;
using Challenge.Services.Marvel.Model;

namespace Challenge.Services.Marvel
{
    public class ComicsService : MarvelServiceBase<Comic>, IComicsService
    {
        private const string BaseUrl = "https://gateway.marvel.com:443/v1/public";
        private readonly HttpClientHandler _httpClientHandler;
        private readonly IMarvelAuthenticatorService _authenticator;

        public ComicsService(HttpClientHandler httpClientHandler, IMarvelAuthenticatorService authenticator) : base(
            httpClientHandler, authenticator)
        {
            _httpClientHandler = httpClientHandler;
            _authenticator = authenticator;
        }

        public async Task<MarvelResult<Comic>> GetComics(string filter, int limit = 10, int offset = 0)
        {
            const string endpoint = "comics";
            var querystring = GetComicQueryString(filter, limit, offset);
            
            return await HttpCall(endpoint, querystring);
        }

        private string GetComicQueryString(string filter, int limit, int offset)
        {
            var queryString = GetQueryString(limit, offset);
            
            if (!string.IsNullOrEmpty(filter))
            {
                queryString = $"titleStartsWith={filter}&{queryString}";
            }

            return queryString;
        }
    }
}