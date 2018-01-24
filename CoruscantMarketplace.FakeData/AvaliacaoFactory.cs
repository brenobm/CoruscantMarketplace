using CoruscantMarketplace.Core.Models;
using System.Collections.Generic;

namespace CoruscantMarketplace.FakeData
{
    public class AvaliacaoFactory
    {
        public static Avaliacao RamdomAvaliacao()
        {
            return new Avaliacao
            {
                Comentario = UtilHelper.RandomString(UtilHelper.RandomInt(50)),
                Recomendacao = UtilHelper.RandomByte(5)
            };
        }

        public static IEnumerable<Avaliacao> RamdomAvaliacoes(int quantidade)
        {
            List<Avaliacao> avaliacoes = new List<Avaliacao>();

            for (int i = 0; i < quantidade; i++)
            {
                avaliacoes.Add(RamdomAvaliacao());
            }

            return avaliacoes;
        }

    }
}
