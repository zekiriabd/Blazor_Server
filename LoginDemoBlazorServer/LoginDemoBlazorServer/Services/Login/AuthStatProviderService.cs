using LoginDemoBlazorServer.Data;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Security.Claims;

namespace LoginDemoBlazorServer.Services.Login
{
    public class AuthStatProviderService : AuthenticationStateProvider
    {
        private ProtectedLocalStorage _ProtectedLocalStorage;

        private UserState CurrentUser { get; set; }
        public AuthStatProviderService(ProtectedLocalStorage protectedLocalStorage)
        {
            _ProtectedLocalStorage = protectedLocalStorage;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var identity = new ClaimsIdentity();
            var resultSession = await _ProtectedLocalStorage.GetAsync<UserState>("SessionUser");
            if (resultSession.Success)
            {
                CurrentUser = resultSession.Value;
                if (CurrentUser != null && await Validation())
                {
                    identity = GetCurrentClaim();
                }
            }
            var authenticationState = new AuthenticationState(new ClaimsPrincipal(identity));
            return await Task.FromResult(authenticationState);
        }
        public async Task<bool> MarkUserAsAuthenticated(UserState currentUser)
        {
            CurrentUser = currentUser;
            var identity = GetCurrentClaim();
            if (await Validation())
            {
                NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(new ClaimsPrincipal(identity))));
                return true;
            }
            else
            {
                return false;
            }
        }

        private async Task<bool> Validation()
        {
            bool findinDB = CurrentUser.Login == "ZekiriA"; ;
            if (findinDB)
            {
                CurrentUser.Login = "ZekiriA";
                CurrentUser.Roles = "Admin";
                await _ProtectedLocalStorage.SetAsync("SessionUser", CurrentUser);
                return true;
            }
            else
            {
                return false;
            }
        }
        private ClaimsIdentity GetCurrentClaim() => new ClaimsIdentity(new[] {
                                    new Claim(ClaimTypes.Name, CurrentUser.Login),
                                    new Claim(ClaimTypes.Role, "")}, "AUTHENTICATION");
    }
}
