namespace Domain.Entities.Requests.Clients
{
    public class MixedMedicineDetailRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string Unit { get; set; }

        public List<MixedMedicineCompositionRequest> Compositions { get; set; }
    }

    public class MixedMedicineCompositionRequest
    {
        public int ProductId { get; set; }
        public double QuantityPerUnit { get; set; }
    }
}
