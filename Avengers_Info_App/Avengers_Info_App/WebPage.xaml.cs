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
	public partial class WebPage : ContentPage
	{
		public WebPage ()
		{
            InitializeComponent ();
            this.Content = new WebView() { Source="http://www.marvel.com"};
        }
	}
}