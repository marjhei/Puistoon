using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace MVCPuistoon
{
    public class LoginModel
    {
        public string KäyttäjäTunnus { get; set; }
        [DataType(DataType.Password)]
        public string Salasana { get; set; }
    }
}
