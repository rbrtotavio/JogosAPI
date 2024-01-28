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
                    Grid[randomLinha, randomColuna]= !JogadorVaiPrimeiro ? 'X' : 'O';
                    simboloInserido = true;
                    return $"Simbolo inserido com sucesso em [{randomLinha}, {randomColuna}]";
                }
                else 
                {
                    return $"Coordenada [{randomLinha}, {randomColuna}] já estava preenchida";
                }
            } while (!simboloInserido);
        }
    }

}