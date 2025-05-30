﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralModels.Administration
{
    public class UserInfo 
    {
        public string userId { get; set; }
        public string userRole { get; set; }
        public string userName { get; set; }
    }

    public class LoginResponce
    {
        public UserInfo userInfo { get; set; }
        public string token { get; set; }
    }
}
