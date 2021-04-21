using ArtigoXUnitTestes.Domain.Services;
using ArtigoXUnitTestes.UnitTests.Domain.Factories;
using Xunit;

namespace ArtigoXUnitTestes.UnitTests.Domain.Services
{
    public class OperacaoFinanceiraServiceTransferirTests
    {
        [Fact]
        public void ContaOrigemTemLimiteESaldoSuficiente_ChamadoComContasEValoresValidos_RetornarSucessoEDebitarDosSaldosELImiteCorretamente()
        {
            const decimal valorTransacao = 500;
            const decimal contaCorrenteOrigemSaldoEsperado = 9500;
            const decimal contaCorrenteOrigemLimiteEsperado = 19500;
            const decimal contaCorrenteDestinoSaldoEsperado = 10500;
            const decimal contaCorrenteDestinoLimiteEsperado = 20000;

            // Arrange
            var contaCorrenteOrigem = ContaCorrenteFactory.GetContaOrigemValida();
            var contaCorrenteDestino = ContaCorrenteFactory.ObterContaDestinoValida();

            var operacaoFinanceiraService = new OperacaoFinanceiraService();

            // Act
            var resultadoOperacao = operacaoFinanceiraService.Transferencia(contaCorrenteOrigem, contaCorrenteDestino, valorTransacao);

            // Assert
            Assert.True(resultadoOperacao);
            Assert.Equal(contaCorrenteOrigemSaldoEsperado, contaCorrenteOrigem.Saldo);
            Assert.Equal(contaCorrenteOrigemLimiteEsperado, contaCorrenteOrigem.Limite);
            Assert.Equal(contaCorrenteDestinoSaldoEsperado, contaCorrenteDestino.Saldo);
            Assert.Equal(contaCorrenteDestinoLimiteEsperado, contaCorrenteDestino.Limite);
        }

        [Fact]
        public void ContaOrigemTemLimiteMasSaldoInsuficiente_ChamadoComContasEValoresValidos_RetornarFalhaENaoAlterarSaldosELimites()
        {
            const decimal valorTransacaoAcimaDoSaldoInicial = 10500;
            const decimal contaCorrenteOrigemSaldoEsperado = 10000;
            const decimal contaCorrenteOrigemLimiteEsperado = 20000;
            const decimal contaCorrenteDestinoSaldoEsperado = 10000;
            const decimal contaCorrenteDestinoLimiteEsperado = 20000;

            // Arrange
            var contaCorrenteOrigem = ContaCorrenteFactory.GetContaOrigemValida();
            var contaCorrenteDestino = ContaCorrenteFactory.ObterContaDestinoValida();

            var operacaoFinanceiraService = new OperacaoFinanceiraService();

            // Act
            var resultadoOperacao = operacaoFinanceiraService.Transferencia(contaCorrenteOrigem, contaCorrenteDestino, valorTransacaoAcimaDoSaldoInicial);

            // Assert
            Assert.False(resultadoOperacao);
            Assert.Equal(contaCorrenteOrigemSaldoEsperado, contaCorrenteOrigem.Saldo);
            Assert.Equal(contaCorrenteOrigemLimiteEsperado, contaCorrenteOrigem.Limite);
            Assert.Equal(contaCorrenteDestinoSaldoEsperado, contaCorrenteDestino.Saldo);
            Assert.Equal(contaCorrenteDestinoLimiteEsperado, contaCorrenteDestino.Limite);
        }

        [Fact]
        public void ContaOrigemTemSaldoMasLimiteInsuficiente_ChamadoComContasEValoresValidos_RetornarFalhaENaoAlterarSaldosELimites()
        {
            const decimal valorTransacaoAcimaDoLimiteInicial = 20500;
            const decimal contaCorrenteOrigemSaldoEsperado = 30000;
            const decimal contaCorrenteOrigemLimiteEsperado = 20000;
            const decimal contaCorrenteDestinoSaldoEsperado = 10000;
            const decimal contaCorrenteDestinoLimiteEsperado = 20000;

            // Assert
            var contaCorrenteOrigem = ContaCorrenteFactory.GetContaOrigemValida();
            var contaCorrenteDestino = ContaCorrenteFactory.ObterContaDestinoValida();

            contaCorrenteOrigem.Creditar(20000);

            var operacaoFinanceiraService = new OperacaoFinanceiraService();

            // Act
            var resultadoOperacao = operacaoFinanceiraService.Transferencia(contaCorrenteOrigem, contaCorrenteDestino, valorTransacaoAcimaDoLimiteInicial);

            // Assert
            Assert.False(resultadoOperacao);
            Assert.Equal(contaCorrenteOrigemSaldoEsperado, contaCorrenteOrigem.Saldo);
            Assert.Equal(contaCorrenteOrigemLimiteEsperado, contaCorrenteOrigem.Limite);
            Assert.Equal(contaCorrenteDestinoSaldoEsperado, contaCorrenteDestino.Saldo);
            Assert.Equal(contaCorrenteDestinoLimiteEsperado, contaCorrenteDestino.Limite);
        }

        [Fact]
        public void ContaOrigemComLimiteESaldoInsuficientes_ChamadoComContasEValoresValidos_RetornarFalhaENaoAlterarSaldosELimites()
        {
            const decimal valorTransacaoAcimaDoSaldoELimiteInicial = 50000;
            const decimal contaCorrenteOrigemSaldoEsperado = 10000;
            const decimal contaCorrenteOrigemLimiteEsperado = 20000;
            const decimal contaCorrenteDestinoSaldoEsperado = 10000;
            const decimal contaCorrenteDestinoLimiteEsperado = 20000;

            // Assert
            var contaCorrenteOrigem = ContaCorrenteFactory.GetContaOrigemValida();
            var contaCorrenteDestino = ContaCorrenteFactory.ObterContaDestinoValida();

            var operacaoFinanceiraService = new OperacaoFinanceiraService();

            // Act
            var resultadoOperacao = operacaoFinanceiraService.Transferencia(contaCorrenteOrigem, contaCorrenteDestino, valorTransacaoAcimaDoSaldoELimiteInicial);

            // Assert
            Assert.False(resultadoOperacao);
            Assert.Equal(contaCorrenteOrigemSaldoEsperado, contaCorrenteOrigem.Saldo);
            Assert.Equal(contaCorrenteOrigemLimiteEsperado, contaCorrenteOrigem.Limite);
            Assert.Equal(contaCorrenteDestinoSaldoEsperado, contaCorrenteDestino.Saldo);
            Assert.Equal(contaCorrenteDestinoLimiteEsperado, contaCorrenteDestino.Limite);
        }
    }
}
