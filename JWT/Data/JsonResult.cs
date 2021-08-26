using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWT.Data
{
	public class JsonResult
	{
		public bool isSuccess { get; set; }
		public object payload { get; set; }
		public object error { get; set; }
	}
}
