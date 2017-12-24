using System.Collections.Generic;
using XamarinNotesNew.Interfaces;

namespace XamarinNotesNew.Classes
{
    public abstract class Repository<T> : IRepository<T> where T : IObjectId
    {
        private DataBase<T> _dbContext = new DataBase<T>();

        public int Create(T newElement)
        {
            return _dbContext.WriteToDB(newElement);
        }

        public void Delete(T element)
        {
            _dbContext.DeleteFromDB(element);
        }

        public List<T> GetAll<T>() where T : IObjectId, new()
        {
            return _dbContext.ReadFromDB<T>();
        }

        public T Get<T>(IObjectId id) where T : IObjectId, new()
        {
            return _dbContext.GetItemFromDB<T>(id);
        }
    }
}
