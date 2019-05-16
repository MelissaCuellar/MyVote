using System.Threading.Tasks;
using MyVote.Common.Models;

namespace MyVote.Common.Services
{
    public interface IApiService
    {
        Task<Response> GetListAsync<T>(string urlBase, string servicePrefix, string controller);

        Task<Response> GetListAsync<T>(string urlBase, string servicePrefix, string controller, string tokenType, string accessToken);

        Task<Response> GetTokenAsync(string urlBase, string servicePrefix, string controller, TokenRequest request);

        Task<Response> RegisterUserAsync(string urlBase, string servicePrefix, string controller, NewUserRequest newUserRequest);
    }
}