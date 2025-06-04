using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Trabalho_Grafos
{
    public class Aresta
    {
        private static int ultimoId = 0;
        private const string _letraID = "e";
        private string id;
        private double pesoDaAresta;
        private Vertice origem;
        private Vertice destino;

        public Aresta(Vertice origem, Vertice destino, double valorPeso) 
        {
            this.pesoDaAresta = valorPeso;
            this.origem = origem;
            this.destino = destino;
            this.id = GerarId(++ultimoId);
        }

        private string GerarId(int valor)
        {
            return $"{_letraID}{valor}";
        }

        public Vertice RetornaVerticeDestino()
        {
            return destino;    
        }

        public Vertice RetornaVerticeOrigem()
        { 
            return origem;
        }
        public string RetornaID() 
        {
            return id;
        }
        public void AlterarPeso(double x)
        {
            this.pesoDaAresta = x;
        }

        public double RetornarPeso()
        {
            return this.pesoDaAresta;
        }
    }
}
