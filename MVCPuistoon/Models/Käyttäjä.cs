using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace MVCPuistoon.Models
{
    public class Käyttäjä
    {
        public string Id { get; set; }
        public string KäyttäjäTunnus { get; set; }
        public string NormalizedUserName { get; set; }
        public string PasswordHash { get; set; }
    }
}