using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace 动动脑益智答题
{
    public partial class FormMain : Form
    {
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

        //记录临时用户名
        public string name;
        public int tiMu;

        //总对题记录,总错题记录
        public int yes = 0;
        public int no = 0;

        //鼠标移动音效
        SoundPlayer playMove = new SoundPlayer(Resource1.move);
        //鼠标点击音效
        SoundPlayer playClick = new SoundPlayer(Resource1.click);
        public FormMain()
        {
            InitializeComponent();

            //窗体弹出效果
            AnimateWindow(this.Handle, 300, AW_CENTER);
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

       

        private void toolStripLabel4_Click(object sender, EventArgs e)
        {

        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            //如果用户在登陆窗体未输入用户名，控件信息则显示临时游戏数据
            FormLogin FormLogin = new FormLogin();
            FormLogin.Close();
            if (FormLogin.uname == null)
            {
                textBox6.Text = yes.ToString();
                textBox7.Text = no.ToString();
            }
            else
            {
                //读取数据库并填充数据
                string key = "Data Source=.;Initial Catalog=DongDongNao;Integrated Security=True";
                SqlConnection conn = new SqlConnection(key);
                conn.Open();
                string strSql = " SELECT * FROM DdnAdmin WHERE name='" + name + "' ";
                SqlCommand cmd = new SqlCommand(strSql, conn);
                SqlDataReader read = cmd.ExecuteReader();

                while (read.Read())
                {
                    string name = read["name"].ToString();
                    textBox1.Text = name;
                    string grade = read["grade"].ToString();
                    textBox2.Text = grade;
                    string UpEmpiricalValue = read["UpEmpiricalValue"].ToString();
                    textBox3.Text = UpEmpiricalValue;
                    string ThisEmpiricalValue = read["ThisEmpiricalValue"].ToString();
                    textBox4.Text = ThisEmpiricalValue;
                    string Title = read["Title"].ToString();
                    textBox5.Text = Title;
                    string yes = read["yes"].ToString();
                    textBox6.Text = yes;
                    string no = read["no"].ToString();
                    textBox7.Text = no;
                }
                conn.Close();
            }

        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
        private void button1_MouseMove(object sender, MouseEventArgs e)
        {
            button1.Text = "----脑筋急转弯----";
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.Text = "脑筋急转弯";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //播放音效
            playClick.Play();
        }

        private void button2_MouseMove(object sender, MouseEventArgs e)
        {
            button2.Text = "----成语填空----";
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            button2.Text = "成语填空";
            playMove.Stop();
        }

        private void button3_MouseMove(object sender, MouseEventArgs e)
        {
            button3.Text = "----古诗接龙----";
        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {
            button3.Text = "古诗接龙";
        }

        private void button4_MouseMove(object sender, MouseEventArgs e)
        {
            button4.Text = "----猜谜语----";
        }

        private void button4_MouseLeave(object sender, EventArgs e)
        {
            button4.Text = "猜谜语";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //播放音效
            playClick.Play();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //播放音效
            playClick.Play();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //播放音效
            playClick.Play();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            //退出程序
            DialogResult Result = MessageBox.Show("确定要退出吗？？","提示",MessageBoxButtons.OKCancel,MessageBoxIcon.Asterisk);
            if (Result == DialogResult.OK)
            {
                //动态关闭窗体
                AnimateWindow(this.Handle, 800, AW_SLIDE + AW_HIDE + AW_CENTER);
                //退出程序
                Application.Exit();
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //动态关闭窗体
            AnimateWindow(this.Handle, 800, AW_SLIDE + AW_HIDE + AW_CENTER);
            //打开答题窗口并让窗体加载脑筋急转弯方法
            this.Close();
            FormTiMu FormTiMu = new FormTiMu();
            FormTiMu.tiMu = 1;
            FormTiMu.Show();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            //动态关闭窗体
            AnimateWindow(this.Handle, 800, AW_SLIDE + AW_HIDE + AW_CENTER);
            //打开答题窗口并让窗体加载成语填空方法
            this.Close();
            FormTiMu FormTiMu = new FormTiMu();
            FormTiMu.tiMu = 2;
            FormTiMu.Show();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            //动态关闭窗体
            AnimateWindow(this.Handle, 800, AW_SLIDE + AW_HIDE + AW_CENTER);
            //打开答题窗口并让窗体加载古诗接龙方法
            this.Close();
            FormTiMu FormTiMu = new FormTiMu();
            FormTiMu.tiMu = 3;
            FormTiMu.Show();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            //动态关闭窗体
            AnimateWindow(this.Handle, 800, AW_SLIDE + AW_HIDE + AW_CENTER);
            //打开答题窗口并让窗体加载猜谜语方法
            this.Close();
            FormTiMu FormTiMu = new FormTiMu();
            FormTiMu.tiMu = 4;
            FormTiMu.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter_1(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {
            //打开帮助窗口
            FormHelp FormHelp = new FormHelp();
            FormHelp.Show();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            //播放音效
            playClick.Play();

            MessageBox.Show("开发中，敬请期待~~","提示",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
        }

        private void button5_MouseMove(object sender, MouseEventArgs e)
        {
            button5.Text = "----附加小游戏----";
        }

        private void button5_MouseLeave(object sender, EventArgs e)
        {
            button5.Text = "附加小游戏";
        }
    }
}
