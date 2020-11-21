using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CS
{
    public partial class UI : Form
    {
        public List<Label> labels = new List<Label>();
        Graph G = new Graph();
        public UI()
        {
            InitializeComponent();
            InitLabel();
        }

        public void InitLabel()
        {
            labels.Add(label1);
            labels.Add(label2);
            labels.Add(label3);
            labels.Add(label4);
            labels.Add(label5);
            labels.Add(label6);
            labels.Add(label7);
            labels.Add(label8);
            labels.Add(label9);
            labels.Add(label10);
            labels.Add(label11);
            labels.Add(label12);
            labels.Add(label13);
        }

        public void ResetLabel()
        {
            for (int i = 0; i < labels.Count; i++)
            {
                labels.ElementAt(i).ForeColor = Color.Black;
            }
        }

        public void ResetTextBox()
        {
            richTextBox1.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ResetLabel();
            ResetTextBox();
            textBox2.Text = "";
            textBox3.Text = "";
            textBox1.Text = "";
            string name = textBox4.Text;
            VexInfo vexInfo=G.CheckInfo(name);
            if (vexInfo == null)
            {
                richTextBox1.Text = "未找到该地名!请重新输入!";
                return;
            }
            richTextBox1.Text = "地名:"+vexInfo.name+"    地点信息:"+vexInfo.introduction;
            for (int i = 0; i < labels.Count; i++)
            {
                if (labels.ElementAt(i).Text.Equals(vexInfo.name))
                {
                    labels.ElementAt(i).ForeColor = Color.Red;
                    break;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ResetLabel();
            ResetTextBox();
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            string name = textBox1.Text;
            VexInfo vexInfo = G.CheckInfo(name);
            if (vexInfo == null)
            {
                richTextBox1.Text = "未找到路径!请重新输入!";
                return;
            }
            int[] vexLocation;
            StringBuilder str = G.PathGateToOthers(name,out vexLocation);
            for (int i = 0; i < vexLocation.Length; i++)
            {
                labels.ElementAt(vexLocation[i]).ForeColor = Color.Red;
            }
            richTextBox1.Text = str.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ResetLabel();
            ResetTextBox();
            textBox1.Text = "";
            textBox4.Text = "";
            string beginName = textBox2.Text;
            string endName = textBox3.Text;
            VexInfo beginVex = G.CheckInfo(beginName);
            VexInfo endVex = G.CheckInfo(endName);
            if (beginVex == null || endVex == null)
            {
                richTextBox1.Text = "未找到该地名";
                return;
            }
            int[] vexLocation;
            StringBuilder str = G.PathOneToOthers(beginName,endName,out vexLocation);
            for (int i = 0; i < vexLocation.Length; i++)
            {
                labels.ElementAt(vexLocation[i]).ForeColor = Color.Red;
            }
            richTextBox1.Text = str.ToString();
        }
    }
}
