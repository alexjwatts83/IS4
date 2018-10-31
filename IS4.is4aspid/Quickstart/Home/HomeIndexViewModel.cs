using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using IdentityServer4.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IdentityServer4.Quickstart.UI
{
    public class ClientViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<string> ValidClaims { get; set; }
        public string Url { get; set; }

        public ClientViewModel()
        {
            ValidClaims = new List<string>();
        }
    }
    public class HomeIndexViewModel 
    {
        public List<ClientViewModel> Clients { get; set; }
        public string NotAuthenticatedMessage { get; set; }

        public HomeIndexViewModel()
        {
            Clients = new List<ClientViewModel>();
        }
    }
}
