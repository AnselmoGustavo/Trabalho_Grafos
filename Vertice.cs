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
        public override string ToString()
        {
            return this.rotuloDoVertice.ToString();
        }
    }
}
