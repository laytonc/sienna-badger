using System.Collections.Generic;

namespace SiennaBadger.Data.Models
{
    public class PageSummary
    {
        public string Url { get; set; }

        public IEnumerable<Word> Words { get; set; }

        public IEnumerable<Image> Images { get; set; }
    }
}
