using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Xml.Linq;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button1_Click(object sender, RoutedEventArgs e)
        {
            dynamic res = null;
            string group_id = "";
            Label1.Content = "";
            Label2.Content = "";
            var text = TextBox1.Text;
            if (ComboBox1.Text == "По аудитории")
            {
               res = await Requester.Get("api/classroom_schedule?classroom=" + TextBox1.Text.ToString());
            }
            else
            {
                var group = await Requester.Get("api/group?group=" + TextBox1.Text.ToString());
                res = await Requester.Get("api/student_schedule?group_id=" + group[0].dept_id.ToString());
            }

            if (res.ToString() == "[]")
            {
                Label1.Content = "Введите верный параметр";
            }

            if (ComboBox1.SelectedItem == null)
            {
                Label2.Content = "Выберите тип поиска";
            }

                    List<Raspisain> raspisains = new List<Raspisain>();
                    foreach (var i in res)
                    {
                        Raspisain raspisain = new Raspisain()
                        {
                            id = i.id,
                            education_group_name = i.education_group_name +" "+ ((i.subgroup == 0) ? "" : "п/г" + i.subgroup),
                            education_group_id = i.education_group_id,
                            day_number = i.day_number,
                            lesson_number = i.lesson_number,
                            place = i.place,
                            subgroup = i.subgroup,
                            teacher_id = i.teacher_id,
                            teacher_name = i.teacher_name,
                            subject = i.subject,
                            type = i.type,
                            date_lesson = i.date_lesson,
                        };
                        raspisains.Add(raspisain);

                    };
                    RaspisainsGrid.ItemsSource = raspisains;
        }
    }
}
