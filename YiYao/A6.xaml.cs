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
using Microsoft.Practices.ServiceLocation;
using Prism.Events;
using YiYao.Events;

namespace YiYao
{
    /// <summary>
    /// A6.xaml 的交互逻辑
    /// </summary>
    public partial class A6 : UserControl, INavigable
    {
        MTMMedCollectDTO medCollectDTO;
        public A6()
        {
            InitializeComponent();
        }
        public void Start(object args)
        {
            if (null != args)
            {

                //customInfo = (MTMCustInfo)args;
                // to do 数据绑定
                medCollectDTO = (MTMMedCollectDTO)args;

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
            Console.WriteLine("data ======== OK ");

            if(String.Equals("yes", medCollectDTO.isallergy))
                isallergy.Text = "是否药物过敏:是";
            else
                isallergy.Text = "是否药物过敏:否";
            allergicdrug.Text = "过敏药物:"+ medCollectDTO.allergicdrug;

            systolicpressure.Text = "收缩压(mmHg):"+ medCollectDTO.systolicpressure;
            diastolicpressure.Text = "舒张压(mmHg):"+ medCollectDTO.diastolicpressure;

            if (String.Equals("law-y", medCollectDTO.hypotensor))
            {
                hypotensor.Text = "是否已服用降压药：是";
            }
            else {
                hypotensor.Text = "是否已服用降压药：否";
            }
        }
        private void jiantou1_png_MouseDown(object sender, MouseButtonEventArgs e)
        {
            (Parent as NavigationManager).GoToPage(typeof(A7));
        }
    }
}
