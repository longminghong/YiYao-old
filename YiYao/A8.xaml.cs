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
using WebService;

namespace YiYao
{
    /// <summary>
    /// A8.xaml 的交互逻辑
    /// </summary>
    public partial class A8 : UserControl, INavigable
    {
        public A8()
        {
            InitializeComponent();
        }

        public void Start(object args)
        {

        }

        public void Stop()
        {

        }

        private void jiantou1_png_MouseDown(object sender, MouseButtonEventArgs e)
        {
            (Parent as NavigationManager).GoToPage(typeof(ShoppingCar));
        }
    }
}
