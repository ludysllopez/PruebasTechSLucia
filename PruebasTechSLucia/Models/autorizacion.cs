namespace PruebasTechSLucia.Models
{
    public class autorizacion
    {
        public string token { get; set; }
		public float subtotalIva { get; set; }
		public float subtotalIva0 { get; set; }
		public float ice { get; set; }
		public double iva { get; set; }
		public string currency { get; set; }

		public int iac { get; set; }
		public int tasaAeroportuaria { get; set; }
		public int agenciaDeViaje { get; set; }

		public string siteDomain { get; set; }
		public string name { get; set; }
		public string phone { get; set; }

		public string address1 { get; set; }

		public string city { get; set; }

		public string region { get; set; }

		public string country { get; set; }

		public string name2 { get; set; }
		public string phone2 { get; set; }

		public string address2 { get; set; }

		public string city2 { get; set; }

		public string region2 { get; set; }

		public string country2 { get; set; }
		public string fullResponse { get; set; }


	}

	public class AmountA
	{
		public string currency { get; set; }
		public float subtotalIva { get; set; }
		public float subtotalIva0 { get; set; }		
		public double iva { get; set; }
		public float ice { get; set; }
		public ExtraTaxes extraTaxes { get; set; }

	}

	public class ExtraTaxes
	{
		public int iac { get; set; }
		public int tasaAeroportuaria { get; set; }
		public int agenciaDeViaje { get; set; }
	}

	public class Metadata
	{
		public Plan plan { get; set; }
	}

	public class Plan
	{
		public Fitness fitness { get; set; }
	}

	public class Fitness
	{
		public string cardio { get; set; }
		public string rumba { get; set; }
		public string pool { get; set; }

	}
}
