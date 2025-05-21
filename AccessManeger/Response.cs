using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monaem.Response
{
    public class ReciveResponse<T>
    {
        public T ReciveData { get; set; }
        public string ErrorMessageg { get; set; }
    }

    public class SendResponse<T>
    {
        public T SendData { get; set; }
        public string ErrorMessage { get; set; }
    }
}
