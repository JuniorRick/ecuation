using System;
using System.Windows.Forms;
using System.Drawing;


namespace Test
{
	public class Complex 
	{
		private double real;
		private double im;

		public Complex( double real, double im)
		{
			this.real = real;
			this.im = im;
		}
	}

	public class MainClass : Form
	{
		TextBox box;
		Label label;
		char oper;
		string ecuation;

		public MainClass()
		{
			this.Text = "Windows DialogForm ";
			this.Size = new System.Drawing.Size(420, 360);

			box = new TextBox();
			box.Location = new System.Drawing.Point(20, 10);
			box.Size = new System.Drawing.Size(380, 20);
			//box.TextChanged += new EventHandler(box_Changed);
			box.Text = "(3 + 2i) + (-2 + 3) * ((1 + 3i) - ((-2 - 3i) * (4 - 2i)))";
			ecuation = box.Text;

			label = new Label();
			label.Location = new System.Drawing.Point(20, 40);
			label.AutoSize = true;
			label.Text = GetFirstOperation(box.Text);

			this.Controls.Add(box);
			this.Controls.Add(label);
		}

		//(3 + 2i) + (-2 + 3) * ((1 + 3i) - ((-2 - 3i) * (4 - 2i)))
		private string GetFirstOperation(string str)
		{
			int leftP = 0, rightP = 0;
			int startIndex = 0;
			str = str.Replace(" ", "");

			for (int i = 0; i < str.Length-1; i++)
			{
				if (str[i] == '(' && str[i + i] == '(')
				{
					leftP = 2;
					startIndex = i + 1;
				}
				else if (str[i] == ')' && str[i + 1] == ')')
				{
					rightP = 2;
				}

				if (leftP == 2 && rightP == 2)
				{
					return str.Substring(startIndex, i + 1);

				}
			}

			return null;
		}

		private void calculate()
		{
			if (ecuation != null)
			{
				string str;
				while ( (str = GetFirstOperation(ecuation)) != null)
				{
					int opIndex = GetHigherOperationPriorityIndex(str);

					while (true)
					{
						
					}

				}

			}
		}

		private int GetHigherOperationPriorityIndex(string str)
		{
			str = str.Replace(" ", "");
			int index = -1;
			for (int i = 0; i < str.Length; i++)
			{
				if (str[i] == 'i' || str[i] == ')')
				{
					if (str[i + 1] == '*')
					{
						oper = '*';
						index = i + 1;
					}
					else if (str[i + 1] == '/')
					{
						oper = '/';
						index = i + 1;
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

		public static void Main()
		{
			
			MainClass form = new MainClass();
			Application.Run(form);
		}
	}
}
