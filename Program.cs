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
            if(ArestaExiste(grafo, idAresta))
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
            else
            {
                Console.WriteLine("Aresta não encontrada no grafo.");
            }
        }

        private static bool ArestaExiste(Grafo grafo, string a)
        {
            foreach (Aresta aresta in grafo.RetornaTodasAsArestasDoGrafo())
            {
                if (aresta.RetornaID() == a)
                {
                    return true;
                }
            }
            return false;
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
            if (VerticeExiste(grafo, rotuloVertice))
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
            else
            {
                Console.WriteLine("Vértice não encontrado no grafo.");
            }
        }

        private static bool VerticeExiste(Grafo grafo, int rotulo)
        {
            foreach (Vertice vertice in grafo.RetornaListaVertices())
            {
                if (vertice.RetornaRotuloDoVertice() == rotulo)
                {
                    return true;
                }
            }
            return false;
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

        public static void ExibirMatrizBusca(string[,] buscaFinalizada)
        {
            
            Console.Write("     ");
            for (int j = 1; j < buscaFinalizada.GetLength(1); j++)
            {
                Console.Write($"{buscaFinalizada[0, j],5}");
            }
            Console.WriteLine();

            
            for (int i = 1; i <= 3; i++)
            {
                
                Console.Write($"{buscaFinalizada[i, 0],5}");

                for (int j = 1; j < buscaFinalizada.GetLength(1); j++)
                {
                    string itemTabela = buscaFinalizada[i, j];

                    if (string.IsNullOrEmpty(itemTabela))
                        Console.Write($"{'∅',5}");
                    else
                        Console.Write($"{itemTabela,5}");
                }

                Console.WriteLine();
            }

        }

        public static void ExibeTabelaDistanciasFloydWarshall(double[,] tabelaFloydWarshall)
        {
            Console.Clear();
            Console.WriteLine("======= Método de Floyd-Warshall ======");
            Console.WriteLine();
            for (int i = 0; i < tabelaFloydWarshall.GetLength(0); i++)
            {
                for (int j = 0; j < tabelaFloydWarshall.GetLength(1); j++)
                {
                    if (i == 0 && j == 0)
                    {
                        Console.Write("      ");
                    }
                    else
                    {
                        Console.Write($"{tabelaFloydWarshall[i, j],6}");
                    }
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
            Console.WriteLine("4 - Visualizar arestas adjacentes a uma aresta específica"); 
            Console.WriteLine("5 - Visualizar vértices adjacentes a um vértice específico"); 
            Console.WriteLine("6 - Visualizar todas as arestas incidentes a um vértice v");
            Console.WriteLine("7 - Visualizar todos os vértices incidentes a uma aresta específica");
            Console.WriteLine("8 - Visualizar o grau de um vértice específico");
            Console.WriteLine("9 - Verficar Adjacência de um vértice em relação a outro");
            Console.WriteLine("10 - Substituir o peso de uma aresta por outro valor");
            Console.WriteLine("11 - Trocar de lugar dois vértices de um grafo");
            Console.WriteLine("12 - Fazer uma busca em profundidade em um grafo");
            Console.WriteLine("13 - Fazer uma busca em largura em um grafo");
            Console.WriteLine("14 - Encontrar caminho mínimo - Algoritmo Floyd Warshall");
            Console.WriteLine("15 - Visualizar o camiho mínimo entre dois vértices");
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
            Grafo grafoDesejado = null;
            Buscas algoritmoBusca;
            int opcao = -1;
            int idGrafo = 0;
            int idVertice1, idVertice2 = 0;
            string idAresta = null;
            string[,] buscaFinalizada;
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
                            Arquivo arquivoGrafo = new Arquivo("C:\\Users\\Gustavo\\Downloads\\arquivoGrafo.txt");
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
                        try
                        {
                            ExibeIdGrafosCriados();
                            Console.WriteLine("Informe o ID do grafo:");
                            idGrafo = int.Parse(Console.ReadLine());
                            grafoDesejado = RetornaGrafoDesejado(idGrafo);
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
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Formato inválido, digite somente os IDs disponíveis.");
                        }
                        catch (KeyNotFoundException)
                        {
                            Console.WriteLine("ID do grafo não encontrado. Tente novamente.");
                        }
                        catch (ArgumentException ex)
                        {
                            Console.WriteLine($"Erro de argumento: {ex.Message}");
                        }
                        break;

                    case 4:
                        try
                        {
                            ExibeIdGrafosCriados();
                            Console.WriteLine("Informe o ID do grafo desejado: ");
                            idGrafo = int.Parse(Console.ReadLine());
                            grafoDesejado = RetornaGrafoDesejado(idGrafo);
                            ExibeIdArestasGrafo(grafoDesejado);
                            Console.WriteLine("Informe o ID da aresta que deseja ver as arestas adjacentes: ");
                            idAresta = Console.ReadLine().ToLower();
                            int rotuloDoVerticePredecessor = grafoDesejado.RetornaRotuloVerticePorIdAresta(idAresta);
                            ExibeArestasAdjacentes(grafoDesejado, rotuloDoVerticePredecessor, idAresta);
                        }
                        catch (KeyNotFoundException)
                        {
                            Console.WriteLine("Id do grafo inexistente, digite somente os IDs disponíveis.");
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Formato inválido, digite somente os IDs disponíveis.");
                        }
                        break;
                    case 5:
                        try
                        {
                            ExibeIdGrafosCriados();
                            Console.WriteLine("Informe o ID do grafo desejado: ");
                            idGrafo = int.Parse(Console.ReadLine());
                            grafoDesejado = RetornaGrafoDesejado(idGrafo);
                            ExibeVerticesDoGrafo(grafoDesejado);
                            Console.WriteLine("Informe qual o vértice que gostaria de ver seus vértices adjacentes: ");
                            int rotulo = int.Parse(Console.ReadLine());
                            ExibeVerticesAdjacentes(grafoDesejado, rotulo);
                        }
                        catch (KeyNotFoundException)
                        {
                            Console.WriteLine("Id do grafo inexistente, digite somente os IDs disponíveis.");
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Formato inválido, digite somente os IDs disponíveis.");
                        }
                        break;
                    case 6:
                        try
                        {
                            Console.Clear();
                            ExibeIdGrafosCriados();
                            Console.WriteLine("Informe o ID do grafo desejado: \n");
                            idGrafo = int.Parse(Console.ReadLine());
                            grafoDesejado = RetornaGrafoDesejado(idGrafo);
                            Console.WriteLine("Informe o vértice desejado: \n");
                            idVertice1 = int.Parse(Console.ReadLine());
                            List<Aresta> arestasIncidentes = new List<Aresta>();
                            foreach (Aresta a in grafoDesejado.RetornaTodasAsArestasDoGrafo())
                            {
                                if (a.RetornaVerticeOrigem().RetornaRotuloDoVertice() == idVertice1 || a.RetornaVerticeDestino().RetornaRotuloDoVertice() == idVertice1)
                                {
                                    arestasIncidentes.Add(a);
                                }
                            }
                            if (arestasIncidentes.Count > 0)
                            {
                                Console.WriteLine($"Arestas incidentes ao vértice {idVertice1}:");
                                foreach (Aresta a in arestasIncidentes)
                                {
                                    Console.WriteLine(a.RetornaID());
                                    Console.WriteLine($"Origem: {a.RetornaVerticeOrigem()}\nDestino: {a.RetornaVerticeDestino()}\nPeso: {a.RetornarPeso()}");
                                }
                            }
                            else
                            {
                                Console.WriteLine($"Não há arestas incidentes ao vértice {idVertice1}.");
                            }
                        }
                        catch(KeyNotFoundException)
                        {
                            Console.WriteLine("Id do grafo inexistente, digite somente os IDs disponíveis.");
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Formato inválido, digite somente os IDs disponíveis.");
                        }
                        break;
                    case 7:
                        try
                        {
                            Console.Clear();
                            ExibeIdGrafosCriados();
                            Console.WriteLine("Informe o ID do grafo desejado: \n");
                            idGrafo = int.Parse(Console.ReadLine());
                            grafoDesejado = RetornaGrafoDesejado(idGrafo);
                            Console.WriteLine("Informe o ID da aresta que deseja ver os vértices incidentes: ");
                            idAresta = "e" + Console.ReadLine();
                            Aresta arestaDesejada = grafoDesejado.ObterArestaPorId(idAresta);
                            if (arestaDesejada != null)
                            {
                                Console.WriteLine($"Vértices incidentes à aresta {idAresta}:\nOrigem: {arestaDesejada.RetornaVerticeOrigem()}\nDestino{arestaDesejada.RetornaVerticeDestino()}");
                            }
                            else
                            {
                                Console.WriteLine($"Aresta {idAresta} não encontrada no grafo.");
                            }
                        }
                        catch(KeyNotFoundException)
                        {
                            Console.WriteLine("Id do grafo inexistente, digite somente os IDs disponíveis.");
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Formato inválido, digite somente os IDs disponíveis.");
                        }
                        break;
                    case 8:
                        try
                        {
                            Console.Clear();
                            ExibeIdGrafosCriados();
                            Console.WriteLine("Informe o ID do grafo desejado: \n");
                            idGrafo = int.Parse(Console.ReadLine());
                            grafoDesejado = RetornaGrafoDesejado(idGrafo);
                            Console.WriteLine("Informe o vértice desejado: \n");
                            idVertice1 = int.Parse(Console.ReadLine());
                            Vertice achado = grafoDesejado.ObterVerticePorRotulo(idVertice1);
                            if (achado != null)
                            {
                                Console.WriteLine($"O grau do vértice {idVertice1} é: {achado.RetornaListaDeArestas().Count()}");
                            }
                            else
                            {
                                Console.WriteLine("Vértice não encontrado no grafo.");
                            }
                          
                        }
                        catch (KeyNotFoundException)
                        {
                            Console.WriteLine("Id do grafo inexistente, digite somente os IDs disponíveis.");
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Formato inválido, digite somente os IDs disponíveis.");
                        }
                        break;
                    case 9:
                        try
                        {
                            Console.Clear();
                            ExibeIdGrafosCriados();
                            Console.WriteLine("Informe o ID do grafo desejado: \n");
                            idGrafo = int.Parse(Console.ReadLine());
                            Console.WriteLine("Informe o vértice desejado: \n");
                            idVertice1 = int.Parse(Console.ReadLine());
                            Console.WriteLine("Informe o vértice que quer verificar a adjacência: \n");
                            idVertice2 = int.Parse(Console.ReadLine());
                            grafoDesejado = RetornaGrafoDesejado(idGrafo);
                            if (VerticeExiste(grafoDesejado, idVertice1) && VerticeExiste(grafoDesejado, idVertice2))
                            {
                                if (grafoDesejado.VerificarAdjacenciaVertice(grafoDesejado.ObterVerticePorRotulo(idVertice1), grafoDesejado.ObterVerticePorRotulo(idVertice2)))
                                {
                                    Console.WriteLine($"Os vértices são adjacentes!");
                                }
                                else
                                {
                                    Console.WriteLine("Os vértices não são adjacentes.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Um ou ambos os vértices não existem no grafo.");
                            }
                        }
                        catch(KeyNotFoundException)
                        {
                            Console.WriteLine("Id do grafo inexistente, digite somente os IDs disponíveis.");
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Formato inválido, digite somente os IDs disponíveis.");
                        }
                        break;
                    case 10:
                        try
                        {
                            Console.Clear();
                            ExibeIdGrafosCriados();
                            Console.WriteLine("Informe o ID do grafo desejado: \n");
                            idGrafo = int.Parse(Console.ReadLine());
                            grafoDesejado = RetornaGrafoDesejado(idGrafo);
                            Console.WriteLine("Informe qual aresta o id da aresta que deseja alterar o peso: \n");
                            idAresta = "e" + int.Parse(Console.ReadLine());
                            if(ArestaExiste(grafoDesejado, idAresta))
                            {
                                foreach (Aresta aresta in grafoDesejado.RetornaTodasAsArestasDoGrafo())
                                {
                                    if (aresta.RetornaID() == idAresta)
                                    {
                                        Console.WriteLine($"Aresta: {aresta.RetornaID()}");
                                        Console.WriteLine($"Peso atual: {aresta.RetornarPeso()}");
                                        Console.WriteLine("Informe o novo peso da aresta: ");
                                        double novoPeso = double.Parse(Console.ReadLine());
                                        aresta.AlterarPeso(novoPeso);
                                        Console.WriteLine($"Peso atualizado com sucesso! Novo peso: {aresta.RetornarPeso()}.");
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine($"Aresta {idAresta} não encontrada no grafo.");
                            }
                        }
                        catch(KeyNotFoundException)
                        {
                            Console.WriteLine("Id do grafo inexistente, digite somente os IDs disponíveis.");
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Formato inválido, digite somente os IDs disponíveis.");
                        }
                        break;
                    case 11:
                        try
                        {
                            Console.Clear();
                            ExibeIdGrafosCriados();
                            Console.WriteLine("Informe o ID do grafo desejado: \n");
                            idGrafo = int.Parse(Console.ReadLine());
                            grafoDesejado = RetornaGrafoDesejado(idGrafo);
                            Console.WriteLine("Informe o vértice desejado: \n");
                            idVertice1 = int.Parse(Console.ReadLine());
                            Console.WriteLine("Informe o id do vértice deseja trocar de lugar: \n");
                            idVertice2 = int.Parse(Console.ReadLine());
                            Grafo copiaGrafoDesejado = grafoDesejado;
                            if(VerticeExiste(grafoDesejado, idVertice1) && VerticeExiste(grafoDesejado, idVertice2))
                            {
                                grafoDesejado.TrocarVertices(idVertice1, idVertice2);
                                Console.WriteLine($"Vértices {idVertice1} e {idVertice2} trocados com sucesso!");
                            }
                            else
                            {
                                Console.WriteLine("Um ou ambos os vértices não existem no grafo.");
                            }
                        }
                        catch (KeyNotFoundException)
                        {
                            Console.WriteLine("Id do grafo inexistente, digite somente os IDs disponíveis.");
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Formato inválido, digite somente os IDs disponíveis.");
                        }
                        break;
                    case 12:
                        try
                        {
                            Console.Clear();
                            ExibeIdGrafosCriados();
                            Console.WriteLine("Informe o ID do grafo desejado: \n");
                            idGrafo = int.Parse(Console.ReadLine());
                            grafoDesejado = RetornaGrafoDesejado(idGrafo);
                            Console.WriteLine("Informe o vértice de origem para a busca: \n");
                            idVertice1 = int.Parse(Console.ReadLine());
                            if (VerticeExiste(grafoDesejado, idVertice1))
                            {
                                algoritmoBusca = new Buscas(grafoDesejado);
                                buscaFinalizada = algoritmoBusca.BuscaProfundidade(idVertice1);
                                Console.WriteLine("Aqui está a tabela de busca em profundidade:\n");
                                ExibirMatrizBusca(buscaFinalizada);
                            }
                            else
                            {
                                Console.WriteLine($"Vértice {idVertice1} não encontrado no grafo.");
                            }
                        }
                        catch(KeyNotFoundException)
                        {
                            Console.WriteLine("Id do grafo inexistente, digite somente os IDs disponíveis.");
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Formato inválido, digite somente os IDs disponíveis.");
                        }
                        break;
                    case 13:
                        try
                        {
                            Console.Clear();
                            ExibeIdGrafosCriados();
                            Console.WriteLine("Informe o ID do grafo desejado: \n");
                            idGrafo = int.Parse(Console.ReadLine());
                            grafoDesejado = RetornaGrafoDesejado(idGrafo);
                            Console.WriteLine("Informe o vértice de origem para a busca: \n");
                            idVertice1 = int.Parse(Console.ReadLine());
                            if(VerticeExiste(grafoDesejado, idVertice1))
                            {
                                algoritmoBusca = new Buscas(grafoDesejado);
                                buscaFinalizada = algoritmoBusca.IniciarBuscaLargura(idVertice1);
                                Console.WriteLine("Aqui está a tabela de busca em largura:\n");
                                ExibirMatrizBusca(buscaFinalizada);
                            }
                            else
                            {
                                Console.WriteLine($"Vértice {idVertice1} não encontrado no grafo.");
                            }
                        }
                        catch(KeyNotFoundException)
                        {
                            Console.WriteLine("Id do grafo inexistente, digite somente os IDs disponíveis.");
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Formato inválido, digite somente os IDs disponíveis.");
                        }
                        break;
                    case 14:
                        try
                        {
                            Console.Clear();
                            ExibeIdGrafosCriados();
                            Console.WriteLine("Informe o ID do grafo desejado: \n");
                            idGrafo = int.Parse(Console.ReadLine());
                            grafoDesejado = RetornaGrafoDesejado(idGrafo);
                            CaminhoMinimo caminhoPorFloydWarshall = new CaminhoMinimo(grafoDesejado);
                            double[,] tabelaFloydWarshall = caminhoPorFloydWarshall.RetornaTabelaFloydWarshall();
                            ExibeTabelaDistanciasFloydWarshall(tabelaFloydWarshall);
                        }
                        catch (KeyNotFoundException)
                        {
                            Console.WriteLine("Id do grafo inexistente, digite somente os IDs disponíveis.");
                        }
                        catch(FormatException)
                        {
                            Console.WriteLine("Formato inválido, digite somente os IDs disponíveis.");
                        }
                        break;
                    case 15:
                        try
                        {
                            Console.Clear();
                            ExibeIdGrafosCriados();
                            Console.WriteLine("Informe o ID do grafo desejado: \n");
                            idGrafo = int.Parse(Console.ReadLine());
                            grafoDesejado = RetornaGrafoDesejado(idGrafo);
                            Console.WriteLine("Informe o vértice de origem: \n");
                            idVertice1 = int.Parse(Console.ReadLine());
                            Console.WriteLine("Informe o vértice de destino: \n");
                            idVertice2 = int.Parse(Console.ReadLine());
                            if(VerticeExiste(grafoDesejado, idVertice1) && VerticeExiste(grafoDesejado, idVertice2))
                            {
                                grafoDesejado.DijkstraCaminhoMinimo(idVertice1, idVertice2);
                            }
                            else
                            {
                                Console.WriteLine("Um ou ambos os vértices não existem no grafo.");
                            }
                            
                        }
                        catch(KeyNotFoundException)
                        {
                            Console.WriteLine("Id do grafo inexistente, digite somente os IDs disponíveis.");
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Formato inválido, digite somente os IDs disponíveis.");
                        }
                        break;
                    case 0:
                        Console.WriteLine("Finalizando o programa...");
                        break;
                }
                Pausa();
            } while (opcao != 0);
            Console.ReadKey();
        }
    }
}
