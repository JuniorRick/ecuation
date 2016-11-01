using System;
using System.Windows.Forms;
using System.Drawing;


namespace VisualCalc
{

    public class MainClass : Form
    {
        TextBox box;
        Label label;
        char oper;
        private Button button1;
        string ecuation;

        public MainClass()
        {
            InitializeComponent();
        }
        //12 3 456 7 890 1 234 5 6789 0 123 4 567 8 90 1 23 4 56
        //(3 + 2i) * ((1 + 3i) - ((-2 - 3i) - (-2 + 3) * (4 - 2i)))
        private string GetFirstOperationString(string str)
        {
            int leftP = 0, rightP = 0;
            int startIndex = 0;
            str = str.Replace(" ", "").Replace("i", "");
           
            for (int i = 0; i < str.Length - 1; i++)
            {         
                if ( (str[i] == '(') && (str[i] == str[i+1]))
                {
                    leftP = 2;
                    startIndex = i + 1;
                }
                else if (str[i] == ')' && (str[i] == str[i + 1]))
                {
                    rightP = 2;
                }

                if (leftP == 2 && rightP == 2)
                {
                    return str.Substring(startIndex, i - startIndex + 1);

                }
            }

            return label.Text;
        }

        private double getNumber(string str, int opIndex,int i, out int k)
        {
            double d = 0.0;
            k = i;

            if (i == opIndex)
                i++;

            if (str[i] == '(')
                i++;

            if (str[i] == '-' || str[i] == '+')
                k++;

            while (str[k] > '0' || str[k] < '9')
            {

                if ( str[k] == '+' || str[k]== ')')
                    break;
                k++;
            }

            bool isDouble = false;
            isDouble = double.TryParse(str.Substring(i, k - i), out d);
            if (isDouble)
                return d;

            return 0.0;
        }

        private void calculate()
        {
            Complex c1 = new Complex(1, 1);
            Complex c2 = new Complex(1, 1);
            if (ecuation != null)
            {
                string str = null;
                
                while ((str = GetFirstOperationString(ecuation)) != null)
                {
                    int opIndex = GetHigherOperationPriorityIndex(str);

                    if (opIndex == -1)
                        break;

                    int i = opIndex;
                    int j = opIndex;
                    while (str[i] != '(')
                        i--;
                    while (str[j] != ')')
                        j++;

                    string substr;

                    substr = str.Substring(i, j - i + 1);

                    opIndex = GetHigherOperationPriorityIndex(substr);
                    //substr = substr.Replace("(", " ");
                    int k;
                    int pos = 0;
                    c1.real = getNumber(substr, opIndex, pos, out k);
                    pos = k;
                    c1.im = getNumber(substr, opIndex, pos, out k);
                    label.Text = c1.real.ToString() + "," + c1.im.ToString();

                    break;

                }
            }
        }

        private int GetHigherOperationPriorityIndex(string str)
        {

            int index = -1;
            for (int i = 0; i < str.Length - 1; i++)
            {
                if (str[i] == ')')
                {
                    if (str[i + 1] == '*')
                    {
                        oper = '*';
                        return i + 1;
                    }
                    else if (str[i + 1] == '/')
                    {
                        oper = '/';
                        return  i + 1;
                    }
                    else if (str[i + 1] == '+')
                    {
                        oper = '+';
                        index = i + 1;
                    }
                    else if (str[i + 1] == '-')
                    {
                        oper = '-';
                        index = i + 1;
                    }
                }
            }

            return index;
        }


        private void box_Changed(object sender, EventArgs e)
        {
            label.Text = box.Text;
        }

        private void InitializeComponent()
        {
            this.box = new System.Windows.Forms.TextBox();
            this.label = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // box
            // 
            this.box.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.box.Location = new System.Drawing.Point(21, 15);
            this.box.Name = "box";
            this.box.Size = new System.Drawing.Size(380, 23);
            this.box.TabIndex = 0;
            this.box.Text = "(3 + 2i) * ((1 + 3i) - ((2 - 3i) - (-2 - 3) * (4 - 2i)))";
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Location = new System.Drawing.Point(20, 40);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(0, 13);
            this.label.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(326, 325);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // MainClass
            // 
            this.ClientSize = new System.Drawing.Size(420, 360);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.box);
            this.Controls.Add(this.label);
            this.Name = "MainClass";
            this.Text = "Windows DialogForm ";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            ecuation = box.Text;
            label.Text = ecuation.Replace(" ", "").Length.ToString();
            calculate();
        }


    }

    public class Complex
    {
        public double real;
        public double im;

        public Complex(double real, double im)
        {
            this.real = real;
            this.im = im;
        }
    }
}