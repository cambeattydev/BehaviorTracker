namespace BehaviorTracker.Client.Services
{
    public class AuthorizationService
    {
        private readonly HttpClient _httpClient;
        private readonly IUriHelper _uriHelper;

        private AuthorizationModel _authorizationModel;

        public AuthorizationService(IUriHelper uriHelper, HttpClient httpClient)
        {
            _uriHelper = uriHelper;
            _httpClient = httpClient;
            Console.WriteLine("About to ensure authorized");
            var task = new Task(async () => await EnsureAuthorized());
            task.RunSynchronously();
            Console.WriteLine($"AuthorizationModel.Roles.Count: {_authorizationModel?.Roles?.Count ?? 0}");
            Console.WriteLine("Ensured authorized");
        }

        private async Task EnsureAuthorized()
        {
            if (_authorizationModel != null) return;
            try
            {
                var authorizationModelResponse = await _httpClient.GetAsync("/api/Account/AuthorizationModel");
                if (authorizationModelResponse.StatusCode == HttpStatusCode.OK)
                {
                    _authorizationModel =
                        Json.Deserialize<AuthorizationModel>(
                            await authorizationModelResponse.Content.ReadAsStringAsync());
                    return;
                }

                _uriHelper.NavigateTo("/login");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error thrown: {ex.Message}");
                _uriHelper.NavigateTo("/login");
            }
        }

        public bool IsInRole(BehaviorTrackerRoles roleName)
        {
            var result = _authorizationModel?.Roles?.Any(role =>
                             role.Equals(roleName.ToString(), StringComparison.InvariantCultureIgnoreCase)) ?? false;
            Console.WriteLine($"Checking for is in role: {roleName.ToString()}\tResult:{result}");
            return result;
        }
    }
}