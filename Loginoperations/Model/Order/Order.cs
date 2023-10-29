using Loginoperations.Enum;

namespace Loginoperations.Model.Order;

public class Order
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; }
    public DateTime DatePosted { get; set; }=DateTime.Now;
    public int UserId { get; set; }
    public User User { get; set; }
    public string OrderStatus { get; set; }
}