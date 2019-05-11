using SQLite;
using System;
using System.Collections.Generic;

namespace Avengers_Info_App
{
    public class Character
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Modified { get; set; }
        public object Comics { get; set; }
        public Image Thumbnail { get; set; }

    }
}
