using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Model.Treeview
{
    public class StepList
    {
        public StepList()
        {
            ZeroStepList = new List<Items>();
            FirstStepList = new List<Items>();
            SecondStepList = new List<Items>();
            ThirdStepList = new List<Items>();
        }
        public List<Items> ZeroStepList
        {
            get;set;
        }

        public List<Items> FirstStepList
        {
            get;set;
        }

        public List<Items> SecondStepList
        {
            get;set;
        }

        public List<Items> ThirdStepList
        {
            get;set;
        }
    }


}
