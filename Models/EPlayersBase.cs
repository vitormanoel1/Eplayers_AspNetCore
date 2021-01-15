using System.Collections.Generic;
using System.IO;

namespace Eplayers_AspNetCore.Models
{
    public abstract class EPlayersBase
    {
        // metodo de criar pasta e arquivo
        public void CreateFolderAndFile(string Caminho)
        {
            // DataBase/Equipe.csv
            string folder = Caminho.Split("/")[0];

            // metodo para criar pasta caso ela não exista
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            // metodo para cirar arquivo caso não exista um
            if (!File.Exists(Caminho))
            {
                File.Create(Caminho);
            }
        }

        public List<string> ReadAllLinesCSV(string Caminho)
        {
            List<string> linhas = new List<string>();

            // using -> responsavel por abrir e fechar determinado tipo de arquivo ou conexão 
            // biblioteca StreamReader -> responsavel por ler as informações do meu CSV
            using (StreamReader file = new StreamReader(Caminho))
            {
                // percorremos um arquivo lido atraves de um laço WHILE
                // vai ler todas as linhas enquanto tiver linhas para ler caso contrario se encerrará o laço
                string linha;
                while ((linha = file.ReadLine()) != null)
                {
                    linhas.Add(linha);
                }
            }

            return linhas;
        }

        // metodo para rescrever o CSV
        public void RewriteCSV(string Caminho, List<string> linhas)
        {
            // biblioteca StreamWriter -> responsavel pela escrita de arquivos
            using (StreamWriter  saidaArquivo = new StreamWriter(Caminho))
            {
                foreach (var item in linhas)
                {
                    saidaArquivo.Write(item+ '\n');
                }
            }
        }
    }
}