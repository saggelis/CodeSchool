using FlipWeen.MVC.Responses.Responses;

namespace FlipWeen.MVC.Client
{
    using System.Threading.Tasks;
    using Models;
    using Responses;

    public interface ILoginClient
    {
        Task<TokenResponse> Login(string email, string password);
        Task<RegisterResponse> Register(RegisterViewModel viewModel);

        Task<UserInfoResponse> GetUserInfo();
    }
}