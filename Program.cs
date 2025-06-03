using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Trabalho_Grafos
{
    internal class Program
    {
        static Dictionary<int, Grafo> todosOsGrafos = new Dictionary<int, Grafo>();  
        public static void PreencherVertices(Grafo grafo, int quantidadeVertices)
        {
            Console.Clear();
            

            for (int i = 0; i < quantidadeVertices; i++) 
            {
                Console.WriteLine($"Informe o {i + 1}º vértice: ");
                int rotulo = int.Parse(Console.ReadLine());
                grafo.PreencheRotuloVertices(rotulo); 
            }
            Console.Clear();
           
        }

        public static Grafo RetornaGrafoDesejado(int id)
        {
            return todosOsGrafos[id];
        }

        public static void ExibeIdGrafosCriados() 
        {
            Console.Clear();
            Console.WriteLine("================== IDs DE GRAFOS CRIADOS ==========================");
            foreach (int chave in todosOsGrafos.Keys)
            {
                Console.WriteLine($"{chave}");
            }
            Console.WriteLine();
            Console.WriteLine("====================================================================");
        }
        public static void PreencherArestas(Grafo grafo, int quantidadeArestas)
        {
           
            for (int i = 0; i < quantidadeArestas; i++)
            {
                Console.WriteLine($"{i + 1}ª aresta: ");
                Console.WriteLine("Informe o vértice de origem: ");
                int origem = int.Parse(Console.ReadLine());
                Console.WriteLine("Informe o vértice de destino: ");
                int destino = int.Parse(Console.ReadLine());
                Console.WriteLine("Informe o peso da aresta: ");
                double peso = double.Parse(Console.ReadLine());
                Console.Clear();    
                grafo.PreenchePesoArestas(origem, destino, peso);
            }
           
        }

        public static void ExibirMatrizAdjacencia(int[,] matrizAdjacenciaGerada, Grafo grafo)
        {
            Console.Clear();
            List<Vertice> vertices = grafo.RetornaListaVertices();
            Console.WriteLine("================== MATRIZ DE ADJACÊNCIA =========================");
            foreach (Vertice vertice in vertices)
            {
                Console.Write(vertice.RetornaRotuloDoVertice().ToString().PadLeft(4));
            }
            Console.WriteLine();
            for (int i = 0; i < matrizAdjacenciaGerada.GetLength(0); i++)
            {
                Console.Write(vertices[i].RetornaRotuloDoVertice().ToString().PadRight(4));

                for (int j = 0; j < matrizAdjacenciaGerada.GetLength(1); j++)
                {
                    Console.Write(matrizAdjacenciaGerada[i, j].ToString().PadRight(4));
                }

                Console.WriteLine();
            }
        }

        public static void ExibirMatrizIncidência(int[,] matrizIncidenciaGerada, Grafo grafo, int quantidadeDeVertices, int quantidadeArestas)
        {
            List<Aresta> arestasTotal = grafo.RetornaTodasAsArestasDoGrafo();

            Console.WriteLine("================== MATRIZ DE INCIDÊNCIA =========================");
            Console.WriteLine();
            foreach (Aresta aresta in arestasTotal)
            {
                Console.Write($"{aresta.RetornaID(),7} ");
            }
            Console.WriteLine();

            for (int i = 0; i < quantidadeDeVertices; i++)
            {
                Console.Write($"{grafo.RetornaListaVertices()[i].RetornaRotuloDoVertice(),8}");

                for (int j = 0; j < quantidadeArestas; j++)
                {
                    Console.Write($"{matrizIncidenciaGerada[i, j],7}");
                }
                Console.WriteLine();
            }
        }

        public static void ExibirListaAdjacencia(List<Vertice>[] listaAdjacenciaGerada,Grafo grafo, int quantidadeDeVertices)
        {
            Console.Clear();
            Console.WriteLine("================== LISTA DE ADJACÊNCIA =========================");
            Console.WriteLine();
            for (int i = 0; i < quantidadeDeVertices; i++ )
            {
                    Console.Write($"{grafo.RetornaListaVertices()[i].RetornaRotuloDoVertice()} -> ");
                    for (int j = 0; j < listaAdjacenciaGerada[i].Count; j++)
                    {
                        Console.Write($"{listaAdjacenciaGerada[i][j].RetornaRotuloDoVertice()} -> ");
                    }
                    Console.WriteLine();    
            }
        }

        public static void Cabecalho()
        {
            Console.WriteLine("===============================================");
            Console.WriteLine("               Seja Bem-Vindo(a)                ");
            Console.WriteLine("    Trabalho - Algoritmos em Grafos!            ");
            Console.WriteLine("===============================================");
            Console.WriteLine();
            Console.WriteLine("               (A)───(B)                        ");
            Console.WriteLine("                │   / │                         ");
            Console.WriteLine("                │  /  │                         ");
            Console.WriteLine("               (D)───(C)───(E)                  ");
            Console.WriteLine();
            Console.WriteLine("===============================================");
        }

        public static int ExibirMenu()
        {
            int opcao = 20;
            Console.Clear();
            Cabecalho();
            Console.WriteLine("1 - Cadastrar Grafo (Manual)");
            Console.WriteLine("2 - Cadastrar Grafo (Arquivo)");
            Console.WriteLine("3 - Visualizar Grafo");
            Console.WriteLine("7 - Verficar Adjacência de um vértice em relação a outro");
            Console.WriteLine("8 - Alterar peso de uma aresta");
            Console.WriteLine("9 - Trocar vértices de lugar");
            Console.WriteLine("0 - Finalizar");

            try
            {
                opcao = int.Parse(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine("Formato inválido, digite somente as opções disponíveis");
            }

            return opcao;

        }

        static void Pausa()
        {
            Console.WriteLine("\nDigite ENTER para continuar.");

            Console.ReadKey();

        }
   
        static void Main(string[] args)
        {

            int quantidadeVertices = 0, quantidadeArestas = 0;
            double densidade = -1;
            Grafo novoGrafo = null;
            int opcao = -1;
            do
            {
                opcao = ExibirMenu();   
                switch (opcao)
                {
                    case 1:
                        Console.Clear();
                        Console.Write("Informe a quantidade de vértices: \n");
                        quantidadeVertices = int.Parse(Console.ReadLine());
                        Console.WriteLine("Informe a quantidade de arestas: ");
                        quantidadeArestas = int.Parse(Console.ReadLine());
                        novoGrafo = new Grafo(quantidadeVertices, quantidadeArestas);
                        PreencherVertices(novoGrafo, quantidadeVertices);
                        PreencherArestas(novoGrafo, quantidadeArestas);
                        todosOsGrafos.Add(novoGrafo.GetHashCode(),novoGrafo);
                        Console.Clear();
                        break;
                    case 2:
                     
                            break;
                    case 3:
                        ExibeIdGrafosCriados();
                        Console.WriteLine("Informe o ID do grafo:");
                        int id = int.Parse(Console.ReadLine());
                        Grafo grafoDesejado = RetornaGrafoDesejado(id); 
                        densidade = grafoDesejado.CalculaDensidadeDoGrafo();
                        if (densidade >= 0.6)
                        {
                            int[,] matrizAdjacenciaGerada = grafoDesejado.GerarMatrizAdjacencia();
                            ExibirMatrizAdjacencia(matrizAdjacenciaGerada, grafoDesejado);
                        }
                        else
                        {
                            List<Vertice>[] listaAdjacenciaGerada = grafoDesejado.GerarListaDeAdjacencia();
                            ExibirListaAdjacencia(listaAdjacenciaGerada, grafoDesejado, quantidadeVertices);
                        }
                        break;
                    case 7:
                        Console.Clear();
                        ExibeIdGrafosCriados();
                        Console.WriteLine("Informe o ID do grafo desejado: \n");
                        int idGrafo=int.Parse(Console.ReadLine());
                        Console.WriteLine("Informe o vértice desejado: \n");
                        int idVertice1= int.Parse(Console.ReadLine());
                        Console.WriteLine("Informe o vértice que quer verificar a adjacência: \n");
                        int idVertrice2=int.Parse(Console.ReadLine());
                        Grafo grafoDesejado2=RetornaGrafoDesejado(idGrafo);
                        if(grafoDesejado2.VerificarAdjacenciaVertice(grafoDesejado2.ObterVerticePorRotulo(idVertice1), grafoDesejado2.ObterVerticePorRotulo(idVertrice2))){
                            Console.WriteLine($"Os vértices são adjacentes!");
                        }
                        else
                        {
                            Console.WriteLine("Os vértices não são adjacentes.");
                        }
                            break;
                    case 8:
                        Console.Clear();
                        ExibeIdGrafosCriados();
                        Console.WriteLine("Informe o ID do grafo desejado: \n");
                        int idGrafo2 = int.Parse(Console.ReadLine());
                        Console.WriteLine("Informe o id da aresta que deseja alterar: \n");
                        string idAresta=Console.ReadLine();
                        Console.WriteLine("Informe o novo peso da aresta: \n");
                        double peso = double.Parse(Console.ReadLine());
                        Grafo grafoDesejado3=RetornaGrafoDesejado(idGrafo2);
                        List<Aresta> todasAsArestas = grafoDesejado3.RetornaTodasAsArestasDoGrafo();
                        foreach(Aresta a in todasAsArestas)
                        {
                            if (a.RetornaID().Equals(idAresta))
                            {
                                Console.Write($"Peso da aresta {a.RetornaID()} foi alterado de: {a.RetornarPeso()}");
                                a.AlterarPeso(peso);
                                Console.Write($", para: {a.RetornarPeso()}.");
                            }
                        }
                        break;

                }
                Pausa();
            } while (opcao != 0);

            Console.ReadKey();
        }
    }
}
