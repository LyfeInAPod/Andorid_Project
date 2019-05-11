using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Avengers_Info_App
{
    public class CharacterTable
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Modified { get; set; }
        public string Comics { get; set; }
        public string Path { get; set; }
        public string Extension { get; set; }
    }
}
