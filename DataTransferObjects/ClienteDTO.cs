using ParanaBancoClienteApi.Models;

namespace ParanaBancoClienteApi.DataTransferObjects
{
    public class ClienteDTO
    {
        public int Id { get; set; }

        public string? Nome { get; set; }

        public string? Sobrenome { get; set; }

        public string? Email { get; set; }

        public List<TelefoneDTO>? Telefones { get; set; }
    }
}
