using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class GroupReport
    {
        public IEnumerable<StudentAvegareInfo> StudentAvegareInfos { get; set; }

        public SummaryMarkInfo SummaryMarkInfo { get; set; }
    }
}
