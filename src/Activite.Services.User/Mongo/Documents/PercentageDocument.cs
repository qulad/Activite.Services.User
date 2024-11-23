namespace Activite.Services.User.Mongo.Documents;

public class PercentageDocument : CouponDocument
{
    public int Percentage { get; set; }

    public decimal MaxDiscountAmount { get; set; }
}