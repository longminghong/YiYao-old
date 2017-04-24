using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Text;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

public enum MEMBERType
{
    MEMBBASIC = 1,//新建会员时，采集基本信息
    MEMBHEALTH = 2,//新建会员时，采集健康信息
    MEMBDRUG = 3,//新建会员时，采集用药信息
    MEMBDISEASE = 4,//新建会员时，采集疾病风险信息
    MEMBQR = 5,//供会员关注和绑定的二维码
    MEMBRISK = 6,//会员评估结果数据
    MEMBRECOMM = 7,//推荐用药数据
    MEMBCART = 8,//药品购物车
    MEMBPLAN = 9,//会员用药计划
    MEMERROR = 999,
}

namespace WebService
{
    
    public class WebSocketSingleton
    {
        public delegate void SocketPageCommandHandleEvent(MEMBERType sender);
        public SocketPageCommandHandleEvent pageCommandHandle;
        
        private static WebSocketSingleton instance;
        private static object _lock = new object();

        private WebSocketSingleton()
        {
            
        }

        public void SetCallback(SocketPageCommandHandleEvent callback)
        {
            pageCommandHandle = callback;
        }
        
        public static WebSocketSingleton GetInstance()
        {
            if (instance == null)
            {
                lock (_lock)
                {
                    if (instance == null)
                    {
                        instance = new WebSocketSingleton();
                    }
                }
            }
            return instance;
        }
        private static CamelCasePropertyNamesContractResolver s_defaultResolver = new CamelCasePropertyNamesContractResolver();
        private static JsonSerializerSettings s_settings = new JsonSerializerSettings()
        {
            DateFormatHandling = DateFormatHandling.IsoDateFormat,
            NullValueHandling = NullValueHandling.Ignore,
            ContractResolver = s_defaultResolver
        };

        /**
         * 
         * Mqtt call back
         * 
         * **/
        static void client_MqttMsgSubscribed(object sender, MqttMsgSubscribedEventArgs e)
        {
            Console.WriteLine("Subscribed for id = " + e.MessageId);
        }
        // 接受消息后的操作
         void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            string responseContent = null;

            //Console.WriteLine("Received = " + Encoding.UTF8.GetString(e.Message) + " on topic " + e.Topic);
            byte[] messageContent = e.Message;
            //responseContent = System.Text.Encoding.Default.GetString(messageContent);
            responseContent = Encoding.UTF8.GetString(e.Message);
            Console.WriteLine("Received = " + Encoding.UTF8.GetString(e.Message) + " on topic " + e.Topic);
            //DataResult < MemberInfo >
            try
            {
                if (!string.IsNullOrWhiteSpace(responseContent))
                {

                    //JObject jObject = JObject.Parse("{'ID':'001','Mark':'Hello Word'}");
                    
                    JObject jObject = JObject.Parse(responseContent);

                    JToken jTypeToken = jObject.GetValue("type");
                    JToken jDataToken = jObject.GetValue("data");

                    Console.WriteLine("json finish"+ jTypeToken.ToString());

                    MEMBERType pageType;

                    pageType = deserializeDataType(jTypeToken.ToString());

                    Console.WriteLine(jDataToken.ToString());

                    pageCommandHandle.Invoke(pageType);

                    //if (pageType == MEMBERType.MEMBBASIC)
                    //{
                    //   var result =  jObject.ToObject<MTMWebServerDataResult<MTMMedPlanDTO>>();
                    //    commandHandle.Invoke(null);
                    //}

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("webSocket:" + ex.InnerException);
            }
            
        }
        // 发布消息后的操作
         void client_MqttMsgPublished(object sender, MqttMsgPublishedEventArgs e)
        {
            Console.WriteLine("MessageId = " + e.MessageId + " Published = " + e.IsPublished);
        }
        // 关闭连接后的操作
         void client_ConnectionClosed(object sender, EventArgs e)
        {
            Console.WriteLine("connect closed");
        }
        // 取消sub后的操作
         void client_MqttMsgUnsubscribed(object sender, MqttMsgUnsubscribedEventArgs e)
        {
            Console.WriteLine("connect closed");
        }

        public void start()
        {
            Console.WriteLine("web socket run.");
            
            string enpoint = "mqf-bym08ztgwf.mqtt.aliyuncs.com";
            int port = 80;
            string user = "LTAIyNRr5QPLury7";
            string pwd = "fxKK7bAPc7J15qwQ/YoNocsGBso=";
            string clientid = "GID_MTM_WEB_TEST_MQTT@@@NEWDAY";//Guid.NewGuid().ToString(); // 获取一个独一无二的id
            string[] topic = new string[] { "MTM_WEB_TEST/pypub" };

            byte[] qosLevels = new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE }; // qos=1

            MqttClient client = new MqttClient(enpoint);
            //client.ProtocolVersion = MqttProtocolVersion.Version_3_1;
            byte code = client.Connect(clientid,
                                        user,
                                        pwd,
                                        true, // cleanSession
                                        60); // keepAlivePeriod
            Console.WriteLine(code);
            Console.WriteLine(client.IsConnected);
            client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;
            client.MqttMsgSubscribed += client_MqttMsgSubscribed;
            client.MqttMsgUnsubscribed += client_MqttMsgUnsubscribed;
            client.MqttMsgPublished += client_MqttMsgPublished;
            client.ConnectionClosed += client_ConnectionClosed;
            client.Subscribe(topic, qosLevels); // sub 的qos=1
        }

        static MEMBERType deserializeDataType(String responseType) {

            MEMBERType memberType;
            memberType = MEMBERType.MEMERROR;
            if (!string.IsNullOrWhiteSpace(responseType))
            {
                if (responseType.Equals("MEMBER-BASIC"))
                {
                    memberType = MEMBERType.MEMBBASIC;
                }
                else if (responseType.Equals("MEMBER-HEALTH"))
                {
                    memberType = MEMBERType.MEMBHEALTH;
                }
                else if (responseType.Equals("MEMBER-DRUG"))
                {
                    memberType = MEMBERType.MEMBDRUG;
                }
                else if (responseType.Equals("MEMBER-DISEASE"))
                {
                    memberType = MEMBERType.MEMBDISEASE;
                }
                else if (responseType.Equals("MEMBER-QR"))
                {
                    memberType = MEMBERType.MEMBQR;
                }
                else if (responseType.Equals("MEMBER-RISK"))
                {
                    memberType = MEMBERType.MEMBRISK;
                }
                else if (responseType.Equals("MEMBER-RECOMM"))
                {
                    memberType = MEMBERType.MEMBRECOMM;
                }
                else if (responseType.Equals("MEMBER-CART"))
                {
                    memberType = MEMBERType.MEMBCART;
                }
                else if (responseType.Equals("MEMBER-PLAN"))
                {
                    memberType = MEMBERType.MEMBPLAN;
                }
                
            }
                return memberType;
        }
    }
}
