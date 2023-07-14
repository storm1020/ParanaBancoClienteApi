namespace ParanaBancoClienteApi.DataTransferObjects
{
    public class ClienteCadastroDTO
    {
        public string? Nome { get; set; }

        public string? Sobrenome { get; set; }

        public string? Email { get; set; }

        public List<TelefoneViewDTO>? Telefones { get; set; }
    }
}
