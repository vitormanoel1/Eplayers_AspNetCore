using System;
using System.Collections.Generic;
using System.IO;
using Eplayers_AspNetCore.Interfaces;

namespace Eplayers_AspNetCore.Models
{
    public class Jogador : EPlayersBase, IJogador
    {
        // atributos
        public int IdJogador { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public int IdEquipe { get; set; }  
        
        private const string PATH = "DataBase/Jogador.csv";

        public Jogador()
        {
            CreateFolderAndFile(PATH);
        }

        public string Preparar(Jogador jogador)
        {
            return $"{jogador.IdJogador};{jogador.Nome};{jogador.Email};{jogador.Senha};{jogador.IdEquipe}";
        }

        public void Create(Jogador jogador)
        {
            string[] linhas = { Preparar(jogador) };
            File.AppendAllLines(PATH, linhas);
        }

        public void Delete(int IdJogador)
        {
            List<string> linhas = ReadAllLinesCSV(PATH);
            linhas.RemoveAll(x => x.Split(";")[0] == IdJogador.ToString());
            RewriteCSV(PATH, linhas);
        }

        public List<Jogador> ReadAll()
        {
            List<Jogador> jogadores = new List<Jogador>();
            string[] linhas = File.ReadAllLines(PATH);

            foreach (var item in linhas)
            {
                string[] linha = item.Split(";");

                Jogador jogador = new Jogador();

                jogador.IdEquipe = Int32.Parse( linha[0] );
                jogador.Nome = linha[1];
                jogador.Email = linha[2];
                jogador.Senha = linha[3];
                jogador.IdJogador = Int32.Parse( linha[4]);

                jogadores.Add(jogador);
            }
            return jogadores;
        }

        public void Update(Jogador jogador)
        {
            List<string> linhas = ReadAllLinesCSV(PATH);

            linhas.RemoveAll(x => x.Split(":")[0] == jogador.IdJogador.ToString());

            linhas.Add( Preparar(jogador));

            RewriteCSV(PATH, linhas);        }
    }
}