using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Avengers_Info_App
{
    public class Database
    {
        string folder = DependencyService.Get<ICurrentPath>().GetCurrentPath();
        public bool CreateDatabase()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Characters.db")))
                {
                    connection.CreateTable<CharacterTable>();
                    return true;
                }
            }
            catch (SQLiteException)
            {
                return false;
            }
        }
        //Add or Insert Operation  

        public bool InsertIntoTable(Character character)
        {
            try
            {
                CharacterTable characterTable = ConvertToCharacterTable(character);
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Characters.db")))
                {
                    connection.Insert(characterTable);
                    return true;
                }
            }
            catch (SQLiteException)
            {
                return false;
            }
        }
        public List<Character> SelectTable()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Characters.db")))
                {
                    List<CharacterTable> characterTables = connection.Table<CharacterTable>().ToList();

                    List<Character> characters = new List<Character>();
                    foreach (CharacterTable characterTable in characterTables)
                    {
                        characters.Add(ConvertToCharacter(characterTable));
                    }

                    return characters;
                }
            }
            catch (SQLiteException)
            {
                return null;
            }
        }

        //Delete Data Operation  

        public bool RemoveTable(Character character)
        {
            try
            {
                CharacterTable characterTable = ConvertToCharacterTable(character);
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Characters.db")))
                {
                    connection.Delete(characterTable);
                    return true;
                }
            }
            catch (SQLiteException)
            {
                return false;
            }
        }

        private CharacterTable ConvertToCharacterTable(Character character)
        {
            CharacterTable characterTable = new CharacterTable()
            {
                Id = character.Id,
                Name = character.Name,
                Description = character.Description,
                Modified = character.Modified,
                Comics = character.Comics.ToString(),
                Path = character.Thumbnail.Path,
                Extension = character.Thumbnail.Extension,
            };

            return characterTable;
        }

        private Character ConvertToCharacter(CharacterTable characterTable)
        {
            Character character = new Character()
            {
                Id = characterTable.Id,
                Name = characterTable.Name,
                Description = characterTable.Description,
                Modified = characterTable.Modified,
                Comics = characterTable.Comics.ToString(),
                Thumbnail = new Image()
                {
                    Path = characterTable.Path,
                    Extension = characterTable.Extension
                }
            };

            return character;
        }
    }
}
