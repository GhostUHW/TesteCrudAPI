using CrudApi.Data;
using CrudApi.Models;
using CrudApi.Repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CrudApi.Repositorios
{
    public class TarefaRepositorio : ITarefaRepositorio
    {
        private readonly SistemaTarefasDBContex _dbContext;
        public TarefaRepositorio(SistemaTarefasDBContex sistemaTarefasDBContex)
        {
            _dbContext = sistemaTarefasDBContex;
        }
        public async Task<TarefaModel> BuscarPorId(int Id)
        {
            return await _dbContext.Tarefas
                .Include(x => x.Usuario)
                .FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<List<TarefaModel>> BuscarTodasTarefas()
        {
            return await _dbContext.Tarefas
                .Include(x => x.Usuario)
                .ToListAsync();
        }


        public async Task<TarefaModel> Adicionar(TarefaModel tarefa)
        {
            await _dbContext.Tarefas.AddAsync(tarefa);
            await _dbContext.SaveChangesAsync();

            return tarefa;
        }

        public async Task<TarefaModel> Atualizar(TarefaModel tarefa, int Id)
        {
            TarefaModel tarefaPorId = await BuscarPorId(Id);

            if (tarefaPorId == null)
            {
                throw new Exception($"Tarefa para o ID:{Id} Não foi encontrada");
            }

            tarefaPorId.Nome = tarefa.Nome;
            tarefaPorId.Descricao = tarefa.Descricao;
            tarefaPorId.Status = tarefa.Status;
            tarefaPorId.UsuarioId = tarefa.UsuarioId;

            _dbContext.Tarefas.Update(tarefaPorId);
            await _dbContext.SaveChangesAsync();

            return tarefaPorId;
            
        }

        public async Task<bool> Apagar(int Id)
        {
            TarefaModel tarefaPorId = await BuscarPorId(Id);

            if (tarefaPorId == null)
            {
                throw new Exception($"Tarefa para o ID:{Id} Não foi encontrada");
            }
            _dbContext.Tarefas.Remove(tarefaPorId);
            await _dbContext.SaveChangesAsync();

            return true;

        }

    }
}
