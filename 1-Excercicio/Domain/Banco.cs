using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace _1_Excercicio
{
    public class Banco
    {
        public IList<ContaCorrente> ContasCorrente { get; set; } = new List<ContaCorrente>();

        public void CriarConta(ContaCorrente contaCorrente)
        {
            this.ContasCorrente.Add(contaCorrente);
        }
        
        public void DeletarConta(ContaCorrente contaCorrente)
        {
            this.ContasCorrente.Remove(contaCorrente);
        }
        
        public void SacarConta(ContaCorrente contaCorrente, double valor)
        {
            if ((contaCorrente.Saldo + contaCorrente.Limite) >= valor)
            {
                contaCorrente.Saldo -= valor;
                contaCorrente.Limite += contaCorrente.Saldo ;
                contaCorrente.Especial = true;
                var movimentacao2 = new Movimentacao()
                {
                    Descricao = "Saque de Conta",
                    Valor = valor,
                    Tipo = Tipo.Debito
                };
                contaCorrente.Movimentacoes.Add(movimentacao2);
            }
            else 
            {
                throw new LimiteAtingidoException ("Nao foi possivel efetuar Saque");
            }
        }
        
        public void DepositarContas(ContaCorrente contaCorrente, double valor)
        {
            contaCorrente.Saldo += valor;
            var movimentacao = new Movimentacao()
            {
                Descricao = "Deposito de Conta",
                Valor = valor,
                Tipo = Tipo.Credito
            };
            contaCorrente.Movimentacoes.Add(movimentacao);
        }
        
        public void EmitirSaldo(ContaCorrente contaCorrente)
        {
            Console.WriteLine(contaCorrente.Saldo);
        }
        
        public void EmitirExtrato(ContaCorrente contaCorrente)
        {
            foreach (var movimentacao in contaCorrente.Movimentacoes.OrderBy(x => x.Data))
            {
                Console.WriteLine("Data: " + movimentacao.Data);
                Console.WriteLine("Descricao: " + movimentacao.Descricao);
                Console.WriteLine("Valor: " + movimentacao.Valor);
                Console.WriteLine("Tipo: " + movimentacao.Tipo + "\n");
            }
        }

        public void TransferirContas(ContaCorrente contaCorrente1, ContaCorrente contaCorrente2, double valor)
        {
            if ((contaCorrente1.Saldo + contaCorrente1.Limite) >= valor )
            {
                contaCorrente1.Saldo -= valor;
                contaCorrente1.Limite += contaCorrente1.Saldo;
                var movimentacao1 = new Movimentacao()
                {
                    Descricao = "Foi descontado para uma transferencia entre contas",
                    Valor = valor,
                    Tipo = Tipo.Debito
                };
                contaCorrente1.Movimentacoes.Add(movimentacao1);

                contaCorrente2.Saldo += valor;
                var movimentacao2 = new Movimentacao()
                {
                    Descricao = "Foi adicionado de uma transferencia entre contas",
                    Valor = valor,
                    Tipo = Tipo.Credito
                };
                contaCorrente1.Movimentacoes.Add(movimentacao2);
            }
            else
            {
                throw new LimiteAtingidoException ("Nao foi possivel efetuar transferencia");
            }
        }
        public class LimiteAtingidoException : TransacaoNaoRealizadaException {
            public LimiteAtingidoException(string message) : base(message)
            {
            }
        }
        
        public class TransacaoNaoRealizadaException : Exception {
            public TransacaoNaoRealizadaException(string message) : base(message)
            {
            }
            
            public TransacaoNaoRealizadaException(string message, Exception inner) : base(message, inner)
            {
            }
        }
    }
}