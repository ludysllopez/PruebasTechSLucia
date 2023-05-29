using PruebasTechSLucia.Models;

namespace PruebasTechSLucia.Services
{
	public interface IAuthorization
	{
		Task<string> CreateAuthorization(autorizacion datos);

		Task<string> getAuthorization(getAutorizacion datos);
	}
}
