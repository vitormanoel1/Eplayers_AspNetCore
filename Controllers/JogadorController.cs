using System;
using Eplayers_AspNetCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Eplayers_AspNetCore.Controllers
{
    [Route("Jogador")]
    public class JogadorController : Controller
    {
        Jogador jogadorModel = new Jogador();

        [Route("Listar")]
        public IActionResult Index()
        {
            ViewBag.Jogadores = jogadorModel.ReadAll();
            return View();
        }

        [Route("Cadastrar")]
        public IActionResult Cadastrar(IFormCollection Formulario)
        {
            Jogador cadJogador = new Jogador();

            cadJogador.IdEquipe = Int32.Parse(Formulario[ "IdEquipe" ]);
            cadJogador.Nome = Formulario [ "Nome" ];
            cadJogador.Email = Formulario [ "Email" ];
            cadJogador.Senha = Formulario [ "Senha" ];
            cadJogador.IdJogador =Int32.Parse(Formulario[ "IdJogador" ]);

            jogadorModel.Create(cadJogador);

            ViewBag.Jogadores = jogadorModel.ReadAll();

            return LocalRedirect("~/Jogador/Listar");
        }

        [Route("{id}")]
        public IActionResult Excluir(int IdJogador)
        {
            jogadorModel.Delete(IdJogador);

            ViewBag.Jogadores = jogadorModel.ReadAll();

            return LocalRedirect("~/Jogador/Listar");
        }
    }
}