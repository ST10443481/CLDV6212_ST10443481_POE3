namespace ABCRetailers.Models;

public class Cart
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string UserId { get; set; } = string.Empty;
    public List<CartItem> Items { get; set; } = new();

    public decimal GetTotal()
    {
        return Items.Sum(i => i.Quantity * i.UnitPrice);
    }
}

public class CartItem
{
    public string ProductId { get; set; } = string.Empty;
    public string ProductName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public string? ImageUrl { get; set; }
}
