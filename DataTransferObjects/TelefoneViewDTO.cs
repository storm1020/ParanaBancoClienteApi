using ParanaBancoClienteApi.Enums;

namespace ParanaBancoClienteApi.DataTransferObjects
{
    public class TelefoneViewDTO
    {
        public string? DDD { get; set; }

        public string? Numero { get; set; }

        public TipoTelefone TipoTelefone { get; set; }
    }
}
