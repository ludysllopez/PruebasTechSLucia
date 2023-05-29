using PruebasTechSLucia.Models;

namespace PruebasTechSLucia.Services
{
	public interface ISubcription
	{
		Task<string> createSubscription(subscription datos);
	}
}
