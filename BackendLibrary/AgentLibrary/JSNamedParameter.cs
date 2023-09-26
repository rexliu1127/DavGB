using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SB.Agent
{
    public class JSNamedParameter
    {
        public string paraName { get; set; }
        public string paraValue { get; set; }
        //0:int,1:string
        public int type { get; set; }

        public JSNamedParameter()
        {
            paraName = "";
            paraValue = "";
            type = 0;
        }
    }
}
