using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace MVCPuistoon.Models
{
    public class RegisterModel
    {
        public string KäyttäjäTunnus { get; set; }
        [DataType(DataType.Password)]
        public string Salasana { get; set; }
        [Compare("Salasana")]
        [DataType(DataType.Password)]
        public string ToistaSalasana { get; set; }
    }
}