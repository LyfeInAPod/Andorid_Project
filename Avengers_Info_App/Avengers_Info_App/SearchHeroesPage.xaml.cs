using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Avengers_Info_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchHereosPage : ContentPage
    {
        public ObservableCollection<Character> Items { get; set; }
        Database database;
        const string _API_PRIVATE_KEY = "";
        const string _API_PUBLIC_KEY = "";
        public SearchHereosPage()
        {
            InitializeComponent();

            Items = new ObservableCollection<Character>();
            MyListView.ItemsSource = Items;
            database = new Database();
            if (!File.Exists(Path.Combine(DependencyService.Get<ICurrentPath>().GetCurrentPath(), "Characters.db")))
            {
                database.CreateDatabase();
            }

            OnFirstLoad();
        }

        private void OnFirstLoad()
        {
            mainActivityIndicator.IsRunning = true;
            try
            {
                //Items.Clear();
                //var ts = Guid.NewGuid().ToString();
                //var _hashService = DependencyService.Get<IHashService>();
                //var hash = _hashService.CreateMd5Hash(ts + _API_PRIVATE_KEY + _API_PUBLIC_KEY);
                //IEnumerable<Character> characters = await GetCharacters($@"http://gateway.marvel.com/v1/public/characters?apikey={_API_PUBLIC_KEY}&hash={hash}&ts={ts}");
                //foreach (Character character in characters)
                //{
                //    Items.Add(character);
                //}

                SaveAvenger("Thor");
                SaveAvenger("Iron Man");
                SaveAvenger("Captain America");
                SaveAvenger("Hulk");
                SaveAvenger("Black Widow");
                SaveAvenger("Hawkeye");
            }
            catch (Exception) { }
            mainActivityIndicator.IsRunning = false;
        }

        private async void SearchTxt_SearchButtonPressed(object sender, EventArgs e)
        {
            mainActivityIndicator.IsRunning = true;

            try
            {
                Items.Clear();
                var ts = Guid.NewGuid().ToString();
                var _hashService = DependencyService.Get<IHashService>();
                var hash = _hashService.CreateMd5Hash(ts + _API_PRIVATE_KEY + _API_PUBLIC_KEY);
                string name = searchTxt.Text;
                IEnumerable<Character> characters = await GetCharacters($@"http://gateway.marvel.com/v1/public/characters?apikey={_API_PUBLIC_KEY}&hash={hash}&ts={ts}&nameStartsWith={name}");
                foreach (Character character in characters)
                {
                    Items.Add(character);
                }
            }
            catch (Exception) { }

            mainActivityIndicator.IsRunning = false;
        }

        public async Task<IEnumerable<Character>> GetCharacters(string url)
        {

            var client = new HttpClient();
            var response = await client.GetStringAsync(url);

            var responseObject = JObject.Parse(response);

            mainActivityIndicator.IsRunning = false;

            return await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<IEnumerable<Character>>(responseObject["data"]["results"].ToString()));

        }

        private async void SaveAvenger(string name)
        {
            var ts = Guid.NewGuid().ToString();
            var _hashService = DependencyService.Get<IHashService>();
            var hash = _hashService.CreateMd5Hash(ts + _API_PRIVATE_KEY + _API_PUBLIC_KEY);
            IEnumerable<Character> characters = await GetCharacters($@"http://gateway.marvel.com/v1/public/characters?apikey={_API_PUBLIC_KEY}&hash={hash}&ts={ts}&name={name}");
            foreach (Character character in characters)
            {
                database.RemoveTable(character);
                database.InsertIntoTable(character);
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