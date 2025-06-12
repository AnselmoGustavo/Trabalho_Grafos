using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabalho_Grafos
{
    class Buscas
    {
        private int t;
        private Grafo grafo;
        private string[,] tabelaBuscaProfundidade;

        public Buscas(Grafo grafo)
        {
            this.grafo = grafo;
            tabelaBuscaProfundidade = new string[4, grafo.RetornaListaVertices().Count+1];
            t = 0;
            PrepararBuscaProfundidade();
        }

        private void IniciarTabelaBuscaProfundidade()
        {
            for (int j = 1; j < tabelaBuscaProfundidade.GetLength(1); j++)
            {
                tabelaBuscaProfundidade[0, j] = grafo.RetornaListaVertices()[j - 1].RetornaRotuloDoVertice().ToString();
            }
            tabelaBuscaProfundidade[1, 0] = "TD";
            tabelaBuscaProfundidade[2, 0] = "TT";
            tabelaBuscaProfundidade[3, 0] = "Pai";

        }
        private int ObterIndiceDoVerticePeloRotulo(int rotulo)
        {
            List<Vertice> listaVertices = grafo.RetornaListaVertices();
            for(int i=0;i<listaVertices.Count; i++)
            {
                if (listaVertices[i].RetornaRotuloDoVertice() == rotulo)
                {
                    return i;
                }
            }
            return -1;
        }
        private void PrepararBuscaProfundidade()
        {
            IniciarTabelaBuscaProfundidade();
            foreach (Vertice v in grafo.RetornaListaVertices())
            {
                v.SetTempoDescoberta(0);
                v.SetTempoTermino(0);
                v.SetPai(null);
            }
        }
        public string[,] BuscaProfundidade(int rotuloRaiz)
        {
            int indiceColuna=ObterIndiceDoVerticePeloRotulo(rotuloRaiz);
            Vertice v = grafo.ObterVerticePorRotulo(rotuloRaiz);
            t++; v.SetTempoDescoberta(t);
            tabelaBuscaProfundidade[1, indiceColuna+1] = t.ToString();
            foreach (Vertice w in v.RetornaVizinhanca())
            {
                if (w.RetornaTempoDescoberta() == 0)
                {
                    v.RetornaArestaEspecifica(w).SetTipoAresta(1);
                    w.SetPai(v);
                    //Adiciona as coisas na tabela antes de retornar a recursão
                    int indiceFilho = ObterIndiceDoVerticePeloRotulo(w.RetornaRotuloDoVertice())+1;
                    tabelaBuscaProfundidade[3, indiceFilho] = w.RetornaPai().ToString();
                    BuscaProfundidade(w.RetornaRotuloDoVertice());
                }
                else
                {
                    if (w.RetornaTempoTermino() == 0)
                    {
                        v.RetornaArestaEspecifica(w).SetTipoAresta(2);
                    }
                    else if (v.RetornaTempoDescoberta() < w.RetornaTempoDescoberta())
                    {
                        v.RetornaArestaEspecifica(w).SetTipoAresta(3);
                    }
                    else
                    {
                        v.RetornaArestaEspecifica(w).SetTipoAresta(4);
                    }
                }
            }
            t++; v.SetTempoTermino(t);
            tabelaBuscaProfundidade[2, indiceColuna+1] = t.ToString();
            return tabelaBuscaProfundidade;
        } 
    }
}
