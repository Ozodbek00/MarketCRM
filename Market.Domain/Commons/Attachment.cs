namespace Market.Domain.Commons
{
    public class Attachment : Auditable
    {
        public string Name { get; set; }
        public string Path { get; set; }
    }
}
