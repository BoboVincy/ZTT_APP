using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Treeview
{
    public class Items
    {
        public int ItemID { get; set; }      // ID
        public string ItemName { get; set; } // 名称
        public int ItemSteps { get; set; }    // 所属的层级
        public int ItemParent { get; set; }  // 父级的ID


        public bool IsExpanded { get; set; } // 节点是否展开
        public bool IsSelected { get; set; } // 节点是否选中

        public List<Items> Children { get; set; }

        public Items()
        {
            Children = new List<Items>();
        }
    }
}
