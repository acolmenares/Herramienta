using System;
using System.Collections.Generic;
using ServiceStack;

namespace Herramienta.Modelos.Interfaces
{
	public abstract class ServiceResponse:IServiceResponse
	{
		
		public Dictionary<string, string> Meta
		{
			get;set;
		}

		public ResponseStatus ResponseStatus
		{
			get; set;
		}
	}
}

