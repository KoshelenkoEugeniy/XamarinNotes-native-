using System.Collections.Generic;
using XamarinNotesNew.Classes;
using XamarinNotesNew.iOS.Interfaces;
using System.IO;

namespace XamarinNotesNew.iOS.Classes
{
    public class SourceOfAttachments: IStorage
    {
        public SourceOfAttachments()
        {
            
        }

        public List<Picture> Source { get; set; }

        public void DeleteFromSource(int byIndex)
        {
            DeleteFromFolderUsing(Path.Combine(DBConnection.appDbPath, Source[byIndex].Name + $".{Source[byIndex].Extension}")); 

            Source.RemoveAt(byIndex);
        }

        public void DeleteFromFolderUsing(string path)
        {
            File.Delete(path);
        }
    }
}
