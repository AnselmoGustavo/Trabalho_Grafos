using System;
using System.Collections.Generic;
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

        public static void PreencheVerticesArquivo(Grafo grafo, HashSet<int> conjuntoVertices)
        {
            foreach (int vertice in conjuntoVertices)
            {
                grafo.PreencheRotuloVertices(vertice);
            }
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

        public static void ExibeIdArestasGrafo(Grafo grafo)
        {
            Console.Clear();
            Console.WriteLine("============= CÓDIGO DAS ARESTAS DISPONÍVEIS =====================");
            foreach (Vertice vertice in grafo.RetornaListaVertices())
            {
                Console.WriteLine($"Vértice: {vertice.RetornaRotuloDoVertice()}");
                foreach (Aresta aresta in vertice.RetornaListaDeArestas())
                {

                    Console.WriteLine($"Aresta: {aresta.RetornaID()}");

                }
                Console.WriteLine();
            }
            Console.WriteLine("==================================================================");
        }

    
        public static void ExibeArestasAdjacentes(Grafo grafo, int rotuloVertice, string idAresta)
        {
            foreach (Vertice vertice in grafo.RetornaListaVertices())
            {
                if (vertice.RetornaRotuloDoVertice() == rotuloVertice)
                {
                    foreach (Aresta aresta in vertice.RetornaListaDeArestas())
                    {
                        if (idAresta != aresta.RetornaID())
                        {
                            Console.WriteLine($"Aresta: {aresta.RetornaID()}");
                        }
                    }
                }
            }
        }

        public static void ExibeVerticesDoGrafo(Grafo grafo)
        {
            Console.Clear();
            Console.WriteLine("============= VÉRTICES DISPONÍVEIS =====================");
            foreach (Vertice vertice in grafo.RetornaListaVertices())
            {
                Console.Write(vertice.RetornaRotuloDoVertice().ToString().PadRight(4));
            }
            Console.WriteLine();
            Console.WriteLine("========================================================");
        }

        public static void ExibeVerticesAdjacentes(Grafo grafo, int rotuloVertice)
        {
            foreach (Vertice vertice in grafo.RetornaListaVertices())
            {
                if (rotuloVertice == vertice.RetornaRotuloDoVertice())
                {
                    foreach (Aresta aresta in vertice.RetornaListaDeArestas())
                    {
                        Console.WriteLine($"Vértice: {aresta.RetornaVerticeDestino().RetornaRotuloDoVertice()}");
                    }
                }
            }
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

       
        public static void PreencheArestasArquivo(Grafo grafo, int quantidadeArestas, List<int> conjuntoDeArestas, List<double> conjuntoPesos)
        {
            int indiceOrigem = 0;
            int indiceDestino = 1;

            for (int i = 0; i < quantidadeArestas; i++)
            {
                grafo.PreenchePesoArestas(conjuntoDeArestas[indiceOrigem], conjuntoDeArestas[indiceDestino], conjuntoPesos[i]);
                indiceOrigem = indiceDestino + 1;
                indiceDestino = indiceOrigem + 1;
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

        public static void ExibirListaAdjacencia(List<Vertice>[] listaAdjacenciaGerada, Grafo grafo, int quantidadeDeVertices)
        {
            Console.Clear();
            Console.WriteLine("================== LISTA DE ADJACÊNCIA =========================");
            Console.WriteLine();
            for (int i = 0; i < quantidadeDeVertices; i++)
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
            Console.WriteLine("4 - Visualizar arestas adjacentes a uma aresta específica"); // mudei aqui
            Console.WriteLine("5 - Visualizar vértices adjacentes a um vértice específico"); // mudei aqui
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
                        try
                        {
                            Console.Clear();
                            Console.Write("Informe a quantidade de vértices: ");
                            quantidadeVertices = int.Parse(Console.ReadLine());
                            Console.Write("Informe a quantidade de arestas: ");
                            quantidadeArestas = int.Parse(Console.ReadLine());
                            novoGrafo = new Grafo(quantidadeVertices, quantidadeArestas);
                            PreencherVertices(novoGrafo, quantidadeVertices);
                            PreencherArestas(novoGrafo, quantidadeArestas);
                            todosOsGrafos.Add(novoGrafo.GetHashCode(), novoGrafo);

                            Console.Clear();
                            Cabecalho();
                            Console.WriteLine("Grafo criado manualmente com sucesso.");
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Você deve inserir apenas números inteiros.");
                        }
                        catch (ArgumentException ex)
                        {
                            Console.WriteLine($" Erro de argumento: {ex.Message}");
                        }

                        break;
                    case 2:
                        try
                        {
                            Arquivo arquivoGrafo = new Arquivo("C:\\Users\\conta\\Downloads\\arquivoGrafo.txt");
                            Grafo grafoArquivo = new Grafo(arquivoGrafo.RetornaQuantidadeVertices(), arquivoGrafo.RetornaQuantidadeVertices());

                            HashSet<int> conjuntoVertices = arquivoGrafo.RetornaVertices();
                            List<int> conjuntoDeArestas = arquivoGrafo.RetornaConjuntoDeArestas();
                            List<double> conjuntoPesos = arquivoGrafo.RetornaPesosArestas();
                            PreencheVerticesArquivo(grafoArquivo, conjuntoVertices);
                            PreencheArestasArquivo(grafoArquivo, arquivoGrafo.RetornaQuantidadeArestas(), conjuntoDeArestas, conjuntoPesos);
                            todosOsGrafos.Add(grafoArquivo.GetHashCode(), grafoArquivo);
                            Console.Clear();
                            Cabecalho();
                            Console.WriteLine("Grafo criado com sucesso a partir do arquivo.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Erro ao criar o grafo: {ex.Message}");
                        }
                        break;
                    case 3:
                        ExibeIdGrafosCriados();
                        Console.WriteLine("Informe o ID do grafo:");
                        int id = int.Parse(Console.ReadLine());
                        Grafo grafoDesejado = RetornaGrafoDesejado(id);
                        densidade = grafoDesejado.CalculaDensidadeDoGrafo();

                        int quantidadeVerticesGrafo = grafoDesejado.RetornaListaVertices().Count;

                        if (densidade >= 0.6)
                        {
                            int[,] matrizAdjacenciaGerada = grafoDesejado.GerarMatrizAdjacencia();
                            ExibirMatrizAdjacencia(matrizAdjacenciaGerada, grafoDesejado);
                        }
                        else
                        {
                            List<Vertice>[] listaAdjacenciaGerada = grafoDesejado.GerarListaDeAdjacencia();
                            ExibirListaAdjacencia(listaAdjacenciaGerada, grafoDesejado, quantidadeVerticesGrafo);
                        }
                        break;
                    case 4:
                        ExibeIdGrafosCriados();
                        Console.WriteLine("Informe o ID do grafo desejado: ");
                        id = int.Parse(Console.ReadLine());
                        grafoDesejado = RetornaGrafoDesejado(id);
                        ExibeIdArestasGrafo(grafoDesejado);
                        Console.WriteLine("Informe o ID da aresta que deseja ver as arestas adjacentes: ");
                        string idAresta = Console.ReadLine().ToLower();
                        int rotuloDoVerticePredecessor = grafoDesejado.RetornaRotuloVerticePorIdAresta(idAresta);
                        ExibeArestasAdjacentes(grafoDesejado, rotuloDoVerticePredecessor, idAresta);
                        break;
                    case 5:
                        ExibeIdGrafosCriados();
                        Console.WriteLine("Informe o ID do grafo desejado: ");
                        id = int.Parse(Console.ReadLine());
                        grafoDesejado = RetornaGrafoDesejado(id);
                        ExibeVerticesDoGrafo(grafoDesejado);
                        Console.WriteLine("Informe qual o vértice que gostaria de ver seus vértices adjacentes: ");
                        int rotulo = int.Parse(Console.ReadLine());
                        ExibeVerticesAdjacentes(grafoDesejado, rotulo);
                        break;

                }
                Pausa();
            } while (opcao != 0);

            Console.ReadKey();
        }
    }
}
