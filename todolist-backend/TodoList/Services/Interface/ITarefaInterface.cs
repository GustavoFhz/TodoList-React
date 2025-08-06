using TodoList.Dto;
using TodoList.Models;

namespace TodoList.Services.Interface
{
    public interface ITarefaInterface
    {
        Task<ResponseModel<TarefaModel>> RegistrarTarefa(TarefaCriacaoDto tarefaCriacaoDto);
        Task<ResponseModel<List<TarefaModel>>> ListarTarefas();
        Task<ResponseModel<TarefaModel>> BuscarTarefaPorId(int id);
        Task<ResponseModel<TarefaModel>> EditarTarefa(TarefaEdicaoDto tarefaEdicaoDto);
        Task<ResponseModel<TarefaModel>> RemoverTarefa(int id);
    }
}
