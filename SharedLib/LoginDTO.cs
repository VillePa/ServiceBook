using System;
using System.ComponentModel.DataAnnotations;

namespace SharedLib
{
	public class LoginDTO
	{
        public class KayttajaDTO
        {
            public string Kayttajatunnus { get; set; }
            public string Salasana { get; set; }

        }

	}
}

