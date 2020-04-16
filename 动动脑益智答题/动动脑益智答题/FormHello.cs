using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 动动脑益智答题
{
    public partial class FormHello : Form
    {
        public FormHello()
        {
            InitializeComponent();
        }

        //初始窗体计时器，时间到则进入登录窗体
        private void Go_Tick(object sender, EventArgs e)
        {
            //打开背景图片窗体
            FormWallpaper FormWallpaper = new FormWallpaper();
            FormWallpaper.Show();
            //打开登录窗体
            FormLogin FormLogin = new FormLogin();
            FormLogin.Show();
            //隐藏当前窗体
            this.Hide();
            //停止事件生成
            Go.Enabled = false;
        }

        private void FormHello_Load(object sender, EventArgs e)
        {

        }
    }
}
