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
using Microsoft.Practices.ServiceLocation;
using Prism.Events;
using YiYao.Events;

namespace YiYao
{
    /// <summary>
    /// A8.xaml 的交互逻辑
    /// </summary>
    public partial class A8 : UserControl, INavigable
    {
        MTMIssueCollectDTO reciveDTO;

        public A8()
        {
            InitializeComponent();
            baojian.ItemsSource = new string[] { "1.png", "2.png", "3.png", "4.png", "1.png" };
            yaoscrollviewer.ManipulationBoundaryFeedback += Yaoscrollviewer_ManipulationBoundaryFeedback;

        }

        private void Yaoscrollviewer_ManipulationBoundaryFeedback(object sender, ManipulationBoundaryFeedbackEventArgs e)
        {
            e.Handled = true;
        }

        public void Start(object args)
        {
            if (null != args)
            {
                reciveDTO = (MTMIssueCollectDTO)args;

                EventAggregator eventAggragator = ServiceLocator.Current.GetInstance<EventAggregator>();
                eventAggragator.GetEvent<WebSocketEvent>().Subscribe(OnWebSocketEvent);
            }
        }

        public void Stop()
        {
            EventAggregator eventAggragator = ServiceLocator.Current.GetInstance<EventAggregator>();
            eventAggragator.GetEvent<WebSocketEvent>().Unsubscribe(OnWebSocketEvent);
        }
        private void OnWebSocketEvent(object data)
        {
            Console.WriteLine("data A8" +
                "" +
                " ======== OK ");

            reciveDTO = (MTMIssueCollectDTO)data;

            Uri riskImageUri = null;
            String ristValueString = "";
            switch (reciveDTO.risklevel) {
                case 1:
                    {
                        riskImageUri = new Uri("pack://application:,,,/人体绿色.png");
                        ristValueString = "健康";
                    }
                    break;
                case 2:
                    {
                        riskImageUri = new Uri("pack://application:,,,/人体黄色.png");
                        ristValueString = "中危";
                    }
                    break;
                case 3:
                    {
                        riskImageUri = new Uri("pack://application:,,,/人体红色.png");
                        ristValueString = "高危";
                    }

                    break;
            }
            textblock_risk.Text = ristValueString;
            try
            {
                BitmapImage bi3 = new BitmapImage();
                bi3.BeginInit();
                bi3.UriSource = riskImageUri;
                bi3.EndInit();
                image_risk.Source = bi3;

                textblock_heigh.Text = reciveDTO.measuredata.height.ToString();
                textblock_weigh.Text = reciveDTO.measuredata.weight.ToString();
                textblock_bmi.Text = reciveDTO.measuredata.BMI;
                textblock_waist.Text = reciveDTO.measuredata.waist.ToString();
            }
            catch (Exception)
            {

                
            }
            

            //int w = riskImage.PixelHeight;
        }
        private void jiantou1_png_MouseDown(object sender, MouseButtonEventArgs e)
        {
            (Parent as NavigationManager).GoToPage(typeof(ShoppingCar));
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            (Parent as NavigationManager).GoToPage(typeof(Dashboard));
        }
    }
}
