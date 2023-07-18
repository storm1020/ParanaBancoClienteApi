using Microsoft.AspNetCore.Mvc;
using Moq;
using ParanaBancoClienteApi.Controllers;
using ParanaBancoClienteApi.DataTransferObjects;
using ParanaBancoClienteApi.Enums;
using ParanaBancoClienteApi.Models;
using ParanaBancoClienteApi.Repositories;
using ParanaBancoClienteApi.Repositories.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParanaBancoClienteApi_Test
{
    public class ClienteControllerTest
    {
        [Fact]
        public async Task ListarTodosClientes_DeveRetornarListaDeCliente()
        {
            //Arrange
            var clientesMdl = new List<ClienteModel>
            {
                new ClienteModel
                {
                    Id = 1,
                    Nome = "Teste",
                    Sobrenome = "Sobrenome Teste",
                    Email = "teste@teste.com.br",
                    Telefones = new List<TelefoneModel>
                    {
                        new TelefoneModel
                        {
                            Id = 1,
                            DDD = "11",
                            Numero = "1234561299",
                            TipoTelefone = TipoTelefone.Celular
                        }
                    }
                },

                new ClienteModel
                {
                    Id = 2,
                    Nome = "Outro",
                    Sobrenome = "Teste",
                    Email = "teste@teste.com.br",
                    Telefones = new List<TelefoneModel>
                    {
                        new TelefoneModel
                        {
                            Id = 1,
                            DDD = "11",
                            Numero = "42428585",
                            TipoTelefone = TipoTelefone.Fixo
                        }
                    }
                }
            };

            var clienteRepositorioMock = new Mock<IClienteRepositorio>();

            clienteRepositorioMock.Setup(_ => _
                .BuscarTodos())
                .ReturnsAsync(clientesMdl);

            var controller = new ClienteController(clienteRepositorioMock.Object);

            //Act
            var result = await controller.ListarTodosClientes();

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            var clientesDTOAssert = Assert.IsAssignableFrom<IEnumerable<ClienteDTO>>(okResult.Value);

            Assert.Equal(2, clientesDTOAssert.Count());
        }

        [Fact]
        public async Task ListarTodosClientes_ErroNoRepositorio_DeveRetornarStatusCode500()
        {
            //Arrange
            var clienteRepositorioMock = new Mock<IClienteRepositorio>();

            clienteRepositorioMock.Setup(_ => _
                .BuscarTodos()).ThrowsAsync(new Exception("Erro no repositório!"));

            var controller = new ClienteController(clienteRepositorioMock.Object);

            //Act
            var result = await controller.ListarTodosClientes();


            //Assert
            var codigoStatusResult = Assert.IsType<ObjectResult>(result.Result);

            Assert.Equal(500, codigoStatusResult.StatusCode);
        }

        [Fact]
        public async Task BuscarClientePorTelefone_DeveRetornarCliente()
        {
            //Arrange  

            var clienteModel = new ClienteModel
            {
                Id = 1,
                Nome = "Teste",
                Sobrenome = "Sobrenome Teste",
                Email = "teste@teste.com.br",
                Telefones = new List<TelefoneModel>
                    {
                        new TelefoneModel
                        {
                            Id = 1,
                            DDD = "11",
                            Numero = "1234561299",
                            TipoTelefone = TipoTelefone.Celular
                        }
                    }
            };

            var clienteDTO = new ClienteDTO
            {
                Nome = "Teste",
                Sobrenome = "Sobrenome Teste",
                Email = "teste@teste.com.br",
                Telefones = new List<TelefoneDTO>
                    {
                        new TelefoneDTO
                        {
                            DDD = "11",
                            Numero = "1234561299",
                            TipoTelefone = "Celular"
                        }
                    }
            };

            var clienteRepositorioMock = new Mock<IClienteRepositorio>();

            clienteRepositorioMock.Setup(_ => _
                .BuscarPorTelefoneEhDdd("11", "1234561299"))
                .ReturnsAsync(clienteModel);

            var controller = new ClienteController(clienteRepositorioMock.Object);

            //Act
            var result = await controller.BuscarClientePorTelefone("11", "1234561299");

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            var valorRetornado = Assert.IsAssignableFrom<ClienteDTO>(okResult.Value);

            Assert.Contains(clienteDTO.Nome, valorRetornado.Nome);
        }        

        [Fact]
        public async Task CadastrarCliente_ErroNoRepositorio_DeveRetornarStatusCode500()
        {
            //Arrange  

            var clienteDTO = new ClienteCadastroDTO
            {
                Nome = "Teste",
                Sobrenome = "Sobrenome Teste",
                Email = "teste@teste.com.br",
                Telefones = new List<TelefoneViewDTO>
                    {
                        new TelefoneViewDTO
                        {
                            DDD = "11",
                            Numero = "1234561299",
                            TipoTelefone = TipoTelefone.Celular
                        }
                    }
            };

            var clienteModel = new ClienteModel
            {
                Id = 1,
                Nome = "Teste",
                Sobrenome = "Sobrenome Teste",
                Email = "teste@teste.com.br",
                Telefones = new List<TelefoneModel>
                    {
                        new TelefoneModel
                        {
                            Id = 1,
                            DDD = "11",
                            Numero = "1234561299",
                            TipoTelefone = TipoTelefone.Celular
                        }
                    }
            };

            var clienteRepositorioMock = new Mock<IClienteRepositorio>();

            clienteRepositorioMock.Setup(_ => _
                .Adicionar(clienteModel))
                .ReturnsAsync(clienteModel);

            var controller = new ClienteController(clienteRepositorioMock.Object);

            //Act
            var result = await controller.Cadastrar(clienteDTO);

            //Assert
            var codigoStatusResult = Assert.IsType<NotFoundResult>(result.Result);

            Assert.Equal(404, codigoStatusResult.StatusCode);
        }

        [Fact]
        public async Task AtualizarEmailCliente_DeveRetornarClienteComEmailAtualizado()
        {
            //Arrange
            var clienteModel = new ClienteModel
            {
                Id = 1,
                Nome = "Teste",
                Sobrenome = "Sobrenome Teste",
                Email = "teste@teste.com.br",
                Telefones = new List<TelefoneModel>
                    {
                        new TelefoneModel
                        {
                            Id = 1,
                            DDD = "11",
                            Numero = "1234561299",
                            TipoTelefone = TipoTelefone.Celular
                        }
                    }
            };

            var clienteRepositorioMock = new Mock<IClienteRepositorio>();

            clienteRepositorioMock.Setup(_ => _
                .AtualizarEmail(It.IsAny<int>(), It.IsAny<string>()))
                .ReturnsAsync(clienteModel);

            var controller = new ClienteController(clienteRepositorioMock.Object);

            var clienteId = 1;

            var novoEmail = "testeAtualizar@gmail.com";

            //Act
            var result = await controller.AtualizarEmail(clienteId, novoEmail);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            clienteRepositorioMock.Verify(repo => repo.AtualizarEmail(clienteId, novoEmail), Times.Once);
        }

        [Fact]
        public async Task AtualizarTelefone_DeveRetornarClienteComTelefoneAtualizado()
        {
            //Arrange
            var clienteModel = new ClienteModel
            {
                Id = 1,
                Nome = "Teste",
                Sobrenome = "Sobrenome Teste",
                Email = "teste@teste.com.br",
                Telefones = new List<TelefoneModel>
                    {
                        new TelefoneModel
                        {
                            Id = 1,
                            DDD = "11",
                            Numero = "1234561299",
                            TipoTelefone = TipoTelefone.Celular
                        }
                    }
            };

            var clienteRepositorioMock = new Mock<IClienteRepositorio>();

            clienteRepositorioMock.Setup(_ => _
                .AtualizarTelefone(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(clienteModel);

            var controller = new ClienteController(clienteRepositorioMock.Object);

            var telefoneAntigo = "1234561299";

            var telefoneNovo = "4444444444";

            //Act
            var result = await controller.AtualizarTelefoneCliente(telefoneAntigo, telefoneNovo);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            clienteRepositorioMock.Verify(repo => repo.AtualizarTelefone(telefoneAntigo, telefoneNovo), Times.Once);
        }


    }
}
