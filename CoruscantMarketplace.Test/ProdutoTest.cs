using CoruscantMarketplace.Core.Impl.Business;
using CoruscantMarketplace.Core.Models;
using CoruscantMarketplace.DataLayer.Repositories;
using CoruscantMarketplace.FakeData;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CoruscantMarketplace.Test
{
    public class ProdutoTest
    {
        [Fact]
        public void InserirProduto()
        {
            Mock<IProdutoRepository> produtoRepositorioMock = new Mock<IProdutoRepository>();
            produtoRepositorioMock.Setup(pr => pr.Inserir(It.IsAny<Produto>())).Returns(() => {
                var p = new Produto();
                p.Id = Guid.NewGuid().ToString();
                return p;
            });

            ProdutoBusiness produtoBusiness = new ProdutoBusiness(produtoRepositorioMock.Object);

            var produto = new Produto();
            produto = produtoBusiness.Inserir(produto);

            Assert.True(!string.IsNullOrEmpty(produto.Id));
        }

        [Fact]
        public void ListarProdutos()
        {
            IEnumerable<Produto> produtos = ProdutoFactory.RandomProdutos(5);
            
            Mock<IProdutoRepository> produtoRepositorioMock = new Mock<IProdutoRepository>();
            produtoRepositorioMock.Setup(pr => pr.ListarTodos()).Returns(() => {
                return produtos;
            });

            ProdutoBusiness produtoBusiness = new ProdutoBusiness(produtoRepositorioMock.Object);

            IEnumerable<Produto> listaProdutos = produtoBusiness.ListarTodos();

            Assert.Equal(listaProdutos, produtos);
        }

        [Fact]
        public void Excluir()
        {
            List<Produto> produtos = ProdutoFactory.RandomProdutos(5).ToList();

            Mock<IProdutoRepository> produtoRepositorioMock = new Mock<IProdutoRepository>();
            produtoRepositorioMock.Setup(pr => pr.Excluir(It.IsAny<string>())).Callback<string>((id) => 
            {
                produtos.RemoveAll(i => i.Id == id);
            });

            produtoRepositorioMock.Setup(pr => pr.ListarTodos()).Returns(() => {
                return produtos;
            });

            ProdutoBusiness produtoBusiness = new ProdutoBusiness(produtoRepositorioMock.Object);

            Produto produto = produtos[UtilHelper.RandomInt(4)];

            produtoBusiness.Excluir(produto.Id);

            IEnumerable<Produto> listaProdutos = produtoBusiness.ListarTodos();


            Assert.True(!listaProdutos.Contains(produto));
        }

        [Fact]
        public void AtualizarPreco()
        {
            List<Produto> produtos = ProdutoFactory.RandomProdutos(1).ToList();

            Mock<IProdutoRepository> produtoRepositorioMock = new Mock<IProdutoRepository>();


            produtoRepositorioMock.Setup(pr =>
                   pr.AtualizarCampos(It.IsAny<string>(), It.IsAny<object>()))
                       .Callback<string, object>((id, valores) =>
                       {
                           foreach (var prop in valores.GetType().GetProperties())
                           {
                               Produto p = produtos.FirstOrDefault(i => i.Id == id);

                               object novoValor = prop.GetValue(valores);

                               var produtoProp = p.GetType().GetProperty(prop.Name);

                               produtoProp.SetValue(p, prop.GetValue(valores));
                           }
                       });

            ProdutoBusiness produtoBusiness = new ProdutoBusiness(produtoRepositorioMock.Object);

            Produto produto = produtos[0];

            decimal precoAntigo = produto.Preco;
            decimal precoNovo = UtilHelper.RandomDecimal();

            produtoBusiness.AtualizarPreco(produto.Id, precoNovo);

            Assert.NotEqual(precoAntigo, produto.Preco, 4);

            Assert.Equal(precoNovo, produto.Preco);
        }
    }
}
