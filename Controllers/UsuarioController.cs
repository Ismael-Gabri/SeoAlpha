using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeoAlpha.Data;
using SeoAlpha.Models;
using SeoAlpha.ViewModels;

namespace SeoAlpha.Controllers
{
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        [HttpGet("v1/usuario")]
        public async Task<IActionResult> GetAsync([FromServices] SeoDataContext context)
        {
            try
            {
                var usuarios = await context.Usuarios.ToListAsync();

                if (usuarios.Count == 0)
                    return Ok("Lista Vazia");
                else
                    return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Falha interna no servidor X(");
                throw;
            }
        }

        [HttpGet("v1/usuario/{id:guid}")]
        public async Task<IActionResult> GetUserByIdAsync([FromRoute] Guid id,[FromServices] SeoDataContext context)
        {
            var usuario = await context.Usuarios.FirstOrDefaultAsync(x => x.Id == id);

            if (usuario == null)
                return BadRequest("Usuario nao encontrado");

            return Ok(usuario);
        }

        [HttpPost("v1/usuario")]
        public async Task<IActionResult> PostAsync([FromBody] CriarUsuarioViewModel model, [FromServices] SeoDataContext context)
        {
            try
            {
                var usuario = new Usuario
                {
                    Nome = model.Nome,
                    Email = model.Email,
                    Senha = model.Senha,
                    DataNascimento = DateTime.UtcNow,
                    DataCriacao = DateTime.UtcNow
                };

                await context.Usuarios.AddAsync(usuario);
                await context.SaveChangesAsync();

                return Created($"v1/usuario/{usuario.Id}", usuario);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, "Nao foi possivel adicionar o usuario");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Falha interna no servidor X(");
            }
        }

        [HttpPut("v1/usuario/{id:guid}")]
        public async Task<IActionResult> PutAsync([FromRoute] Guid id,[FromBody] EditarUsuarioViewModel model,[FromServices] SeoDataContext context)
        {
            var usuario = await context.Usuarios.FirstOrDefaultAsync(x => x.Id == id);

            if (usuario == null)
                return BadRequest();

            try
            {
                usuario.Nome = model.Nome;
                usuario.Senha = model.Senha;

                context.Usuarios.Update(usuario);
                await context.SaveChangesAsync();

                return Ok(usuario);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, "Nao foi possivel atualizar o usuario");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Falha interna no servidor X(");
            }
        }

        [HttpDelete("v1/usuario/{id:guid}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id, [FromServices] SeoDataContext context)
        {
            var usuario = await context.Usuarios.FirstOrDefaultAsync(x => x.Id == id);

            if (usuario == null)
                return BadRequest();

            try
            {
                context.Usuarios.Remove(usuario);
                await context.SaveChangesAsync();
                return Ok(usuario);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, "Nao foi possivel remover o usuario");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Falha interna no servidor X(");
            }
        }
    }
}
