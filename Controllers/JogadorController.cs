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
    public class JogadorController : Controller
    {
        private readonly ILogger<JogadorController> _logger;

        public JogadorController(ILogger<JogadorController> logger)
        {
            _logger = logger;
        }

        Context c = new Context();

        [Route("Listar")]
        public IActionResult Index()
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");

            ViewBag.Jogador = c.Jogador.ToList();
            ViewBag.Equipe = c.Equipe.ToList();
            
            return View();
        }

        [Route("Cadastrar")]
        public IActionResult Cadastrar(IFormCollection form)
        {
            Jogador novoJogador = new Jogador();

            novoJogador.Name = form["Name"].ToString();

            novoJogador.Email = form["Email"].ToString();

            novoJogador.Senha = form["Senha"].ToString();

            novoJogador.IdEquipe = int.Parse(form["IdEquipe"]);

            c.Jogador.Add(novoJogador);

            c.SaveChanges();

            return LocalRedirect("~/Jogador/Listar");
        }


        [Route("Excluir/{id}")]
        public IActionResult Excluir(int id)
        {
            Jogador jogadorBuscado = c.Jogador.First(j => j.IdJogador == id);

            c.Remove(jogadorBuscado);

            c.SaveChanges();

            return LocalRedirect("~/Jogador/Listar");
        }


        [Route("Editar/{id}")]
        public IActionResult Editar(int id)
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");

            Jogador jogadorBuscado = c.Jogador.First(x => x.IdJogador == id);

            ViewBag.Jogador = jogadorBuscado;
            ViewBag.Equipe = c.Equipe;

            return View("Edit");
        }


        [Route("Atualizar")]
        public IActionResult Atualizar(IFormCollection form)
        {
            Jogador jogadorAtualizado = new Jogador();

            jogadorAtualizado.IdJogador = int.Parse(form["IdJogador"].ToString());
            jogadorAtualizado.Name = form["Name"].ToString();
            jogadorAtualizado.Email = form["Email"].ToString();
            jogadorAtualizado.Senha = form["Senha"].ToString();
            jogadorAtualizado.IdEquipe = int.Parse(form["IdEquipe"].ToString());

            Jogador jogadorBuscado = c.Jogador.First(j => j.IdJogador == jogadorAtualizado.IdJogador);

            jogadorBuscado.IdJogador = jogadorAtualizado.IdJogador;
            jogadorBuscado.Name = jogadorAtualizado.Name;
            jogadorBuscado.Email = jogadorAtualizado.Email;
            jogadorBuscado.Senha = jogadorAtualizado.Senha;
            jogadorBuscado.IdEquipe = jogadorAtualizado.IdEquipe;

            c.Jogador.Update(jogadorBuscado);

            c.SaveChanges();

            return LocalRedirect("~/Jogador/Listar");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}