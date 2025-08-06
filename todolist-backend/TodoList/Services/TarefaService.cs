using Microsoft.EntityFrameworkCore;
using TodoList.Data;
using TodoList.Dto;
using TodoList.Models;
using TodoList.Services.Interface;

namespace TodoList.Services
{
    public class TarefaService : ITarefaInterface
    {
        private readonly AppDbContext _context;
        public TarefaService(AppDbContext context)
        {
            _context = context; 
        }
        public async Task<ResponseModel<TarefaModel>> BuscarTarefaPorId(int id)
        {
            ResponseModel<TarefaModel> response = new ResponseModel<TarefaModel>();

            try
            {
                var tarefa = await _context.Tarefas.FindAsync(id);
                if (tarefa == null)
                {
                    response.Data = null;
                    response.Message = "Tarefa não encontrada.";
                    return response;
                }
                response.Data = tarefa;
                response.Message = "Tarefa encontrada com sucesso.";
                return response;

            }
            catch(Exception ex)
            {
                response.Status = false;
                response.Message = "Erro ao buscar tarefa: " + ex.Message;
                return response;
            }
        }

        public async Task<ResponseModel<TarefaModel>> EditarTarefa(TarefaEdicaoDto tarefaEdicaoDto)
        {
            ResponseModel<TarefaModel> response = new ResponseModel<TarefaModel>();

            try
            {
                var tarefaBanco = _context.Tarefas.Find(tarefaEdicaoDto.Id);

                if(tarefaBanco == null)
                {
                    response.Status = false;
                    response.Message = "Tarefa não encontrada.";
                    return response;
                }
                tarefaBanco.Title = tarefaEdicaoDto.Title;
                tarefaBanco.Description = tarefaEdicaoDto.Description;
                tarefaBanco.IsCompleted = tarefaEdicaoDto.IsCompleted;

                _context.Tarefas.Update(tarefaBanco);
                await _context.SaveChangesAsync();

                response.Message = "Tarefa editada com sucesso.";
                response.Data = tarefaBanco;    
                return response;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = "Erro ao editar tarefa: " + ex.Message;
                return (response);
            }
        }

        public async Task<ResponseModel<List<TarefaModel>>> ListarTarefas()
        {
            ResponseModel<List<TarefaModel>> response = new ResponseModel<List<TarefaModel>>();

            try
            {
                var tarefas = await _context.Tarefas.ToListAsync();

                response.Data = tarefas;
                response.Message = "Tarefas listadas com sucesso.";
                return (response);
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = "Erro ao listar tarefas: " + ex.Message;
                return (response);
            }
        }

        public async Task<ResponseModel<TarefaModel>> RegistrarTarefa(TarefaCriacaoDto tarefaCriacaoDto)
        {
            ResponseModel<TarefaModel> response = new ResponseModel<TarefaModel>();

            try
            {
                if (TarefaExiste(tarefaCriacaoDto))
                {
                    response.Status = false;
                    response.Message = "Tarefa já existe.";
                    return response;
                }

                TarefaModel tarefa = new TarefaModel
                {
                    Title = tarefaCriacaoDto.Title,
                    Description = tarefaCriacaoDto.Description,
                    IsCompleted = tarefaCriacaoDto.IsCompleted
                };

                _context.Add(tarefa);
                await _context.SaveChangesAsync();

                response.Message = "Tarefa registrada com sucesso.";
                response.Data = tarefa;
                return response;

            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = "Erro ao registrar tarefa: " + ex.Message;
                return response;
            }
        }

        public async Task<ResponseModel<TarefaModel>> RemoverTarefa(int id)
        {
            ResponseModel<TarefaModel> response = new ResponseModel<TarefaModel>();

            try
            {
                var tarefa = await _context.Tarefas.FindAsync(id);
                if (tarefa == null)
                {
                    response.Status = false;
                    response.Message = "Tarefa não encontrada.";
                    return response;
                }
                response.Data = tarefa;
                response.Message = "Tarefa removida com sucesso.";

                _context.Tarefas.Remove(tarefa);
                await _context.SaveChangesAsync();
                return response;
            }
            catch(Exception ex)
            {
                response.Status = false;
                response.Message = "Erro ao remover tarefa: " + ex.Message;
                return response;
            }
        }

        public bool TarefaExiste(TarefaCriacaoDto tarefaCriacaoDto)
        {
            return _context.Tarefas.Any(item => item.Title == tarefaCriacaoDto.Title);
        }
    }
}
