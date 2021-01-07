using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Model.Treeview
{
    public class Item
    {
        private int _ItemID;
        public int ItemID
        {
            get
            {
                return _ItemID;
            }
            set {
                _ItemID = value;
                OnPropertyChanged(new PropertyChangedEventArgs("ItemID"));
            }
        }

        private string _ItemName;
        public string ItemName
        {
            get
            {
                return _ItemName;
            }
            set
            {
                _ItemName = value;
                OnPropertyChanged(new PropertyChangedEventArgs("ItemName"));
            }
        }

        private int _ItemStep;
        public int ItemStep
        {
            get
            {
                return _ItemStep;
            }
            set
            {
                _ItemStep = value;
                OnPropertyChanged(new PropertyChangedEventArgs("ItemStep"));
            }
        }

        private int _ItemParent;
        public int ItemParent
        {
            get
            {
                return _ItemParent;
            }
            set
            {
                _ItemParent = value;
                OnPropertyChanged(new PropertyChangedEventArgs("ItemParent"));
            }
        }


        private ObservableCollection<Item> _Children = new ObservableCollection<Item>();
        public ObservableCollection<Item> Children
        {
            get {
                return _Children;
            }
        }


        private bool _IsExpanded;
        public bool IsExpanded
        {
            get
            {
                return _IsExpanded;
            }
            set
            {
                _IsExpanded = value;
                OnPropertyChanged(new PropertyChangedEventArgs("IsExpanded"));
            }
        }


        private bool _IsSelected;
        public bool IsSelected
        {
            get
            {
                return _IsSelected;
            }
            set
            {
                _IsSelected = value;
                OnPropertyChanged(new PropertyChangedEventArgs("IsSelected"));
            }
        }

        #region 属性变化通知事件

        /// <summary>
        /// 属性变化通知事件
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 属性变化通知
        /// </summary>
        /// <param name="e"></param>
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, e);
            }
        }

        #endregion
    }
}
