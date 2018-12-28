using System.Threading;
using System.Threading.Tasks;
using Bank.Commun.Domain.Dto;
using Bank.Domain.Entities;
using Bank.Domain.Lancamentos.Commands.CriarNovaOperacao;
using Bank.Infra.CrossCutting.Interfaces;
using MediatR;

namespace Bank.Domain.Lancamentos.Services
{
    public class LancamentoService : ILancamentoServiceWrite
    {
        private readonly IRepository<ContaCorrente> _repository;
        private readonly IMediator _mediator;

        public LancamentoService(IRepository<ContaCorrente> repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        public async Task<Result> CriarNovoLancamento(Request request, CancellationToken cancellationToken)
        {
            var result = new Result();

            var contaCorrenteCredito = await _repository.GetByIdAsync(request.ContaCorrenteCredito);
            var contaCorrenteDebito = await _repository.GetByIdAsync(request.ContaCorrenteDebito);

            if (contaCorrenteCredito == null)
                result.AddValidation("Não encontramos a conta para credito informada.");

            if (contaCorrenteDebito == null)
                result.AddValidation("Não encontramos a conta para debito informada.");

            if (result.HasValidation) return result;

            var saldoDebitoAnterior = contaCorrenteDebito.Saldo;
            var saldoCreditoAnterior = contaCorrenteCredito.Saldo;

            contaCorrenteCredito.Lancamento(TipoTransacao.Credito, request.Valor);
            contaCorrenteDebito.Lancamento(TipoTransacao.Debito, request.Valor);

            foreach (var validate in contaCorrenteCredito.Validades)
                result.AddValidation(validate);

            foreach (var validate in contaCorrenteDebito.Validades)
                result.AddValidation(validate);

            if (result.HasValidation) return result;

            await _repository.SaveChangeAsync();


            var credito = _mediator.Publish(new Notification
            {
                TipoTransacao = TipoTransacao.Credito,
                Valor = request.Valor,
                Id = request.ContaCorrenteCredito,
                SaldoAnterior = saldoCreditoAnterior,
                SaldoAtual = contaCorrenteCredito.Saldo
            }, cancellationToken);

            var debito = _mediator.Publish(new Notification
            {
                TipoTransacao = TipoTransacao.Debito,
                Valor = request.Valor,
                Id = request.ContaCorrenteDebito,
                SaldoAnterior = saldoDebitoAnterior,
                SaldoAtual = contaCorrenteDebito.Saldo
            }, cancellationToken);

            Task.WaitAll(credito, debito);


            return result;
        }
    }
}