namespace Authentication.Domain
{
    public class ShareDomain
    {
        public long Id { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string CreatedBy { get; set; } = "";
        public DateTime? LastModifiedDate { get; set; } = null;
        public string LastModifiedBy { get; set; } = "";
        public bool Active { get; set; } = true;
    }
}
