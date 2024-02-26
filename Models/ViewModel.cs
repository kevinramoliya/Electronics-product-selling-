using System.Data;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Models
{
    public class ViewModel
    {
        public DataTable CartTable { get; set; }
        public DataTable AddressTable { get; set; }
    }
}
