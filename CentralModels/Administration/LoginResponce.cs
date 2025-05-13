using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralModels.Administration
{
    public class LoginResponce
    {
        public string userId {  get; set; }
        public string token { get; set; }
        public string userRole { get; set; }
        public string userName { get; set; }
    }
}
