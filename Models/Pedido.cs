public class Pedido
{

    //lacar pedido, fechar pedido , calcular(fechar a conta)
    
    public int Id { get; set; }
    public int PizzaId { get; set; }
    public int MesaId { get; set; }
    public DateTime Horario { get; set;}
    public string? Status { get; set; }

}
