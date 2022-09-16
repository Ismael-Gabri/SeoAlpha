using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureIdentity.Password;
using SeoAlpha.Data;
using SeoAlpha.Models;
using SeoAlpha.Services;
using SeoAlpha.ViewModels;

namespace SeoAlpha.Controllers
{
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly TokenService _tokenService;
        public AccountController(TokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost("v1/accounts/usuario")]
        public async Task<IActionResult> RegistrarUsuarioAsync([FromBody] CriarUsuarioViewModel model, [FromServices] SeoDataContext context)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values);

            try
            {
                var usuario = new Usuario
                {
                    Nome = model.Nome,
                    Email = model.Email,
                    Senha = PasswordHasher.Hash(model.Senha),
                    DataNascimento = DateTime.UtcNow,
                    DataCriacao = DateTime.UtcNow,
                    CargoId = 3
                };

                await context.Usuarios.AddAsync(usuario);
                await context.SaveChangesAsync();

                return Created($"v1/usuario/{usuario.Id}", new ResultViewModel<Usuario>(usuario));
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<Usuario>("Nao foi possivel adicionar o usuario"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel<Usuario>("Falha interna no servidor X("));
            }
        }

        [HttpPost("v1/accounts/login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginViewModel model, [FromServices] TokenService tokenService,[FromServices] SeoDataContext context)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values);

            var usuario = await context.Usuarios.FirstOrDefaultAsync(x => x.Email == model.Email);

            if (usuario == null)
                return NotFound(new ResultViewModel<Usuario>("Usuario Nao encontrado :("));

            if (!PasswordHasher.Verify(usuario.Senha, model.Senha))
                return StatusCode(401, new ResultViewModel<string>("Usuario ou senha invalidos"));

            try
            {
                var token = tokenService.GerarToken(usuario);
                return Ok(new ResultViewModel<string>(token, null));
            }
            catch (Exception)
            {
                return StatusCode(500);
                throw;
            }
        }





        //Testes
        [AllowAnonymous]
        [HttpPost("v1/login")]
        public IActionResult Login()
        {
            var token = _tokenService.GerarToken(null);

            return Ok(token);
        }

        [Authorize(Roles = "aluno")]
        [HttpGet("v1/user")]
        public IActionResult GetUser()
        {
            return Ok($"Login bem sucedido como => {User.Identity.Name} <=");
        }
    }
}
