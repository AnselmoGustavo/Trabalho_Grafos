using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabalho_Grafos
{
    public class CaminhoMinimo
    {
        private Grafo grafo;
        private double[,] tabelaFloydWarshall;


        public CaminhoMinimo(Grafo grafo)
        {
            this.grafo = grafo;
            tabelaFloydWarshall = new double[grafo.RetornaListaVertices().Count() + 1, grafo.RetornaListaVertices().Count() + 1];
            InicializarTopoTabelaFloydWarshall();
            InicializarLateralTabelaFloydWarshall();
            InicializarDistancias();
        }

        private void InicializarTopoTabelaFloydWarshall()
        {
            
            for (int i = 1; i < tabelaFloydWarshall.GetLength(1); i++)
            {
                tabelaFloydWarshall[0, i] = grafo.RetornaListaVertices()[i - 1].RetornaRotuloDoVertice();
            }
        }

        private void InicializarLateralTabelaFloydWarshall()
        {
            for (int i = 1; i < tabelaFloydWarshall.GetLength(0); i++)
            {
                tabelaFloydWarshall[i, 0] = grafo.RetornaListaVertices()[i - 1].RetornaRotuloDoVertice();
            }
        }

        private void InicializarDistancias()
        {
            for (int i = 1; i < tabelaFloydWarshall.GetLength(0); i++)
            {
                for (int j = 1; j < tabelaFloydWarshall.GetLength(1); j++)
                {
                    if (j != i)
                    {
                        tabelaFloydWarshall[i, j] = double.MaxValue;
                    }
                    tabelaFloydWarshall[i, i] = 0;
                }
            }
            AdicionaPesoArestas();
        }

        private void AdicionaPesoArestas()
        {
            List<Aresta> listaDeArestas = grafo.RetornaTodasAsArestasDoGrafo();
            int verticeOrigem, verticeDestino;
            double pesoAresta;

            for (int i = 0; i < listaDeArestas.Count; i++)
            {
                Aresta aresta = listaDeArestas[i];
                verticeOrigem = aresta.RetornaVerticeOrigem().RetornaRotuloDoVertice();
                verticeDestino = aresta.RetornaVerticeDestino().RetornaRotuloDoVertice();
                pesoAresta = aresta.RetornarPeso();
                tabelaFloydWarshall[verticeOrigem, verticeDestino] = pesoAresta;

            }
            AlgoritmoFloydWarshall();
        }

        private void AlgoritmoFloydWarshall()
        {

            for (int k = 1; k < tabelaFloydWarshall.GetLength(0); k++)
            {
                for (int i = 1; i < tabelaFloydWarshall.GetLength(0); i++)
                {
                    for (int j = 1; j < tabelaFloydWarshall.GetLength(0); j++)
                    {
                        if (tabelaFloydWarshall[i, j] > (tabelaFloydWarshall[i, k] + tabelaFloydWarshall[k, j]))
                        {
                            tabelaFloydWarshall[i, j] = tabelaFloydWarshall[i, k] + tabelaFloydWarshall[k, j];
                        }
                    }
                }
            }
        }

        public double[,] RetornaTabelaFloydWarshall()
        {
            return tabelaFloydWarshall;
        }


    }
}

