using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace MvcFakes
{
    public class FakeHttpResponse : HttpResponseBase
    {
        private StringBuilder sb = new StringBuilder();


        public override void Write(string s)
        {
            sb.Append(s);
        }

        public override string ToString()
        {
            return sb.ToString();
        }


    }
}
