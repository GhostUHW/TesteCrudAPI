using CrudApi.Models;
using CrudApi.Repositorios.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrudApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarefaController : ControllerBase
    {
        private readonly ITarefaRepositorio _tarefaRepositorio;
        public TarefaController(ITarefaRepositorio tarefaRepositorio)
        {
            _tarefaRepositorio = tarefaRepositorio;
        }

        [HttpGet]
        public async Task<ActionResult<List<TarefaModel>>> ListarTodas()
        {
            List<TarefaModel> tarefas = await _tarefaRepositorio.BuscarTodasTarefas();
            return Ok(tarefas);
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<TarefaModel>> BuscarPorId(int Id)
        {
            TarefaModel tarefa = await _tarefaRepositorio.BuscarPorId(Id);
            return Ok(tarefa);
        }

        [HttpPost]
        public async Task<ActionResult<TarefaModel>> Cadastrar([FromBody] TarefaModel tarefaModel)
        {
            TarefaModel tarefa = await _tarefaRepositorio.Adicionar(tarefaModel);
            return Ok(tarefa);
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<TarefaModel>> Atualizar([FromBody] TarefaModel tarefaModel, int Id)
        {
            tarefaModel.Id = Id;
            TarefaModel tarefa = await _tarefaRepositorio.Atualizar(tarefaModel, Id);
            return Ok(tarefa);
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult<UsuarioModel>> Apagar(int Id)
        {
            bool apagado = await _tarefaRepositorio.Apagar(Id);
            return Ok(apagado);
        }

    }
}
