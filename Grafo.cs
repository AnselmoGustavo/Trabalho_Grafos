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
            //tirei a criação da aresta de dentro do if pra poder adicionar ao conjunto de arestas do grafo, no if agora ele só adiciona ao vértice de origem
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

        //Vou alterar esse método para retornar todas as arestas do grafo, só que só pegando a lista de arestas do grafo, sem ter que passar pelos vértices
        /*public List<Aresta> RetornaTodasAsArestasDoGrafo()
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
        }*/

        public List<Aresta> RetornaTodasAsArestasDoGrafo()
        {
            return conjuntoDeArestas;
        }

        public int[,] GerarMatrizIncidência()
        {
            //alterar isso aqui porque não precisamos mais do método pra retornar todas as arestas do grafo, já que agora temos o conjunto de arestas
            //List<Aresta> arestaTotal = RetornaTodasAsArestasDoGrafo();
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
        public void TrocarVertices(int rotulo1, int rotulo2)
        {
            Vertice v1 = ObterVerticePorRotulo(rotulo1);
            Vertice v2 = ObterVerticePorRotulo(rotulo2);
            
            if(v1== null || v2 == null)
            {
                Console.WriteLine("Um ou ambos os vértices não existem no grafo.");
                return;
            }
            //Vou percorrer todas as arestas e trocar as origens das arestas relacionadas a esses vértices
            for(int i=0; i<conjuntoDeArestas.Count; i++)
            {
                Aresta a = conjuntoDeArestas[i];
                //Aqui eu troco as origens de cada aresta relacionada aos dois vértices, se não for relacionada, só n faz nada
                if(a.RetornaVerticeOrigem() == v1)
                {
                    a.SetOrigem(v2);
                }
                else if (a.RetornaVerticeOrigem() == v2)
                {
                    a.SetOrigem(v1);
                }
                //Aqui eu troco os destinos de cada aresta relacionada aos dois vértices, se não for relacionada, só n faz nada
                if (a.RetornaVerticeDestino() == v1)
                {
                    a.SetDestino(v2);
                }
                else if (a.RetornaVerticeDestino() == v2)
                {
                    a.SetDestino(v1);
                }

                for(int j=0; j< conjuntoDeVertices.Count; j++)
                {
                    Vertice v = conjuntoDeVertices[j];
                    //Depois de já ter trocado as origens e destinos de todas as arestas, vou limpar a aresta de cada vértice do grafo pra poder inserir as novas sem dar problema
                    v.LimpaArestas(); 
                }
                //Agora que limpei todas as arestas, bora repovoar essa lista com as novas e as que não foram modificadas
                for(int k = 0; k < conjuntoDeArestas.Count; k++)
                {
                    Aresta a2 = conjuntoDeArestas[k];
                    
                    a2.RetornaVerticeOrigem().InsereNaListaDeArestas(a2);
                }
            }
        }

    }
}
