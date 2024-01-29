using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JogosAPI.Models
{
    public class JogoDaVelha
    {
        public char[,] Grid { get; set; }
        public bool JogadorVaiPrimeiro { get; private set; }
        private bool JogoInciado { get; set; }
        private bool Empate { get; set; }
        private Random Random { get; set; }
        public JogoDaVelha()
        {
            Random = new();
            JogadorVaiPrimeiro = true;
            Grid = new char[3, 3];
            InicializarGrid();
        }
        /// <summary>
        /// Método de inicialização de uma matriz 3x3 com caracteres em branco
        /// </summary>
        private void InicializarGrid()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Grid[i, j] = ' ';
                }
            }
            JogoInciado = false;
        }
        /// <summary>
        /// Método de inserção aleatória de símbolo por parte do CPU
        /// </summary>
        public void CPUInserirSímbolo()
        {
            bool simboloInserido = false;

            if (!Empate)
            {
                do
                {
                    int randomLinha = Random.Next(0, 3);
                    int randomColuna = Random.Next(0, 3);
                    char coordenada = Grid[randomLinha, randomColuna];

                    if (coordenada == ' ')
                    {
                        Grid[randomLinha, randomColuna] = !JogadorVaiPrimeiro ? 'X' : 'O';
                        simboloInserido = true;
                        JogoInciado = true;
                    }
                } while (!simboloInserido);
            }
        }
        /// <summary>
        /// Método usado para que o Jogador insira a linha e a coluna a qual ele gostaria de inserir seu símbolo.
        /// </summary>
        /// <param name="linha"> Inteiro de 1 a 3</param>
        /// <param name="coluna">Inteiro de 1 a 3</param>
        /// <returns>Mensagem de sucesso ou falha do processo de inserção</returns>
        public string InserirSímbolo(int linha, int coluna)
        {
            char coordenada = Grid[linha - 1, coluna - 1];

            if (coordenada == ' ')
            {
                Grid[linha - 1, coluna - 1] = JogadorVaiPrimeiro ? 'X' : 'O';
                JogoInciado = true;
                return $"Adicionado com sucecsso em [{linha}, {coluna}]";
            }
            else
            {
                return $"A coordenada ({linha}, {coluna}) já está preenchida com [{coordenada}]";
            }
        }
        /// <summary>
        /// Método que altera a ordem de jogadas
        /// </summary>
        /// <returns>Mensagem referente ao sucesso ou falha da alteração</returns>
        public string AlterarOrdem()
        {
            if (!JogoInciado)
            {
                JogadorVaiPrimeiro = !JogadorVaiPrimeiro;
                return $"Ordem Alterada - {(JogadorVaiPrimeiro ? "CPU[O] / Jogador[X]" : "CPU[X] / Jogador[O]")}"  ;
            }
            return "Não é possível alterar a ordem de jogadas após o início do jogo";
        }
        /// <summary>
        /// Método que verifica se há vitória na matriz
        /// </summary>
        /// <returns>
        /// Caracter referente ao símbolo vitorioso[X ou O] ou [' '] se o jogo não houver vencedores ainda
        /// </returns>
        public char VerificarVitoria()
        {
            char vencedor = ' ';

            if ((Grid[0, 0] == Grid[1, 1] && Grid[1, 1] == Grid[2, 2]) ||
                (Grid[0, 2] == Grid[1, 1] && Grid[1, 1] == Grid[2, 0]))
            {
                vencedor = Grid[1, 1];
            }

            for (int i = 0; i < 3; i++)
            {
                if (Grid[i, 0] == Grid[i, 1] && Grid[i, 1] == Grid[i, 2])
                {
                    vencedor = Grid[i, 0];
                }

                if (Grid[0, i] == Grid[1, i] && Grid[1, i] == Grid[2, i])
                {
                    vencedor = Grid[0, i];
                }
            }
            Empate = VerificarEmpate();
            if (Empate) vencedor = 'E';

            return vencedor;
        }
        /// <summary>
        /// Método que verifica a condição de empate do Jogo da Velha
        /// </summary>
        /// <returns>Verdadeiro se empatou, falso se não empatou</returns>
        private bool VerificarEmpate()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (Grid[i, j] == ' ')
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }

}