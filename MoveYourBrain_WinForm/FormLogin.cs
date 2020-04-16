using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.Runtime.InteropServices;
using System.Data.SqlClient;

namespace 动动脑益智答题
{
    public partial class FormLogin : Form
    {
        //保存临时用户名信息
        public string uname = string.Empty;

        //窗体弹出或消失效果
        [DllImport("user32.dll", EntryPoint = "AnimateWindow")]
        private static extern bool AnimateWindow(IntPtr handle, int ms, int flags);
        public const Int32 AW_HOR_POSITIVE = 0x00000001;
        public const Int32 AW_HOR_NEGATIVE = 0x00000002;
        public const Int32 AW_VER_POSITIVE = 0x00000004;
        public const Int32 AW_VER_NEGATIVE = 0x00000008;
        public const Int32 AW_CENTER = 0x00000010;
        public const Int32 AW_HIDE = 0x00010000;
        public const Int32 AW_ACTIVATE = 0x00020000;
        public const Int32 AW_SLIDE = 0x00040000;
        public const Int32 AW_BLEND = 0x00080000;

        public FormLogin()
        {
            InitializeComponent();

            //窗体弹出效果
            AnimateWindow(this.Handle, 300, AW_CENTER);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //播放音效
            SoundPlayer play = new SoundPlayer(Resource1.click);
            play.Play();

            //如果用户名与密码输入为空时则进入游客模式
            if (textBox1.Text == "" && textBox2.Text == "")
            {
                MessageBox.Show("您已进入游客模式，请尽情享受游戏吧 *^_^* ~~ \n\n(注意：该模式下不保存用户信息数据，如等级，经验值等)", "游客模式", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                //打开游戏主菜单窗口
                FormMain FormMain = new FormMain();
                FormMain.Show();
                this.Close();
            }
            else
            {
                //读取数据库并判断账号密码是否正确
                string key = "Data Source=.;Initial Catalog=DongDongNao;Integrated Security=True";
                SqlConnection conn = new SqlConnection(key);
                conn.Open();
                string strSql = " SELECT name,pwd FROM DdnAdmin";
                SqlCommand cmd = new SqlCommand(strSql, conn);
                SqlDataReader read = cmd.ExecuteReader();

                if (read.Read())
                {
                    MessageBox.Show("用户验证成功！！请尽情享受游戏吧 *^_^* ~~", "登录成功", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    //动态关闭窗体
                    AnimateWindow(this.Handle, 800, AW_SLIDE + AW_HIDE + AW_CENTER);
                    //打开游戏主菜单窗口
                    FormMain FormMain = new FormMain();
                    //传递临时用户名
                    FormMain.name = read["name"].ToString();
                    FormMain.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("请检查以下错误\n1.用户名或密码输入错误2.该账户不存在,注册一个账户试试。", "登录失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                conn.Close();
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {
            //判断用户是否退出程序
            DialogResult exit = MessageBox.Show("是否退出程序？？","提示",MessageBoxButtons.OKCancel,MessageBoxIcon.Asterisk);
            if (exit == DialogResult.OK)
            {
                //动态关闭窗体
                AnimateWindow(this.Handle, 800, AW_SLIDE + AW_HIDE + AW_CENTER);
                Application.Exit();
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("开发中，敬请期待~~","提示",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("开发中，敬请期待~~\n请使用以下测试账号登录：\n用户名：Admin\n密码：123456", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void label14_Click(object sender, EventArgs e)
        {
            MessageBox.Show("1.用户名与密码输入为空时点击登录则以游客模式登录游戏\n(注意：该模式下不保存用户信息数据，如等级，经验值等)\n\n*部分功能可能点击无效，敬请期待后续版本更新...","帮助信息",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void FormLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
