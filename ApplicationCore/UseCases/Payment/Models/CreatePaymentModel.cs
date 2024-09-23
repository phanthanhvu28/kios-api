namespace ApplicationCore.UseCases.Payment.Models;
public class CreatePaymentModel
{
    public string OrderCode { get; set; }
    public string PaymentMethod { get; set; }
    public string StoreCode { get; set; }
    public decimal Amount { get; set; }
}
