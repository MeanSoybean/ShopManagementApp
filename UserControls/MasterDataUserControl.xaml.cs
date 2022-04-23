﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ShopManagementApp.UserControls
{
    /// <summary>
    /// Interaction logic for MasterDataUserControl.xaml
    /// </summary>
    public partial class MasterDataUserControl : UserControl
    {
        public enum MasterDataAction
        {
            AddNewCategory,
            DeleteSelectedCategory,
            AddNewProduct,
            EditSelectedProduct,
            DeleteSelectedProduct,
        }
        public MasterDataUserControl()
        {
            InitializeComponent();
        }
        public void HandleParentEvent(MasterDataAction action)
        {
            switch (action)
            {
                //TODO
                case MasterDataAction.AddNewCategory:
                    break;
                case MasterDataAction.DeleteSelectedCategory:
                    break;
                case MasterDataAction.AddNewProduct:
                    break;
                case MasterDataAction.EditSelectedProduct:
                    break;
                case MasterDataAction.DeleteSelectedProduct:
                    break;
                default:
                    break;
            }
        }
    }
}
