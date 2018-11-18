using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Remote_Controller
{
    public partial class MainForm : Form
    {
        string sip; //상대 IP
        int sport;  //상대 port
        RemoteCleintForm rcf = null;//원격 호스트 화면(제어 화면)
        VirtualCursorForm vcf = null;//가상 커서
        public MainForm()
        {
            InitializeComponent();
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            vcf = new VirtualCursorForm(); //가상 커서 생성
            rcf = new RemoteCleintForm(); //원격 호스트 화면 폼 생성
            //원격 제어 요청 수신 이벤트 핸들러 등록
            Remote.Singleton.RecvedRCInfo += new RecvRCInfoEventHandler(
                                                     Remote_RecvedRCInfo);
        }
        delegate void Remote_Dele(object sender, RecvRCInfoEventArgs e);
        void Remote_RecvedRCInfo(object sender, RecvRCInfoEventArgs e)
        {
            if (this.InvokeRequired)
            {
                object[] objs = new object[2] { sender, e };
                this.Invoke(new Remote_Dele(Remote_RecvedRCInfo), objs);
            }
            else
            {
                tbox_controller_ip.Text = e.IPAddressStr;// 요청 IP 주소를 표시
                sip = e.IPAddressStr; //요청 IP 주소 설정
                sport = e.Port; //요청 포트 설정
                btn_ok.Enabled = true; //요청 수락 버튼 활성화
            }
        }
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Remote.Singleton.Stop();//원격 호스트 멈춤
            Controller.Singleton.Stop();//원격 컨트롤러 멈춤
            Application.Exit();
        }

        private void btn_setting_Click(object sender, EventArgs e)
        {
            if (tbox_ip.Text == NetworkInfo.DefaultIP) //자신의 아이피와 같을 때
            {
                MessageBox.Show("같은 호스트를 원격 제어할 수 없습니다.");
                tbox_ip.Text = string.Empty;
                return;
            }

            string host_ip = tbox_ip.Text;
            Rectangle rect = Remote.Singleton.Rect;
            Controller.Singleton.Start(host_ip);//컨트롤러 가동

            rcf.ClientSize = new Size(rect.Width - 40, rect.Height - 80);
            rcf.Show();//원격 제어 화면 폼 시각화
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            this.Hide(); //자신의 화면을 숨기기
            Remote.Singleton.RecvEventStart();//원격 제어 이벤트 수신 서버 가동
            timer_send_img.Start(); //이미지 전송 타이머 가동
            vcf.Show(); //가상 커서 시각화
        }
        private void noti_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show(); //자신을 시각화
        }
        private void timer_send_img_Tick(object sender, EventArgs e)
        {
            Rectangle rect = Remote.Singleton.Rect; //사각 영역 구함
            Bitmap bitmap = new Bitmap(rect.Width, rect.Height); //비트맵 생성
            Graphics gp = Graphics.FromImage(bitmap);//비트맵에서 Graphics 개체 생성

            Size size2 = new Size(rect.Width, rect.Height);
            gp.CopyFromScreen(new Point(0, 0), new Point(0, 0), size2);//화면 복사

            gp.Dispose();
            try
            {
                ImageClient ic = new ImageClient(sip, NetworkInfo.ImgPort);
                ic.SendImageAsync(bitmap, null);//이미지를 비동기로 전송

            }
            catch
            {
                timer_send_img.Stop();
                MessageBox.Show("서버에 연결 실패");
                this.Close();
            }
        }
        
    }
   
}
