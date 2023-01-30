using System.Text;
using Microsoft.AspNetCore.Components;

namespace MntNft_TestBlazorApp
{
    public interface IAuthService
    {
        User User { get; }
        Task Login(string mail, string password);
        Task Logout();
    }

    public class AuthService : IAuthService
    {
        private const string CorrectEmail = "testMail@gmail.com";
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

        public async Task Login(string mail, string password)
        {
            if (mail.Equals(CorrectEmail) && password.Equals(CorrectPassword))
            {
                User = new User { Name = "TestUser", Email = CorrectEmail, Password = CorrectPassword };

                User.AuthData = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{mail}:{password}"));
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
