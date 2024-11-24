namespace Activite.Services.User.DTOs;

public class PercentageCouponDto : CouponDto
{
    public int Percentage { get; set; }

    public decimal MaxDiscountAmount { get; set; }
} 