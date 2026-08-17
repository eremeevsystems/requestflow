using System.Collections.Generic;
using System.Threading.Tasks;
using RequestFlowClient.Models;

namespace RequestFlowClient.Services
{
    public class RequestService
    {
        private readonly ApiService _api;

        public RequestService(ApiService api)
        {
            _api = api;
        }

        public async Task<List<Request>> GetRequestsAsync()
        {
            return await _api.GetAsync<List<Request>>("/requests");
        }

        public async Task<Request> GetRequestAsync(long id)
        {
            return await _api.GetAsync<Request>($"/requests/{id}");
        }

        public async Task<Request> UpdateStatusAsync(long id, string status, string comment = null)
        {
            var data = new { status, comment };
            return await _api.PatchAsync<Request>($"/requests/{id}/status", data);
        }

        public async Task<List<RequestHistory>> GetHistoryAsync(long id)
        {
            return await _api.GetAsync<List<RequestHistory>>($"/requests/{id}/history");
        }
    }
}