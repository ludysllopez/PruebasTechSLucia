namespace PruebasTechSLucia.Models
{
    public class subscription
    {
        public string token { get; set; }
        public string planName { get; set; }
        public string periodicity { get; set; }
        public string startDate { get; set; }
		public string documentType { get; set; }
		public string documentNumber { get; set; }
		public string email { get; set; }
		public string firstName { get; set; }
		public string lastName { get; set; }
		public string phoneNumber { get; set; }
		public float subtotalIva { get; set; }
		public float subtotalIva0 { get; set; }
		public float ice { get; set; }
		public double iva { get; set; }
		public string currency { get; set; }
		public int iac { get; set; }
		public int tasaAeroportuaria { get; set; }
		public int agenciaDeViaje { get; set; }
		public string cardio { get; set; }
		public string rumba { get; set; }
		public string pool { get; set; }

	}

	public class ContactDetails
	{
		public string documentType { get; set; }
		public string documentNumber { get; set; }
		public string email { get; set; }
		public string firstName { get; set; }
		public string lastName { get; set; }
		public string phoneNumber { get; set; }		

	}

    public class Amount
    {
        public float subtotalIva { get; set; }
        public float subtotalIva0 { get; set; }
        public float ice { get; set; }
        public double iva { get; set; }
        public string currency { get; set; }

	}

    
           
}

