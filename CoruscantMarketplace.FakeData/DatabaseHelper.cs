using CoruscantMarketplace.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace CoruscantMarketplace.FakeData
{
    public class DatabaseHelper
    {
        public const int QTD_LOJAS = 500;
        public const int QTD_CATEGORIAS = 100;
        public const int QTD_FABRICANTES = 1000;
        public const int QTD_LOJAS_POR_PRODUTO = 200;

        public static IList<string> Lojas { get; set; }

        public static IList<int> GerarIndicesLoja()
        {
            return UtilHelper.RandomIntList(QTD_LOJAS_POR_PRODUTO, QTD_LOJAS).ToList();
        }

        // 10.000 Produtos / 200 lojas = 50 produtos unicos
        // 500 lojas
        // 100 categorias
        // Cada produto em mais de 200 lojas

        public static IEnumerable<Produto> GerarProdutos(int quantidade)
        {
            IList<Produto> produtos = new List<Produto>();

            Lojas = GerarNomes(QTD_LOJAS);
            IList<string> categorias = GerarNomes(QTD_CATEGORIAS);
            IList<string> fabricantes = GerarNomes(QTD_FABRICANTES);

            for (int i = 0; i < quantidade; i++)
            {
                int indiceFabricante = i / QTD_FABRICANTES;
                int indiceCategoria = i / QTD_CATEGORIAS;
                
                string nomeProduto = UtilHelper.RandomString(50);

                Produto produto = new Produto
                {
                    Nome = nomeProduto,
                    Fabricante = fabricantes[indiceFabricante],
                    Avaliacao = AvaliacaoFactory.RamdomAvaliacoes(UtilHelper.RandomInt(20)),
                    Categoria = categorias[indiceCategoria],
                    Loja = "",
                    Preco = 0
                };
                    

                produtos.Add(produto);
            }

            return produtos;
        }


        private static IList<string> GerarNomes(int quantidade)
        {
            List<string> nomes = new List<string>();

            Enumerable.Range(1, quantidade).ToList().ForEach(p =>
               {
                   nomes.Add(UtilHelper.RandomString(50));
               });

            return nomes;
        }
    }
}
