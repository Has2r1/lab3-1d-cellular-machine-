using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab3__1d_cellular_machine_
{
    
    public partial class Form1 : Form
    {
        private Random random = new Random();
        private Dictionary<(bool, bool, bool), bool> currentRule = new Dictionary<(bool, bool, bool), bool>();
        private int lines = 100;
        private int Ypos = 0;
        private int Xpos = 0;
        private List<List<bool>> data = new List<List<bool>>();
        private readonly Brush whiteBrush = new SolidBrush(Color.White);
        private readonly Brush blackBrush = new SolidBrush(Color.Black);
        private Graphics g;
        private List<bool> genZero = new List<bool>();
        private readonly List<int> rules = new List<int>()
        {
            30, 90, 184,
            150, 105, 101, 73, 
            54, 60, 137, 
            110
        };
        public Form1()
        {
            InitializeComponent();
            g = panel1.CreateGraphics();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //int.TryParse(rule.Text, out ruleNumber);
            int.TryParse(numberOfLines.Text, out lines);

            

            foreach (int rule in rules)
            {
                button1.Text = "новая";//rule.ToString();
                var binary = Convert.ToString(rule, 2).PadLeft(8, '0').ToCharArray();
                currentRule.Clear();
                currentRule.Add((false, false, false), binary[0] == '1');
                currentRule.Add((false, false, true), binary[1] == '1');
                currentRule.Add((false, true, false), binary[2] == '1');
                currentRule.Add((false, true, true), binary[3] == '1');
                currentRule.Add((true, false, false), binary[4] == '1');
                currentRule.Add((true, false, true), binary[5] == '1');
                currentRule.Add((true, true, false), binary[6] == '1');
                currentRule.Add((true, true, true), binary[7] == '1');

                genZero.Clear();
                for (int i = 0; i < lines; i++)
                {
                    genZero.Add(random.NextDouble() >= 0.1);
                }

                for (int k = 0; k < 40; k++)
                {
                    data.Clear();
                    data.Add(new List<bool>(genZero));
                    for (int i = 1; i < lines; i++)
                    {
                        data.Add(new List<bool>());
                        for (int j = 0; j < lines; j++)
                        {
                            data[i].Add(currentRule[(data[i - 1][(j - 1 + lines) % lines], data[i - 1][j], data[i - 1][(j + 1) % lines])]);
                        }
                    }
                    genZero = new List<bool>(data[lines - 1]);

                    for (int i = 0; i < lines; i++)
                    {
                        Ypos = 10 + i * 10;
                        for (int j = 0; j < lines; j++)
                        {
                            Xpos = 10 + j * 10;
                            if (data[i][j])
                            {
                                g.FillRectangle(whiteBrush, Xpos, Ypos, 10, 10);
                            }
                            else
                            {
                                g.FillRectangle(blackBrush, Xpos, Ypos, 10, 10);
                            }
                        }
                    }
                }
            }
        } 
    }
}
