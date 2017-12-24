using XamarinNotesNew.Classes;
using XamarinNotesNew.iOS.Interfaces;
using System.Collections.Generic;
using System.IO;

namespace XamarinNotesNew.iOS.Classes
{
    public class SourceOfNotes: IStorage
    {
        public SourceOfNotes()
        {
        }

        public List<Note> Source { get; set; }

        public void DeleteFromSource(int byIndex)
        {
            if (Source[byIndex].Attachments.Count > 0)
            {
                foreach (var item in Source[byIndex].Attachments)
                {
                    DeleteFromFolderUsing(Path.Combine(DBConnection.appDbPath, item.Name + $".{item.Extension}"));
                }
            }

            Source.RemoveAt(byIndex);
        }

        public void DeleteFromFolderUsing(string path)
        {
            File.Delete(path);
        }
    }
}
