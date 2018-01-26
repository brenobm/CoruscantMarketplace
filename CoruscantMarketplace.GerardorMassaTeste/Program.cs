using System;
using System.Linq;
using CoruscantMarketplace.FakeData;
using RestSharp;
using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

namespace CoruscantMarketplace.GerardorMassaTeste
{
    class Program
    {
        private static int qtdSucesso = 0;
        private static int qtdErro = 0;

        static void Main(string[] args)
        {
            Console.WriteLine("Olá, favor digitar a URL base da api (ex: http://localhost/api/)");
            var url = Console.ReadLine();
            GerarMassa(url);
            Console.WriteLine("Geração Finalizada");
            Console.WriteLine($"{qtdSucesso} Inseridos com Sucesso.");
            Console.WriteLine($"{qtdErro} Não inseridos.");
            Console.ReadKey();
        }

        private static void GerarMassa(string url)
        {
            var produtos = DatabaseHelper.GerarProdutos(100).ToList();

            var token = GerarToken(url);

            Parallel.ForEach(produtos, (produto) =>
            {
                IRestResponse response = null;

                try
                {
                    RestClient restClient = new RestClient(url + "produto");

                    for (int i = 0; i < DatabaseHelper.QTD_LOJAS_POR_PRODUTO; i++)
                    {
                        var request = new RestRequest(Method.POST);
                        request.AddHeader("content-type", "application/json");
                        request.AddHeader("Authorization", $"Bearer {token}");

                        var indicesLoja = DatabaseHelper.GerarIndicesLoja();

                        produto.Loja = DatabaseHelper.Lojas[indicesLoja[i]];
                        produto.Preco = UtilHelper.RandomDecimal() * UtilHelper.RandomInt(250);

                        request.AddParameter("application/json", JsonConvert.SerializeObject(produto), ParameterType.RequestBody);
                        response = restClient.Execute(request);

                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            Console.WriteLine("Produto inserido");
                            Interlocked.Increment(ref qtdSucesso);
                        }
                        else
                        {
                            var erro = new { error = "" };

                            erro = JsonConvert.DeserializeAnonymousType(response.Content, erro);

                            Console.WriteLine($"Produto não inserido - {erro.error}");
                            Interlocked.Increment(ref qtdErro);
                        }
                    }
                }
                catch (Exception ex)
                {
                    var erro = new { error = "" };

                    if (response != null)
                        erro = JsonConvert.DeserializeAnonymousType(response.Content, erro);

                    Console.WriteLine($"Erro: {erro.error} - {ex.Message}");
                    Interlocked.Increment(ref qtdErro);
                }
            });
        }

        private static string GerarToken(string url)
        {
            RestClient restClient = new RestClient(url + "AuthToken");

            var request = new RestRequest(Method.GET);
            request.AddHeader("content-type", "application/json");

            IRestResponse response = restClient.Execute(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<string>(response.Content);
            }
            else
            {
                throw new Exception("Erro ao gerar o token de acesso");
            }
        }
    }
}
