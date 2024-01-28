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
    public class TesteController : ControllerBase
    {
        private static JogoDaVelha jogo = new JogoDaVelha();
        public string Nome { get; set; }
        public TesteController()
        {
            Nome = "teste";
        }

        [HttpGet("GetNome")]
        public IActionResult GetNome()
        {
            return Ok(Nome);
        }

        [HttpGet("GetGrid")]
        public IActionResult GetGrid()
        {
            string jsonGrid = JsonConvert.SerializeObject(jogo.Grid);
            return Ok(jsonGrid);
        }
    }
}