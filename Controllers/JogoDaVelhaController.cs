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
            jogo.CPUInserirSímbolo();
            string jsonGrid = JsonConvert.SerializeObject(jogo.Grid);
            return Ok(jsonGrid);
        }

        [HttpPost("Inserir/{linha}/{coluna}")]
        public IActionResult PostSimbolo(int linha, int coluna)
        {
            if (linha >= 1 && linha <= 3 && coluna >= 1 && coluna <= 3)
            {
                string resposta = jogo.InserirSímbolo(linha, coluna);
                return Ok(resposta);
            }
            return NotFound("A linha e a coluna devem ser iguais a números de [1] a [3]");
        }

        [HttpPut("AlterarOrdem")]
        public IActionResult UpdateOrdem()
        {
            return Ok(jogo.AlterarOrdem());
        }
    }
}