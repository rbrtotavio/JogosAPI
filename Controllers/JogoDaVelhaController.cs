using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JogosAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace JogosAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JogoDaVelhaController : ControllerBase
    {
        private static JogoDaVelha jogo = new JogoDaVelha();

        [HttpGet("GetGrid")]
        public IActionResult GetGrid()
        {
            string jsonGrid;
            Console.WriteLine(jogo.ResultadoJogo.ToString());

            if (jogo.ResultadoJogo == MensagemRespostas.EmAndamento)
            {
                MensagemRespostas respostas = jogo.CPUInserirSímbolo();
                jsonGrid = JsonConvert.SerializeObject(jogo.Grid);
                return TratarRespostas(respostas, jsonGrid);
            }
            else
            {
                jsonGrid = JsonConvert.SerializeObject(jogo.Grid);
                return TratarRespostas(jogo.ResultadoJogo, jsonGrid);
            }
        }

        private IActionResult TratarRespostas(MensagemRespostas resposta, string jsonGrid)
        {
            return resposta switch
            {
                MensagemRespostas.Derrota => Ok(new { mensagem = "Você perdeu... Reinicie para tentar novamente.", grid = jsonGrid }),
                MensagemRespostas.Empate => Ok(new { mensagem = "O Jogo foi um empate!! Reinicie para jogar novamente.", grid = jsonGrid }),
                MensagemRespostas.Vitoria => Ok(new { mensagem = "Parabéns você venceu!! Reinicie para jogar novamente.", grid = jsonGrid }),
                MensagemRespostas.VezDoJogador => Ok(new { mensagem = "Agora é sua vez, tente lançar uma jogada.", grid = jsonGrid }),
                _ => Ok(jsonGrid)
            };
        }

        [HttpPost("Inserir/{linha}/{coluna}")]
        public IActionResult PostSimbolo(int linha, int coluna)
        {
            if (jogo.ResultadoJogo == MensagemRespostas.EmAndamento)
            {
                if (linha >= 1 && linha <= 3 && coluna >= 1 && coluna <= 3)
                {
                    MensagemRespostas resposta = jogo.InserirSímbolo(linha, coluna);
                    return resposta switch
                    {
                        MensagemRespostas.Sucesso => Ok(new { mensagem = $"A inserção foi um sucesso em [{linha}, {coluna}]" }),
                        MensagemRespostas.CoordenadaJaPreencida => Ok(new { mensagem = $"A coordenada [{linha}, {coluna}] já está preenchida" }),
                        MensagemRespostas.Vitoria => Ok(new { mensagem = "Parabéns você venceu!! Reinicie para jogar novamente." }),
                        MensagemRespostas.Empate => Ok(new { mensagem = "O Jogo foi um empate!! Reinicie para jogar novamente." }),
                        MensagemRespostas.VezDoCPU => Ok(new { mensagem = $"É a vez do CPU, Verifique onde ele posicionou a jogada!" }),
                        _ => BadRequest(resposta),
                    };
                }
                return NotFound(new { mensage = "A linha e a coluna devem ser iguais a números de [1] a [3]" });
            }
            else
            {
                return jogo.ResultadoJogo switch
                {
                    MensagemRespostas.Derrota => Ok(new { mensagem = "Você perdeu... Reinicie para tentar novamente." }),
                    MensagemRespostas.Empate => Ok(new { mensagem = "O Jogo foi um empate!! Reinicie para jogar novamente." }),
                    MensagemRespostas.Vitoria => Ok(new { mensagem = "Parabéns você venceu!! Reinicie para jogar novamente." }),
                    _ => BadRequest("Estado de jogo não reconhecido.")
                };
            }
        }

        [HttpPut("AlterarOrdem")]
        public IActionResult UpdateOrdem()
        {
            MensagemRespostas resultado = jogo.AlterarOrdem();
            return resultado switch
            {
                MensagemRespostas.OrdemAlterada => Ok(new { mensagem = $"Ordem alterada!" }),
                MensagemRespostas.OrdemNaoAlterada => Ok(new { mensagem = $"Não é possivel alterar a ordem de jogadas após o início do jogo" }),
                _ => BadRequest(resultado),
            };
        }

        [HttpDelete("ReiniciarGrid")]
        public IActionResult ReiniciarGrid()
        {
            MensagemRespostas resultado = jogo.ReiniciarGrid();
            return resultado switch
            {
                MensagemRespostas.GridReiniciado => Ok(new { mensagem = $"GridReiniciado" }),
                _ => BadRequest(resultado),
            };
        }
    }
}