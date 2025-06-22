
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
        private List<Aresta> conjuntoDeArestas;
        private int quantidadeDeVertices;
        private int quantidadeDeArestas;

        public Grafo(int quantidadeDeVertices, int quantidadeDeArestas)
        {
            id = ++_ultimoID;
            this.quantidadeDeVertices = quantidadeDeVertices;
            this.quantidadeDeArestas = quantidadeDeArestas;
            conjuntoDeVertices = new List<Vertice>(quantidadeDeVertices);
            conjuntoDeArestas = new List<Aresta>(quantidadeDeArestas);
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
            
            Aresta novaAresta = new Aresta(verticeDeOrigem, verticeDeDestino, peso);
            conjuntoDeArestas.Add(novaAresta);

            if (verticeDeOrigem != null && verticeDeDestino != null)
            {
                verticeDeOrigem.InsereNaListaDeArestas(novaAresta);
            }
            else
            {
                Console.WriteLine("Vértice de origem e/ou de destino inválidos");
            }
        }

        
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

        public Aresta ObterArestaPorId(string id)
        {
            foreach (Aresta aresta in conjuntoDeArestas)
            {
                if (aresta.RetornaID() == id)
                {
                    return aresta;
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
            return conjuntoDeArestas;
        }

        public int[,] GerarMatrizIncidência()
        {
            int[,] matrizDeIncidencia = new int[conjuntoDeVertices.Count, quantidadeDeArestas];

            for (int i = 0; i < conjuntoDeArestas.Count; i++)
            {
                Aresta aresta = conjuntoDeArestas[i];
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

        public int RetornaRotuloVerticePorIdAresta(string idAresta)
        {
            int rotuloArestaDesejada = -10000;
            foreach (Vertice vertice in conjuntoDeVertices)
            {
                foreach (Aresta aresta in vertice.RetornaListaDeArestas())
                {
                    if (idAresta == aresta.RetornaID())
                    {
                        rotuloArestaDesejada = vertice.RetornaRotuloDoVertice();
                    }
                }
            }
            return rotuloArestaDesejada;
        }

        public bool VerificarAdjacenciaVertice(Vertice x, Vertice y)
        {
            List<Aresta> arestasVertice = x.RetornaListaDeArestas();
            foreach (Aresta aresta in arestasVertice)
            {
                if (aresta.RetornaVerticeDestino() == y)
                {
                    return true;
                }
            }
            return false;

        }

        public override int GetHashCode()
        {
            return id.GetHashCode();
        }

        public List<Vertice> RetornaListaVertices()
        {
            return conjuntoDeVertices;
        }
        public void TrocarVertices(int rotulo1, int rotulo2)
        {
            Vertice v1 = ObterVerticePorRotulo(rotulo1);
            Vertice v2 = ObterVerticePorRotulo(rotulo2);

            if (v1 == null || v2 == null)
            {
                Console.WriteLine("Um ou ambos os vértices não existem no grafo.");
                return;
            }
            
            for (int i = 0; i < conjuntoDeArestas.Count; i++)
            {
                Aresta a = conjuntoDeArestas[i];
                
                if (a.RetornaVerticeOrigem() == v1)
                {
                    a.SetOrigem(v2);
                }
                else if (a.RetornaVerticeOrigem() == v2)
                {
                    a.SetOrigem(v1);
                }
                
                if (a.RetornaVerticeDestino() == v1)
                {
                    a.SetDestino(v2);
                }
                else if (a.RetornaVerticeDestino() == v2)
                {
                    a.SetDestino(v1);
                }

                for (int j = 0; j < conjuntoDeVertices.Count; j++)
                {
                    Vertice v = conjuntoDeVertices[j];
                    
                    v.LimpaArestas();
                }
               
                for (int k = 0; k < conjuntoDeArestas.Count; k++)
                {
                    Aresta a2 = conjuntoDeArestas[k];

                    a2.RetornaVerticeOrigem().InsereNaListaDeArestas(a2);
                }
            }
        }

        public void DijkstraCaminhoMinimo(int rotuloOrigem, int rotuloDestino)
        {
            Vertice origem = ObterVerticePorRotulo(rotuloOrigem);
            Vertice destino = ObterVerticePorRotulo(rotuloDestino);

            if (origem == null || destino == null)
            {
                Console.WriteLine("Origem ou destino não existem.");
                return;
            }

            
            Dictionary<Vertice, double> distancias = new Dictionary<Vertice, double>();
            Dictionary<Vertice, Vertice> predecessores = new Dictionary<Vertice, Vertice>();

           
            foreach (Vertice v in conjuntoDeVertices)
            {
                distancias[v] = double.PositiveInfinity;
                v.SetDistancia(int.MaxValue);
            }

            distancias[origem] = 0;
            origem.SetDistancia(0);

            FiladePrioridades<Vertice> fila = new FiladePrioridades<Vertice>();
            fila.Enqueue(origem);

            while (!fila.IsEmpty())
            {
                Vertice verticeAtual = fila.Dequeue();

                foreach (Aresta aresta in verticeAtual.RetornaListaDeArestas())
                {
                    Vertice v = aresta.RetornaVerticeDestino();
                    double peso = aresta.RetornarPeso();
                    double novaDistancia = distancias[verticeAtual] + peso;

                    if (novaDistancia < distancias[v])
                    {
                        distancias[v] = novaDistancia;
                        v.SetDistancia((int)novaDistancia);
                        predecessores[v] = verticeAtual;

                        
                        FiladePrioridades<Vertice> filaTemp = new FiladePrioridades<Vertice>();
                        while (!fila.IsEmpty())
                        {
                            Vertice temp = fila.Dequeue();
                            if (temp != v)
                                filaTemp.Enqueue(temp);
                        }
                        filaTemp.Enqueue(v);
                        fila = filaTemp;
                    }
                }
            }

            if (!predecessores.ContainsKey(destino))
            {
                Console.WriteLine("Não existe caminho da origem até o destino.");
                return;
            }

            Stack<Vertice> pilhaCaminho = new Stack<Vertice>();
            Vertice atual = destino;
            while (atual != null && atual != origem)
            {
                pilhaCaminho.Push(atual);
                predecessores.TryGetValue(atual, out atual);
            }
            pilhaCaminho.Push(origem);

            Console.WriteLine($"Caminho mínimo de {rotuloOrigem} até {rotuloDestino}:");
            while (pilhaCaminho.Count > 0)
            {
                Vertice v = pilhaCaminho.Pop();
                Console.Write(v.RetornaRotuloDoVertice());
                if (pilhaCaminho.Count > 0)
                    Console.Write(" -> ");
            }

            Console.WriteLine($"\nCusto total: {distancias[destino]}");
        }
    }
}
