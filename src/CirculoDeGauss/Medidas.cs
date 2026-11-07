namespace Conde.CirculoDeGauss;

/// <summary>As tabelas do README. Todo numero de la sai daqui.</summary>
public static class Medidas
{
    /// <summary>Os raios da tabela principal.</summary>
    public static readonly int[] Raios = { 1, 2, 3, 5, 10, 20, 50, 100, 200, 500, 1000, 2000, 5000 };

    public static List<LinhaDoErro> Erros() => Raios.Select(Erro.Linha).ToList();

    /// <summary>
    /// As tres contagens, lado a lado, nos raios em que a varredura quadrada
    /// ainda termina.
    /// </summary>
    public static List<(int Raio, long Varredura, long Colunas, long Divisores)> TresContagens(int ate)
    {
        var linhas = new List<(int, long, long, long)>();
        for (var r = 1; r <= ate; r++)
            linhas.Add((r, Reticulado.PorVarredura(r), Reticulado.PorColunas(r),
                        Reticulado.PorDivisores(r)));
        return linhas;
    }

    /// <summary>Quantas divergencias entre as tres contagens, de 1 ate o raio dado.</summary>
    public static (int Raios, int Divergencias) Conferencia(int ate)
    {
        var divergencias = 0;
        for (var r = 1; r <= ate; r++)
        {
            var a = Reticulado.PorVarredura(r);
            if (a != Reticulado.PorColunas(r) || a != Reticulado.PorDivisores(r)) divergencias++;
        }
        return (ate, divergencias);
    }

    /// <summary>
    /// O expoente posto a prova: para cada um, o maior |erro| / r^expoente nos
    /// raios de 1 a <paramref name="ate"/>, e onde ele acontece.
    /// </summary>
    public static List<(double Expoente, double Maior, int Raio)> Expoentes(int ate)
    {
        var linhas = new List<(double, double, int)>();
        foreach (var expoente in new[] { 0.0, 0.5, 2.0 / 3.0, 1.0 })
        {
            var (maior, onde) = Erro.MaiorRazao(ate, expoente);
            linhas.Add((expoente, maior, onde));
        }
        return linhas;
    }

    /// <summary>A contagem de borda pelos dois caminhos: busca e divisores.</summary>
    public static List<(int Raio, long PorBusca, long PorDivisores)> Borda(int ate)
    {
        var linhas = new List<(int, long, long)>();
        for (var r = 1; r <= ate; r++)
            linhas.Add((r, Reticulado.NaBorda(r), Reticulado.R2PorDivisores((long)r * r)));
        return linhas;
    }
}
