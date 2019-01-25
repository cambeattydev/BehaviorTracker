using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using BehaviorTracker.Client.Models;
using Microsoft.AspNetCore.Blazor.Services;
using Microsoft.JSInterop;

namespace BehaviorTracker.Client.Services
{
    public class AuthorizationService
    {
        public AuthorizationService(IUriHelper uriHelper, HttpClient httpClient)
        {
            _uriHelper = uriHelper;
            _httpClient = httpClient;
            Console.WriteLine("About to ensure authorized");
             var task = new Task(async () => await EnsureAuthorized());
             task.RunSynchronously();
             Console.WriteLine("Ensured authorized");
        }

        private AuthorizationModel _authorizationModel;
        private readonly HttpClient _httpClient;
        private readonly IUriHelper _uriHelper;

        private async Task EnsureAuthorized()
        {
            if (_authorizationModel != null) return;
            try
            {
                var authorizationModelResponse = await _httpClient.GetAsync("/api/Account/AuthorizationModel");
                if (authorizationModelResponse.StatusCode == HttpStatusCode.OK)
                {
                    _authorizationModel = Json.Deserialize<AuthorizationModel>(await authorizationModelResponse.Content.ReadAsStringAsync());
                    return;
                }
            }
            catch (Exception ex)
            {
                _uriHelper.NavigateTo("/login");
            }

            _uriHelper.NavigateTo("/login");
        }

        public bool IsInRole(string roleName) =>
            _authorizationModel.Roles.Any(role => role.Equals(roleName, StringComparison.InvariantCultureIgnoreCase));



    }
}