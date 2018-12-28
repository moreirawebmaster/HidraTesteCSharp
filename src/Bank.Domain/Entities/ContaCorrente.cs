using Bank.Commun.Domain.Entitie;
using System;
using System.Collections.Generic;

namespace Bank.Domain.Entities
{
    public class ContaCorrente : BaseEntity
    {
        public int NumeroConta { get; protected set; }
        public int Digito { get; protected set; }
        public decimal Saldo { get; protected set; }
        public virtual ICollection<Extrato> Extratos { get; set; }

        public ContaCorrente Lancamento(TipoTransacao tipoTrancao, decimal valor)
        {
            
            switch (tipoTrancao)
            {
                case TipoTransacao.Credito:
                    return Credito(valor);
                case TipoTransacao.Debito:
                    return Debito(valor);
                default:
                    throw new ArgumentOutOfRangeException(nameof(tipoTrancao), tipoTrancao, null);
            }

        }

        private ContaCorrente Debito(decimal valor)
        {
            if (valor > Saldo)
                AddValidades("Você não tem saldo suficiênte para essa operação.");

            if (valor <= 0)
                AddValidades("Valor informado precisar ser maior que zero");

            if (IsValid())
                Saldo -= valor;

            return this;
        }

        private ContaCorrente Credito(decimal valor)
        {
            if (valor <= 0)
                AddValidades("Valor informado precisar ser maior que zero");

            if (IsValid())
                Saldo += valor;

            return this;
        }


        public static ContaCorrente NovaConta(decimal saldo)
        {
            var contaCorrente = new ContaCorrente
            {
                NumeroConta = new Random().Next(11111, 88888),
                Digito = new Random().Next(1, 9),
                Saldo = saldo
            };

            return contaCorrente;
        }
    }
}