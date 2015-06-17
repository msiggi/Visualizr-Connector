using System;

namespace Visualizr.Connector
{
    public class ProcessValue
    {
        public ProcessValue()
        { }

        public ProcessValue(string category, string name, string value, string appKey)
        {
            this.Category = category;
            this.Name = name;
            this.Value = value;
            this.ApplicationKey = appKey;
            this.Timestamp = DateTime.Now;
            this.HaveToArchive = true;
        }

        public ProcessValue(string category, string name, string value, string appKey, bool haveToArchive)
        {
            this.Category = category;
            this.Name = name;
            this.Value = value;
            this.ApplicationKey = appKey;
            this.Timestamp = DateTime.Now;
            this.HaveToArchive = haveToArchive;
        }

        public string Category { get; set; }

        public string Name { get; set; }

        public DateTime Timestamp { get; set; }

        public string Value { get; set; }

        public bool HaveToArchive { get; set; }

        public string ApplicationKey { get; set; }
    }
}