namespace PruebasTechSLucia.Models
{
    public class subscription
    {
        public string token { get; set; }
        public string planName { get; set; }
        public string periodicity { get; set; }
        public contactDetails ContactDetails { get; set; }
        public amount Amount { get; set; }
        public string startDate { get; set; }
        public metadata Metadata { get; set; }
    }

    public class contactDetails
    {

        public string documentType { get; set; }
        public string documentNumber { get; set; }
        public string email { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string phoneNumber { get; set; }

    }

    public class amount
    {
        public float subtotalIva { get; set; }
        public float subtotalIva0 { get; set; }
        public float ice { get; set; }
        public double iva { get; set; }
        public string currency { get; set; }
    }

    public class metadata
    {
        public plan Plan { get; set; }
    }

    public class plan
    {
        public fitness Fitness { get; set; }
    }

    public class fitness
    {
        public string cardio { get; set; }
        public string rumba { get; set; }
        public string pool { get; set; }

    }
           
}

