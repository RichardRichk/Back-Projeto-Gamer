using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Back_Projeto_Gamer.Models
{
    public class Jogador
    {
        
        [Key] //DATA ANNOTATION - IdEquipe
        //using System.ComponentModel.DataAnnotations;
        public int IdJogador { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Senha { get; set; }

        [ForeignKey("Equipe")] //Data annotation - IdEquipe
        //using System.ComponentModel.DataAnnotations.Schema;
        public int IdEquipe { get; set; }

        public Equipe Equipe { get; set; }

    }
}