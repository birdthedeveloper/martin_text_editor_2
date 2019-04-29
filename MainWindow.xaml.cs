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

namespace martin_text_editor_2
{
    public partial class MainWindow : Window
    {
        bool upperCase = false;

        static string upperRow = "\tqwertzuiopú)";
        static string middleRow = "asdfghjklů§";
        static string bottomRow = "\\yxcvbnm,.-";

        public MainWindow()
        {
            InitializeComponent();
            InitCustomLayout();
        }

        List<Tuple<string, Grid>> TupleList()
        {
            List<Tuple<string, Grid>> list = new List<Tuple<string, Grid>>();

            list.Add(new Tuple<string, Grid>(upperRow, UpperGrid));
            list.Add(new Tuple<string, Grid>(middleRow, MiddleGrid));
            list.Add(new Tuple<string, Grid>(bottomRow, BottomGrid));

            return list;
        }

        private void InitCustomLayout()
        {
            var list = TupleList();

            foreach (Tuple<string, Grid> tuple in list)
            {
                // create a char array from string
                char[] charArray = tuple.Item1.ToCharArray();

                // add cells to the grid
                foreach (char a in charArray)
                {
                    tuple.Item2.ColumnDefinitions.Add(new ColumnDefinition());
                }

                // for loop - create button with all the properties and asign an [EventHandler]
                for (int i = 0; i < charArray.Length; i++)
                {
                    Button button = new Button();
                    button.Width = 30.0;
                    button.Height = 30.0;
                    button.Content = charArray[i].ToString();
                    button.Click += new RoutedEventHandler(OnClick);

                    Grid.SetColumn(button, i);
                    tuple.Item2.Children.Add(button);
                }
            }

            void OnClick(object sender, RoutedEventArgs e)
            {
                Button button = (Button)sender;
                MyTextBox.AppendText((string)button.Content);
                MyTextBox.Focus();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var list = TupleList();
            foreach (var tuple in list)
            {
                var buttons = tuple.Item2.Children.OfType<Button>();
                foreach (Button button in buttons)
                {
                    string content = button.Content.ToString();
                    button.Content = upperCase == true ? content.ToLower() : content.ToUpper();
                }
            }
            upperCase = !upperCase;
        }

        // space button
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MyTextBox.AppendText(" ");
            MyTextBox.Focus();
        }

        //back space button
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            string text = MyTextBox.Text;
            MyTextBox.Text = text.Substring(0, text.Length - 1);
            MyTextBox.Focus();
        }
    }
}
