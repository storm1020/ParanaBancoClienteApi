using ParanaBancoClienteApi.Enums;
using ParanaBancoClienteApi.Models;

namespace ParanaBancoClienteApi.Repositories.Interfaces
{
    public interface IClienteRepositorio
    {
        Task<List<ClienteModel>> BuscarTodos();

        Task<ClienteModel> BuscarPorTelefoneEhDdd(string ddd, string telefone);

        Task<ClienteModel> BuscarPorId(int id);

        Task<ClienteModel> Adicionar(ClienteModel cliente);

        Task<ClienteModel> Atualizar(ClienteModel cliente, int idCliente);

        Task<ClienteModel> AtualizarTelefone(string telefoneAntigo, string telefoneNovo);

        Task<ClienteModel> AtualizarEmail(int idCliente, string email);

        Task<bool> Apagar(int idUsuario);

        Task<bool> ApagarPorEmail(string email);

    }
}
