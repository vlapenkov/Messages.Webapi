namespace RK.Statistic.Domain
{
    public class ProductReadStatistics
    {
        public Guid Id { get; set; }
        public string Page { get; set; }
        public string Production { get; set; }
        public string Category { get; set; }
        public string Producer { get; set; }
        public string UserName { get; set; }
        public DateTime Created { get; set; }

    }
}