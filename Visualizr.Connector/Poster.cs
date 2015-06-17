using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Visualizr.Connector
{
    public class Poster
    {
        public event EventHandler<PostErrorEventArgs> PostErrorOccurred;

        private string _uri;

        /// <summary>
        ///
        /// </summary>
        /// <param name="uri">Uri of Visualizr.de-Web-Service</param>
        public Poster(string uri)
        {
            _uri = uri;
        }

        /// <summary>
        /// Posting to 'http://storage.visualizr.de/api/processvalues/'
        /// </summary>
        public Poster()
        {
            _uri = "http://storage.visualizr.de/api/processvalues/";
        }

        /// <summary>
        /// Sends values to www.visualizr.de.
        /// </summary>
        /// <param name="values">Your values. The "ApplicationKey" has to be a string of your choice, please use it on visualizr.de.</param>
        /// <returns></returns>
        public async Task PostProcessValuesAsync(List<ProcessValue> values)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var response = await client.PostAsJsonAsync<List<ProcessValue>>(_uri, values);
                }
                catch (Exception ex)
                {
                    PostErrorOccurred(this, new PostErrorEventArgs
                    {
                        Url = _uri,
                        PostErrorException = ex
                    });
                }
            }
        }

        /// <summary>
        /// Gets the values which have this "ApplicationKey"
        /// </summary>
        /// <param name="applicationKey">Your ApplicationKey</param>
        /// <returns></returns>
        public async Task<IEnumerable<ProcessValue>> GetProcessValuesAsync(string applicationKey)
        {
            using (var client = new HttpClient())
            {
                var uri = string.Concat(_uri, "?applicationKey=", applicationKey);
                var response = await client.GetAsync(uri);

                var pValues = await response.Content.ReadAsAsync<IEnumerable<ProcessValue>>();
                return pValues;
            }
        }

        public class PostErrorEventArgs : EventArgs
        {
            public string Url { get; set; }

            public Exception PostErrorException { get; set; }
        }
    }
}