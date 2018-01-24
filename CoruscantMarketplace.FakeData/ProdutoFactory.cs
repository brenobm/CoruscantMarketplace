using CoruscantMarketplace.Core.Models;
using System;
using System.Collections.Generic;

namespace CoruscantMarketplace.FakeData
{
    public class ProdutoFactory
    {
        public static Produto RandomProduto()
        {
            return new Produto
            {
                Id = Guid.NewGuid().ToString(),
                Nome = UtilHelper.RandomString(UtilHelper.RandomInt(50)),
                Categoria = UtilHelper.RandomString(UtilHelper.RandomInt(50)),
                Fabricante = UtilHelper.RandomString(UtilHelper.RandomInt(50)),
                Loja = UtilHelper.RandomString(UtilHelper.RandomInt(50)),
                Preco = UtilHelper.RandomDecimal(),
                Avaliacao = AvaliacaoFactory.RamdomAvaliacoes(UtilHelper.RandomInt(15))
            };
        }

        public static IEnumerable<Produto> RandomProdutos(int quantidade)
        {
            List<Produto> produtos = new List<Produto>();

            for(int i = 0; i < quantidade; i++)
            {
                produtos.Add(RandomProduto());
            }

            return produtos;
        }
    }
}
