using XamarinNotesNew.Classes;

namespace XamarinNotesNew.iOS.Interfaces
{
    public interface ISaveChangesDelegate
    {
        void Save(Note myNote);
        void Add(Note myNote);
    }
}
