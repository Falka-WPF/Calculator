using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calculator
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool triger = false;
        public MainWindow()
        {
            InitializeComponent();
            mHelper = new MathHelper();
        }
        string buf { get; set; }
        MathHelper mHelper { get; set; }
        string History { get
            {
                return History_textBox.Text;
            }
            set
            {
                History_textBox.Text = value;

            } }

        string InputText
        {
            get
            {
                return Input_TextBox.Text;
            }
            set
            {
                Input_TextBox.Text = value;
            }
        }
        string[] operators = {"+","-","/","*"};
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string pressed_button = (e.Source as Button).Content as string;
            if (pressed_button == "CE")
            {
                Input_TextBox.Text = "";
                History_textBox.Text = "";

            }
            else if (pressed_button == "C")
            {
                buf = "";
                InputText = "";
            }
            else if (pressed_button == "<")
            {
                triger = false;
                if (InputText.Length > 0)
                {
                    string bf = "";
                    for (int i = 0; i < InputText.Length - 1; i++)
                    {
                        bf += InputText[i];
                    }
                    InputText = bf;
                }
            }
            else if (operators.Contains(pressed_button))
            {
                if (History.Contains('='))
                {
                    History = InputText;
                }
                bool tg = false;
                int index = -1;
                for (int i = 0; i < operators.Length; i++)
                {
                    if (History.Contains(operators[i]))
                    {
                        for (int x = 0; x < History.Length; x++)
                        {
                            if (History[x] == operators[i][0]) { 
                                index = x;
                                break;
                                }
                        }
                        tg = true;
                    }
                }
                if (tg == true && index != -1)
                {
                    char[] buf = History.ToCharArray();
                    History = "";
                    if (pressed_button.Length > 0)
                    {
                        buf[index] = pressed_button[0];
                        StringBuilder sb = new StringBuilder();
                        foreach (char b in buf)
                            sb.Append(b);
                        History = sb.ToString();
                    }
                }
                else
                {
                    History = InputText + pressed_button;
                    triger = true;
                }
            }
            else if (pressed_button == "=")
            {
                string firstSTR = History;
                string secoundSTR = InputText;
                char MathOperator= firstSTR[firstSTR.Length-1];
                var result = firstSTR.Reverse().Skip(1).Reverse();
                StringBuilder sb = new StringBuilder();
                foreach(var item in result)
                {
                    sb.Append(item);
                }
                double first=0, secound=0;
                try
                {
                    first = Double.Parse(sb.ToString());
                    secound = Double.Parse(secoundSTR);

                    double res = mHelper.MakeOperation(first, secound, MathOperator);
                    Console.WriteLine(MathOperator);
                    History = $"{first}{MathOperator}{secound}=";
                    InputText = res.ToString();
                }
                catch(DivideByZeroException)
                {
                    MessageBox.Show("You can`t divide by 0", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                catch(Exception err)
                {
                    MessageBox.Show($"Internal error\n {err}","Error",MessageBoxButton.OK,MessageBoxImage.Error);
                    return;
                }
            }
            else if(pressed_button== ",")
            {
                if (InputText.Length > 0)
                {
                    var result = from s in InputText
                                 where s == pressed_button[0]
                                 select s;
                    if (result.Count() == 0)
                        InputText += pressed_button;
                }
            }
            else if (History.Contains('='))
            {
                History = InputText;
                InputText = pressed_button;
            }else if(triger == true)
            {
                InputText = pressed_button;
                triger = false;
            }
            else
            {
                    InputText += pressed_button;
            }
        }
    }
}
