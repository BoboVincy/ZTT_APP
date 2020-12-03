using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Panuon.UI.Silver;
using Model.Treeview;
using System.Collections.ObjectModel;

namespace View.TreeView
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class Window1 : WindowX
    {
        Item UIProperty;
        StepList itemViewModel = new StepList();
        public Window1()
        {
            InitializeComponent();
            ShowTreeView();

        }

        public ObservableCollection<Item> ItemList = new ObservableCollection<Item>();

        // 构造轮廓选择的树形结构
        private void InitTreeView()
        {
            // 添加树形结构
            Item item = GetTreeData();
            ItemList.Clear();
            ItemList.Add(item);
        }


        private void ShowTreeView()
        {
            List<Items> ItemList = new List<Items>();
            List<Items> NodeList = new List<Items>();
            List<Items> Node1List = new List<Items>();
            NodeList.Add(new Items
            {
                ItemID = 2,
                ItemName = "2w",
            });
            Node1List.Add(new Items
            {
                ItemID = 2,
                ItemName = "2w",
                Children = NodeList
            });

            ItemList.Add(new Items
            {
                ItemID = 1,
                ItemName = "11",
                Children = NodeList
            });

            ItemList.Add(new Items
            {
                ItemID = 1,
                ItemName = "11",
                Children = Node1List
            });
            ItemList.Add(new Items
            {
                ItemID = 1,
                ItemName = "11"
            });
            ItemList.Add(new Items
            {
                ItemID = 1,
                ItemName = "11"
            });

            TreeView.ItemsSource = ItemList;

        }
        // 构造树形结构
        private Item GetTreeData()
        {
            /*
             *  rootItem
             *      |----zeroTreeItem
             *                 |----firstTreeItem
             *                            |----secondTreeItem
             */

            // 根节点
            Item rootItem = new Item();
            rootItem.ItemID = -1;
            rootItem.ItemName = " -- 请选择轮廓 -- ";
            rootItem.ItemStep = -1;
            rootItem.ItemParent = -1;
            rootItem.IsExpanded = true; // 根节点默认展开
            rootItem.IsSelected = false;

            for (int i = 0; i < itemViewModel.ZeroStepList.Count; i++) // 零级分类
            {
                Items zeroStepItem = itemViewModel.ZeroStepList[i];
                Item zeroTreeItem = new Item();
                zeroTreeItem.ItemID = zeroStepItem.ItemID;
                zeroTreeItem.ItemName = zeroStepItem.ItemName;
                zeroTreeItem.ItemStep = zeroStepItem.ItemSteps;
                zeroTreeItem.ItemParent = zeroStepItem.ItemParent;
                if (i == 0)
                {
                    zeroTreeItem.IsExpanded = true; // 只有需要默认选中的第一个零级分类是展开的
                }
                zeroTreeItem.IsSelected = false;
                rootItem.Children.Add(zeroTreeItem); // 零级节点无条件加入根节点

                for (int j = 0; j < itemViewModel.FirstStepList.Count; j++) // 一级分类
                {
                    Items firstStepItem = itemViewModel.FirstStepList[j];
                    if (firstStepItem.ItemParent == zeroStepItem.ItemID) //零级节点添加一级节点
                    {
                        Item firstTreeItem = new Item();
                        firstTreeItem.ItemID = firstStepItem.ItemID;
                        firstTreeItem.ItemName = firstStepItem.ItemName;
                        firstTreeItem.ItemStep = firstStepItem.ItemSteps;
                        firstTreeItem.ItemParent = firstStepItem.ItemParent;
                        if (j == 0)
                        {
                            firstTreeItem.IsExpanded = true; // 只有需要默认选中的第一个一级分类是展开的
                        }
                        firstTreeItem.IsSelected = false;
                        zeroTreeItem.Children.Add(firstTreeItem);

                        for (int k = 0; k < itemViewModel.SecondStepList.Count; k++) // 二级分类
                        {
                            Items secondStepItem = itemViewModel.SecondStepList[k];
                            if (secondStepItem.ItemParent == firstStepItem.ItemID) // 一级节点添加二级节点
                            {
                                Item secondTreeItem = new Item();
                                secondTreeItem.ItemID = secondStepItem.ItemID;
                                secondTreeItem.ItemName = secondStepItem.ItemName;
                                secondTreeItem.ItemStep = secondStepItem.ItemSteps;
                                secondTreeItem.ItemParent = secondStepItem.ItemParent;
                                if (k == 0)
                                {
                                    // 默认选中第一个二级分类
                                    secondTreeItem.IsExpanded = true; // 已经没有下一级了，这个属性无所谓
                                    secondTreeItem.IsSelected = true;
                                }
                                firstTreeItem.Children.Add(secondTreeItem);
                            }
                        }
                    }
                }
            }

            return rootItem;
        }
    }

}
