﻿using ArtigoXUnitTestes.Application.Services;
using ArtigoXUnitTestes.Domain.Entities;
using ArtigoXUnitTestes.Domain.Repositories;
using ArtigoXUnitTestes.Infrastructure.Services;
using ArtigoXUnitTestes.UnitTests.Application.Factories;
using ArtigoXUnitTestes.UnitTests.Domain.Factories;
using AutoFixture;
using Moq;
using System.Linq;
using Xunit;

namespace ArtigoXUnitTestes.UnitTests.Application.Services
{
    public class ContaCorrenteServiceNotificarContaCorrenteTests
    {
        [Fact]
        public void ContaExistenteNotificacaoFuncionando_ChamadoDocumentoValido_RetornarSucesso()
        {
            // Arrange
            var fixture = new Fixture();
            var contaCorrente = fixture.Create<ContaCorrente>();
            // ANTES: var contaCorrente = ContaCorrenteFactory.GetContaOrigemValida();

            fixture.RepeatCount = 5;
            var operacoes = fixture.CreateMany<OperacaoContaCorrente>().ToList();

            var respostaNotificacaoViewModel = RespostaNotificacaoViewModelFactory.ObterRespostaSucesso();

            var contaCorrenteRepositoryMock = new Mock<IContaCorrenteRepository>();
            var notificacaoServiceMock = new Mock<INotificacaoService>();

            contaCorrenteRepositoryMock.Setup(ccr => ccr.ObterPorDocumento(contaCorrente.Documento)).Returns(contaCorrente);
            notificacaoServiceMock.Setup(ns => ns.Notificar(contaCorrente)).Returns(respostaNotificacaoViewModel);

            var contaCorrenteService = new ContaCorrenteService(notificacaoServiceMock.Object, contaCorrenteRepositoryMock.Object);

            // Act
            var resposta = contaCorrenteService.NotificarContaCorrente(contaCorrente.Documento);

            // Assert
            contaCorrenteRepositoryMock.Verify(ccr => ccr.ObterPorDocumento(contaCorrente.Documento), Times.Once);
            notificacaoServiceMock.Verify(ns => ns.Notificar(It.IsAny<ContaCorrente>()), Times.Once);

            Assert.True(resposta);
        }

        [Fact]
        public void ContaExistenteMasNotificacaoNaoFuncionando_ChamadoDocumentoValido_RetornarFalha()
        {
            // Arrange
            var fixture = new Fixture();
            var contaCorrente = fixture.Create<ContaCorrente>();

            var respostaNotificacaoViewModel = RespostaNotificacaoViewModelFactory.ObterRespostaFalha();

            var contaCorrenteRepositoryMock = new Mock<IContaCorrenteRepository>();
            var notificacaoServiceMock = new Mock<INotificacaoService>();

            contaCorrenteRepositoryMock.Setup(ccr => ccr.ObterPorDocumento(contaCorrente.Documento)).Returns(contaCorrente);
            notificacaoServiceMock.Setup(ns => ns.Notificar(contaCorrente)).Returns(respostaNotificacaoViewModel);

            var contaCorrenteService = new ContaCorrenteService(notificacaoServiceMock.Object, contaCorrenteRepositoryMock.Object);

            // Act
            var resposta = contaCorrenteService.NotificarContaCorrente(contaCorrente.Documento);

            // Assert
            contaCorrenteRepositoryMock.Verify(ccr => ccr.ObterPorDocumento(contaCorrente.Documento), Times.Once);
            notificacaoServiceMock.Verify(ns => ns.Notificar(It.IsAny<ContaCorrente>()), Times.Once);

            Assert.False(resposta);
        }

        [Fact]
        public void ContaExistenteSemDocumentoNotificacaoNaoFuncionando_ChamadoDocumentoNull_RetornarFalha()
        {
            // Arrange
            var contaCorrente = ContaCorrenteFactory.GetContaOrigemSemDocumento();
            var respostaNotificacaoViewModel = RespostaNotificacaoViewModelFactory.ObterRespostaFalha();

            var contaCorrenteRepositoryMock = new Mock<IContaCorrenteRepository>();
            var notificacaoServiceMock = new Mock<INotificacaoService>();

            contaCorrenteRepositoryMock.Setup(ccr => ccr.ObterPorDocumento(contaCorrente.Documento)).Returns((ContaCorrente)null);
            notificacaoServiceMock.Setup(ns => ns.Notificar(contaCorrente)).Returns(respostaNotificacaoViewModel);

            var contaCorrenteService = new ContaCorrenteService(notificacaoServiceMock.Object, contaCorrenteRepositoryMock.Object);

            // Act
            var resposta = contaCorrenteService.NotificarContaCorrente(contaCorrente.Documento);

            // Assert
            contaCorrenteRepositoryMock.Verify(ccr => ccr.ObterPorDocumento(contaCorrente.Documento), Times.Once);
            notificacaoServiceMock.Verify(ns => ns.Notificar(It.IsAny<ContaCorrente>()), Times.Never);

            Assert.False(resposta);

        }
    }
}
