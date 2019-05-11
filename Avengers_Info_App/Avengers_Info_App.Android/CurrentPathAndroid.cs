using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Avengers_Info_App.Droid
{
    public class CurrentPathAndroid : ICurrentPath
    {
        public string GetCurrentPath()
        {
            string path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments));
            return path;
        }
    }
}