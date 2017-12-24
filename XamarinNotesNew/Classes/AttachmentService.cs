using System.Collections.Generic;

namespace XamarinNotesNew.Classes
{
    public class AttachmentService: Repository<Picture>
    {
        public AttachmentService()
        {
        }

        public int WriteToDB(Picture item)
        {
            return Create(item);
        }

        public void DeleteFromDB(Picture item)
        {
            Delete(item);
        }

        public List<Picture> ReadFromDB()
        {
            return GetAll<Picture>();
        }
    }
}
