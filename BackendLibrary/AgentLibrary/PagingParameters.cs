using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SB.Agent
{
    public class PagingParameters
    {
        public int Pages { get; set; }
        public int PageIndex { get; set; }
        public int PageLength { get; set; }
        public List<JSNamedParameter> JSNamedParameters { get; set; }

        public PagingParameters()
        {
            Pages = 5;
            PageIndex = 1;
            PageLength = 10;
            JSNamedParameters = new List<JSNamedParameter>();
        }

    }
}
