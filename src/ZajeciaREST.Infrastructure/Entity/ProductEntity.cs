namespace ZajeciaREST.Infrastructure.Entity;

internal class ProductEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public List<CartEntity> Carts { get; } = new();
}
