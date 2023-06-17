using System.Threading.Tasks;
using Challenge.Services.Marvel.Model;

namespace Challenge.Services.Marvel
{
    public interface IComicsService
    {
        Task<MarvelResult<Comic>> GetComics(string filter, int limit = 10, int offset = 0);
    }
}