﻿namespace Remote_Controller
{
    partial class MainForm
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tbox_ip = new System.Windows.Forms.TextBox();
            this.tbox_controller_ip = new System.Windows.Forms.TextBox();
            this.btn_setting = new System.Windows.Forms.Button();
            this.btn_ok = new System.Windows.Forms.Button();
            this.timer_send_img = new System.Windows.Forms.Timer(this.components);
            this.noti = new System.Windows.Forms.NotifyIcon(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tbox_ip
            // 
            this.tbox_ip.Location = new System.Drawing.Point(163, 51);
            this.tbox_ip.Name = "tbox_ip";
            this.tbox_ip.Size = new System.Drawing.Size(328, 25);
            this.tbox_ip.TabIndex = 0;
            // 
            // tbox_controller_ip
            // 
            this.tbox_controller_ip.Location = new System.Drawing.Point(163, 107);
            this.tbox_controller_ip.Name = "tbox_controller_ip";
            this.tbox_controller_ip.ReadOnly = true;
            this.tbox_controller_ip.Size = new System.Drawing.Size(328, 25);
            this.tbox_controller_ip.TabIndex = 0;
            this.tbox_controller_ip.TextChanged += new System.EventHandler(this.MainForm_Load);
            // 
            // btn_setting
            // 
            this.btn_setting.Location = new System.Drawing.Point(574, 53);
            this.btn_setting.Name = "btn_setting";
            this.btn_setting.Size = new System.Drawing.Size(148, 23);
            this.btn_setting.TabIndex = 1;
            this.btn_setting.Text = "설정하기";
            this.btn_setting.UseVisualStyleBackColor = true;
            this.btn_setting.Click += new System.EventHandler(this.btn_setting_Click);
            // 
            // btn_ok
            // 
            this.btn_ok.Enabled = false;
            this.btn_ok.Location = new System.Drawing.Point(574, 109);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(148, 23);
            this.btn_ok.TabIndex = 1;
            this.btn_ok.Text = "원격 제어 허용";
            this.btn_ok.UseVisualStyleBackColor = true;
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // timer_send_img
            // 
            this.timer_send_img.Tick += new System.EventHandler(this.timer_send_img_Tick);
            // 
            // noti
            // 
            this.noti.Text = "noti";
            this.noti.Visible = true;
            this.noti.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.noti_MouseDoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(132, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "원격 호스트 주소 :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 113);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(152, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "원격 컨트롤러 주소 : ";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(759, 174);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_ok);
            this.Controls.Add(this.btn_setting);
            this.Controls.Add(this.tbox_controller_ip);
            this.Controls.Add(this.tbox_ip);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbox_ip;               // 원격 제어 대상 ip
        private System.Windows.Forms.TextBox tbox_controller_ip;    // 원격 제어 요청 ip
        private System.Windows.Forms.Button btn_setting;            // 원격 제어 대상에게 요청
        private System.Windows.Forms.Button btn_ok;                 // 원격 제어 허용
        private System.Windows.Forms.Timer timer_send_img;          // 이미지 송신 주기 타이머
        private System.Windows.Forms.NotifyIcon noti;               // 원격 제어를 당할때 noti 아이콘
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

