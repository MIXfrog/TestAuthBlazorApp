using System.Text;
using Microsoft.AspNetCore.Components;

namespace MntNft_TestBlazorApp.Services
{
    public interface IAuthService
    {
        User User { get; }
        Task Login(string login, string password);
        Task Logout();
    }

    public class AuthService : IAuthService
    {
        private const string CorrectLogin = "TestLogin";
        private const string CorrectPassword = "password";

        public User User { get; private set; }

        private ILocalStorageService _localStorageService;
        private NavigationManager _navigationManager;

        public AuthService(ILocalStorageService localStorageService,
            NavigationManager navigationManager)
        {
            _localStorageService = localStorageService;
            _navigationManager = navigationManager;
        }

        public async Task Login(string login, string password)
        {
            if (login.Equals(CorrectLogin) && password.Equals(CorrectPassword))
            {
                User = new User { Login = CorrectLogin, Password = CorrectPassword, Name = "TestUserName" };

                User.AuthData = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{login}:{password}"));
                await _localStorageService.SetItem("user", User);
            }
        }

        public async Task Logout()
        {
            User = null;
            await _localStorageService.RemoveItem("user");
            _navigationManager.NavigateTo("login");
        }
    }
}
