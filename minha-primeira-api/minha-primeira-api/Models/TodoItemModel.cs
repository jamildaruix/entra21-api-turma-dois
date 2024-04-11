using minha_primeira_api.Dtos;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace minha_primeira_api.Models
{
    [Table("TodoItem")]
    public class TodoItemModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(TypeName = "VARCHAR"), StringLength(250), Required]
        public string Nome { get; set; }

        [Column(TypeName = "VARCHAR"), StringLength(15)]
        public string? Apelido { get; set; }

        [Required]
        public bool Ativo { get; set; }

        [Required]
        public DateTime Cadastro { get; set; }
    }
}
