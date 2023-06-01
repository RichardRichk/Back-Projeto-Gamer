using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Back_Projeto_Gamer.Models
{
    public class Equipe
    {

        [Key] //DATA ANNOTATION - IdEquipe
        //using System.ComponentModel.DataAnnotations;
        public int IdEquipe { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }


        //referencia que a classe equipe tera acesso a COLLECTION "Jogador"
        public ICollection<Jogador> Jogador { get; set; }
    }
} 