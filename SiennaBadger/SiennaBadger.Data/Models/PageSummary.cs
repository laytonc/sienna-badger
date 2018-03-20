using System.Collections.Generic;
using System.Net;

namespace SiennaBadger.Data.Models
{
    public class PageSummary
    {
        /// <summary>
        /// Gets the status code of the server's response, if any.
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// Number of words parsed from the page
        /// </summary>
        public int WordCount { get; set; }

        /// <summary>
        ///List of words in the page
        /// </summary>
        public IEnumerable<Word> Words { get; set; }

        /// <summary>
        ///List of images found on the page
        /// </summary>
        public IEnumerable<Image> Images { get; set; }
    }
}
