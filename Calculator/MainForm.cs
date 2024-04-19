using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;
using System.Configuration;


namespace Calculator
{
    public partial class MainForm : Form
    {
        short bracket = 0;

        char[] requiredSybol = new char[]
            {
                '+',
                '-',
                '×',
                '÷',
                '(',
                
            };
        public MainForm()
        {
            InitializeComponent();
            label.Text = "0";
            memory.Text = string.Empty;
            answer.Text = string.Empty;
            label.BackColor = BackColor;
        }

        private void WriteNumber(object sender, MouseEventArgs e)
        {
            Button button = sender as Button;

            label.Text += button.Text;

            if (label.Text[0] == '0' && label.Text.Length > 1 && !label.Text.Contains(","))
            {
                label.Text = label.Text.Substring(1);
            }

            if (label.Text != "0") delAll.Text = "CE";
        }

        private void roudedBtton31_Click(object sender, EventArgs e)
        {
            label.Text = label.Text.Substring(0, label.Text.Length - 1);

            try
            {
                double number = Convert.ToDouble(label.Text);
                label.Text = number.ToString();
            }
            catch { }

            if (label.Text == string.Empty) label.Text = "0";
        }

        private void roudedBttonClick(object sender, EventArgs e)
        {
            double number = Convert.ToDouble(label.Text);
            try
            {
                label.Text = (-1 * number).ToString();
            }
            catch
            {
                label.Text = "Ошибка";
            }
        }

        private void pointWrite(object sender, MouseEventArgs e)
        {
            if (!label.Text.Contains(","))
            {
                label.Text += ",";
            }
        }

        private void modul(object sender, MouseEventArgs e)
        {
            if (label.Text.Contains("-")) roudedBttonClick(null, null);
        }

        private void delAllClick(object sender, EventArgs e)
        {
            if (delAll.Text == "CE")
            {
                label.Text = "0";
                delAll.Text = "C";
            }
            else if (memory.Text != string.Empty)
            {
                label.Text = "0";
                memory.Text = string.Empty;
            }

        }

        private void factorial(object sender, MouseEventArgs e)
        {
            textValidation('(' + label.Text + ")!");
            try
            {
                int factorial = Convert.ToInt32(label.Text);
                answer.Text = calculatingFactorial(factorial).ToString();
            }
            finally
            {
                label.Text = "0";
            }
        }
        private void logClic(object sender, MouseEventArgs e)
        {
            textValidation("log" + '(' + label.Text + ')');
            try
            {
                double log = Convert.ToDouble(label.Text);
                answer.Text = Math.Log(log).ToString();
            }
            finally
            {
                label.Text = "0";
            }
        }

        private void lnClic(object sender, MouseEventArgs e)
        {
            textValidation("ln" + '(' + label.Text + ')');
            try
            {
                double ln = Convert.ToDouble(label.Text);
                answer.Text = Math.Log(2.718281828459045, ln).ToString();
            }
            finally
            {
                label.Text = "0";
            }
        }

        private long calculatingFactorial(long i)
        {
            if (i == 0)
            {
                return 1;
            }
            else
            {
                return i * calculatingFactorial(i - 1);
            }
        }

        private void textValidation(string text)
        {

            if (memory.Text != string.Empty && requiredSybol.Contains(memory.Text[memory.Text.Length - 1]))
            {
                memory.Text += text;
            }
            else
            {
                memory.Text = text;
            }
        }

        private void chekSybol(object sender, MouseEventArgs e)
        {
            Button button = sender as Button;

            if (memory.Text == string.Empty && label.Text == "0")
            {
                memory.Text = "0" + button.Text;
            }
            else if (memory.Text == string.Empty && label.Text != "0")
            {
                memory.Text = label.Text + button.Text;
            }
            else if (requiredSybol.Contains(memory.Text[memory.Text.Length - 1]))
            {
                memory.Text += label.Text + button.Text;
            }
            else
            {
                memory.Text += button.Text;
            }
            label.Text = "0";
        }

        private void exponentClic(object sender, EventArgs e) => zeroAndValid("2,718281828");

        private void piClic(object sender, EventArgs e) => zeroAndValid("3,1415926535");

        private void oneToShareClic(object sender, EventArgs e) => zeroAndValid($"1/{label.Text}");

        private void sqearClic(object sender, EventArgs e) => zeroAndValid($"({label.Text})^2");

        private void rootClic(object sender, EventArgs e) => zeroAndValid($"√({label.Text})");

        private void leftBracketClic(object sender, EventArgs e)
        {
            bracket++;
            textValidation("(");
        }

        private void reightBracketClic(object sender, EventArgs e)
        {
            if (bracket > 0) 
            {
                if(label.Text == "0")
                {
                    memory.Text += ')';
                }
                else
                {
                    if (memory.Text[memory.Text.Length - 1] == ')') memory.Text += '×';
                    zeroAndValid($"{label.Text})");
                }
                bracket--;
            }
        }

        private void zeroAndValid(string text)
        {
            textValidation(text);
            label.Text = "0";
        }

        private void sqerY(object sender, MouseEventArgs e)
        {
            bracket++;
            zeroAndValid($"{label.Text}^(");
        }

        private void tenSqerClic(object sender, MouseEventArgs e)
        {
            bracket++;
            zeroAndValid("10^(");
        }
    }
}


