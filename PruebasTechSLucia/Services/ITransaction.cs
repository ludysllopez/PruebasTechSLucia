using Microsoft.AspNetCore.Mvc;
using PruebasTechSLucia.Models;

namespace PruebasTechSLucia.Services
{
	public interface ITransaction
	{
		Task<string> ObtenerListadoTransacciones(listTransactionModel lt);
	}
}
