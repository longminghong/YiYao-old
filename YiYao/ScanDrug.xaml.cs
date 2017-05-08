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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace YiYao
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class ScanDrug : UserControl
    {
        const int Scan = 1;
        const int Push = 2;
        string drugId;
        Storyboard scan2;
        Storyboard scan1;
        Storyboard scan3;
        Storyboard scan4;
        Storyboard scan5;
        int state = Scan;
        string readingImageString;
        List<DrugInfo> drugInfo;
        DrugInfo selectedDrug;
        void InitializingDrugInfo()
        {
            drugInfo = new List<DrugInfo>
            {
                new DrugInfo
                {
                    DrugID = "6958703500010",
                    DrugName = "苯磺酸氨氯地平片",
                    DrugImageSource = "6958703500010.png",
                    DrugProperties = new DrugProperty {
                                DrugUseWay = "请固定在每天的同一时间用适量水送服，服药时间不受食物影响。",
                                DrugUnwell = "-	常见的不良反应有水肿、面部潮红、心悸、嗜睡、头晕、腹痛；\n水肿常常发生于脚踝，如果您不能耐受请就诊咨询，医生会给您调整治疗方案。",
                                DrugNotice = "-	一些药物可能会影响该药的效果，如果您正在服用其它药物，请告知您的医生；\n服药期间需要定期监测血压评估血压控制情况，医生会根据情况调整药物或剂量，家庭自测血压的方法请点击下方相关阅读",
                                DrugStoreAdv = "-	请将药品放在原包装中储存，放置于阳光直射不到的干燥的地方，例如抽屉、储物柜；\n-   为避免孩子误服，请将药品储存在孩子接触不到的地方。",
                     },
                },

                new DrugInfo
                {
                    DrugID = "6958703500096",
                    DrugName = "阿托伐他汀钙片",
                    DrugImageSource = "6958703500096.png",
                    DrugProperties = new DrugProperty {
                                DrugUseWay = "-	请固定在每天的同一时间用适量水送服，服药时间不受食物影响。",
                                DrugUnwell = "-	常见不良反应有腹泻、关节痛、肌痛、尿道感染、四肢疼痛，同时该药可能会引起转氨酶升高，用药前建议您抽血检测，用药时如果出现相关症状建议您就诊咨询；\n-   如果出现肌痛、肌无力等肌肉症状，建议您就诊咨询，医生会抽血检查肌酸激酶进行评估，必要时（若超过正常上限10倍）会停药更换其它药物。",
                                DrugNotice = "-	健康的生活方式是药物治疗的基础，在用药期间请注意保持健康的饮食习惯（详细请点击下方相关阅读）；\n-   治疗期间，需定期抽血测血脂水平评估治疗效果； \n-   在服药期间请不要过量饮酒，也不要大量食用或饮用葡萄柚（西柚），因为它们会影响药效；\n-   该药有较明显的药物间相互作用，服用新的药物（包括非处方药和中草药）前请咨询医生或药师。",
                                DrugStoreAdv = "-	请将药品放在原包装中储存，放置于阳光直射不到的干燥的地方，例如抽屉、储物柜；\n-   为避免孩子误服，请将药品储存在孩子接触不到的地方。\n[相关阅读]：高血脂患者的健康饮食习惯“5.高血脂的健康饮食习惯”",
                     },
                },
                new DrugInfo
                {
                    DrugID = "6950641900181",
                    DrugName = "硫酸氢氯吡格雷片",
                    DrugImageSource = "6950641900181.png",
                    DrugProperties = new DrugProperty {
                                DrugUseWay = "请于饭前用适量水送服，请注意整片吞下，不要掰开、研碎或咀嚼服用。",
                                DrugUnwell = "胃肠道不适包括恶心、呕吐、胃肠道和腹部疼痛；\n该药会使出血的风险增加，因此你需要留意自己是否有出血的表现。如果出现的话，请及时告诉您的医生，他们会帮助您进行分析判断，并决定是否要调整治疗方案。",
                                DrugNotice = "如果您近期要进行拔牙或手术，请告知医生正在服用该药；\n服药时止血时间可能比平长。",
                                DrugStoreAdv = "请将药品放在原包装中储存，放置于阳光直射不到的干燥的地方，例如抽屉、储物柜； ",
                     },
                },
                new DrugInfo
                {
                    DrugID = "6924147659034",
                    DrugName = "阿司匹林肠溶片",
                    DrugImageSource = "6924147659034.png",
                    DrugProperties = new DrugProperty {
                                DrugUseWay = "-	请于饭前用适量水送服，注意整片吞下，不要掰开、研碎或咀嚼服用。",
                                DrugUnwell = "-	胃肠道不适包括恶心、呕吐、胃肠道和腹部疼痛；-   该药会使出血的风险增加，因此您需要留意自己是否有出血的表现（如鼻出血、眼底出血等，详情请点击下方相关阅读）。如果有，请及时告诉您的医生，他们会帮助您进行分析判断，并决定是否要调整治疗方案。 ",
                                DrugNotice = "-	如果您近期要进行拔牙或手术，请告知医生正在服用该药；-   服药时止血时间可能比平时长。",
                                DrugStoreAdv = "-	请将药品放在原包装中储存，放置于阳光直射不到的干燥的地方，例如抽屉、储物柜； -   为避免孩子误服，请将药品储存在孩子接触不到的地方。",
                     },
                },
                new DrugInfo
                {
                    DrugID = "",
                    DrugName = "",
                    DrugImageSource = "",
                    DrugProperties = new DrugProperty {
                                DrugUseWay = "",
                                DrugUnwell = "",
                                DrugNotice = "",
                                DrugStoreAdv = "",
                     },
                },
                new DrugInfo
                {
                    DrugID = "",
                    DrugName = "",
                    DrugImageSource = "",
                    DrugProperties = new DrugProperty {
                                DrugUseWay = "",
                                DrugUnwell = "",
                                DrugNotice = "",
                                DrugStoreAdv = "",
                     },
                },
                new DrugInfo
                {
                    DrugID = "",
                    DrugName = "",
                    DrugImageSource = "",
                    DrugProperties = new DrugProperty {
                                DrugUseWay = "",
                                DrugUnwell = "",
                                DrugNotice = "",
                                DrugStoreAdv = "",
                     },
                },
                new DrugInfo
                {
                    DrugID = "",
                    DrugName = "",
                    DrugImageSource = "",
                    DrugProperties = new DrugProperty {
                                DrugUseWay = "",
                                DrugUnwell = "",
                                DrugNotice = "",
                                DrugStoreAdv = "",
                     },
                },
                new DrugInfo
                {
                    DrugID = "",
                    DrugName = "",
                    DrugImageSource = "",
                    DrugProperties = new DrugProperty {
                                DrugUseWay = "",
                                DrugUnwell = "",
                                DrugNotice = "",
                                DrugStoreAdv = "",
                     },
                },
                new DrugInfo
                {
                    DrugID = "",
                    DrugName = "",
                    DrugImageSource = "",
                    DrugProperties = new DrugProperty {
                                DrugUseWay = "",
                                DrugUnwell = "",
                                DrugNotice = "",
                                DrugStoreAdv = "",
                     },
                },
                
            };
        }

        public ScanDrug()
        {
            drugId = "";

            InitializeComponent();
            
            InitializingDrugInfo();

            this.Loaded += (s, e) =>
            {
                Window.GetWindow(this).TextInput += ScanDrug_TextInput;
                
                FindStoryboard();
            };
            this.Unloaded += (s, e) =>
            {

            };
        }


        private void FindStoryboard()
        {
            scan1 = FindResource("Scan1") as Storyboard;
            scan2 = FindResource("Scan2") as Storyboard;
            scan3 = FindResource("Scan3") as Storyboard;

            scan5 = FindResource("Scan5") as Storyboard;

            scan1.Duration = TimeSpan.Parse("0:0:2.4");

            scan2.BeginTime = TimeSpan.FromSeconds(-3);
            scan2.Duration = TimeSpan.FromSeconds(7);

            scan3.BeginTime = TimeSpan.FromSeconds(-7);
            scan3.Duration = TimeSpan.FromSeconds(9);

            scan5.BeginTime = TimeSpan.FromSeconds(-14);
            scan5.Duration = TimeSpan.FromSeconds(20);

            scan1.Completed += (s, e) =>
            {
                scan2.Begin();
            };
            scan2.Completed += (s, e) =>
            {
                scan2.Begin();
            };
            scan3.Completed += (s, e) =>
            {
                scan5.Begin();
            };
            scan5.Completed += (s, e) =>
            {
                arrow.Visibility = Visibility.Visible;
                reading.Visibility = Visibility.Visible;
            };

            scan1.Begin();
            //scan2.Begin();
            //scan3.Begin();
            // scan5.Begin();
            // scan4.Begin();
        }

        private void ScanDrug_TextInput(object sender, TextCompositionEventArgs e)
        {
            if (e.Text == "\r")
            {
                DoWork();
            }
            else
            {
                drugId += e.Text;
            }
        }


        private void DoWork()
        {
            if (state == Scan)
            {
                
                var drug = drugInfo.FirstOrDefault(d => d.DrugID == drugId);
                selectedDrug = (DrugInfo)drug;
                if (drug != null)
                {
                    try
                    {   
                        BitmapImage bitmap = new BitmapImage();

                        bitmap.BeginInit();

                        String uriString;

                        uriString = "pack://application:,,,/Images/ScanDrug/";
                        uriString += drug.DrugImageSource;

                        //readingImageString = "pack://application:,,,/Images/ScanDrug/r";
                        //readingImageString += drug.DrugImageSource;


                        bitmap.UriSource = new Uri(uriString, UriKind.Absolute);
                        bitmap.EndInit();
                        // small icon
                        yao.Source = bitmap;

                        textBlock1.Text = drug.DrugProperties.DrugUnwell;
                        textBlock1_Copy.Text = "";


                        textBlock7.Text = drug.DrugProperties.DrugUseWay;

                        textBlock5.Text = drug.DrugProperties.DrugNotice;
                        textBlock1_Copy2.Text = "";

                        textBlock4.Text = drug.DrugProperties.DrugStoreAdv;

                        scan2.Stop();
                        scan3.Begin();
                        state++;
                    }
                    catch (Exception)
                    {
    
                    }
                }
            }
            drugId = string.Empty;
        }

        protected override void OnTouchDown(TouchEventArgs e)
        {
            base.OnTouchDown(e);
            PointerDown();
        }


        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            PointerDown();
        }

        private void PointerDown()
        {
            if (state == Scan)
            {
                scan2.Stop();
                scan3.Begin();
                state++;
            }

        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Storyboard show = new Storyboard();

            DoubleAnimation d1 = new DoubleAnimation(1, TimeSpan.FromMilliseconds(300));
            show.Children.Add(d1);
            Storyboard.SetTarget(d1, pop);
            Storyboard.SetTargetProperty(d1, new PropertyPath("Opacity"));

            ObjectAnimationUsingKeyFrames d2 = new ObjectAnimationUsingKeyFrames();
            ObjectKeyFrame k1 = new DiscreteObjectKeyFrame
            {
                KeyTime = TimeSpan.FromMilliseconds(0),
                Value = Visibility.Visible
            };
            d2.KeyFrames.Add(k1);
            show.Children.Add(d2);
            Storyboard.SetTarget(d2, pop);
            Storyboard.SetTargetProperty(d2, new PropertyPath("Visibility"));
            show.Begin();
            try
            {
                BitmapImage bitmap = new BitmapImage();

                bitmap.BeginInit();

                readingImageString = "pack://application:,,,/Images/ScanDrug/r";
                readingImageString += selectedDrug.DrugImageSource;

                bitmap.UriSource = new Uri(readingImageString, UriKind.Absolute);

                bitmap.EndInit();
                readingImage.Source = bitmap;
            }
            catch (Exception)
            {
                
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Storyboard show = new Storyboard();

            DoubleAnimation d1 = new DoubleAnimation(0, TimeSpan.FromMilliseconds(300));
            show.Children.Add(d1);
            Storyboard.SetTarget(d1, pop);
            Storyboard.SetTargetProperty(d1, new PropertyPath("Opacity"));

            ObjectAnimationUsingKeyFrames d2 = new ObjectAnimationUsingKeyFrames();
            ObjectKeyFrame k1 = new DiscreteObjectKeyFrame
            {
                KeyTime = TimeSpan.FromMilliseconds(300),
                Value = Visibility.Collapsed
            };
            d2.KeyFrames.Add(k1);
            show.Children.Add(d2);
            Storyboard.SetTarget(d2, pop);
            Storyboard.SetTargetProperty(d2, new PropertyPath("Visibility"));
            show.Begin();
        }
    }
  

 
    class DrugInfo
    {
        public string DrugID { get; set; }
        public string DrugName { get; set; }
        public string DrugImageSource { get; set; }
        public DrugProperty DrugProperties { get; set; }

    }
    class DrugProperty
    {
        public string DrugUseWay { get; set; }
        public string DrugUnwell { get; set; }
        public string DrugNotice { get; set; }
        public string DrugStoreAdv { get; set; }
    }

}
