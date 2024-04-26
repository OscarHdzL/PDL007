using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Modelos.Request
{

    public class DtParametersrequest
    {
        public int draw { get; set; }
        public List<columns> columns { get; set; }
        public List<order> order { get; set; }
        public int? start { get; set; }
        public int? length { get; set; }
        public search search { get; set; }
    }

    public class order
    {
        public int? column { get; set; }
        public string dir { get; set; }
    }

    public class columns
    {
        public string data { get; set; }
        public string name { get; set; }
        public bool searchable { get; set; }
        public bool orderable { get; set; }
        public search search { get; set; }

    }
    public class search
    {
        public string value { get; set; }
        public bool regex { get; set; }
    }
}
