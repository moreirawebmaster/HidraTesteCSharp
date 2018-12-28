using System.Threading;
using System.Threading.Tasks;
using Bank.Commun.Domain.Dto;
using Bank.Domain.Extratos.Commands.Criar;
using CriarNovaOperacao = Bank.Domain.Lancamentos.Commands.CriarNovaOperacao;
namespace Bank.Domain.Lancamentos.Services
{
    /* Metodo criado para exemplificar o CQRS, não terá leitura em lançamentos
     public interface ILancamentoServiceRead
    {
        
    }*/

    public interface ILancamentoServiceWrite
    {
        Task<Result> CriarNovoLancamento(CriarNovaOperacao.Request request, CancellationToken cancellationToken);
    }
}