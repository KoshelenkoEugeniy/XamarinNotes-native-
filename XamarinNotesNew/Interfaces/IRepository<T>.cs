using System.Collections.Generic;

namespace XamarinNotesNew.Interfaces
{
    public interface IRepository<T>
    {
        int Create(T newElement);

        void Delete(T element);

        List<T> GetAll<T>() where T : IObjectId, new();

        T Get<T>(IObjectId id) where T : IObjectId, new();
    }
}
