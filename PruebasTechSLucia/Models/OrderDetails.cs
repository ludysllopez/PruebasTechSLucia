namespace PruebasTechSLucia.Models
{
	public class OrderDetails
	{
		public string siteDomain { get; set; }
		public ShippingDetails shippingDetails { get; set; }		
		public ShippingDetails billingDetails { get; set; }

		

	}

	public class ShippingDetails
	{
		public string name { get; set; }
		public string phone { get; set; }

		public string address1 { get; set; }

		public string city { get; set; }

		public string region { get; set; }

		public string country { get; set; }

		
	}

	public class ThreeDomainSecure
	{
		public string cavv { get; set; }
		public string eci { get; set; }
		public string xid { get; set; }
		public string specificationVersion { get; set; }
		public bool acceptRisk { get; set; }
	}

	public class ThreeDomainSecure2
	{
		
	}


}
