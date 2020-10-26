using System;
using System.Collections.Generic;
using System.Text;

namespace MyLib.Models
{
	 public class CyrillicExeption : Exception
    {
        public CyrillicExeption()
        { }

        public CyrillicExeption(string message)
            : base(message)
        { }

        public CyrillicExeption(string message, Exception innerException)
            : base(message, innerException)
        { }
    }


}
