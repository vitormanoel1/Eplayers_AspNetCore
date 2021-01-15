using System;
using System.Collections.Generic;
using System.IO;
using Eplayers_AspNetCore.Interfaces;

namespace Eplayers_AspNetCore.Models
{
    public class Equipe : EPlayersBase, IEquipe
    {
        // ID indentificador
        public int IdEquipe { get; set; }
        public string Nome { get; set; }
        public string Imagem { get; set; }

        private const string PATH = "DataBase/Equipe.csv";

        public Equipe()
        {
            CreateFolderAndFile(PATH);
        }

        public string Preparar(Equipe NovaEquipe)
        {
            return $"{NovaEquipe.IdEquipe};{NovaEquipe.Nome};{NovaEquipe.Imagem}";
        }

        public void Create(Equipe NovaEquipe)
        {
            // preparando as linhas do CSV
            string[] linhas = { Preparar(NovaEquipe) };
            File.AppendAllLines(PATH, linhas);
        }

        public void Delete(int idEquipe)
        {
            List<string> linhas = ReadAllLinesCSV(PATH);

            //removemos a linha que tenha o codigo alterado
            linhas.RemoveAll(x => x.Split(";")[0] == idEquipe.ToString());

            //chamando o metodo de rescrita
            RewriteCSV(PATH, linhas);
        }

        public List<Equipe> ReadAll()
        {
            List<Equipe> equipes = new List<Equipe>();
            // Ler todas as linhas do CSV
            string[] linhas = File.ReadAllLines(PATH);

            // Percorrer as linhas e adicionar na lista de equipes cada objeto equipe
            foreach (var item in linhas)
            {
                // 1;Loud;Loud.jpg
                string[] linha = item.Split(";");
                // depois de ter feito isso eu vou ter 
                // [0] = 1
                // [1] = Loud
                // [2] = Loud.jpg

                // criação do objeto equipe
                Equipe equipe = new Equipe();

                // alimentamos o objeto equipe coms suas propriedades
                equipe.IdEquipe = Int32.Parse( linha[0]);
                equipe.Nome = linha[1];
                equipe.Imagem = linha[2];

                // adicionamos a equipe na lista de equipes
                equipes.Add(equipe);
            }

            return equipes;
        }

        public void Update(Equipe NovaEquipe)
        {
            List<string> linhas = ReadAllLinesCSV(PATH);

            //removemos a linha que tenha o codigo alterado
            linhas.RemoveAll(x => x.Split(";")[0] == NovaEquipe.IdEquipe.ToString());

            // adiciona a linha alterada no final do arquivo com o mesmo codigo
            linhas.Add( Preparar(NovaEquipe) );

            //chamando o metodo de rescrita
            RewriteCSV(PATH, linhas);
        }
    }
} 