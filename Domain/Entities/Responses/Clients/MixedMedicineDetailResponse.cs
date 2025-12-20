namespace Domain.Entities.Responses.Clients
{
    public class MixedMedicineDetailResponse : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Unit { get; set; }
        public double Price { get; set; }
        public double CurrentStock { get; set; }

        public List<MixedMedicineCompositionResponse> Compositions { get; set; }
    }
    public class MixedMedicineCompositionResponse : BaseEntity
    {
        public int MixedMedicineId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public double ProductPrice { get; set; }
        public double ProductBoughtPrice { get; set; }
        public double QuantityPerUnit { get; set; }
    }
}
