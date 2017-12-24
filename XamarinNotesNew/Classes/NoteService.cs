using System;
using System.Collections.Generic;

namespace XamarinNotesNew.Classes
{
    public class NoteService : Repository<NoteForDB>
    {
        public NoteService()
        {
        }

        public int WriteToDB(NoteForDB item)
        {
            return Create(item);
        }

        public void DeleteFromDB(NoteForDB item)
        {
            Delete(item);
        }

        public List<NoteForDB> ReadFromDB()
        {
            return GetAll<NoteForDB>();
        }
    }
}