namespace ApplicationCore.Constants;
public struct Order
{
    public struct Status
    {
        public const string New = "New"; // Mới
        public const string Partial = "Partial"; // Thanh toán 1 phần
        public const string Finish = "Finish"; // Thanh toán xong đơn hàng
    }
}
