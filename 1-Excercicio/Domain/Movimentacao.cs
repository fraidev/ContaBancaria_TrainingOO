namespace _1_Excercicio
{
    public class Movimentacao
    {
        public string Descricao { get; set; }
        public double Valor { get; set; }
        public Tipo Tipo {get;set;}
    }
    public enum Tipo
    {
        Credito,
        Debito
    } 
}