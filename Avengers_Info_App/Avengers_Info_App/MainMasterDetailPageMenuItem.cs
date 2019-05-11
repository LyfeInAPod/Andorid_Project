using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avengers_Info_App
{

    public class MainMasterDetailPageMenuItem
    {
        public MainMasterDetailPageMenuItem()
        {
            TargetType = typeof(SearchHereosPage);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}