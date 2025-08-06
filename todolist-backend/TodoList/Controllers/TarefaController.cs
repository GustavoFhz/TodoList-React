using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoList.Dto;
using TodoList.Services.Interface;

namespace TodoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarefaController : ControllerBase
    {
        private readonly ITarefaInterface _tarefaInterface;
        public TarefaController(ITarefaInterface tarefaInterface)
        {
            _tarefaInterface = tarefaInterface;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarTarefaPorId(int id)
        {
            var tarefa = await _tarefaInterface.BuscarTarefaPorId(id);
            return Ok(tarefa);
        }
        [HttpPut]
        public async Task<IActionResult> EditarTarefa([FromBody] TarefaEdicaoDto tarefaEdicaoDto)
        {
            var tarefa = await _tarefaInterface.EditarTarefa(tarefaEdicaoDto);
            return Ok(tarefa);
        }

        [HttpGet]
        public async Task<IActionResult> ListarTarefas()
        {
            var tarefas = await _tarefaInterface.ListarTarefas();
            return Ok(tarefas);
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarTarefa([FromBody]TarefaCriacaoDto tarefaCriacaoDto)
        {
            var tarefa = await _tarefaInterface.RegistrarTarefa(tarefaCriacaoDto);
            return Ok(tarefa);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarTarefa(int id)
        {
            var tarefa = await _tarefaInterface.RemoverTarefa(id);
            return Ok(tarefa);
        }
    }
}
