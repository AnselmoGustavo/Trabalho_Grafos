using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace Trabalho_Grafos
{
    public class Arquivo
    {
        private string caminhoArquivo;
        private List<string> linhasDoArquivo;
        private string leituraLinha;


        public Arquivo(string caminho)
        {
            this.caminhoArquivo = caminho;
            this.linhasDoArquivo = File.ReadAllLines(caminho).ToList();
            this.leituraLinha = this.linhasDoArquivo[0];
        }

        public string[] RetornaPrimeiraLinha()
        {
            string[] qtdVerticesEArestas = leituraLinha.Split(' ');
            return qtdVerticesEArestas;
        }
        public int RetornaQuantidadeVertices()
        {
            return int.Parse(RetornaPrimeiraLinha()[0]);
        }
        public int RetornaQuantidadeArestas()
        {
            return int.Parse(RetornaPrimeiraLinha()[1]);
        }

        public HashSet<int> RetornaVertices()
        {
            HashSet<int> conjuntoVertices = new HashSet<int>();
            for (int i = 1; i < linhasDoArquivo.Count; i++)
            {
                var partes = linhasDoArquivo[i].Split(' ');
                int primeiroValor = int.Parse(partes[0]);
                int segundoValor = int.Parse(partes[1]);
                conjuntoVertices.Add(primeiroValor);
                conjuntoVertices.Add(segundoValor);
            }
            return conjuntoVertices;
        }

        public List<double> RetornaPesosArestas()
        {
            List<double> conjuntoPesoArestas = new List<double>();
            for (int i = 1; i < linhasDoArquivo.Count; i++)
            {
                string[] partes = linhasDoArquivo[i].Split(' ');
                conjuntoPesoArestas.Add(int.Parse(partes[2]));
            }
            return conjuntoPesoArestas;
        }
        public List<int> RetornaConjuntoDeArestas()
        {
            List<int> conjuntoDeArestas = new List<int>();
            int verticeOrigem, verticeDestino;

            for (int i = 1; i < linhasDoArquivo.Count; i++)
            {
                string[] partes = linhasDoArquivo[i].Split(' ');
                verticeOrigem = int.Parse(partes[0]);
                verticeDestino = int.Parse(partes[1]);
                conjuntoDeArestas.Add(verticeOrigem);
                conjuntoDeArestas.Add(verticeDestino);
            }
            return conjuntoDeArestas;
        }
    }
}
