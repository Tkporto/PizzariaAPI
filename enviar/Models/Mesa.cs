public class Mesa
{
    public int Id { get; set; }
    public int Lugares { get; set; }
    public double TotalValor { get; set; }
    public List<Pizza> ?Pizzas {get; set;}

}