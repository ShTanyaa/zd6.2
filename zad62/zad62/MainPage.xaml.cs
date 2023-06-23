using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace zad62
{
    public partial class MainPage :ContentPage
    {
        private ListView hotelListView;
        private int selected=-1;
        private List<Hotel> hotels = new List<Hotel>
        {
            new Hotel
                {
                    Name = "Все на высшем уровне",
                    Number = 101,
                    Director = "Иванов Иван",
                    Phone = "123456789",
                    Category = "★★★★",
                    Capacity = 50,
                    Price = 1000,
                    Surname = "Угрюмов",
                    Address = "Улица Пушкина 2",
                    Age = 30,
                    CheckInDate = DateTime.Now,
                    StayDuration = 3,
                    IsPaymentCash = true
                },
                new Hotel
                {
                    Name = "Лазанья",
                    Number = 201,
                    Director = "Шумкова Татьяна",
                    Phone = "89004567712",
                    Category = "★★★★★",
                    Capacity = 1,
                    Price = 1750,
                    Surname = "Иванова",
                    Address = "Улица Ленина 10",
                    Age = 25,
                    CheckInDate = DateTime.Now.AddDays(1),
                    StayDuration = 2,
                    IsPaymentCash = false
                },
                new Hotel
                {
                    Name = "Ночник",
                    Number = 50,
                    Director = "Пупкин Василий",
                    Phone = "89056332244",
                    Category ="★★★★★",
                    Capacity = 3,
                    Price = 1300,
                    Surname = "Иванов",
                    Address = "Улица Гагарина 29",
                    Age = 40,
                    CheckInDate = DateTime.Now.AddDays(1),
                    StayDuration = 2,
                    IsPaymentCash = false
                }
        };
        public MainPage ()
        {
            InitializeComponent( );
            Title = "Онлайн-бронирование гостиницы";
            hotelListView = new ListView
            {
                ItemsSource = hotels,
                ItemTemplate = new DataTemplate(() =>
                {
                    var nameLabel = new Label( );
                    nameLabel.SetBinding(Label.TextProperty, "Name");

                    var image = new Image( );
                    image.SetBinding(Image.SourceProperty,"imageSource");

                    return new ViewCell
                    {
                        View = new StackLayout
                        {
                            Orientation = StackOrientation.Horizontal,
                            Children = { image ,nameLabel },
                            Padding = new Thickness(10)
                        }
                    };
                })
            };
            hotelListView.ItemSelected += OnItemSelected;
            var selectCapacity = new Button
            {
                Text = "Расчет стоимости проживания",
                StyleId = "buttonstyle"
            };
            selectCapacity.Clicked += ThreePage;
            var calculate = new Button
            {
                Text = "Выбор количества мест",
                StyleId = "buttonstyle"
            };
            calculate.Clicked += TwoPage;
            var onesort = new Button
            {
                Text = "Сортировка от меньшего к большему по категории",
                StyleId = "buttonstyle"
            };
            onesort.Clicked += OnSortone;
            var twosort = new Button
            {
                Text = "Сортировка от большего к меньшему по категории",
                StyleId = "buttonstyle"
            };
            twosort.Clicked += OnSorttwo;
            Content = new StackLayout
            {
                Children = { hotelListView , onesort, twosort,selectCapacity,calculate}
            };
        }
        private void OnItemSelected (object sender, SelectedItemChangedEventArgs e)
        {
            if ( e.SelectedItem is Hotel selectedHotel )
            {
                var message = $"Название: {selectedHotel.Name}\n" +
                    $"Свободных номеров: {selectedHotel.Number}\n" +
                    $"Директор: {selectedHotel.Director}\n" +
                     $"Возраст: {selectedHotel.Age}\n"+
                $"Телефон: {selectedHotel.Phone}\n" +
                    $"Категория: {selectedHotel.Category}\n" +
                    $"Стоимость за один день: {selectedHotel.Price}\n" +
                    $"Адрес: {selectedHotel.Address}\n";
                DisplayAlert("Выбран отель", message, "ОК");
                selected = hotels.IndexOf(selectedHotel);
                hotelListView.SelectedItem = null;
            }
        }

        private void OnSortone (object sender, EventArgs e)
        {
            var sortedHotels = hotels.OrderBy(a => a.Category).ToList( );
            hotelListView.ItemsSource = sortedHotels;
        }

        private void OnSorttwo (object sender, EventArgs e)
        {
            var sortedHotels = hotels.OrderByDescending(a => a.Category).ToList( );
            hotelListView.ItemsSource = sortedHotels;
        }

        private async void ThreePage (object sender, EventArgs e)
        {
            if (selected != -1)
                await Navigation.PushModalAsync(new ThreePage(hotels[selected]));
            else DisplayAlert("Ошибка", "Сначало выберите гостиницу в списке", "ОК");
        }
        private async void TwoPage (object sender, EventArgs e)
        {
            if (selected!=-1)
            await Navigation.PushModalAsync(new TwoPage(hotels[selected]));
            else
                DisplayAlert("Ошибка", "Сначало выберите гостиницу в списке", "ОК");
        }

        public void Export(string name, int numbers)
        {
            Hotel izmhotel = hotels.FirstOrDefault(h => h.Name == name); // Поиск элемента с заданным значением name
            if (izmhotel != null)
            {
                izmhotel.Number = numbers;
                // Обновление списка в ListView
                hotelListView.ItemsSource = null;
                hotelListView.ItemsSource = hotels;
            }
        }
    }
}