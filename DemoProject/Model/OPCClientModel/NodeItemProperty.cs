using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.OPCClientModel
{
    public class NodeItemProperty
    {
        public string Channel
        {
            get;set;
        }

        public string Device
        {
            get;set;
        }

        public string Tag
        {
            get;set;
        }

        public string Type
        {
            get;set;
        }

        public List<NodeItemProperty> Children
        {
            get;set;
        }

        public NodeItemProperty()
        {
            Children = new List<NodeItemProperty>();
        }
    }
}
