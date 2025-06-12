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
        private string[,] tabelaBuscaLargura;
        private Queue<Vertice> filaBuscaLargura;

        public Buscas(Grafo grafo)
        {
            this.grafo = grafo;
            tabelaBuscaProfundidade = new string[4, grafo.RetornaListaVertices().Count+1];
            tabelaBuscaLargura = new string[4, grafo.RetornaListaVertices().Count + 1];
            t = 0;
            PrepararBuscaProfundidade();
            PrepararBuscaLargura();
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

        private void IniciarTabelaBuscaLargura()
        {
            for (int j = 1; j < tabelaBuscaLargura.GetLength(1); j++)
            {
                tabelaBuscaLargura[0, j] = grafo.RetornaListaVertices()[j - 1].RetornaRotuloDoVertice().ToString();
            }
            tabelaBuscaLargura[1, 0] = "L";
            tabelaBuscaLargura[2, 0] = "Nível";
            tabelaBuscaLargura[3, 0] = "Pai";
        }
        private void PrepararBuscaLargura()
        {
            IniciarTabelaBuscaLargura();
            filaBuscaLargura = new Queue<Vertice>();
            foreach (Vertice v in grafo.RetornaListaVertices())
            {
                v.SetIndice(0);
                v.SetNivel(0);
                v.SetPai(null);
            }
        }
        private bool VerificarIndiceZero()
        {
            foreach (Vertice v in grafo.RetornaListaVertices())
            {
                if (v.RetornaIndice() == 0)
                {
                    return true;
                }
            }
            return false;
        }
        public string[,] IniciarBuscaLargura(int rotuloRaiz)
        {
            while (VerificarIndiceZero())
            {
                int indiceColuna = ObterIndiceDoVerticePeloRotulo(rotuloRaiz);
                Vertice raiz = grafo.ObterVerticePorRotulo(rotuloRaiz);
                t++; raiz.SetIndice(t);
                tabelaBuscaLargura[1, indiceColuna+1] = raiz.ToString();
                tabelaBuscaLargura[2, indiceColuna + 1] = raiz.RetornaNivel().ToString();
                filaBuscaLargura.Enqueue(raiz);
                BuscaLargura();
            }
            return tabelaBuscaLargura;
        }
        public string[,] BuscaLargura()
        {
            while (filaBuscaLargura.Count>0)
            {
                Vertice v = filaBuscaLargura.Dequeue();

                foreach(Vertice w in v.RetornaVizinhanca())
                {
                    if (w.RetornaIndice() == 0)
                    {
                        v.RetornaArestaEspecifica(w).SetTipoAresta(1);
                        w.SetPai(v);
                        w.SetNivel(v.RetornaNivel() + 1);
                        t++;
                        w.SetIndice(t);

                        //Preencher Tabela
                        int indiceFilho = ObterIndiceDoVerticePeloRotulo(w.RetornaRotuloDoVertice()) + 1;
                        tabelaBuscaLargura[3,indiceFilho] = w.RetornaPai().ToString();
                        tabelaBuscaLargura[2,indiceFilho] = w.RetornaNivel().ToString();
                        tabelaBuscaLargura[1, indiceFilho] = w.RetornaIndice().ToString();

                        filaBuscaLargura.Enqueue(w);
                    }
                    else if(v.RetornaPai()!=w && v.RetornaNivel() > w.RetornaNivel())
                    {
                        v.RetornaArestaEspecifica(w).SetTipoAresta(2);
                    }
                    else
                    {
                        v.RetornaArestaEspecifica(w).SetTipoAresta(4);
                    }
                }
            }
            return tabelaBuscaLargura;
        }
    }
}
