using Conde.CirculoDeGauss;
using Xunit;

namespace Conde.CirculoDeGauss.Testes;

/// <summary>
/// O comportamento: as tres contagens, a raiz inteira e a borda. Os numeros das
/// tabelas ficam no <c>MedidasTestes</c>.
/// </summary>
public class NucleoTestes
{
    [Fact]
    public void ORaioUmPegaCincoPontos()
    {
        // A origem e os quatro vizinhos dela.
        Assert.Equal(5, Reticulado.PorVarredura(1));
        Assert.Equal(5, Reticulado.PorColunas(1));
        Assert.Equal(5, Reticulado.PorDivisores(1));
    }

    [Fact]
    public void ORaioZeroPegaSoAOrigem()
    {
        Assert.Equal(1, Reticulado.PorVarredura(0));
        Assert.Equal(1, Reticulado.PorColunas(0));
        Assert.Equal(1, Reticulado.PorDivisores(0));
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(7)]
    [InlineData(23)]
    [InlineData(60)]
    [InlineData(137)]
    public void AsTresContagensConcordam(int r)
    {
        var porVarredura = Reticulado.PorVarredura(r);
        Assert.Equal(porVarredura, Reticulado.PorColunas(r));
        Assert.Equal(porVarredura, Reticulado.PorDivisores(r));
    }

    [Fact]
    public void ARaizInteiraENewtonSemPontoFlutuante()
    {
        Assert.Equal(0, Reticulado.RaizInteira(0));
        Assert.Equal(1, Reticulado.RaizInteira(1));
        Assert.Equal(1, Reticulado.RaizInteira(3));
        Assert.Equal(2, Reticulado.RaizInteira(4));
        Assert.Equal(3, Reticulado.RaizInteira(15));
        Assert.Equal(1_000_000, Reticulado.RaizInteira(1_000_000_000_000));
        Assert.Equal(999_999, Reticulado.RaizInteira(999_999_999_999));
        Assert.Throws<ArgumentOutOfRangeException>(() => Reticulado.RaizInteira(-1));
    }

    [Fact]
    public void ARaizInteiraNuncaPassaDoQuadrado()
    {
        for (long n = 0; n < 20000; n++)
        {
            var r = Reticulado.RaizInteira(n);
            Assert.True(r * r <= n);
            Assert.True((r + 1) * (r + 1) > n);
        }
    }

    [Fact]
    public void ABordaDoRaioCincoTemDozePontos()
    {
        // (5,0), (0,5), (3,4), (4,3) e os simetricos: doze ao todo. E o primeiro
        // raio em que a borda passa dos quatro obvios.
        Assert.Equal(12, Reticulado.NaBorda(5));
        Assert.Equal(12, Reticulado.R2PorDivisores(25));
    }

    [Theory]
    [InlineData(1)]
    [InlineData(5)]
    [InlineData(13)]
    [InlineData(25)]
    [InlineData(65)]
    public void ABordaPelosDoisCaminhos(int r)
    {
        Assert.Equal(Reticulado.NaBorda(r), Reticulado.R2PorDivisores((long)r * r));
    }

    [Fact]
    public void OTeoremaDosDivisoresValeParaTodoNAteMil()
    {
        // r2(n) = 4 * (divisores 4k+1 menos divisores 4k+3), conferido contra a
        // contagem direta de x^2 + y^2 = n.
        for (long n = 1; n <= 1000; n++)
        {
            long direta = 0;
            var teto = Reticulado.RaizInteira(n);
            for (long x = -teto; x <= teto; x++)
            {
                var resto = n - x * x;
                if (resto < 0) continue;
                var y = Reticulado.RaizInteira(resto);
                if (y * y != resto) continue;
                direta += y == 0 ? 1 : 2;
            }
            Assert.Equal(direta, Reticulado.R2PorDivisores(n));
        }
    }

    [Fact]
    public void ACoroaCresceSempre()
    {
        // Aumentar o raio nunca tira ponto de dentro.
        long anterior = 0;
        for (var r = 0; r <= 200; r++)
        {
            var agora = Reticulado.PorColunas(r);
            Assert.True(agora >= anterior);
            anterior = agora;
        }
    }

    [Fact]
    public void OErroDoRaioUmEOMaiorEmProporcao()
    {
        var l = Erro.Linha(1);
        Assert.Equal(5, l.Pontos);
        Assert.Equal(1.858, Math.Round(l.Diferenca, 3));
    }
}
