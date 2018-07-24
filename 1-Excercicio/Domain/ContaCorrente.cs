using System.Collections;
using System.Collections.Generic;

namespace _1_Excercicio
{
    public class ContaCorrente
    {
        public int Numero { get; set; }
        public double Saldo { get; set; }
        public double Limite { get; set; }
        public bool Especial { get; set; }
        public IList<Movimentacao> Movimentacoes { get; set; }

        public ContaCorrente(int numero, double saldo, double limite)
        {
            Numero = numero;
            Saldo = saldo;
            Limite = limite;
            Especial = false;
            Movimentacoes = new List<Movimentacao>();
        }
    }
}