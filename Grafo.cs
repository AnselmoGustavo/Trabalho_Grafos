using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabalho_Grafos
{
    public class Grafo
    {
        private static int _ultimoID = 0;
        private int id;
        private List<Vertice> conjuntoDeVertices;
        private int quantidadeDeVertices;
        private int quantidadeDeArestas;

        public Grafo(int quantidadeDeVertices, int quantidadeDeArestas) 
        {
            id = ++_ultimoID;
            this.quantidadeDeVertices = quantidadeDeVertices;
            this.quantidadeDeArestas = quantidadeDeArestas; 
            conjuntoDeVertices = new List<Vertice>(quantidadeDeVertices);   
        }

        public void PreencheRotuloVertices(int rotulo)
        {
            if (conjuntoDeVertices.Count < quantidadeDeVertices)
            {
                Vertice vertice = new Vertice(rotulo);
                conjuntoDeVertices.Add(vertice);
            }
            else
            {
                Console.WriteLine("Não é possível adicionar mais vértices. Capacidade máxima atingida.");
            }
        }

        public void PreenchePesoArestas(int origem, int destino, double peso)
        {
            Vertice verticeDeOrigem = ObterVerticePorRotulo(origem);
            Vertice verticeDeDestino = ObterVerticePorRotulo(destino);
            Aresta novaAresta = null;

            if (verticeDeOrigem != null && verticeDeDestino != null)
            {
                 novaAresta = new Aresta(verticeDeOrigem, verticeDeDestino, peso);
                 verticeDeOrigem.InsereNaListaDeArestas(novaAresta);
            }
            else 
            {
                Console.WriteLine("Vértice de origem e/ou de destino inválidos");
            }
        }

        //Mudei isso aqui pra público pra poder usar no adjacência de vértices
        public Vertice ObterVerticePorRotulo(int rotulo)
        {
            foreach (Vertice vertice in conjuntoDeVertices)
            {
                if (rotulo == vertice.RetornaRotuloDoVertice())
                {
                    return vertice;
                }
            }

            return null;
        }

        public double CalculaDensidadeDoGrafo()
        {
            return quantidadeDeArestas / (quantidadeDeVertices * (quantidadeDeVertices - 1));
        }

        public int[,] GerarMatrizAdjacencia()
        {
            int[,] matrizDeAdjacencia = new int[conjuntoDeVertices.Count, conjuntoDeVertices.Count];

            for (int i = 0; i < conjuntoDeVertices.Count; i++)
            {
                Vertice verticeOrigem = conjuntoDeVertices[i];

                foreach (Aresta aresta in verticeOrigem.RetornaListaDeArestas()) 
                {
                    int indiceVerticeDestino = conjuntoDeVertices.IndexOf(aresta.RetornaVerticeDestino());

                    if (indiceVerticeDestino != -1)
                    {
                        matrizDeAdjacencia[i, indiceVerticeDestino] = 1;
                    }
                } 
                
            }

            
            return matrizDeAdjacencia;  
        }

        public List<Aresta> RetornaTodasAsArestasDoGrafo() 
        {
            List<Aresta> arestaTotal = new List<Aresta>();

            foreach (Vertice vertice in conjuntoDeVertices)
            {
                foreach (Aresta aresta in vertice.RetornaListaDeArestas()) 
                {
                    if (!arestaTotal.Contains(aresta))
                    {
                        arestaTotal.Add(aresta);
                    }
                }
            }
            return arestaTotal;
        }

        public int[,] GerarMatrizIncidência() 
        {
            List<Aresta> arestaTotal = RetornaTodasAsArestasDoGrafo();  
            int[,] matrizDeIncidencia = new int[conjuntoDeVertices.Count, quantidadeDeArestas];

            for (int i = 0; i < arestaTotal.Count; i++)
            {
                Aresta aresta = arestaTotal[i];
                int indiceVerticeOrigem = conjuntoDeVertices.IndexOf(aresta.RetornaVerticeOrigem());
                int indiceVerticeDestino = conjuntoDeVertices.IndexOf(aresta.RetornaVerticeDestino());

                matrizDeIncidencia[indiceVerticeOrigem, i] = -1;
                matrizDeIncidencia[indiceVerticeDestino, i] = 1;
            }
            return matrizDeIncidencia;
        }

        public List<Vertice>[] GerarListaDeAdjacencia()
        {
            List<Vertice>[] listaAdjacência = new List<Vertice>[quantidadeDeVertices];

            for (int i = 0; i < quantidadeDeVertices; i++)
            {
                listaAdjacência[i] = new List<Vertice>();
            }

            for (int i = 0; i < quantidadeDeVertices; i++)
            {
                Vertice vertice = conjuntoDeVertices[i];

                foreach (Aresta aresta in vertice.RetornaListaDeArestas()) 
                {
                    
                    listaAdjacência[i].Add(aresta.RetornaVerticeDestino());
                }
            }
            return listaAdjacência;
        }

        public bool VerificarAdjacenciaVertice(Vertice x, Vertice y)
        {
            bool adjacente = false;
            List<Aresta> arestasVertice=x.RetornaListaDeArestas();
            foreach (Aresta aresta in arestasVertice)
            {
                if(aresta.RetornaVerticeDestino() == y)
                {
                    adjacente=true;
                    return adjacente;
                }
            }
            return adjacente;
        }

        public override int GetHashCode()
        {
            return id.GetHashCode();
        }

        public List<Vertice> RetornaListaVertices()
        {
            return conjuntoDeVertices;
        }
        

    }
}
