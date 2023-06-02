using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Back_Projeto_Gamer.Models;
using Microsoft.EntityFrameworkCore;

namespace Back_Projeto_Gamer.Infra
{
    public class Context : DbContext //So utiliza com pacote baixado: using Microsoft.EntityFrameworkCore;
    {
        public Context()
        {

        }

        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //STRING DE CONEXAO COM O BANCO


                //Data Source : O nome do servidor do gerenciador do banco
                //initial catalog : nome do banco de dados


                //AUTENTICACAO PELO WINDOWS
                //Integrated Security : Autenticacao pelo windows
                //TrustServerCertificate : Autenticacao pelo windows


                // ATENTICACAO PELO SQLSERVER
                //user Id = "nome do seu usuario de login"
                //pwd = "senha do seu usuario"
                //TrustServerCertificate

                optionsBuilder.UseSqlServer("Data Source = NOTE20-S15; initial catalog = gamerManha; user Id = sa; pwd = Senai@134; TrustServerCertificate = true");
            }
        }


        //Referencia de classes e tabelas 
        public DbSet<Jogador> Jogador { get; set; }
        public DbSet<Equipe> Equipe { get; set; }
    }
}