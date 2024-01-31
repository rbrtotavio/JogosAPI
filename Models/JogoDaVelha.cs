using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;


namespace JogosAPI.Models
{
    public enum MensagemRespostas
    {
        EmAndamento,
        Sucesso,
        CoordenadaJaPreencida,
        VezDoCPU,
        OrdemAlterada,
        OrdemNaoAlterada,
        GridReiniciado,
        Vitoria,
        Empate,
        Derrota,
        VezDoJogador,
    }
    public class JogoDaVelha
    {
        public char[,] Grid { get; private set; }
        public bool JogadorVaiPrimeiro { get; private set; }
        private bool JogoInciado { get; set; }
        private bool VezDoCPU { get; set; }
        public MensagemRespostas ResultadoJogo { get; private set; }
        private Random Random { get; set; }
        public JogoDaVelha()
        {
            Random = new();
            IniciarJogo();
        }

        private void IniciarJogo()
        {
            JogadorVaiPrimeiro = true;
            VezDoCPU = false;
            Grid = new char[3, 3];
            ResultadoJogo = MensagemRespostas.EmAndamento;
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
        public MensagemRespostas CPUInserirSímbolo()
        {
            bool simboloInserido = false;
            if (VezDoCPU)
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
                        VezDoCPU = false;

                        char resultado = VerificarVitoria();
                        if (Grid[randomLinha, randomColuna] == resultado)
                        {
                            ResultadoJogo = MensagemRespostas.Derrota;
                            return ResultadoJogo;
                        }
                        else if (resultado == 'E')
                        {
                            ResultadoJogo = MensagemRespostas.Empate;
                            return ResultadoJogo;
                        }
                    }
                } while (!simboloInserido);
            }

            return MensagemRespostas.VezDoJogador;
        }
        /// <summary>
        /// Método usado para que o Jogador insira a linha e a coluna a qual ele gostaria de inserir seu símbolo.
        /// </summary>
        /// <param name="linha"> Inteiro de 1 a 3</param>
        /// <param name="coluna">Inteiro de 1 a 3</param>
        /// <returns>Mensagem de sucesso ou falha do processo de inserção</returns>
        public MensagemRespostas InserirSímbolo(int linha, int coluna)
        {
            char coordenada = Grid[linha - 1, coluna - 1];

            if (!VezDoCPU)
            {
                if (coordenada == ' ')
                {
                    Grid[linha - 1, coluna - 1] = JogadorVaiPrimeiro ? 'X' : 'O';
                    JogoInciado = true;
                    VezDoCPU = true;
                    char resultado = VerificarVitoria();
                    if (Grid[linha - 1, coluna - 1] == resultado)
                    {
                        ResultadoJogo = MensagemRespostas.Vitoria;
                        return ResultadoJogo;
                    }
                    else if (resultado == 'E')
                    {
                        ResultadoJogo = MensagemRespostas.Empate;
                        return ResultadoJogo;
                    }
                    return MensagemRespostas.Sucesso;
                }
                else
                {
                    return MensagemRespostas.CoordenadaJaPreencida;
                }
            }
            return MensagemRespostas.VezDoCPU;
        }
        /// <summary>
        /// Método que altera a ordem de jogadas
        /// </summary>
        /// <returns>Mensagem referente ao sucesso ou falha da alteração</returns>
        public MensagemRespostas AlterarOrdem()
        {
            if (!JogoInciado)
            {
                JogadorVaiPrimeiro = !JogadorVaiPrimeiro;
                VezDoCPU = !VezDoCPU;
                return MensagemRespostas.OrdemAlterada;
            }
            return MensagemRespostas.OrdemNaoAlterada;
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
                    vencedor = Grid[i, 1];
                    break;
                }
                if (Grid[0, i] == Grid[1, i] && Grid[1, i] == Grid[2, i])
                {
                    vencedor = Grid[1, i];
                    break;
                }
            }


            if (VerificarEmpate()) vencedor = 'E';

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

        public MensagemRespostas ReiniciarGrid()
        {
            VezDoCPU = !JogadorVaiPrimeiro;
            ResultadoJogo = MensagemRespostas.EmAndamento;
            InicializarGrid();
            return MensagemRespostas.GridReiniciado;
        }
    }

}