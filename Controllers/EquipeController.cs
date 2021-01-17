using System;
using System.IO;
using Eplayers_AspNetCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Eplayers_AspNetCore.Controllers
{
    // especificação de rota. exemplo http://www.EPlayers.com.br/Equipe
    [Route("Equipe")]
    public class EquipeController : Controller
    {
        // uma instacia é criar um objeto (equipeModel) baseado em um determinado modelo/estrutura (Equipe),
        // então o objeto (equipeModel) vai ter a estrutura de (Equipe).
        // instanciando uma classe da pasta models
        Equipe equipeModel = new Equipe();

        // (index) vai listar as equipes , e retorna uma (View).
        // (IActionResult = Resultado da ação da Interface).
        [Route("Listar")] //exemplo http://www.EPlayers.com.br/Equipe/Listar
        public IActionResult Index()
        {
            // (ViewBag = recurso que armazena algumas informações e passa para a (View).)
            // (ViewBag.Equipe) vai receber o (equipeModel) + metodo que listar todos (ReadAll),
            // listando todas as equipes e enviando para a view através do (ViewBAg).
            ViewBag.Equipes = equipeModel.ReadAll();
            return View();
        }

        // (IFormCollection = Coleção de formulários da interface)
        [Route("Cadastrar")] //exemplo http://www.EPlayers.com.br/Equipe/Cadastrar
        public IActionResult Cadastrar(IFormCollection Formulario)
        {
            // criamos uma nova instancia de equipe
            // e armazenamos os dados enviados pelo formulário
            // e salvamos no objeto (criarEquipe)
            Equipe criarEquipe = new Equipe();

            // convertendo ( Formulario[ "IdEquipe" ] ) para (int32)
            criarEquipe.IdEquipe = Int32.Parse( Formulario[ "IdEquipe" ] );
            criarEquipe.Nome = Formulario[ "Nome" ];
            criarEquipe.Imagem = Formulario[ "Imagem" ];

            // Upload Início
            // verificamos se o usuário anexou um arquivo
                if (Formulario.Files.Count > 0)
                {
                    //se sim
                    // armazenamos o arquivo na variável
                    var file = Formulario.Files[0];
                    var folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Equipes" );

                    // verificamos se a pasta Equipes não existe
                    if (!Directory.Exists(folder))
                    {
                        // Se a pasta não existir, a criamos
                        Directory.CreateDirectory(folder);
                    }
                                                    // localgot:5001     +                + Equipes + equipe.jpg 
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", folder, file.FileName );
                    
                    using (var stream =  new FileStream(path, FileMode.Create))
                    {
                        // salvamos o arquivo no caminho especificado
                        file.CopyTo(stream);
                    }
                    

                    criarEquipe.Imagem = file.FileName;
                }
                else
                {
                    criarEquipe.Imagem = "padrao.png";
                }
            // Upload Término

            // depois de receber todas as infromações,
            // chamamos o método (create) para salvar (criarEquipe) no CSV.
            equipeModel.Create(criarEquipe);

            // recarregando a lista atualizada após um novo cadastro de equipe.
            ViewBag.Equipes = equipeModel.ReadAll();

            // invés de retorna uma (view), retorna com (LocalRedirect ("~/Equipe")), 
            // (~) = retorna na página raiz + (Equipe) para redirecionar para a mesma paginá ( ["~/Equipe"] ). 
            return LocalRedirect ("~/Equipe/Listar");
        }

        // http://localhost:5001/Equipe/2
        [Route("{id}")]
        // criamos o método de excluir
        public IActionResult Excluir(int IdEquipe)
        {
            equipeModel.Delete(IdEquipe);

            ViewBag.Equipes = equipeModel.ReadAll();

            return LocalRedirect("~/Equipes/Listar");
        }
    }
}