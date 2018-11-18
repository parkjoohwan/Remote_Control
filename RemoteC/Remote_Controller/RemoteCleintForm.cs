//RemoteCleintForm.cs
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Remote_Controller
{
    /// <summary>
    /// 원격 클라이언트 폼
    /// </summary>
    public partial class RemoteCleintForm : Form
    {
        bool check;//이미지 수신 여부
        Size csize; //클라이언트 폼 크기
        SendEventClient EventSC
        {
            get
            {
                return Controller.Singleton.SendEventClient;
            }
        }
        /// <summary>
        /// 생성자
        /// </summary>
        public RemoteCleintForm()
        {
            InitializeComponent();
        }
        private void RemoteCleintForm_Load(object sender, EventArgs e)
        {
            Controller.Singleton.RecvedImage += new RecvImageEventHandler(Singleton_RecvImageEventHandler);
        }

        void Singleton_RecvImageEventHandler(object sender, RecvImageEventArgs e)
        {
            if (check == false)
            {
                Controller.Singleton.StartEventClient();
                check = true;

                csize = e.Image.Size;
            }

            pbox_remote.Image = e.Image;
        }

        private void pbox_remote__MouseMove(object sender, MouseEventArgs e)
        {
            if (check == true)
            {
                Point pt = ConvertPoint(e.X, e.Y);
                EventSC.SendMouseMove(pt.X, pt.Y);
            }
        }

        private Point ConvertPoint(int x, int y)
        {
            int nx = csize.Width * x / pbox_remote.Width;
            int ny = csize.Height * y / pbox_remote.Height;
            return new Point(nx, ny);
        }
        private void pbox_remote_MouseDown(object sender, MouseEventArgs e)
        {
            if (check == true)
            {
                Text = e.Location.ToString();
                EventSC.SendMouseDown(e.Button);
            }
        }

        private void pbox_remote_MouseUp(object sender, MouseEventArgs e)
        {
            if (check == true)
            {
                EventSC.SendMouseUp(e.Button);
            }
        }

        private void RemoteCleintForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (check == true)
            {
                EventSC.SendKeyDown(e.KeyValue);
            }
        }

        private void RemoteCleintForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (check == true)
            {
                EventSC.SendKeyUp(e.KeyValue);
            }

        }
    }
}
