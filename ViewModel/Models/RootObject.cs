using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeWatcher.Models
{
    public class RootObject
    {
        public List<string> fields { get; set; }
        public List<BikeStation> values { get; set; }
        public int nb_results { get; set; }
        public string table_href { get; set; }
        public string layer_name { get; set; }
    }
}
