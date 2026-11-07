namespace Conde.CirculoDeGauss;

/// <summary>
/// Quantos pontos de coordenadas inteiras cabem num circulo de raio r centrado
/// na origem. Tres jeitos de contar, que nao compartilham uma linha: a
/// varredura quadrada, a soma por colunas e a identidade de divisores.
///
/// A contagem e sempre INTEIRA e exata. Ponto flutuante so aparece quando se
/// compara a contagem com a area, pi*r^2, e so ali.
/// </summary>
public static class Reticulado
{
    /// <summary>
    /// A varredura burra: olha todo ponto do quadrado de lado 2r+1 e testa
    /// x^2 + y^2 &lt;= r^2. Custa (2r+1)^2 testes e existe para ser o juiz dos
    /// outros dois.
    /// </summary>
    public static long PorVarredura(int r)
    {
        var limite = (long)r * r;
        long dentro = 0;
        for (long x = -r; x <= r; x++)
            for (long y = -r; y <= r; y++)
                if (x * x + y * y <= limite) dentro++;
        return dentro;
    }

    /// <summary>
    /// A soma por colunas: para cada x, a coluna vai de -piso(raiz(r^2-x^2)) ate
    /// +piso(...), entao tem 2*piso + 1 pontos. Custa 2r+1 raizes.
    /// </summary>
    public static long PorColunas(int r)
    {
        var limite = (long)r * r;
        long dentro = 0;
        for (long x = -r; x <= r; x++)
        {
            var alturaQuadrada = limite - x * x;
            if (alturaQuadrada < 0) continue;
            dentro += 2 * RaizInteira(alturaQuadrada) + 1;
        }
        return dentro;
    }

    /// <summary>
    /// A identidade de Gauss: N(r) = 1 + 4 * soma de (piso(n/(4j+1)) -
    /// piso(n/(4j+3))), com n = r^2. Ela sai de contar, para cada n, de quantos
    /// jeitos n e soma de dois quadrados — e essa contagem e um teorema sobre
    /// DIVISORES, nao sobre geometria. Nenhuma raiz quadrada aqui.
    /// </summary>
    public static long PorDivisores(int r)
    {
        var n = (long)r * r;
        long soma = 0;
        for (long j = 0; 4 * j + 1 <= n; j++)
            soma += n / (4 * j + 1) - n / (4 * j + 3);
        return 1 + 4 * soma;
    }

    /// <summary>
    /// Quantos pontos caem EXATAMENTE em cima do circulo de raio r, ou seja,
    /// r2(r^2): de quantos jeitos r^2 e soma de dois quadrados com sinal e
    /// ordem.
    /// </summary>
    public static long NaBorda(int r)
    {
        var alvo = (long)r * r;
        long emCima = 0;
        for (long x = -r; x <= r; x++)
        {
            var resto = alvo - x * x;
            if (resto < 0) continue;
            var y = RaizInteira(resto);
            if (y * y != resto) continue;
            emCima += y == 0 ? 1 : 2;
        }
        return emCima;
    }

    /// <summary>
    /// r2(n) pelo teorema dos divisores: 4 vezes (divisores da forma 4k+1 menos
    /// divisores da forma 4k+3). Juiz da contagem de borda.
    /// </summary>
    public static long R2PorDivisores(long n)
    {
        if (n == 0) return 1;
        long um = 0, tres = 0;
        for (long d = 1; d <= n; d++)
        {
            if (n % d != 0) continue;
            if (d % 4 == 1) um++;
            else if (d % 4 == 3) tres++;
        }
        return 4 * (um - tres);
    }

    /// <summary>A raiz quadrada inteira, por Newton, sem ponto flutuante.</summary>
    public static long RaizInteira(long n)
    {
        if (n < 0) throw new ArgumentOutOfRangeException(nameof(n));
        if (n < 2) return n;

        var x = n;
        var y = (x + 1) / 2;
        while (y < x)
        {
            x = y;
            y = (x + n / x) / 2;
        }
        return x;
    }
}
