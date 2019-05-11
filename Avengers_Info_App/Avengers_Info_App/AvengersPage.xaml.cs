using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Avengers_Info_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AvengersPage : ContentPage
    {
        public ObservableCollection<Character> Items { get; set; }
        Database database;

        public AvengersPage()
        {
            InitializeComponent();

            Items = new ObservableCollection<Character>();
            database = new Database();
			
			MyListView.ItemsSource = Items;

            OnFirstLoad();
        }

        private void OnFirstLoad()
        {
            Items.Clear();
            List<Character> characters = database.SelectTable();
            foreach(Character character in characters)
            {
                Items.Add(character);
            }
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            //Deselect Item
            ((ListView)sender).SelectedItem = null;

            await Navigation.PushAsync(new HeroInfoPage(e.Item as Character));
        }
    }
}
