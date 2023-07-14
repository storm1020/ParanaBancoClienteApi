using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Extensions;
using ParanaBancoClienteApi.DataTransferObjects;
using ParanaBancoClienteApi.Enums;
using ParanaBancoClienteApi.Exceptions;
using ParanaBancoClienteApi.Models;
using ParanaBancoClienteApi.Repositories.Interfaces;
using System.Runtime.InteropServices;

namespace ParanaBancoClienteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : Controller
    {
        private readonly IClienteRepositorio _clienteRepositorio;

        public ClienteController(IClienteRepositorio clienteRepositorio)
        {
            _clienteRepositorio = clienteRepositorio;
        }


        [HttpGet]
        public async Task<ActionResult<List<ClienteDTO>>> ListarTodosClientes()
        {
            try
            {
                var clientes = await _clienteRepositorio.BuscarTodos();

                var clientesDTO = clientes.Select(c => new ClienteDTO
                {
                    Id = c.Id,
                    Nome = c.Nome,
                    Email = c.Email,
                    Sobrenome = c.Sobrenome,
                    Telefones = c.Telefones.Select(c => new TelefoneDTO
                    {
                        DDD = c.DDD,
                        Numero = c.Numero,
                        TipoTelefone = c.ObterDescricaoEnum(c.TipoTelefone)

                    }).ToList()
                });

                if (clientesDTO == null)
                {
                    return NotFound();
                }

                return Ok(clientesDTO);
            }
            catch (Exception)
            {
                return StatusCode(500, $"Ocorreu um erro interno no servidor.");
            }
        }

        [HttpGet("clientes/{ddd},{numeroTelefone}")]
        public async Task<ActionResult<ClienteDTO>> BuscarClientePorTelefone(string ddd, string numeroTelefone)
        {
            try
            {
                var cliente = await _clienteRepositorio.BuscarPorTelefoneEhDdd(ddd, numeroTelefone);

                ClienteDTO clienteDTO = new()
                {
                    Id = cliente.Id,
                    Nome = cliente.Nome,
                    Sobrenome = cliente.Sobrenome,
                    Email = cliente.Email,
                    Telefones = cliente.Telefones.Select(c => new TelefoneDTO
                    {
                        DDD = c.DDD,
                        Numero = c.Numero,
                        TipoTelefone = c.ObterDescricaoEnum(c.TipoTelefone)
                    }).ToList()
                };

                if (clienteDTO == null)
                {
                    return NotFound();
                }

                return Ok(clienteDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro interno no servidor. Mensagem: {ex.Message} ");
            }
        }

        [HttpPost("clientes")]
        public async Task<ActionResult<ClienteModel>> Cadastrar([FromBody] ClienteCadastroDTO cliente)
        {
            List<TelefoneModel> telefoneMdl = cliente.Telefones.Select(t => new TelefoneModel
            {
                DDD = t.DDD,
                Numero = t.Numero,
                TipoTelefone = t.TipoTelefone
            }).ToList();

            ClienteModel clienteMdl = new()
            {
                Nome = cliente.Nome,
                Sobrenome = cliente.Sobrenome,
                Email = cliente.Email,
                Telefones = telefoneMdl
            };

            try
            {
                var clienteCadastrado = await _clienteRepositorio.Adicionar(clienteMdl);

                if (clienteCadastrado == null)
                {
                    return NotFound();
                }

                return Ok(clienteCadastrado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro interno no servidor. Mensagem: {ex.Message} ");
            }

        }


        [HttpPut("clientes/{idCliente},{email}")]
        public async Task<ActionResult<ClienteModel>> AtualizarEmail(int idCliente, string email)
        {
            try
            {
                var cliente = await _clienteRepositorio.AtualizarEmail(idCliente, email);

                ClienteDTO clienteDTO = new()
                {
                    Id = cliente.Id,
                    Nome = cliente.Nome,
                    Email = cliente.Email,
                    Telefones = cliente.Telefones.Select(c => new TelefoneDTO
                    {
                        DDD = c.DDD,
                        Numero = c.Numero,
                        TipoTelefone = c.ObterDescricaoEnum(c.TipoTelefone)
                    }).ToList()
                };

                if (clienteDTO == null)
                {
                    return NotFound();
                }

                return Ok(clienteDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro interno no servidor. Mensagem: {ex.Message} ");
            }
        }

        [HttpPut("clientesTelefone/{telefoneAntigo},{telefoneNovo}")]
        public async Task<ActionResult<ClienteModel>> AtualizarTelefoneCliente(string telefoneAntigo, string telefoneNovo)
        {
            try
            {
                var cliente = await _clienteRepositorio.AtualizarTelefone(telefoneAntigo, telefoneNovo);

                ClienteDTO clienteDTO = new()
                {
                    Id = cliente.Id,
                    Nome = cliente.Nome,
                    Email = cliente.Email,
                    Telefones = cliente.Telefones.Select(c => new TelefoneDTO
                    {
                        DDD = c.DDD,
                        Numero = c.Numero,
                        TipoTelefone = c.ObterDescricaoEnum(c.TipoTelefone)
                    }).ToList()
                };

                if (clienteDTO == null)
                {
                    return NotFound();
                }

                return Ok(clienteDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro interno no servidor. Mensagem: {ex.Message} ");
            }
        }

        [HttpDelete("clientes/{email}")]
        public async Task<ActionResult<ClienteModel>> Deletar(string email)
        {
            try
            {
                bool apagado = await _clienteRepositorio.ApagarPorEmail(email);

                return Ok(apagado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro interno no servidor. Mensagem: {ex.Message} ");
            }
        }
    }
}
