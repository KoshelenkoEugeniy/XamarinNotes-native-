using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using XamarinNotesNew.Interfaces;
using SQLite;

namespace XamarinNotesNew.Classes
{
    public class DataBase<T> : IDataBase<T>
    {
        
        public DataBase()
        {
            DBConnection.CreateConnection();
        }

        public T GetItemFromDB<T>(IObjectId id) where T: IObjectId, new()
        {
            return (from i in DBConnection.databaseConnection.Table<T>()
                    where i.Id == id.Id
                    select i).FirstOrDefault();
        }

        public List<T> ReadFromDB<T>() where T: IObjectId, new()
        {
            return (from i in DBConnection.databaseConnection.Table<T>()
                    select i).ToList();
        }

        public int WriteToDB(T item)
        {
            DBConnection.databaseConnection.Insert(item);

            return (int)SQLite3.LastInsertRowid(DBConnection.databaseConnection.Handle);
        }

        public void DeleteFromDB(T item)
        {
            DBConnection.databaseConnection.Delete(item);
        }

    }
}
