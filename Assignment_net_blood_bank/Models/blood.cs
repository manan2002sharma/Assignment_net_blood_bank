namespace Assignment_net_blood_bank.Models
{
    public class blood
    {
        //This is model with given mentioned fields in assignment
        public int Id { get; set; } 
        public string? DonorName { get; set; }
        public int age { get; set; }
        public string? BloodType { get; set; }
        public string? ContactInfo { get; set; }
        public int Quantity { get; set; }
        public DateTime CollectionDate { get; set; }
        public DateTime ExpiratoinDate  { get; set; }
        public string? Status { get; set; }
    }
}
