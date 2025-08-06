using System.ComponentModel.DataAnnotations;

namespace TodoList.Dto
{
    public class TarefaCriacaoDto
    {
        [Required(ErrorMessage = "O título é obrigatório.")]
        public string Title { get; set; }
        [Required(ErrorMessage = "A descrição é obrigatória.")]
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
    }
}
