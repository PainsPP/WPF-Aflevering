using Microsoft.Win32;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
//using System.Windows.Forms;



namespace WPF_Projekt
{
    public class Person
    {
        public Person(string data)
        {
            // ID, Name, Age, Score 
            var L = data.Split(';');
            Id      = int.Parse(L[0]);
            Name    = L[1];
            Age     = int.Parse(L[2]);
            Score   = int.Parse(L[3]);
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int Score { get; set; }
        
        public String ListBoxToString
        {
            get { return ($"{Name}\nId: {Id}"); }
        }

    }


    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Person> persons;

        public MainWindow()
        {
            InitializeComponent();
            persons = new ObservableCollection<Person>();
            listBox.ItemsSource = persons;
        }


        private void open_Click(object sender, RoutedEventArgs e)
        {
            Stream myStream = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == true)
            {
                if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                    using (myStream)
                    {
                        string[] text = File.ReadAllLines(openFileDialog1.FileName);
                        foreach (string str in text)
                        {
                            persons.Add(new Person(str));
                        }
                    }
                    barItem_count.Content = $"Person Count: {persons.Count}";
                    barItem_time.Content = $"Last Updated: {DateTime.Now}";
                }
            }
        }

        private void listbox_UserClick(object sender, SelectionChangedEventArgs e)
        {
            if (listBox.SelectedIndex == -1) return;
            txtId.DataContext = listBox.SelectedItem;
            txtName.DataContext = listBox.SelectedItem;
            txtAge.DataContext = listBox.SelectedItem;
            txtScore.DataContext = listBox.SelectedItem;
        }
    }
}
