using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace 动动脑益智答题
{
    public partial class FormTiMu : Form
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

        //判断用户选择的题目类型
        public int tiMu;

        //题目索引
        public int i = 0;

        //对题记录,错题记录
        public int njjzwYes = 0, njjzwNo = 0;            //脑筋急转弯
        public int cytkYes = 0, cytkNo = 0;               //成语填空
        public int gsjlYes = 0, gsjlNo = 0;                 //古诗接龙
        public int cmyYes = 0, cmyNo = 0;               //猜谜语

        //总对题记录,总错题记录
        public int yes = 0;
        public int no = 0;

        //错题控制，避免每次答错都加一次次数
        public bool njjzwFlag = true;
        public bool cytkFlag = true;
        public bool gsjlFlag = true;
        public bool cmyFlag = true;

        //脑筋急转弯：问题
        public string[] njjzw = { "两个胖子(猜一地名)" , "离婚的主要起因是什么？" , "大象的右耳朵像什么？" ,
                                            "什么东西肥的快？瘦的也快？" , "用什么拖地最干净？" , "什么油不能点燃？" ,
                                            "偷什么东西不犯法？" , "什么样的人死后还会出现？" , "什么路最窄？" , "什么布不能做衣服?" ,
                                            "什么东西只能加不能剪？" , "什么门不能关？" , "什么船不靠水就能行？" , "什么帽不能戴？" ,
                                            "什么鱼不能吃？" , "大家都不想得到的是什么？" , "什么东西晚上才会长出尾巴？" ,
                                            "第一个登上月球的中国姑娘是谁？" , "蚊子咬在什么地方你不会觉得痒？" ,
                                            "将要来却永远来不了的是什么？" ,"拿什么东西不用手？" , "什么话可以世界通用？" ,
                                              "最坚固的锁怕什么？" , "什么飞机常常没有明确的目的地？" , "什么掌不能拍？" ,
                                              "为什么大家喜欢看漫画？" , "什么东西越生气，它便越大？" , "什么车不能做？" ,
                                              "门里站着一个人(猜一字)" , "小偷最怕碰到那个机关？" , "什么人始终不敢洗澡？"};
        //脑筋急转弯：答案
        public string[] ndn = { "合肥" , "结婚" , "左耳朵" , "汽球" , "用力" , "酱油" , "偷笑" , "电影中的人" , "冤家路窄" , "瀑布" ,
                                          "年龄" , "球门" , "宇宙飞船" , "螺丝帽" , "木鱼" , "得病" , "流星" , "嫦娥" , "别人身上" , "明天" ,
                                          "拿主意" , "电话" , "钥匙" , "纸飞机" ,"仙人掌" , "无聊" , "脾气" , "风车" , "闪" , "公安机关" , "泥人"};


        //成语填空：问题
        public string[] cytk = { "耳目（）新" , "爱不（）手" , "安（）定国" , "春光明（）" , "垂帘听（）" , "春风（）面" ,
                                    "功成名（）" , "功败（）成" , "高官厚（）" , "高（）远瞩" , "供认不（）" , "对答如（）" ,
                                    "多愁善（）" , "多多益（）" , "夺（）而出" , "过眼云（）" , "过（）拆桥" , "高山（）水" ,
                                    "形单（）只" , "恶贯满（）" , "过（）不忘" , "（）目寸光" , "乐不思（）" , "逆水行（）" ,
                                    "破（）沉舟" , "黄发垂（）" , "阡（）交通" , "（）然开朗" , "对牛弹（）" , "车水马（）"};
        //成语填空：答案
        public string[] cdn = { "一" , "释" , "邦" , "媚" , "政" , "满" , "就" , "垂" , "禄" , "瞻" , "讳" , "流" ,
                                    "感" , "善" , "眶" , "烟" , "河" , "流" , "影" , "盈" , "目" , "鼠" , "蜀" , "舟",
                                    "釜" , "髫" , "陌" , "豁" , "琴" ,"龙"};


        //古诗接龙：问题
        public string[] gsjl = { "日照香炉生紫烟" , "不知细叶谁裁出" , "会当凌绝顶" , "国破山河在" , "造化钟神秀" , "感时花溅泪",
                                    "大弦嘈嘈如急雨", "仰天大笑出门去" , "怀旧空吟闻笛赋" , "折戟沉沙铁未销" , "山河破碎风飘絮" ,
                                    "人生自古谁无死", "征蓬出汉塞" , "大漠孤烟直" , "仍怜故乡水" };
        //古诗接龙：答案
        public string[] gdn = { "遥看瀑布挂前川" , "二月春风似剪刀" , "一览众山小" , "城春草木深" , "阴阳割昏晓" , "恨别鸟惊心",
                                    "小弦窃窃如私语" , "我辈岂是蓬蒿人" , "到乡翻似烂柯人" , "自将磨洗认前朝" , "身世浮沉雨打萍" ,
                                    "留取丹心照汗青" , "归雁入胡天" , "长河落日圆" , "万里送行舟" };

        //猜谜语：问题
        public string[] cmy = { "一口咬掉牛尾巴（打一汉字）" , "三水压倒山（打一汉字）" , "风平浪静（打一城市）" ,
                                    "黄河解冻（打一成语）" , "草上飞（打一汉字）" , "农产品（打一成语）" , "九十九（打一汉字）" ,
                                    "三个十不出头（打一字）" , "一半满一半空（打一汉字）" , "嘴巴不多却能闹（打一汉字）"};
        //猜谜语：答案
        public string[] cmydn = { "告", "当", "宁波", "江苏", "早", "土生土长", "白", "正", "江", "吵" };


        //脑筋急转弯方法
        public void NaoJinJiZhuanWan()
        {
            labelTitle.Text = "脑筋急转弯 (共"+ njjzw.Length + "题)";
            groupBox1.Text = "第" + (i + 1) + "题";
            labelTi.Text = njjzw[i];
        }

        //成语填空方法
        public void ChengYuTianKong()
        {
            labelTitle.Text = "成语填空 (共" + cytk.Length + "题)";
            groupBox1.Text = "第" + (i + 1) + "题";
            labelTi.Text = cytk[i];
        }

        //古诗接龙方法
        public void GuShiJieLong()
        {
            labelTitle.Text = "古诗接龙 (共" + gsjl.Length + "题)";
            groupBox1.Text = "第" + (i + 1) + "题";
            labelTi.Text = gsjl[i];
        }

        //猜谜语方法
        public void CaiMiYu()
        {
            labelTitle.Text = "猜谜语 (共" + cmy.Length + "题)";
            groupBox1.Text = "第" + (i + 1) + "题";
            labelTi.Text = cmy[i];
        }

        public FormTiMu()
        {
            InitializeComponent();

            //窗体弹出效果
            AnimateWindow(this.Handle, 300, AW_CENTER);
        }

        private void label3_Click(object sender, EventArgs e)
        {
            //动态关闭窗体
            AnimateWindow(this.Handle, 800, AW_SLIDE + AW_HIDE + AW_CENTER);
            //隐藏当前窗体
            this.Close();
            FormMain FormMain = new FormMain();
            //传递当前临时数据到主窗体
            FormMain.yes = yes;
            FormMain.no = no;
            FormMain.Show();
        }

        private void FormNJJZW_Load(object sender, EventArgs e)
        {
            //判断用户选择的题目类型并加载相应方法
            if (tiMu == 1)
            {
                //加载脑筋急转弯方法
                NaoJinJiZhuanWan();
            }
            else if (tiMu == 2)
            {
                //加载成语填空方法
                ChengYuTianKong();
            }
            else if (tiMu == 3)
            {
                //加载古诗接龙方法
                GuShiJieLong();
            }
            else if (tiMu == 4)
            {
                //加载猜谜语方法
                CaiMiYu();
            }

        }

        //判断用户输入答案是正确
        private void button1_Click(object sender, EventArgs e)
        {
            if (tiMu == 1)      //脑筋急转弯
            {
                if (i == njjzw.Length)
                {
                    MessageBox.Show("恭喜通关，您真是太厉害啦！！*^_^* !!\n试着玩玩其他模式吧~~\n\n(当前题目类型已解锁跳关模式)", "恭喜通关", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    labelLock.Visible = false;
                }

                if (textBox1.Text == ndn[i])
                {
                    MessageBox.Show("恭喜，回答正确！！\n点击确定进入下一题 *^_^* !!", "回答正确", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    //索引加1
                    i++;
                    //对题次数加一
                    njjzwYes++;
                    yes++;
                    //判断当前题是否答错过，如果打错过，则错题次数也加一
                    if (njjzwFlag == false)
                    {
                        njjzwNo++;
                        //还原开关状态
                        njjzwFlag = true;
                    }
                    //最大值等于当前题目值
                    numericUpDown1.Maximum = i;
                    //刷新界面
                    groupBox1.Text = "第" + (i + 1) + "题";
                    labelTi.Text = njjzw[i];
                    //清空输入的文本
                    textBox1.Text = null;
                }
                else
                {
                    MessageBox.Show("很遗憾，回答错误~~\n再好好想想吧 -_- ~~", "回答错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //错题次数加一
                    njjzwNo++;
                    njjzwFlag = false;
                }
            }
            else if (tiMu == 2)     //成语填空
            {
                if (i == cytk.Length)
                {
                    MessageBox.Show("恭喜通关，您真是太厉害啦！！*^_^* !!\n试着玩玩其他模式吧~~\n\n(当前题目类型已解锁跳关模式)", "恭喜通关", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    labelLock.Visible = false;
                }

                if (textBox1.Text == cdn[i])
                {
                    MessageBox.Show("恭喜，回答正确！！\n点击确定进入下一题 *^_^* !!", "回答正确", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    //索引加1
                    i++;
                    //对题次数加一
                    cytkYes++;
                    yes++;
                    //判断当前题是否答错过，如果打错过，则错题次数也加一
                    if (cytkFlag == false)
                    {
                        cytkNo++;
                    }
                    //最大值等于当前题目值
                    numericUpDown1.Maximum = i;
                    //刷新界面
                    groupBox1.Text = "第" + (i + 1) + "题";
                    labelTi.Text = cytk[i];
                    //清空输入的文本
                    textBox1.Text = null;
                }
                else
                {
                    MessageBox.Show("很遗憾，回答错误~~\n再好好想想吧 -_- ~~", "回答错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //错题次数加一
                    cytkNo++;
                    cytkFlag = false;
                }
            }
            else if (tiMu == 3)     //古诗接龙
            {
                if (i == gsjl.Length)
                {
                    MessageBox.Show("恭喜通关，您真是太厉害啦！！*^_^* !!\n试着玩玩其他模式吧~~\n\n(当前题目类型已解锁跳关模式)", "恭喜通关", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    labelLock.Visible = false;
                }

                if (textBox1.Text == gdn[i])
                {
                    MessageBox.Show("恭喜，回答正确！！\n点击确定进入下一题 *^_^* !!", "回答正确", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    //索引加1
                    i++;
                    //对题次数加一
                    gsjlYes++;
                    yes++;
                    //判断当前题是否答错过，如果打错过，则错题次数也加一
                    if (gsjlFlag == false)
                    {
                        gsjlNo++;
                    }
                    //最大值等于当前题目值
                    numericUpDown1.Maximum = i;
                    //刷新界面
                    groupBox1.Text = "第" + (i + 1) + "题";
                    labelTi.Text = gsjl[i];
                    //清空输入的文本
                    textBox1.Text = null;
                }
                else
                {
                    MessageBox.Show("很遗憾，回答错误~~\n再好好想想吧 -_- ~~", "回答错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //错题次数加一
                    gsjlNo++;
                    gsjlFlag = false;
                }
            }
            else if (tiMu == 4)     //猜谜语
            {
                if (i == cmy.Length)
                {
                    MessageBox.Show("恭喜通关，您真是太厉害啦！！*^_^* !!\n试着玩玩其他模式吧~~\n\n(当前题目类型已解锁跳关模式)", "恭喜通关", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    labelLock.Visible = false;
                }

                if (textBox1.Text == cmydn[i])
                {
                    MessageBox.Show("恭喜，回答正确！！\n点击确定进入下一题 *^_^* !!", "回答正确", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    //索引加1
                    i++;
                    //对题次数加一
                    cmyYes++;
                    yes++;
                    //判断当前题是否答错过，如果打错过，则错题次数也加一
                    if (cmyFlag == false)
                    {
                        cmyNo++;
                    }
                    //最大值等于当前题目值
                    numericUpDown1.Maximum = i;
                    //刷新界面
                    groupBox1.Text = "第" + (i + 1) + "题";
                    labelTi.Text = cmy[i];
                    //清空输入的文本
                    textBox1.Text = null;
                }
                else
                {
                    MessageBox.Show("很遗憾，回答错误~~\n再好好想想吧 -_- ~~", "回答错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //错题次数加一
                    cytkNo++;
                    cmyFlag = false;
                }
            }
            
        }

        //判断用户选择的题目类型并提示相应答案
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (tiMu == 1)      //脑筋急转弯
            {
                MessageBox.Show("本题答案是：" + ndn[i] + "\n这次应该能过关了吧~~", "答案", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (tiMu == 2)     //成语填空
            {
                MessageBox.Show("本题答案是：" + cdn[i] + "\n这次应该能过关了吧~~", "答案", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (tiMu == 3)     //古诗接龙
            {
                MessageBox.Show("本题答案是：" + gdn[i] + "\n这次应该能过关了吧~~", "答案", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (tiMu == 4)     //猜谜语
            {
                MessageBox.Show("本题答案是：" + cmydn[i] + "\n这次应该能过关了吧~~", "答案", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }

        //跳转到上一题
        private void button2_Click(object sender, EventArgs e)
        {
            if (tiMu == 1)      //脑筋急转弯
            {
                //加载拦截方法
                esc();
                //索引减一
                --i;
                //刷新界面
                groupBox1.Text = "第" + (i + 1) + "题";
                labelTi.Text = njjzw[i];
                //清空输入的文本
                textBox1.Text = null;
            }
            else if (tiMu == 2)     //成语填空
            {
                //加载拦截方法
                esc();
                //索引减一
                --i;
                //刷新界面
                groupBox1.Text = "第" + (i + 1) + "题";
                labelTi.Text = cytk[i];
                //清空输入的文本
                textBox1.Text = null;
            }
            else if (tiMu == 3)     //古诗接龙
            {
                //加载拦截方法
                esc();
                //索引减一
                --i;
                //刷新界面
                groupBox1.Text = "第" + (i + 1) + "题";
                labelTi.Text = gsjl[i];
                //清空输入的文本
                textBox1.Text = null;
            }
            else if (tiMu == 4)     //猜谜语
            {
                //加载拦截方法
                esc();
                //索引减一
                --i;
                //刷新界面
                groupBox1.Text = "第" + (i + 1) + "题";
                labelTi.Text = cmy[i];
                //清空输入的文本
                textBox1.Text = null;
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (tiMu == 1)      //脑筋急转弯
            {
                //加载拦截方法
                esc();
                //索引减一
                ++i;
                //刷新界面
                groupBox1.Text = "第" + (i + 1) + "题";
                labelTi.Text = njjzw[i];
                //清空输入的文本
                textBox1.Text = null;
            }
            else if (tiMu == 2)     //成语填空
            {
                //加载拦截方法
                esc();
                //索引减一
                ++i;
                //刷新界面
                groupBox1.Text = "第" + (i + 1) + "题";
                labelTi.Text = cytk[i];
                //清空输入的文本
                textBox1.Text = null;
            }
            else if (tiMu == 3)     //古诗接龙
            {
                //加载拦截方法
                esc();
                //索引减一
                ++i;
                //刷新界面
                groupBox1.Text = "第" + (i + 1) + "题";
                labelTi.Text = gsjl[i];
                //清空输入的文本
                textBox1.Text = null;
            }
            else if (tiMu == 4)     //猜谜语
            {
                //加载拦截方法
                esc();
                //索引减一
                ++i;
                //刷新界面
                groupBox1.Text = "第" + (i + 1) + "题";
                labelTi.Text = cmy[i];
                //清空输入的文本
                textBox1.Text = null;
            }
        }

        //跳转关卡按钮
        private void button4_Click(object sender, EventArgs e)
        {
            //判断用户选择的题目类型并执行相应功能
            if (tiMu == 1)      //脑筋急转弯
            {
                //跳转关卡
                i = Convert.ToInt32(numericUpDown1.Text) - 1;
                //刷新界面
                groupBox1.Text = "第" + (i + 1) + "题";
                labelTi.Text = njjzw[i];
                //清空输入的文本
                textBox1.Text = null;
            }
            else if (tiMu == 2)     //成语填空
            {
                //跳转关卡
                i = Convert.ToInt32(numericUpDown1.Text) - 1;
                //刷新界面
                groupBox1.Text = "第" + (i + 1) + "题";
                labelTi.Text = cytk[i];
                //清空输入的文本
                textBox1.Text = null;
            }
            else if (tiMu == 3)     //古诗接龙
            {
                //跳转关卡
                i = Convert.ToInt32(numericUpDown1.Text) - 1;
                //刷新界面
                groupBox1.Text = "第" + (i + 1) + "题";
                labelTi.Text = gsjl[i];
                //清空输入的文本
                textBox1.Text = null;
            }
            else if (tiMu == 4)     //猜谜语
            {
                //跳转关卡
                i = Convert.ToInt32(numericUpDown1.Text) - 1;
                //刷新界面
                groupBox1.Text = "第" + (i + 1) + "题";
                labelTi.Text = cmy[i];
                //清空输入的文本
                textBox1.Text = null;
            }

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        //拦截变量避免数组越界
        public void esc()
        {
            if (i == 0)
            {
                i++;
            }

            if (i == njjzw.Length - 1)
            {
                i--;
            }
        }

    }
}
