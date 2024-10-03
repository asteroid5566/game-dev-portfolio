using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        int i;
        bool start = false;
        bool[] card = new bool[52];
        int[] dealer = new int[5];
        int[] player = new int[5];
        Random ran = new Random();
        int sumD, sumP, idxD, idxP;
        int m1, m2;
        char current;

        int point(int n)
        {
            n %= 13;
            if (n >= 10)
                return 10;
            else
                return n + 1;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (start)
                    return;

                current = 'P';
                sumD = sumP = 0;
                button6.Visible = false;
                button7.Visible = false;
                button8.Visible = false;
                button11.Visible = false;
                button12.Visible = false;
                button13.Visible = false;

                m1 = Convert.ToInt32(textBox1.Text);
                m2 = Convert.ToInt32(textBox2.Text);

                if (m1 < m2)
                    throw new Exception("籌碼不足");
                if (m2 <= 0)
                    throw new Exception("下注籌碼不可為0");
                textBox2.ReadOnly = true;

                for (int i = 0; i < 52; i++)
                    card[i] = false;
                for (int i = 0; i < 5; i++)
                {
                    dealer[i] = -1;
                    player[i] = -1;
                }

                dealer[0] = ran.Next(52);
                card[dealer[0]] = true;
                button4.Text = "";
                if (dealer[0] >= 13 && dealer[0] <= 38)
                    button4.ForeColor = Color.Red;
                do
                {
                    dealer[1] = ran.Next(52);
                }
                while (card[dealer[1]]);

                card[dealer[1]] = true;
                button5.Text = poker(dealer[1]);

                if (dealer[1] >= 13 && dealer[1] <= 38)
                    button5.ForeColor = Color.Red;
                do
                {
                    player[0] = ran.Next(52);
                }
                while (card[player[0]]);
                card[player[0]] = true;
                button9.Text = poker(player[0]);
                if (player[0] >= 13 && player[0] <= 38)
                    button9.ForeColor = Color.Red;

                do
                {
                    player[1] = ran.Next(52);
                }
                while (card[player[1]]);

                card[player[1]] = true;
                button10.Text = poker(player[1]);
                if (player[1] >= 13 && player[1] <= 38)
                {
                    button10.ForeColor = Color.Red;
                }

                if ((dealer[0] % 13 == 0 && dealer[1] % 13 >= 9) || (dealer[0] % 13 >= 9 && dealer[1] % 13 == 0))
                {
                    if ((player[0] % 13 == 0 && player[1] % 13 >= 9) || (player[0] % 13 >= 9 && player[1] % 13 == 0))
                    {
                        button4.Text = poker(dealer[0]);
                        MessageBox.Show("雙方都是 black jack，平手!!", "和局", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        textBox2.ReadOnly = false;
                    }
                    else
                    {
                        button4.Text = poker(dealer[0]);
                        m1 -= m2;
                        textBox1.Text = "" + m1;
                        MessageBox.Show("莊家 black jack，你輸了" + m2 + "籌碼", "好可惜", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        textBox2.ReadOnly = false;
                    }
                    return;
                }
                if ((player[0] % 13 == 0 && player[1] % 13 >= 9) || (player[0] % 13 >= 9 && player[1] % 13 == 0))
                {
                    button4.Text = poker(dealer[0]);
                    m1 += m2;
                    textBox1.Text = "" + m1;
                    MessageBox.Show("玩家 black jack，你贏了" + m2 + "籌碼", "恭喜你", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    textBox2.ReadOnly = false;
                    return;
                }
                start = true;
                if (dealer[0] % 13 < 9)
                    sumD += dealer[0] % 13 + 1;
                else
                    sumD += 10;
                if (dealer[1] % 13 < 9)
                    sumD += dealer[1] % 13 + 1;
                else
                    sumD += 10;
                
                if (player[0] % 13 < 9)
                    sumP += player[0] % 13 + 1;
                else
                    sumP += 10;
                if (player[1] % 13 < 9)
                    sumP += player[1] % 13 + 1;
                else
                    sumP += 10;

                idxD = 2;
                idxP = 2;
                textBox3.Text = "" + sumD;
                textBox4.Text = "" + sumP;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "錯誤訊息", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (start)
            {
                if (current == 'P')
                {
                    current = 'D';
                    sumD = point(dealer[0]) + point(dealer[1]);
                    
                    while (sumD < 21 && idxD < 5 && start)
                    {
                        do
                        {
                            dealer[idxD] = ran.Next(52);
                        }
                        while (card[dealer[idxD]]);

                        card[dealer[idxD]] = true;
                        if (idxD == 2)
                        {
                            button6.Text = poker(dealer[idxD]);
                            if (dealer[idxD] >= 13 && dealer[idxD] <= 38)
                                button6.ForeColor = Color.Red;
                            button6.Visible = true;
                        }
                        else if (idxD == 3)
                        {
                            button7.Text = poker(dealer[idxD]);
                            if (dealer[idxD] >= 13 && dealer[idxD] <= 38)
                                button7.ForeColor = Color.Red;
                            button7.Visible = true;
                        }
                        else if (idxD == 4)
                        {
                            button8.Text = poker(dealer[idxD]);
                            if (dealer[idxD] >= 13 && dealer[idxD] <= 38)
                                button8.ForeColor = Color.Red;
                            button8.Visible = true;
                        }

                        string s = "";
                        sumD = 0;
                        for (i = 0; i <= idxD; i++)
                        {
                            sumD += point(dealer[i]);
                        }
                        s = s + sumD + "=";
                        if (sumD > 21)
                        {
                            MessageBox.Show("莊家爆掉，你贏了" + m2 + "籌碼", "恭喜你", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            m1 += m2;
                            textBox1.Text = "" + m1;
                            button4.Text = poker(dealer[0]);
                            start = false;
                        }
                        if (idxD == 5)
                        {
                            if (sumD < sumP)
                            {
                                MessageBox.Show("平手", "恭喜你", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                start = false;
                            }
                            if (sumD > sumP)
                            {
                                MessageBox.Show("莊家點數較高，你輸了" + m2 + "籌碼", "好可惜", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                m1 -= m2;
                                textBox1.Text = "" + m1;
                                start = false;
                            }

                            if (sumD < sumP)
                            {
                                MessageBox.Show("你的點數較高，你贏了" + m2 + "籌碼", "恭喜你", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                m1 += m2;
                                textBox1.Text = "" + m1;
                                start = false;
                            }
                        }
                        idxD++;
                        //textBox4.Text = "" + sumP;

                        for (i = 0; i < idxD; i++)
                        {
                            s = s + "," + point(dealer[i]);
                        }
                        textBox3.Text = s;
                    }
                }
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (start)
            {
                m1 = Convert.ToInt32(textBox1.Text);
                m2 = Convert.ToInt32(textBox2.Text);
                if (current == 'P')
                {
                    if (idxP >= 5)
                        return;
                    do
                    {
                        player[idxP] = ran.Next(52);
                    }
                    while (card[player[idxP]]);

                    card[player[idxP]] = true;
                    if (idxP == 2)
                    {
                        button11.Text = poker(player[idxP]);
                        if (player[idxP] >= 13 && player[idxP] <= 38)
                            button11.ForeColor = Color.Red;
                        button11.Visible = true;
                    }
                    else if (idxP == 3)
                    {
                        button12.Text = poker(player[idxP]);
                        if (player[idxP] >= 13 && player[idxP] <= 38)
                            button12.ForeColor = Color.Red;
                        button12.Visible = true;
                    }
                    else if (idxP == 4)
                    {
                        button13.Text = poker(player[idxP]);
                        if (player[idxP] >= 13 && player[idxP] <= 38)
                            button13.ForeColor = Color.Red;
                        button13.Visible = true;
                    }

                    bool lose = false;
                    string s = "";
                    sumP = 0;
                    for (i = 0; i <= idxP; i++)
                    {
                        sumP += point(player[i]);
                    }
                    s = s + sumP + "=";
                    if (sumP > 21)
                    {
                        MessageBox.Show("玩家爆掉，你輸了" + m2 + "籌碼", "好可惜", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        button4.Text = poker(dealer[0]);
                        lose = true;
                        m1 -= m2;
                        textBox1.Text = "" + m1;
                    }
                    idxP++;
                    //textBox4.Text = "" + sumP;

                    for (i = 0; i < idxP; i++)
                    {
                        s = s + "," + point(player[i]);
                    }
                    textBox4.Text = s;

                    if (lose)
                    {
                        start = false;
                        return;
                    }
                }
            }
        }

        string poker(int a)
        {
            string str = "";
            if (a <= 12)
                str = "\u2660\r\n";
            else if (a <= 25)
                str = "\u2665\r\n";
            else if (a <= 38)
                str = "\u2666\r\n";
            else
                str = "\u2663\r\n";

            if (a % 13 == 0)
                str += "A";
            else if (a % 13 == 12)
                str += "K";
            else if (a % 13 == 11)
                str += "Q";
            else if (a % 13 == 10)
                str += "J";
            else
                str += (a % 13 + 1);
            return str;
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
