using System.Collections.Generic;

namespace XamarinNotesNew.Interfaces
{
    public interface IDataBase<T>
    {
        int WriteToDB(T item);

        void DeleteFromDB(T item);

        List<T> ReadFromDB<T>() where T : IObjectId, new();

        T GetItemFromDB<T>(IObjectId id) where T: IObjectId, new();
    }
}
