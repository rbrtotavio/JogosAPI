using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace JogosAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TesteController : ControllerBase
    {
        public string Nome { get; set; }
        public TesteController()
        {
            Nome = "teste";
        }

        [HttpGet]
        [Route("GetNome")]
        public IActionResult GetNome()
        {
            return Ok(Nome);
        }
    }
}