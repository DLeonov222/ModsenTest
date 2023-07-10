using ModsenTest.Dtos;

namespace ModsenTest.Services.Interfaces
{
    public interface IAuthService
    {
        public Task<string> Login(LoginDto loginDto);

        public Task<RegisteredAuthorDto> Register(RegisterAuthorDto registerAuthorDto);
    }
}
