using ParanaBancoClienteApi.Enums;

namespace ParanaBancoClienteApi.DataTransferObjects
{
    public class TelefoneDTO
    {
        public string? DDD { get; set; }

        public string? Numero { get; set; }

        public string? TipoTelefone { get; set; }
    }
}
