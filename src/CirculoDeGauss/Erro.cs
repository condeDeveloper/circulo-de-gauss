namespace Conde.CirculoDeGauss;

/// <summary>Uma linha da tabela do erro: o raio, a contagem e o quanto ela foge da area.</summary>
public readonly record struct LinhaDoErro(int Raio, long Pontos, double Area, double Diferenca);

/// <summary>
/// O problema do circulo de Gauss nao e contar — contar e facil. E dizer o
/// quanto a contagem foge da area, e com que rapidez essa diferenca cresce.
///
/// Gauss provou que a diferenca nao passa de uma constante vezes r. Desde entao
/// o expoente vem caindo devagar, e a conjectura e que ele seja 1/2 (mais um
/// fiozinho). Aqui o expoente nao e provado: e MEDIDO num intervalo de raios, e
/// o que a medida mostra e qual expoente segura a razao e qual deixa ela fugir.
/// </summary>
public static class Erro
{
    public static LinhaDoErro Linha(int r)
    {
        var pontos = Reticulado.PorColunas(r);
        var area = Math.PI * r * r;
        return new LinhaDoErro(r, pontos, area, pontos - area);
    }

    /// <summary>
    /// O maior |diferenca| / r^expoente entre os raios de 1 a <paramref name="ate"/>,
    /// e o raio onde ele acontece.
    /// </summary>
    public static (double Maior, int Raio) MaiorRazao(int ate, double expoente)
    {
        var maior = 0.0;
        var onde = 1;
        for (var r = 1; r <= ate; r++)
        {
            var razao = Math.Abs(Linha(r).Diferenca) / Math.Pow(r, expoente);
            if (razao > maior)
            {
                maior = razao;
                onde = r;
            }
        }
        return (maior, onde);
    }

    /// <summary>
    /// O maior numero de pontos em cima do circulo, para raios de 1 a
    /// <paramref name="ate"/>, com o raio onde acontece.
    /// </summary>
    public static (int Raio, long Pontos) RaioMaisPovoado(int ate)
    {
        var melhorRaio = 1;
        long melhor = 0;
        for (var r = 1; r <= ate; r++)
        {
            var naBorda = Reticulado.NaBorda(r);
            if (naBorda > melhor)
            {
                melhor = naBorda;
                melhorRaio = r;
            }
        }
        return (melhorRaio, melhor);
    }

    /// <summary>
    /// Quantos raios de 1 a <paramref name="ate"/> tem exatamente quatro pontos
    /// na borda — os (r,0), (0,r) e os dois simetricos, e nada mais.
    /// </summary>
    public static int RaiosComQuatroNaBorda(int ate)
    {
        var quantos = 0;
        for (var r = 1; r <= ate; r++)
            if (Reticulado.NaBorda(r) == 4) quantos++;
        return quantos;
    }

    /// <summary>
    /// Em quantos raios de 1 a <paramref name="ate"/> a contagem FICA ABAIXO da
    /// area. Se o erro fosse um sorteio honesto, seria perto da metade.
    /// </summary>
    public static int RaiosAbaixoDaArea(int ate)
    {
        var quantos = 0;
        for (var r = 1; r <= ate; r++)
            if (Linha(r).Diferenca < 0) quantos++;
        return quantos;
    }
}
