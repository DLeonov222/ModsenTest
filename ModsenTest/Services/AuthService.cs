using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using ModsenTest.Dtos;
using ModsenTest.Models;
using ModsenTest.Repositories.Interfaces;
using ModsenTest.Services.Interfaces;

namespace ModsenTest.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IAuthorRepository _authorRepository;

        public AuthService(IAuthorRepository authorRepository, IMapper mapper, IConfiguration configuration)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<string> Login(LoginDto loginDto)
        {
            var author = await _authorRepository.GetAuthorByNameAsync(loginDto.FirstName, loginDto.LastName) ??
                         throw new InvalidCredentialException("No Author with this name");

            if (!BCrypt.Net.BCrypt.Verify(loginDto.Password, author.Password))
                throw new InvalidCredentialException("Wrong password");

            var handler = new JsonWebTokenHandler();

            var key = Encoding.UTF8.GetBytes(_configuration.GetSection("ApplicationSettings").GetValue<string>("Secret") ??
                                             string.Empty);

            var token = handler.CreateToken(new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("sub", author.Id.ToString()),
                    new Claim("firstName", author.FirstName),
                    new Claim("lastName", author.LastName)
                }),
                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256),
                Expires = DateTime.Now.AddDays(30)
            });

            return token;
        }

        public async Task<RegisteredAuthorDto> Register(RegisterAuthorDto registerAuthorDto)
        {
            var author = _mapper.Map<Author>(registerAuthorDto);
            author.Password = BCrypt.Net.BCrypt.HashPassword(registerAuthorDto.Password);
            if (await _authorRepository.GetAuthorByNameAsync(author.FirstName, author.LastName) != null)
                throw new Exception("Author with this name already exists");

            var createdAuthor = await _authorRepository.CreateAuthorAsync(author);
            var createdAuthorDto = _mapper.Map<RegisteredAuthorDto>(createdAuthor);
            createdAuthorDto.Password = createdAuthor.Password;

            return createdAuthorDto;
        }
    }
}
