using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Form1 : Form
    {
        private double num1 = 0;
        private double num2 = 0;
        private char op = ' ';

        public Form1()
        {
            InitializeComponent();
            CreateCalculatorButtons();
        }

        private void CreateCalculatorButtons()
        {
            int buttonWidth = 75;
            int buttonHeight = 75;
            int startX = 20;
            int startY = 100;

            for (int i = 0; i <= 9; i++)
            {
                Button button = new Button();
                button.Text = i.ToString();
                button.Size = new Size(buttonWidth, buttonHeight);
                button.Location = new Point(startX + (i % 3) * (buttonWidth + 10), startY + (i / 3) * (buttonHeight + 10));
                button.Font = new Font(button.Font.FontFamily, 30);
                button.Click += new EventHandler(Button_Click);
                this.Controls.Add(button);
            }

            string[] operators = { "+", "-", "x", "/" };
            int opStartX = startX + 3 * (buttonWidth + 10) + 20;
            int opStartY = startY;

            for (int i = 0; i < operators.Length; i++)
            {
                Button button = new Button();
                button.Text = operators[i];
                button.Size = new Size(buttonWidth, buttonHeight);
                button.Location = new Point(opStartX, opStartY + i * (buttonHeight + 10));
                button.Font = new Font(button.Font.FontFamily, 30);
                button.Click += new EventHandler(Button_Click);
                this.Controls.Add(button);
            }

            Button equalButton = new Button();
            equalButton.Text = "=";
            equalButton.Size = new Size(buttonWidth, buttonHeight);
            equalButton.Location = new Point(startX + 2 * (buttonWidth + 10), startY + 4 * (buttonHeight + 10));
            equalButton.Font = new Font(equalButton.Font.FontFamily, 30);
            equalButton.Click += new EventHandler(Button_Click);
            this.Controls.Add(equalButton);

            Button clearButton = new Button();
            clearButton.Text = "C";
            clearButton.Size = new Size(buttonWidth, buttonHeight);
            clearButton.Font = new Font(clearButton.Font.FontFamily, 30);
            clearButton.Location = new Point(startX, startY + 4 * (buttonHeight + 10));
            clearButton.Click += new EventHandler(Button_Click);
            this.Controls.Add(clearButton);

            Label outputLabel = new Label();
            outputLabel.Text = "0";
            outputLabel.AutoSize = false;
            outputLabel.Size = new Size(10 * buttonWidth, 10 * buttonHeight);
            outputLabel.Location = new Point(startX, startY - buttonHeight - 15);
            outputLabel.Font = new Font(outputLabel.Font.FontFamily, 50);
            this.Controls.Add(outputLabel);
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            Label outputLabel = this.Controls[Controls.Count - 1] as Label;
            double result = CalculateResult();

            if (int.TryParse(button.Text, out int num))
            {
                if (outputLabel.Text.Length >= 10)
                    return;

                outputLabel.Text = num.ToString();
                if (op == ' ')
                {
                    num1 = num1 * 10 + num;
                    outputLabel.Text = num1.ToString("N0");
                }
                else
                {
                    num2 = num2 * 10 + num;
                    outputLabel.Text = num2.ToString("N0");
                }
            }

            else if (button.Text == "+" || button.Text == "-" || button.Text == "x" || button.Text == "/")
            {
                op = button.Text[0];
            }

            else if (button.Text == "=")
            {
                string formattedResult;

                if (Math.Abs(result) >= 10000000)
                {
                    formattedResult = result.ToString("0.###E+0");
                }
                else
                {
                    formattedResult = result.ToString("N0");
                }
                outputLabel.Text = formattedResult;
                num1 = result;
                num2 = 0;
                op = ' ';
            }

            else if (button.Text == "C")
            {
                outputLabel.Text = "0";
                num1 = 0;
                num2 = 0;
                op = ' ';
            }

        }

        private double CalculateResult()
        {
            switch (op)
            {
                case '+':
                    return num1 + num2;
                case '-':
                    return num1 - num2;
                case 'x':
                    return num1 * num2;
                case '/':
                    return num1 / num2;
                default:
                    return num1;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
