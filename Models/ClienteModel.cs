using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParanaBancoClienteApi.Models
{
    public class ClienteModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string? Nome { get; set; }

        public string? Sobrenome { get; set; }

        public string? Email { get; set; }

        public List<TelefoneModel>? Telefones { get; set; }
    }
}
