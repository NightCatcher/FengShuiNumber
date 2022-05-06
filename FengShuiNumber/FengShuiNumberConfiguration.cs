using System.Collections.Generic;

namespace FengShuiNumber
{
    public class FengShuiNumberConfiguration
    {
        public int NumberLengthLimit { get; set; }
        public IDictionary<string, IEnumerable<string>> HeadNumbers { get; set; }

        public IEnumerable<string> TabooPairNumbers { get; set; }
        public IEnumerable<string> NicePairNumbers { get; set; }
        public IEnumerable<string> FengShuiRate { get; set; }
    }
}
