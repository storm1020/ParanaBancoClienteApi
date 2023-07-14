namespace ParanaBancoClienteApi.Exceptions
{
    public class ClienteNaoEncontradoException : Exception
    {
        public int ClienteId { get; }

        public ClienteNaoEncontradoException(int clienteId)
            : base($"O cliente do Id {clienteId} não foi encontrado no banco de dados.")
        {
            ClienteId = clienteId;
        }
    }
}
