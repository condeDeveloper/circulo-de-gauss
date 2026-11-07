# circulo-de-gauss

**Quantos pontos de coordenadas inteiras cabem num círculo de raio 5.000?**

78.539.677. A área do círculo é 78.539.816,34. A diferença é de **139 pontos** —
num total de setenta e oito milhões.

Contar é a parte fácil. O problema do círculo de Gauss é dizer o quanto essa
diferença cresce. Gauss provou que ela não passa de uma constante vezes *r*.
Desde 1906 o expoente vem caindo devagar — 2/3, depois 7/11, depois 131/208 — e
a conjectura é que o certo seja 1/2 mais um fiozinho. Ninguém provou.

Aqui o expoente não é provado. Ele é **medido**, e a medida mostra uma coisa que
a prova não mostra: qual expoente já estava decidido no raio 1, e qual ainda
estava subindo quando a conta parou.

## A contagem e o erro

| raio | pontos | área | diferença | \|dif\|/√r |
|-----:|-------:|-----:|----------:|-----------:|
| 1 | 5 | 3,14 | +1,86 | 1,858 |
| 2 | 13 | 12,57 | +0,43 | 0,307 |
| 3 | 29 | 28,27 | +0,73 | 0,419 |
| 5 | 81 | 78,54 | +2,46 | 1,100 |
| 10 | 317 | 314,16 | +2,84 | 0,898 |
| 20 | 1.257 | 1.256,64 | +0,36 | 0,081 |
| 50 | 7.845 | 7.853,98 | −8,98 | 1,270 |
| 100 | 31.417 | 31.415,93 | +1,07 | 0,107 |
| 200 | 125.629 | 125.663,71 | −34,71 | 2,454 |
| 500 | 785.349 | 785.398,16 | −49,16 | 2,199 |
| 1.000 | 3.141.549 | 3.141.592,65 | −43,65 | 1,380 |
| 2.000 | 12.566.345 | 12.566.370,61 | −25,61 | 0,573 |
| 5.000 | 78.539.677 | 78.539.816,34 | −139,34 | 1,971 |

Repare na coluna da diferença. Nos raios pequenos ela é positiva; a partir do 50
ela fica quase sempre negativa. **Em 981 dos 1.000 primeiros raios a contagem
fica abaixo da área.** Se o erro fosse um sorteio honesto, seriam uns 500.

## Três contagens que não se falam

O número de pontos sai de três lugares que não compartilham uma linha de código:

1. **A varredura quadrada:** olha os (2*r*+1)² pontos do quadrado e testa
   *x*² + *y*² ≤ *r*². Burra de propósito — ela é o juiz dos outros dois.
2. **A soma por colunas:** para cada *x*, a coluna tem 2⌊√(*r*²−*x*²)⌋ + 1
   pontos. Custa 2*r*+1 raízes inteiras.
3. **A identidade de Gauss:** *N* = 1 + 4·Σ(⌊*n*/(4*j*+1)⌋ − ⌊*n*/(4*j*+3)⌋),
   com *n* = *r*². Esta não sabe o que é geometria: é um teorema sobre
   **divisores**, e não tira uma raiz quadrada.

| raio | varredura | colunas | divisores |
|-----:|----------:|--------:|----------:|
| 1 | 5 | 5 | 5 |
| 5 | 81 | 81 | 81 |
| 10 | 317 | 317 | 317 |
| 12 | 441 | 441 | 441 |

De 1 a 300: **zero divergências** entre as três.

Toda contagem aqui é inteira e exata, inclusive a raiz quadrada, que é feita por
Newton sobre `long`. Ponto flutuante só aparece quando se compara a contagem com
π*r*², e só ali.

## O expoente, medido

Para cada expoente, o maior valor de |diferença| / *r*^expoente entre *r* = 1 e
*r* = 5.000, e o raio onde ele acontece:

| expoente | maior razão | onde |
|---------:|------------:|-----:|
| 0 | 487,518 | 4.751 |
| 1/2 | 7,529 | 2.728 |
| 2/3 | 2,601 | 72 |
| 1 | 1,858 | **1** |

A coluna "onde" é a interessante.

Com expoente **1** — a cota que Gauss provou — o recorde acontece no raio 1 e
**nunca mais é batido** em cinco mil raios. A cota sobra tanto que o caso mais
apertado é o círculo de raio 1.

Com expoente **2/3** o recorde está em *r* = 72, ainda cedo: a razão sobe, dá o
pico e desce.

Com expoente **1/2** o recorde está em *r* = 2.728, perto do fim do intervalo. A
razão **ainda estava subindo** quando a medição parou. Isso não prova nada — mas
é exatamente o desenho que se espera de um expoente que está no limite.

## Os pontos em cima do círculo

Quantos pontos caem exatamente na borda, ou seja, de quantos jeitos *r*² é soma
de dois quadrados:

| raio | por busca | pelo teorema dos divisores |
|-----:|----------:|---------------------------:|
| 1 a 4 | 4 | 4 |
| 5 | 12 | 12 |
| 10 | 12 | 12 |
| 13 | 12 | 12 |
| 15 | 12 | 12 |
| 17 | 12 | 12 |
| 20 | 12 | 12 |

Dos mil primeiros raios, **433** têm só os quatro pontos dos eixos. O campeão é
o raio **325**, com **60** pontos na borda — 325 = 5²·13, e cada primo da forma
4*k*+1 multiplica as maneiras de escrever a soma.

## O que as medidas me corrigiram

- **Esperava o erro simétrico em torno de zero.** Não é: em 981 dos 1.000
  primeiros raios a contagem fica **abaixo** da área. A diferença não oscila em
  volta do zero, ela mora de um lado.
- **Ia reportar só o maior erro, e o maior erro não diz nada.** 139 pontos em
  78 milhões parece ótimo, e 1,86 em 5 parece péssimo — e é o raio 1 que manda
  na cota de Gauss. O que importa é a razão com *r*^expoente, não o erro cru.
- **Quase usei `Math.Sqrt` na contagem por colunas.** Com `double`, um ponto
  exatamente em cima da borda pode cair para fora por erro de arredondamento, e
  a contagem erra em silêncio. A raiz inteira por Newton resolve e não custa
  nada.
- **O expoente 2/3 parecia pior que 1/2 pelo valor da razão.** É o contrário: o
  que separa os dois não é o valor, é **onde** o recorde acontece. O 2/3 bate o
  recorde em *r* = 72 e depois desce; o 1/2 bate em *r* = 2.728 e ainda estava
  subindo.

## Quatro achados

1. Em **981 dos 1.000** primeiros raios a contagem de pontos fica **abaixo** da
   área do círculo: o erro não oscila em torno de zero, ele mora de um lado.
2. Com o expoente 1 de Gauss, o caso mais apertado em cinco mil raios é o
   **raio 1**, e nenhum outro chega perto.
3. Com o expoente 1/2 da conjectura, o recorde acontece em **r = 2.728** e a
   razão ainda estava subindo quando a medição parou.
4. Três contagens que não compartilham uma linha — varredura, colunas e
   identidade de divisores — dão o mesmo número em **300 raios seguidos**, sem
   uma divergência.

## Rodar

```bash
dotnet build src/CirculoDeGauss/CirculoDeGauss.csproj -c Release
dotnet run --project ferramentas/Medidor -c Release -- erro
dotnet run --project ferramentas/Medidor -c Release -- contagens
dotnet run --project ferramentas/Medidor -c Release -- expoente
dotnet run --project ferramentas/Medidor -c Release -- borda
```

São 26 testes.

```bash
dotnet test testes/CirculoDeGauss.Testes/CirculoDeGauss.Testes.csproj -c Release
```

## Licença

MIT.
