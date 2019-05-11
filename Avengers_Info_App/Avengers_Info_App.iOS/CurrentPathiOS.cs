using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

namespace Avengers_Info_App.iOS
{
    public class CurrentPathiOS : ICurrentPath
    {
        public string GetCurrentPath()
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "..", "Library");
            return path;
        }
    }
}