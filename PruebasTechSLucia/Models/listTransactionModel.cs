namespace PruebasTechSLucia.Models
{
	public class listTransactionModel
	{
		public DateTime from { get; set; }
		public DateTime to { get; set; }
		public string approval_code { get; set; }
		public string approved_transaction_amount { get; set; }
		public string bin_card { get; set; }
		public string card_holder_name { get; set;}
	}

}
