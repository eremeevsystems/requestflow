using System.Threading.Tasks;
using RequestFlowClient.Models;

namespace RequestFlowClient.Services
{
    public class AuthService
    {
        private readonly ApiService _api;

        public AuthService(ApiService api)
        {
            _api = api;
        }

        public async Task<AuthResponse> LoginAsync(string username, string password)
        {
            var loginData = new { username, password };
            return await _api.PostAsync<AuthResponse>("/auth/login", loginData);
        }

        public void SetToken(string token)
        {
            _api.SetAuthToken(token);
        }
    }
}
