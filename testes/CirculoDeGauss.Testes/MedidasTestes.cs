using Conde.CirculoDeGauss;
using Xunit;

namespace Conde.CirculoDeGauss.Testes;

/// <summary>Os numeros das tabelas do README, presos. Todos saem do medidor.</summary>
public class MedidasTestes
{
    [Fact]
    public void ATabelaDoErro()
    {
        var linhas = Medidas.Erros();
        Assert.Equal(13, linhas.Count);

        var esperado = new (int Raio, long Pontos, double Diferenca)[]
        {
            (1, 5, 1.86), (2, 13, 0.43), (3, 29, 0.73), (5, 81, 2.46),
            (10, 317, 2.84), (20, 1257, 0.36), (50, 7845, -8.98),
            (100, 31417, 1.07), (200, 125629, -34.71), (500, 785349, -49.16),
            (1000, 3141549, -43.65), (2000, 12566345, -25.61),
            (5000, 78539677, -139.34),
        };

        for (var i = 0; i < esperado.Length; i++)
        {
            var (raio, pontos, diferenca) = esperado[i];
            Assert.Equal(raio, linhas[i].Raio);
            Assert.Equal(pontos, linhas[i].Pontos);
            Assert.Equal(diferenca, Math.Round(linhas[i].Diferenca, 2));
        }
    }

    [Fact]
    public void AsTresContagensNaoDivergemUmaVez()
    {
        var (raios, divergencias) = Medidas.Conferencia(300);
        Assert.Equal(300, raios);
        Assert.Equal(0, divergencias);

        var linhas = Medidas.TresContagens(12);
        Assert.Equal(12, linhas.Count);
        Assert.Equal((1, 5L, 5L, 5L), linhas[0]);
        Assert.Equal((12, 441L, 441L, 441L), linhas[11]);
        foreach (var (_, varredura, colunas, divisores) in linhas)
        {
            Assert.Equal(varredura, colunas);
            Assert.Equal(varredura, divisores);
        }
    }

    [Fact]
    public void ATabelaDosExpoentes()
    {
        var linhas = Medidas.Expoentes(5000);
        Assert.Equal(4, linhas.Count);

        Assert.Equal(487.518, Math.Round(linhas[0].Maior, 3));
        Assert.Equal(4751, linhas[0].Raio);

        Assert.Equal(7.529, Math.Round(linhas[1].Maior, 3));
        Assert.Equal(2728, linhas[1].Raio);

        Assert.Equal(2.601, Math.Round(linhas[2].Maior, 3));
        Assert.Equal(72, linhas[2].Raio);

        Assert.Equal(1.858, Math.Round(linhas[3].Maior, 3));
        Assert.Equal(1, linhas[3].Raio);
    }

    [Fact]
    public void OExpoenteUmEOQueJaEstaDecididoNoPrimeiroRaio()
    {
        // Com expoente 1 o maior valor da razao acontece em r = 1 e nunca mais e
        // batido ate 5.000: a cota de Gauss sobra. Com expoente 1/2 o maior
        // acontece em r = 2.728, perto do fim do intervalo medido — a razao
        // ainda estava subindo quando a medicao parou.
        var linhas = Medidas.Expoentes(5000);
        Assert.Equal(1, linhas[3].Raio);
        Assert.True(linhas[1].Raio > 2000);
    }

    [Fact]
    public void ATabelaDaBorda()
    {
        var linhas = Medidas.Borda(20);
        Assert.Equal(20, linhas.Count);
        foreach (var (_, busca, divisores) in linhas) Assert.Equal(busca, divisores);

        Assert.Equal((5, 12L, 12L), linhas[4]);
        Assert.Equal((10, 12L, 12L), linhas[9]);
        Assert.Equal((13, 12L, 12L), linhas[12]);

        // Os raios 1, 2, 3, 4, 6, 7, 8, 9, 11, 12, 14, 16, 18 e 19 tem so os
        // quatro pontos dos eixos.
        var soQuatro = linhas.Where(l => l.PorBusca == 4).Select(l => l.Raio).ToList();
        Assert.Equal(new[] { 1, 2, 3, 4, 6, 7, 8, 9, 11, 12, 14, 16, 18, 19 }, soQuatro);
    }

    [Fact]
    public void OTrezentosEVinteECincoEOCampeaoAteMil()
    {
        var (raio, pontos) = Erro.RaioMaisPovoado(1000);
        Assert.Equal(325, raio);
        Assert.Equal(60, pontos);
        Assert.Equal(433, Erro.RaiosComQuatroNaBorda(1000));
    }

    [Fact]
    public void AContagemFicaQuaseSempreABAIXODaArea()
    {
        // O achado: em 981 dos 1.000 primeiros raios a contagem de pontos fica
        // ABAIXO da area do circulo. Se o erro fosse um sorteio honesto, seriam
        // uns 500.
        Assert.Equal(981, Erro.RaiosAbaixoDaArea(1000));
    }
}
