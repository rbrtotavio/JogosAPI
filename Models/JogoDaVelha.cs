using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JogosAPI.Models
{
    public class JogoDaVelha
    {
        public char[,] Grid { get; set; }
        private bool JogadorVaiPrimeiro { get; set; }
        private Random Random { get; set; }
        public JogoDaVelha()
        {
            Random = new();
            JogadorVaiPrimeiro = true;
            Grid = new char[3, 3];
            InicializarGrid();
        }
        private void InicializarGrid()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Grid[i, j] = ' ';
                }
            }
        }
        public string CPUInserirSímbolo()
        {
            bool simboloInserido = false;

            do
            {
                int randomLinha = Random.Next(0, 3);
                int randomColuna = Random.Next(0, 3);
                char coordenada = Grid[randomLinha, randomColuna];

                if (coordenada == ' ')
                {
                    Grid[randomLinha, randomColuna] = !JogadorVaiPrimeiro ? 'X' : 'O';
                    simboloInserido = true;
                    return $"Simbolo inserido com sucesso em [{randomLinha + 1}, {randomColuna + 1}]";
                }
                else
                {
                    return $"Coordenada [{randomLinha + 1}, {randomColuna + 1}] já estava preenchida";
                }
            } while (!simboloInserido);
        }

        public string InserirSímbolo(int linha, int coluna)
        {
            do
            {
                if (!CoordenadaValida(linha, coluna))
                {
                    Console.WriteLine("#####################");
                    Console.WriteLine($"Os valores da linha e da coluna devem estar entre [1] e [3]");
                    Console.WriteLine("#####################");
                }
            }
            while (!CoordenadaValida(linha, coluna));

            char coordenada = Grid[linha - 1, coluna - 1];

            if (coordenada == ' ')
            {
                Grid[linha - 1, coluna - 1] = JogadorVaiPrimeiro ? 'X' : 'O';
                return $"Adicionado com sucecsso em [{linha}, {coluna}]";
            }
            else
            {
                return $"A coordenada ({linha}, {coluna}) já está preenchida com [{coordenada}]";
            }
        }
        private static bool CoordenadaValida(int linha, int coluna)
        {
            return linha >= 1 && linha <= 3 && coluna >= 1 && coluna <= 3;
        }

        public void AlterarOrdem()
        {
            JogadorVaiPrimeiro = !JogadorVaiPrimeiro;
        }
    }

}