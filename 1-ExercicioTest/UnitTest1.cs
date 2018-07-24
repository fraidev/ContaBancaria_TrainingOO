using NUnit.Framework;
using _1_Excercicio;

namespace Tests
{
    public class Tests
    {
        //Criacao de Conta
        [Test]
        public void Conta_Deve_Ser_Criada_Ao_Criar()
        {
            var banco = new Banco();
            var conta = new ContaCorrente(12345, 500, 1000);
            banco.CriarConta(conta);

            Assert.AreEqual(banco.ContasCorrente.Count,1);
            Assert.AreEqual(banco.ContasCorrente[0].Numero,conta.Numero);
        }
        
        //Exclusao de Conta
        [Test]
        public void Conta_Deve_Ser_Deletada_Ao_Ser_Excluida()
        {
            var banco = new Banco();
            var conta = new ContaCorrente(12345, 500, 1000);
            banco.CriarConta(conta);
            banco.DeletarConta(conta);
            
            Assert.AreEqual(banco.ContasCorrente.Count,0);
        }
        
        //Saque
        [Test]
        public void Saldo_Deve_Ser_Mantido()
        {
            var banco = new Banco();
            var conta = new ContaCorrente(12345, 500, 1000);
            banco.CriarConta(conta);
            
            Assert.AreEqual(conta.Saldo, 500);
            Assert.AreEqual(banco.ContasCorrente[0].Saldo, 500);
        }
        [Test]
        public void Deve_Descontar_Quando_Sacar()
        {
            var banco = new Banco();
            var conta = new ContaCorrente(12345, 500, 1000);
            banco.CriarConta(conta);

            banco.SacarConta(conta, 300);
            
            Assert.AreEqual(conta.Saldo, 500-300);
            Assert.AreEqual(banco.ContasCorrente[0].Saldo, 500-300);
        }
        [Test]
        public void Deve_Descontar_Do_Limite_Quando_Sacar_E_Estiver_Sem_Saldo_E_Com_Limite()
        {
            var banco = new Banco();
            var conta = new ContaCorrente(12345, 500, 1000);
            banco.CriarConta(conta);

            banco.SacarConta(conta, 800);
            
            Assert.AreEqual(conta.Saldo, -300);
            Assert.AreEqual(banco.ContasCorrente[0].Saldo, -300);
            
            Assert.AreEqual(conta.Limite, 700); 
            Assert.AreEqual(banco.ContasCorrente[0].Limite, 700);
        }
        
        [Test]
        public void Deve_Regeitar_Saques_Maiores_Que_O_Limite_Saldo_Somados()
        {
            var banco = new Banco();
            var conta = new ContaCorrente(12345, 500, 1000);
            banco.CriarConta(conta);
            
            banco.SacarConta(conta, 1501);
            
            Assert.AreEqual(conta.Saldo, 500);
            Assert.AreEqual(banco.ContasCorrente[0].Saldo, 500);
            Assert.AreEqual(conta.Limite, 1000);
            Assert.AreEqual(banco.ContasCorrente[0].Limite, 1000);
        }
        
        //Deposito
        [Test]
        public void Deve_Bonificar_Quando_Depositar()
        {
            var banco = new Banco();
            var conta = new ContaCorrente(12345, 500, 1000);
            banco.CriarConta(conta);
            
            banco.DepositarContas(conta, 1500);
            
            Assert.AreEqual(conta.Saldo, 500+1500);
            Assert.AreEqual(banco.ContasCorrente[0].Saldo, 500+1500);
        }
        //TransferirContas
        [Test]
        public void TransferirContas_Deve_Enviar_Valores_Corretos()
        {
            var banco = new Banco();
            var conta = new ContaCorrente(12345, 500, 1000);
            var conta2 = new ContaCorrente(12345, 700, 1000);
            banco.CriarConta(conta);
            banco.CriarConta(conta2);
            
            banco.TransferirContas(conta, conta2, 300);
            
            Assert.AreEqual(conta.Saldo, 500-300);
            Assert.AreEqual(conta2.Saldo, 700+300);
        }
        [Test]
        public void TransferirContas_Deve_Usar_Limite_Se_Estiver_Sem_Saldo()
        {
            var banco = new Banco();
            var conta = new ContaCorrente(12345, 500, 1000);
            var conta2 = new ContaCorrente(12345, 700, 1000);
            
            banco.TransferirContas(conta, conta2, 600);
            
            Assert.AreEqual(conta.Saldo, -100);
            Assert.AreEqual(conta.Limite, 1000-100);
            Assert.AreEqual(conta2.Saldo, 700+600);
        }
        [Test]
        public void TransferirContas_Deve_Rejeitar_Se_Estiver_Sem_Saldo_E_Sem_Limite()
        {
            var banco = new Banco();
            var conta = new ContaCorrente(12345, 500, 1000);
            var conta2 = new ContaCorrente(12345, 700, 1000);
            
            banco.TransferirContas(conta, conta2, 1600);
            
            Assert.AreEqual(conta.Saldo, 500);
            Assert.AreEqual(conta2.Saldo, 700);
        }
        
        
    }
}