using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabalho_Grafos
{
    public class Vertice
    {
        private int rotuloDoVertice;
        private List<Aresta> listaDeArestas;
        private int TempoDescoberta;
        private int TempoTermino;
        private Vertice Pai;
        private int Indice;
        private int Nivel;

        public Vertice(int rotulo) 
        {
            this.rotuloDoVertice = rotulo;
            listaDeArestas = new List<Aresta>();
        }

        public int RetornaRotuloDoVertice()
        {
            return this.rotuloDoVertice;    
        }
        public void InsereNaListaDeArestas(Aresta aresta)
        { 
            listaDeArestas.Add(aresta); 
        }
        public List<Aresta> RetornaListaDeArestas()
        {
            return listaDeArestas;
        }
        public void LimpaArestas()
        {
            listaDeArestas.Clear();
        }
        public void SetTempoDescoberta(int tempo)
        {
            this.TempoDescoberta = tempo;
        }
        public void SetTempoTermino(int tempo)
        {
            this.TempoTermino = tempo;
        }
        public int RetornaTempoDescoberta()
        {
            return this.TempoDescoberta;
        }
        public int RetornaTempoTermino()
        {
            return this.TempoTermino;
        }
        public void SetPai(Vertice pai)
        {
            this.Pai = pai;
        }
        public Vertice RetornaPai()
        {
            return this.Pai;
        }
        public List<Vertice> RetornaVizinhanca()
        {
            List<Vertice> vizinhos = new List<Vertice>();
            foreach (Aresta aresta in listaDeArestas)
            {
                vizinhos.Add(aresta.RetornaVerticeDestino());
            }
            return vizinhos;
        }
        public Aresta RetornaArestaEspecifica(Vertice destino)
        {
            foreach(Aresta aresta in listaDeArestas)
            {
                if (aresta.RetornaVerticeDestino() == destino)
                {
                    return aresta;
                }
            }
            return null;
        }
        public override string ToString()
        {
            return this.rotuloDoVertice.ToString();
        }
    }
}
