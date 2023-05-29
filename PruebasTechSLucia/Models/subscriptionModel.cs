namespace PruebasTechSLucia.Models
{
    public class subscriptionModel
    {
        public string token { get; set; }
        public string planName { get; set; }
        public string periodicity { get; set; }
        public ContactDetails contactDetails { get; set; }
        public Amount amount { get; set; }
        public string startDate { get; set; }
        public Metadata metadata { get; set; }	}

    
           
}

