//NetworkInfo.cs
using System.Net;
using System.Net.Sockets;

namespace Remote_Controller
{
    /// <summary>
    /// 네트워크 정보 클래스 - 정적 클래스
    /// </summary>
    public static class NetworkInfo
    {
        /// <summary>
        /// 이미지 서버 포트 - 가져오기
        /// </summary>
        public static short ImgPort
        {
            get
            {
                return 20001;
            }
        }
        /// <summary>
        /// 원격 제어 요청 포트 - 가져오기
        /// </summary>
        public static short SetupPort
        {
            get
            {
                return 20002;
            }
        }
        /// <summary>
        /// 이벤트 서버 포트
        /// </summary>
        public static short EventPort
        {
            get
            {
                return 20003;
            }
        }

        /// <summary>
        /// 디폴트 IP 주소 문자열 - 가져오기
        /// </summary>
        public static string DefaultIP
        {
            get
            {
                //호스트 이름 구하기
                string host_name = Dns.GetHostName();
                //호스트 엔트리 구하기
                IPHostEntry host_entry = Dns.GetHostEntry(host_name);
                //호스트 주소 목록 반복
                foreach (IPAddress ipaddr in host_entry.AddressList)
                {
                    //주소 체계가 InterNetwork일 때
                    if (ipaddr.AddressFamily == AddressFamily.InterNetwork)
                    {
                        return ipaddr.ToString();//IP 주소 문자열 반환
                    }
                }
                return string.Empty;//빈 문자열 반환
            }
        }
    }

    public class Controller
    {
        static Controller singleton;

        public static Controller Singleton
        {
            get
            {
                return singleton;
            }
        }

        static Controller()//정적 생성자
        {
            singleton = new Controller();//단일체 생성
        }
        private Controller()//접근 가시성이 private인 기본 생성자
        {
        }
        ImageServer img_sever = null; //이미지 수신 서버
        SendEventClient sce = null; //키보드, 마우스 제어 이벤트 전송 클라이언트
        public event RecvImageEventHandler RecvedImage = null;//이미지 수신 이벤트
        string host_ip; //원격 제어 호스트 IP 문자열
        public SendEventClient SendEventClient //이벤트 전송 클라이언트 접근자
        {
            get
            {
                return sce;
            }
        }
        public string MyIP//로컬 IP문자열 접근자
        {
            get
            {
                return NetworkInfo.DefaultIP;
            }
        }
        public void Start(string host_ip)
        {
            this.host_ip = host_ip;
            img_sever = new ImageServer(MyIP, NetworkInfo.ImgPort);//이미지 서버 가동
                                                                   //구독 요청
            img_sever.RecvedImage += new RecvImageEventHandler(img_sever_RecvedImage);
            SetupClient.Setup(host_ip, NetworkInfo.SetupPort); //원격 제어 요청
        }
        //이미지 수신 이벤트 핸들러
        void img_sever_RecvedImage(object sender, RecvImageEventArgs e)
        {
            if (RecvedImage != null)//이미지 수신 이벤트 구독자가 있을 때
            {
                RecvedImage(this, e);//이미지 수신 이벤트 게시(By Pass)
            }
        }
        public void StartEventClient()
        {
            sce = new SendEventClient(host_ip, NetworkInfo.EventPort); //이벤트 송신 클라이언트 개체 생성
        }
        public void Stop()//원격 컨트롤러 멈춤
        {
            if (img_sever != null)
            {
                img_sever.Close();//이미지 서버 닫기
                img_sever = null;
            }
        }
    }
}