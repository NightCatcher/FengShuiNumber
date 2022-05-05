using System.Collections.Generic;

namespace FengShuiNumber
{
    public class FengShuiNumberConfiguration
    {
        public int NumberLengthLimit { get; set; }
        public HeadNumbers HeadNumbers { get; set; }
        public IEnumerable<string> TabooPairNumbers { get; set; }
        public IEnumerable<string> NicePairNumbers { get; set; }
        public IEnumerable<string> FengShuiRate { get; set; }
    }

    public class HeadNumbers
    {
        public IEnumerable<string> Viettel { get; set; }
        public IEnumerable<string> Vinaphone { get; set; }
        public IEnumerable<string> Mobifone { get; set; }
    }
}
