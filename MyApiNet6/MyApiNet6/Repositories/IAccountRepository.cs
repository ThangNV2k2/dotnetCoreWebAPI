using Microsoft.AspNetCore.Identity;
using MyApiNet6.Models;

namespace MyApiNet6.Repositories
{
    public interface IAccountRepository
    {
        public Task<IdentityResult> SignUpAsync(SignUpModel model);
        public Task<String> SignInAsync(SignInModel model);
    }
}
