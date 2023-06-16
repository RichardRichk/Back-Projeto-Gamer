using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Back_Projeto_Gamer.Infra;
using Back_Projeto_Gamer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Back_Projeto_Gamer.Controllers
{
    [Route("[controller]")]
    public class CadastroController : Controller
    {
        private readonly ILogger<CadastroController> _logger;

        public CadastroController(ILogger<CadastroController> logger)
        {
            _logger = logger;
        }

        [TempData]
        public string Message{ get; set; }
        Context c = new Context();

        public IActionResult Index()
        {

            ViewBag.Equipe = c.Equipe.ToList();

            return View();
        }

        public IActionResult Cadastrar(IFormCollection form)
        {
            Jogador novoJogador = new Jogador();

            novoJogador.Name = form["Name"].ToString();

            novoJogador.Email = form["Email"].ToString();

            novoJogador.Senha = form["Senha"].ToString();

            novoJogador.IdEquipe = int.Parse(form["IdEquipe"]);

            Jogador jogadorBuscado = c.Jogador.FirstOrDefault(j => j.Email == novoJogador.Email);

            if (jogadorBuscado != null)
            {
                message = "Email ja cadastrado!";

                return LocalRedirect("~/Jogador/Listar");
            }
            else
            {
                
            c.Jogador.Add(novoJogador);

            c.SaveChanges();

            return LocalRedirect("~/Jogador/Listar");

            }

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}