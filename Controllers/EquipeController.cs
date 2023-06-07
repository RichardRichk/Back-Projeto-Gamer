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
    public class EquipeController : Controller
    {
        private readonly ILogger<EquipeController> _logger;

        public EquipeController(ILogger<EquipeController> logger)
        {
            _logger = logger;
        }

        //instancia do contexto para acessar banco de dados
        Context c = new Context(); 

        [Route("Listar")] //http://localhost/Equipe/Listar
        public IActionResult Index()
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");

            //Variavel que armazena as equipes listadas no banco de dados
            ViewBag.Equipe = c.Equipe.ToList();

            //Retonrna view de (equipe9TELA)
            return View();
        }


        [Route("Cadastrar")]
        public IActionResult Cadastrar(IFormCollection form)
        {
            //instancia do objeto equipe
            Equipe novaEquipe = new Equipe(); 

            //Atribuicao de valores rebidos do formulario
            novaEquipe.Name = form["Nome"].ToString();

            //AQUI ESTSVS CHEGANDO STRING(NAO QUEREMOS DESTA FORMA)
            // novaEquipe.Image = form["imagem"].ToString();

            //INICIO DA LOGICA DO UPLOAD DA IMAGEM:
            if (form.Files.Count > 0)
            {
                
                var file = form.Files[0];

                var folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Equipes");

                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                //gera o caminho completo ate o caminho do arquivo(imagem - nome com extensao)
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/", folder, file.FileName);
                //PODE usar desta forma == var path = Path.Combine(folder, file.FileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                novaEquipe.Image = file.FileName;

            }

            else
            {
                novaEquipe.Image = "padrao.png";
            }
            //FIM DA LOGICA DO UPLOAD DA IMAGEM:


            //Adiciona objeto na tabela do BD
            c.Equipe.Add(novaEquipe);

            //Salva as alteracoes feitas no BD
            c.SaveChanges();

    //=================================================================================
            //atualiza a lista testar sem esse autozicao
            // ViewBag.Equipe = c.Equipe.ToList();
            //NAO NECESSARIO UTILIZAR A ATUALIZACAO, 
            //POIS JA ESTA SENDO REDIRECIONADO NO return ABAIXO
    //=================================================================================
            
            //retorna para o local chamando a rota de listar(metodo index)
            return LocalRedirect("~/Equipe/Listar");
        }


        [Route("Excluir/{id}")]
        public IActionResult Excluir(int id)
        {
            Equipe equipeBuscada = c.Equipe.First(e => e.IdEquipe == id);

            c.Remove(equipeBuscada);

            c.SaveChanges();

            return LocalRedirect("~/Equipe/Listar");
           
        }

        [Route("Editar/{id}")]
        public IActionResult Editar(int id)
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");

            Equipe equipeBuscada = c.Equipe.First(x => x.IdEquipe == id);

            ViewBag.Equipe = equipeBuscada;

            return View("Edit");
        }


        [Route("Atualizar")]
        public IActionResult Atualizar(IFormCollection form)
        {
            Equipe equipeAtualizada = new Equipe();

            equipeAtualizada.IdEquipe = int.Parse(form["IdEquipe"].ToString());

            equipeAtualizada.Name = form["Nome"].ToString();

            if (form.Files.Count > 0)
            {
                var file = form.Files[0];

                var folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Equipe");

                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                var path = Path.Combine(folder, file.FileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                equipeAtualizada.Image = file.FileName;
            }
            else
            {
                equipeAtualizada.Image = "padrao.png";
            }
            
            Equipe equipeBuscada = c.Equipe.First(x => x.IdEquipe == equipeAtualizada.IdEquipe);

            equipeBuscada.Name = equipeAtualizada.Name;
            equipeBuscada.Image = equipeAtualizada.Image;

            c.Equipe.Update(equipeBuscada);

            c.SaveChanges();

            return LocalRedirect("~/Equipe/Listar");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}