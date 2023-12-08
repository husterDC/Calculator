using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Calculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            addNumberBtnClick();
            addFunctionBtnClick();
            LoadIndex(index);
            

        }
        int index = -1;
        List<Button> buttonList;

        List<string> present = new List<string>();
        List<double> result = new List<double>();

        void addNumberBtnClick()
        {
            buttonList = new List<Button>() { btn1, btn2, btn3, btn4, btn5, btn6, btn7, btn8, btn9 };

            foreach (Button button in buttonList)
            {
                button.Click += NumberButton_Click;
            }

        }

        void LoadIndex(int index)
        {
            if (index == -1)
            {
                labelIndex.Text = "now";
            } else
            {
                labelIndex.Text = "-"+(index + 1).ToString();
            }
        }
        void addFunctionBtnClick()
        {
            buttonList = new List<Button>() { btnAdd, btnSub, btnMul, btnDiv, btnDot, btnExp, btnSqrt, btnOpenBracket,btnCloseBracket };

            foreach (Button button in buttonList)
            {
                button.Click += FunctionButton_Click;
            }

        }

        private List<Button> GetAllButtons(Control control)
        {
            var buttons = new List<Button>();

            foreach (Control childControl in control.Controls)
            {
                if (childControl is Button button)
                {
                    buttons.Add(button);
                }

                buttons.AddRange(GetAllButtons(childControl));
            }

            return buttons;
        }

        private void FunctionButton_Click(object sender, EventArgs e)
        {               
            string function = (sender as Button).Text;
            switch (function)
            {
                case "x":
                    function = "*";
                    break;
                case "pow":
                    //function = "pow(";
                    //break;
                    MessageBox.Show("Chức năng đang phát triển");
                    return;
                case "sqrt":
                    //function = "sqrt(";
                    //break;
                    MessageBox.Show("Chức năng đang phát triển");
                    return;
            }
            textBoxDisplay.Text += function;
        }

        private void NumberButton_Click(object sender, EventArgs e)
        {
            string btnText = (sender as Button).Text;
            textBoxDisplay.Text += btnText;
        }


        
        private void btnEqual_Click(object sender, EventArgs e)
        {

            if (textBoxDisplay.Text != "")
            {
                string temp = textBoxDisplay.Text;

                if (temp.Contains("btnSqrt"))
                {
                    
                }

                // Chuyển đổi hàm pow và sqrt thành POW và SQRT
                temp = temp.Replace("pow(", "POW(").Replace("sqrt(", "SQRT(");

                string expression = temp; // Chuỗi biểu thức

                try
                {
                    DataTable table = new DataTable();
                    table.Columns.Add("expression", typeof(string), expression);

                    // Thực hiện tính toán
                    DataRow row = table.NewRow();
                    table.Rows.Add(row);
                    double ans = double.Parse((string)row["expression"]);
                    textBoxResult.Text = (string)row["expression"];
                    if (result.Count >= 5)
                    {
                        present.RemoveAt(0);
                        result.RemoveAt(0);
                    }
                    present.Add(textBoxDisplay.Text);
                    result.Add(ans);

                    GetAllButtons(this).ForEach(button => button.Enabled = false);
                    btnContinue.Enabled = true;
                    btnClear.Enabled = true;
                }
                catch
                {
                    MessageBox.Show("Xin nhập phép tính đúng định dạng");
                }
            }



        }

        private void btnAns_Click(object sender, EventArgs e)
        {
            if (result.Count > 0)
            {
                //textBoxResult.Text = "";
                textBoxDisplay.Text += result[result.Count-1].ToString();
            }
            

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (textBoxDisplay.Text != "")
            {
                string display = textBoxDisplay.Text;
                display = display.Substring(0, display.Length - 1);
                textBoxDisplay.Text = display;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            GetAllButtons(this).ForEach(button => button.Enabled = true);
            textBoxDisplay.Text = "";
            textBoxResult.Text = "";
            temp.Clear();
            index = -1;
            LoadIndex(index);
        }

        Dictionary<string,string> temp = new Dictionary<string,string>();
        bool up = false;
        private void btnUp_Click(object sender, EventArgs e)
        {
            if (index == -1)
            {
                up = true;
                temp.Add(textBoxDisplay.Text, textBoxResult.Text);
                
            }
            if (index < result.Count -1 )
            {
                index++;
                          
            }
            textBoxDisplay.Text = present[result.Count - index - 1];
            textBoxResult.Text = result[result.Count - index - 1].ToString();
            LoadIndex(index);
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            
            if (index < result.Count && index > -1)
            {
                index--;
                if (index != -1)
                {
                    textBoxDisplay.Text = present[result.Count - index - 1];
                    textBoxResult.Text = result[result.Count - index - 1].ToString();
                    LoadIndex(index);
                }

                               
            }
            if (index == -1 && up)
            {
                textBoxDisplay.Text = temp.Keys.First().ToString();
                textBoxResult.Text = temp.Values.First().ToString();
                temp.Clear();
                LoadIndex(index);
                up = false;
                
            }
            
            
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            GetAllButtons(this).ForEach(button => button.Enabled = true);
        }

        
    }
}