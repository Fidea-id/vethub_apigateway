namespace Domain.Entities.DTOs
{
    public class DataResultDTO<T>
    {
        //public DataResultDTO(IEnumerable<T> data, int count)
        //{
        //    Data = data;
        //    TotalData = count;
        //}
        public IEnumerable<T> Data { get; set; }
        public int TotalData { get; set; }
    }
}
