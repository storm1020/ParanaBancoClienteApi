using Microsoft.EntityFrameworkCore;
using ParanaBancoClienteApi.Data;
using ParanaBancoClienteApi.DataTransferObjects;
using ParanaBancoClienteApi.Enums;
using ParanaBancoClienteApi.Exceptions;
using ParanaBancoClienteApi.Models;
using ParanaBancoClienteApi.Repositories.Interfaces;
using System.Formats.Asn1;
using System.Linq;

namespace ParanaBancoClienteApi.Repositories
{
    public class ClienteRepositorio : IClienteRepositorio
    {
        private readonly ParanabancoClienteDbContext _dbContext;

        public ClienteRepositorio(ParanabancoClienteDbContext paranabancoClienteDbContext)
        {
            _dbContext = paranabancoClienteDbContext;
        }


        public async Task<List<ClienteModel>> BuscarTodos()
        {
            return await _dbContext.Clientes
                .Include(x => x.Telefones)
                .ToListAsync();
        }

        public async Task<ClienteModel> BuscarPorId(int id)
        {
            return await _dbContext.Clientes.FirstAsync(x => x.Id == id);
        }

        public async Task<ClienteModel> BuscarPorTelefoneEhDdd(string ddd, string telefone)
        {
            return await _dbContext.Clientes
                .Include(x => x.Telefones)
                .FirstAsync(c => c.Telefones.Any(t => t.DDD == ddd && t.Numero == telefone));
        }

        public async Task<ClienteModel> Adicionar(ClienteModel cliente)
        {
            await _dbContext.Clientes.AddAsync(cliente);

            await _dbContext.SaveChangesAsync();


            return cliente;
        }


        public async Task<ClienteModel> Atualizar(ClienteModel cliente, int idCliente)
        {
            ClienteModel clientePorId = await BuscarPorId(idCliente);

            if (clientePorId == null)
            {
                throw new ClienteNaoEncontradoException(idCliente);
            }

            clientePorId.Email = cliente.Email;

            clientePorId.Nome = cliente.Nome;

            clientePorId.Sobrenome = cliente.Sobrenome;

            clientePorId.Telefones = cliente.Telefones;


            _dbContext.Clientes.Update(clientePorId);

            await _dbContext.SaveChangesAsync();


            return clientePorId;
        }

        public async Task<ClienteModel> AtualizarEmail(int idCliente, string email)
        {
            var cliente = await BuscarPorId(idCliente);

            if (cliente == null)
            {
                throw new Exception($"O Cliente do e-mail: {email}, não foi encontrado no banco de dados!");
            }

            cliente.Email = email;


            _dbContext.Clientes.Update(cliente);

            await _dbContext.SaveChangesAsync();


            return cliente;
        }

        public async Task<ClienteModel> AtualizarTelefone(string telefoneAntigo, string telefoneNovo)
        {
            ClienteModel clientePorTel = await _dbContext.Clientes
                                                .Include(x => x.Telefones)
                                                .FirstAsync(c => c.Telefones.Any(t => t.Numero == telefoneAntigo));

            if (clientePorTel == null)
            {
                throw new Exception($"Cliente com o Telefone: {telefoneAntigo} não encontrado!");
            }

            TelefoneModel telefone = clientePorTel.Telefones.First(t => t.Numero == telefoneAntigo);

            if (telefone != null)
            {
                telefone.Numero = telefoneNovo;
            }

            _dbContext.Update(clientePorTel);

            await _dbContext.SaveChangesAsync();

            return clientePorTel;
        }

        public async Task<bool> Apagar(int idCliente)
        {
            ClienteModel cliente = await BuscarPorId(idCliente);

            if (cliente == null)
            {
                throw new ClienteNaoEncontradoException(idCliente);
            }

            _dbContext.Remove(cliente);

            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ApagarPorEmail(string email)
        {
            ClienteModel cliente = await _dbContext.Clientes.FirstAsync(x => x.Email == email);

            if (cliente == null)
            {
                throw new Exception($"O Cliente com e-mail: {email}, não foi encontrado no banco de dados!");
            }

            _dbContext.Clientes.Remove(cliente);

            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
