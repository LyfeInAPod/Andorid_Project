using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Avengers_Info_App
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HeroInfoPage : ContentPage
	{
        Character character;
		public HeroInfoPage (Character character)
		{
			InitializeComponent ();

            this.character = character;

        }

        protected override void OnAppearing()
        {
            thumbnailImg.Source = character.Thumbnail.FullPath;
            nameLbl.Text = character.Name;
            descriptionLbl.Text = character.Description;
            dateLbl.Text = character.Modified.ToShortDateString();

            var responseObject = JObject.Parse(character.Comics.ToString());

            IEnumerable<Comic> comics = JsonConvert.DeserializeObject<IEnumerable<Comic>>(responseObject["items"].ToString());
            foreach (Comic comic in comics)
            {
                comicsLbl.Text += comic.Name + Environment.NewLine;
            }
        }
    }
}