namespace PruebasTechSLucia.Models
{
    public class autorizacionModel
    {
        public string token { get; set; }

        public AmountA amount { get; set; }
		public OrderDetails ordenDetails { get; set; }
		public string fullResponse { get; set; }

        public ThreeDomainSecure threeDomainSecure { get; set; }

	}

    
}
