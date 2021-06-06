using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenbrugerApp
{
    public class SkraldData // Martin
    {
        public string SkraldeID { get; set; }
        public string Mængde { get; set; }
        public string Måleenhed { get; set; }
        public string Kategori { get; set; }
        public string Beskrivelse { get; set; }
        public string Ansvarlig { get; set; }
        public string CVR { get; set; }
        public DateTime Tid { get; set; }
    }
}
