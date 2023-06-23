using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace zad62
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ThreePage :ContentPage
    {
        private Label label1;
        private Label label2;
        private Entry entry;
        private Button button;
        private RadioButton radbutton1;
        private RadioButton radbutton2;
        private RadioButton radbutton3;
        double price = 1;
        private int number = 1;
        public ThreePage (Hotel hotels)
        {
            Title = "Расчет стоимости проживания";
            label1 = new Label();
            label2 = new Label();
            entry = new Entry();
            button = new Button();
            price = hotels.Price;
            label1.Text = $"Гостиница: {hotels.Name}";
            label2.Text = $"Выбрано билетов: {number}";
            entry.Placeholder = "Введите срок проживания";
            button.Text = "Расчитать";
            button.Clicked += OnSaveButtonClicked;

            radbutton1 = new RadioButton { Content = "Пенсионер" };
            radbutton2 = new RadioButton { Content = "Студент или учащийся" };
            radbutton3 = new RadioButton { Content = "Многодетная семья" };

            var stackLayout = new StackLayout();
            stackLayout.Children.Add(label1);
            stackLayout.Children.Add(label2);
            stackLayout.Children.Add(entry);
            stackLayout.Children.Add(radbutton1);
            stackLayout.Children.Add(radbutton2);
            stackLayout.Children.Add(radbutton3);
            stackLayout.Children.Add(button);
            Content = stackLayout;
        }
        public ThreePage (string hotel, int numb,double pric)
        {
            InitializeComponent();
            number = numb;
            price = pric;
            Title = "Расчет стоимости проживания";
            label1 = new Label();
            label2 = new Label();
            entry = new Entry();
            button = new Button();
            label1.Text = $"Гостиница: {hotel}";
            label2.Text = $"Выбрано мест: {number}";
            entry.Placeholder = "Введите срок проживания";
            button.Text = "Расчитать";
            button.Clicked += OnSaveButtonClicked;

            radbutton1 = new RadioButton { Content = "Пенсионер" };
            radbutton2 = new RadioButton { Content = "Студент или учащийся" };
            radbutton3 = new RadioButton { Content = "Многодетная семья" };

            var stackLayout = new StackLayout();
            stackLayout.Children.Add(label1);
            stackLayout.Children.Add(label2);
            stackLayout.Children.Add(entry);
            stackLayout.Children.Add(radbutton1);
            stackLayout.Children.Add(radbutton2);
            stackLayout.Children.Add(radbutton3);
            stackLayout.Children.Add(button);
            Content = stackLayout;
        }

        private async void OnSaveButtonClicked (object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(entry.Text) >= 1 && radbutton1.IsChecked)
                {
                    double res = ((price - price * 0.4) * number) * double.Parse(entry.Text);
                    DisplayAlert("Сообщение (пенсионер)", $"Стоимость номера(ов): {res}", "ОК");
                }
                else if (Convert.ToInt32(entry.Text) >= 1 && radbutton2.IsChecked)
                {
                    double res = ((price - price * 0.2) * number)* double.Parse(entry.Text) ;
                    DisplayAlert("Сообщение (студенты или учащиеся)", $"Стоимость номера(ов): {res}", "ОК");
                } 
                else if (Convert.ToInt32(entry.Text) >= 1 && radbutton3.IsChecked)
                {
                    double res = ((price - price * 0.1) * number) * double.Parse(entry.Text);
                    DisplayAlert("Сообщение (мн. семьи)", $"Стоимость номера(ов): {res}", "ОК");
                }
                    else
                    DisplayAlert("Ошибка", "Был найден отрицательный ввод", "ОК");
            } catch { DisplayAlert("Ошибка", "Был найден некорректный ввод числа", "ОК"); }

        }
    }
}