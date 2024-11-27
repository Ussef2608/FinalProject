using Salon.Models;

namespace Salon.ViewModel
{
    public class HomeViewModel
    {
        public IEnumerable<Service> Services { get; set; }
        public IEnumerable<ServiceDetails> Top4ServiceDetails { get; set; }
    }
}
