using Loginoperations.Enum;

namespace Loginoperations.Dto.Order;

public class UpdateStatusRequest
{
    public OrderStatus Status { get; set; }
}