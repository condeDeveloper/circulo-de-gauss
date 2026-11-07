using Conde.CirculoDeGauss;

var qual = args.Length > 0 ? args[0] : "tudo";

if (qual is "erro" or "tudo")
{
    Console.WriteLine("raio | pontos | area | diferenca | |dif|/raiz(r)");
    foreach (var l in Medidas.Erros())
        Console.WriteLine($"{l.Raio,5} | {l.Pontos,12} | {l.Area,16:F2} | {l.Diferenca,10:F2} | {Math.Abs(l.Diferenca) / Math.Sqrt(l.Raio),13:F3}");
    Console.WriteLine();
}

if (qual is "contagens" or "tudo")
{
    Console.WriteLine("raio | varredura | colunas | divisores");
    foreach (var (raio, varredura, colunas, divisores) in Medidas.TresContagens(12))
        Console.WriteLine($"{raio,4} | {varredura,9} | {colunas,7} | {divisores,9}");
    var (raios, divergencias) = Medidas.Conferencia(300);
    Console.WriteLine($"de 1 a {raios}: {divergencias} divergencias entre as tres");
    Console.WriteLine();
}

if (qual is "expoente" or "tudo")
{
    Console.WriteLine("expoente | maior |erro|/r^expoente ate r=5000 | onde");
    foreach (var (expoente, maior, raio) in Medidas.Expoentes(5000))
        Console.WriteLine($"{expoente,8:F3} | {maior,34:F3} | {raio,6}");
    Console.WriteLine();
}

if (qual is "borda" or "tudo")
{
    Console.WriteLine("raio | pontos na borda, por busca | pelo teorema dos divisores");
    foreach (var (raio, busca, divisores) in Medidas.Borda(20))
        Console.WriteLine($"{raio,4} | {busca,27} | {divisores,26}");
    var (maisPovoado, pontos) = Erro.RaioMaisPovoado(1000);
    Console.WriteLine($"raio com mais pontos na borda ate 1000: {maisPovoado}, com {pontos}");
    Console.WriteLine($"raios ate 1000 com exatamente quatro: {Erro.RaiosComQuatroNaBorda(1000)}");
    Console.WriteLine($"raios ate 1000 com contagem abaixo da area: {Erro.RaiosAbaixoDaArea(1000)}");
    Console.WriteLine();
}
