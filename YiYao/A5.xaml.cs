﻿using Microsoft.Practices.ServiceLocation;
using Prism.Events;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WebService;
using YiYao.Events;
using Microsoft.Practices.ServiceLocation;
using Prism.Events;
using System.Windows.Media.Animation;

namespace YiYao
{
    /// <summary>
    /// A5.xaml 的交互逻辑
    /// </summary>
    public partial class A5 : UserControl, INavigable
    {
        private MTMHealthCollectDTO healthDTO;

        public A5()
        {
            InitializeComponent();

            this.Loaded += (s, e) =>
            {
                var loadingAnimation = FindResource("A5Storyboard1") as Storyboard;
                loadingAnimation.Completed += (ss, ee) =>
                {
                    
                };
                loadingAnimation.Begin();
            };
        }

        public void Start(object args)
        {
            if (null != args)
            {   
                //customInfo = (MTMCustInfo)args;
                // to do 数据绑定
                healthDTO = (MTMHealthCollectDTO)args;
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
            Console.WriteLine("A5 ======== OK ");

            if (null != data) {
                healthDTO = data as MTMHealthCollectDTO;
                String foodString = "";
                if (1 == healthDTO.eatingpreference)
                    foodString  += "饮食偏好：高盐 ";
                else if (0 == healthDTO.eatingpreference)
                    foodString += "饮食偏好：低盐 ";
                else
                    foodString  += "饮食偏好：未知 ";

                foodString += "                ";
                
                if (String.Equals("no", healthDTO.smoking))
                    foodString += "抽烟：否";
                else if (String.Equals("yes", healthDTO.smoking))
                    foodString += "抽烟：是";
                else
                    foodString += "抽烟：未知";

                foodString += "                       ";
                   
                if (String.Equals("no", healthDTO.drinking))
                    foodString+= "酗酒：否";
                else if (String.Equals("yes", healthDTO.drinking))
                    foodString+= "酗酒：是";
                else
                    foodString += "酗酒：未知";

                textBlock2.Text = foodString;

                String bodyString = "";
                
                bodyString += "身高:" + healthDTO.height;
                bodyString += "             ";
                bodyString += "体重:" + healthDTO.weight;
                bodyString += "             ";
                bodyString += "腹围:" + healthDTO.waist;

                textBlock3.Text = bodyString;

                //heartrate.Text = "心率(次/分):" + healthDTO.heartrate;

                String pressure = "";

                pressure += "收缩压:" + healthDTO.systolicpressure;
                pressure += "             ";
                pressure += "舒张压:" + healthDTO.diastolicpressure;

                textBlock5.Text = pressure;

                String blood = "";

                blood += "空腹血糖:" + healthDTO.fastsugar;
                blood += "             ";
                blood += "随机血糖:" + healthDTO.randomsugar;
                blood += "             ";
                blood += "糖化血红蛋白:" + healthDTO.HbA1c;
                textBlock7.Text = blood;

                String liveblood = "";
                liveblood += "总胆固醇:" + healthDTO.cholesterol;
                liveblood += "             ";
                liveblood += "甘油三脂:" + healthDTO.triglyceride;
                liveblood += "             ";
                liveblood += "LDL-C:" + healthDTO.ldl;
                liveblood += "         ";
                 
                textBlock9.Text = liveblood;

                textBlock10.Text = "HDL-C:" + healthDTO.hdl;
                //===========================================

                //if (1 == healthDTO.eatingpreference)
                //    eatingpreference.Text = "饮食偏好：高盐";
                //else if (0 == healthDTO.eatingpreference)
                //    eatingpreference.Text = "饮食偏好：低盐";
                //else
                //    eatingpreference.Text = "饮食偏好：未知";

                //if (String.Equals("no", healthDTO.smoking))
                //    smoking.Text = "是否抽烟：否";
                //else if (String.Equals("yes", healthDTO.smoking))
                //    smoking.Text = "是否抽烟：是";
                //else
                //    smoking.Text = "是否抽烟：未知";

                //if (String.Equals("no", healthDTO.drinking))
                //    drinking.Text = "是否酗酒:否";
                //else if (String.Equals("yes", healthDTO.drinking))
                //    drinking.Text = "是否酗酒:是";
                //else
                //    drinking.Text = "是否酗酒:未知";
                //height.Text = "身高(cm):"+healthDTO.height;
                //weight.Text = "体重(KG):"+healthDTO.weight;
                //waist.Text = "腹围(cm):"+healthDTO.waist;
                //heartrate.Text = "心率(次/分):" + healthDTO.heartrate;
                //systolicpressure.Text = "收缩压(mmol/L):" + healthDTO.systolicpressure;
                //diastolicpressure.Text = "舒张压(mmol/L):" + healthDTO.diastolicpressure;
                //fastsugar.Text = "空腹血糖(mmol/L):" + healthDTO.fastsugar;
                //randomsugar.Text = "随机血糖(mmol/L):" + healthDTO.randomsugar;
                //HbA1c.Text = "糖化血红蛋白(mmol/L):" + healthDTO.HbA1c;
                //cholesterol.Text = "总胆固醇:" + healthDTO.cholesterol;
                //triglyceride.Text = "甘油三脂:" + healthDTO.triglyceride;
                //ldl.Text = "LDL-C:" + healthDTO.ldl;
                //hdl.Text = "HDL-C:" + healthDTO.hdl;
            }
        }

        private void jiantou1_png_MouseDown(object sender, MouseButtonEventArgs e)
        {
            (Parent as NavigationManager).GoToPage(typeof(A6));
        }

    }
}
