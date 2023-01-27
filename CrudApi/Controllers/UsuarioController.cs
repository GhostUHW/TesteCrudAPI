using CrudApi.Models;
using CrudApi.Repositorios.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrudApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        public UsuarioController(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        [HttpGet]
        public async Task<ActionResult<List<UsuarioModel>>> BuscarTodosUsuarios()
        {
            List<UsuarioModel> usuarios = await _usuarioRepositorio.BuscarTodosUsuarios();
            return Ok(usuarios);
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<UsuarioModel>> BuscarPorId(int Id)
        {
            UsuarioModel usuario = await _usuarioRepositorio.BuscarPorId(Id);
            return Ok(usuario);
        }

        [HttpPost]
        public async Task<ActionResult<UsuarioModel>> Cadastrar([FromBody] UsuarioModel usuarioModel)
        {
            UsuarioModel usuario = await _usuarioRepositorio.Adicionar(usuarioModel);
            return Ok(usuario);
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<UsuarioModel>> Atualizar([FromBody] UsuarioModel usuarioModel, int Id)
        {
            usuarioModel.Id = Id;
            UsuarioModel usuario = await _usuarioRepositorio.Atualizar(usuarioModel, Id);
            return Ok(usuario);
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult<UsuarioModel>> Apagar(int Id)
        {
            bool apagado = await _usuarioRepositorio.Apagar(Id);
            return Ok(apagado);
        }

    }
}
