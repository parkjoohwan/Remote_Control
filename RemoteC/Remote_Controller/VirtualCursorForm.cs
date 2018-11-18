///VirtualCursorForm.cs
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Remote_Controller
{ 
    /// <summary>
    /// 가상 커서
    /// </summary>
    public partial class VirtualCursorForm : Form
    {
        /// <summary>
        /// 생성자
        /// </summary>
        public VirtualCursorForm()
        {
            InitializeComponent();
        }

        private void VirtualCursorForm_Load(object sender, EventArgs e)
        {
            Remote.Singleton.RecvedKMEvent += new RecvKMEEventHandler(Singleton_RecvedKMEvent);
        }

        void Singleton_RecvedKMEvent(object sender, RecvKMEEventArgs e)
        {
            if (e.MT == MsgType.MT_M_MOVE)
            {
                Location = new Point(e.Now.X + 3, e.Now.Y + 3);
            }
        }
    }
}