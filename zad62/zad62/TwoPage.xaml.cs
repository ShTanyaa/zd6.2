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
    public partial class TwoPage :ContentPage
    {
        private Label label1;
        private Label label2;
        private Entry entry;
        double price = 1;
        private Button button;
        private int number = 0;
        private string name;
        public TwoPage ()
        {
            
        }
        public TwoPage (Hotel hotel)
        {
            
            InitializeComponent( );
            Title = "Выбор количества мест";
            label1 = new Label( );
            label2 = new Label( );
            entry = new Entry( );
            button = new Button( );
            price = hotel.Price;
            label1.Text = $"Гостиница: {hotel.Name}";
            name = hotel.Name;
            hotel.Number = hotel.Number - number;
            number = hotel.Number;
            label2.Text = $"Свободных номеров: {number}";
            entry.Placeholder = "Введите сколько мест будете арендовывать";
            button.Text = "Арендовать";
            var stackLayout = new StackLayout( );
            button.Clicked += OnSaveButtonClicked;
            stackLayout.Children.Add(label1);
            stackLayout.Children.Add(label2);
            stackLayout.Children.Add(entry);
            stackLayout.Children.Add(button);
            Content = stackLayout;
            
        }
        private async void OnSaveButtonClicked (object sender, EventArgs e)
        {
            try
            {
                if ( Convert.ToInt32(entry.Text) <= 10 && Convert.ToInt32(entry.Text) >= 1 )
                {
                    number = number - Convert.ToInt32(entry.Text);
                    label2.Text = $"Свободных номеров: {number}";
                    DisplayAlert("Сообщение", $"Вы успешно арендовали {entry.Text} номер(ов)", "Хорошо");
                    await Navigation.PushModalAsync(new ThreePage(name, int.Parse(entry.Text),price));
                    entry.Text = "";
                }
                else if ( Convert.ToInt32(entry.Text) > 10 )
                    DisplayAlert("Ошибка", "Нельзя бронировать на одного человека больше 10 номеров в гостинице", "ОК");
                else DisplayAlert("Ошибка", "Был найден отрицательный ввод", "ОК");
            }
            catch { DisplayAlert("Ошибка", "Был найден некорректный ввод числа", "ОК"); }
            
        }
    }
}