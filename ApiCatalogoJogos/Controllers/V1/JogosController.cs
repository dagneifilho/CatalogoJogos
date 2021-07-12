using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiCatalogoJogos.ViewModel;
using ApiCatalogoJogos.InputModel;
using ApiCatalogoJogos.Services;



namespace ApiCatalogoJogos.Controllers.V1 {
    [Route("api/V1/[controller]")]
    [ApiController]
    public  class JogosController : ControllerBase {
        private readonly IJogoService _jogoService;
        JogosController(IJogoService jogoService) {

            _jogoService = jogoService;
        }
        [HttpGet]
        public async Task<ActionResult<List<JogoViewModel>>> Obter() {

            var result = await _jogoService.Obter(1, 5);
            return Ok(result);

        }

        [HttpGet("{idJogo:guid}")]
        public async Task<ActionResult<List<JogoViewModel>>> Obetr(Guid idJogo) {
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<JogoViewModel>> InserirJogo(JogoInputModel jogo) {
            return Ok();
        }

        [HttpPut("{idJogo:guid}")]
        public async Task<ActionResult> AtualizarJogo(Guid idJogo, JogoInputModel jogo) {
            return Ok();
        }
        
        [HttpPatch("{idJogo:guid}/preco/{preco:double}")]
        public async Task<ActionResult> AtualilzarJogo(Guid idJgo, double preco) {
            return Ok();
        }
        
        [HttpDelete]
        public async Task<ActionResult> ApagarJogo(Guid idJogo){
            return Ok();
        }
    }
}
