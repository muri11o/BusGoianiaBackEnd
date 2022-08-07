using BusGoiania.AuthProvider.Data.Conta;
using BusGoiania.AuthProvider.DTOs;
using BusGoiania.AuthProvider.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BusGoiania.AuthProvider.Controllers
{
    [Route("v1/api/auth")]
    public class AuthController : MainController
    {
        private readonly SignInManager<Usuario> _signInManager;
        private readonly UserManager<Usuario> _userManager;
        private readonly AppSettings _appSettings;

        public AuthController(SignInManager<Usuario> signInManager,
            UserManager<Usuario> userManager,
            IOptions<AppSettings> appSettings, 
            INotifier notifier) : base(notifier)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _appSettings = appSettings.Value;
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginUserDTO loginUserDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return CustomResponse(ModelState);

                var result = await _signInManager.PasswordSignInAsync(loginUserDTO.Email, loginUserDTO.Senha, false, true);

                if (result.Succeeded)
                {
                    return CustomResponse(await GerarJwt(loginUserDTO.Email));
                }

                if (result.IsLockedOut)
                {
                    NotifyError("Usuário temporariamente bloqueado por tentativas inválidas");
                    return CustomResponse(loginUserDTO);
                }

                NotifyError("Usuário ou Senha incorretos");
                return CustomResponse(loginUserDTO);
            }
            catch (Exception ex)
            {
                NotifyError($"Falha ao processar a requisição. Detalhes {ex.Message}");
                return CustomResponse();
            }
        }

        private async Task<LoginResponseDTO> GerarJwt(string email)
        {

            var user = await _userManager.FindByNameAsync(email);
            var claims = await _userManager.GetClaimsAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim("nome", user.Nome));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _appSettings.Emissor,
                Audience = _appSettings.ValidoEm,
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddHours(_appSettings.ExpiracaoHoras),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            var encodedToken = tokenHandler.WriteToken(token);

            var response = new LoginResponseDTO
            {
                AccessToken = encodedToken,
                ExpiresIn = TimeSpan.FromHours(_appSettings.ExpiracaoHoras).TotalSeconds,
                UserToken = new UserTokenDTO
                {
                    Id = user.Id,
                    Email = user.Email,
                    Claims = claims.Select(c => new ClaimDTO { Type = c.Type, Value = c.Value })
                }
            };

            return response;
        }

        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
    }
}
