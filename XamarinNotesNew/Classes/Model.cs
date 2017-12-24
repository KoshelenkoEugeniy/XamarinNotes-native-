using System;
using System.Collections.Generic;
using XamarinNotesNew.Interfaces;
using System.Linq;

namespace XamarinNotesNew.Classes
{
    public class Model: IObservable
    {
        private List<IObserver> observers { get; set; }

        List<Note> NoteList = new List<Note>();

        NoteService serviceForNotes = new NoteService();

        AttachmentService serviceForAttachments = new AttachmentService();

        public Model()
        {
            observers = new List<IObserver>();
        }

        public void RegisterObserver(IObserver newObserver)
        {
            observers.Add(newObserver);
        }

        public void RemoveObserver(IObserver currentObserver)
        {
            observers.Remove(currentObserver);
        }

        public void NotifyObservers(string answer)
        {
            if (answer == "ok")
            {
                PrepareNoteFromDB();
            }
            else
            {
                NoteList = null;
            }

            foreach (IObserver item in observers)
            {
                item.Update(NoteList, answer);
            }
        }

        public void ToDo(string task, Note newNote)
        {
            try
            {
                switch (task)
                {
                    case "Create":
                        PrepareForCreating(newNote);    
                        break;
                    case "Delete":
                        PrepareForDeleting(newNote);    
                        break;
                    case "Change":
                        PrepareForUpdating(newNote);  // update element in local collection
                        break;
                    case "Synchronize":
                    default:
                        NotifyObservers("ok");
                        return;
                }

                NotifyObservers("ok");
            }
            catch (Exception ex)
            {
                NotifyObservers(ex.Message);
            }
        }

        private void PrepareForCreating(Note currentNote)
        {
            NoteForDB noteDB = new NoteForDB();

            noteDB = PrepareNoteDB(currentNote);

            int noteId = serviceForNotes.WriteToDB(noteDB);

            if (currentNote.Attachments.Count > 0) 
            {
                foreach (var element in currentNote.Attachments)
                {
                    Picture pictureDB = new Picture();
                    pictureDB = element;
                    pictureDB.NoteId = noteId;

                    int pictureID = serviceForAttachments.WriteToDB(pictureDB);
                }
            }
        }

        private void PrepareForDeleting(Note currentNote)
        {
            NoteForDB noteDB = new NoteForDB();

            noteDB = PrepareNoteDB(currentNote);

            serviceForNotes.DeleteFromDB(noteDB);

            if (currentNote.Attachments.Count > 0)
            {
                foreach (var element in currentNote.Attachments)
                {
                    Picture pictureDB = new Picture();
                    pictureDB = element;
                    pictureDB.NoteId = noteDB.Id;

                    serviceForAttachments.DeleteFromDB(pictureDB);
                }
            }
        }

        private NoteForDB PrepareNoteDB (Note item)
        {
            NoteForDB temp = new NoteForDB();

            temp.Id = item.Id;
            temp.State = item.Status;
            temp.Text = item.Text;
            temp.DateOfCreation = item.DateOfCreation;

            return temp;
        }

        private void PrepareNoteFromDB()
        {
            List <Note> notesToShow = new List<Note>();
            List<NoteForDB> notes = new List<NoteForDB>();
            List<Picture> attachments = new List<Picture>();
            List<Picture> currentAttachments = new List<Picture>();

            notes = serviceForNotes.ReadFromDB();
            attachments = serviceForAttachments.ReadFromDB();

            foreach(NoteForDB item in notes)
            {
                Note currentNote = new Note();
                currentNote.Id = item.Id;
                currentNote.Status = item.State;
                currentNote.Text = item.Text;
                currentNote.DateOfCreation = item.DateOfCreation;

                currentAttachments = (from i in attachments
                                        where i.NoteId == item.Id
                                        select i).ToList();

                currentNote.Attachments = currentAttachments;

                notesToShow.Add(currentNote);
            }
            NoteList = notesToShow;
        }

        public void PrepareForUpdating(Note currentNote)
        {
            Note noteForDeleting = new Note(); 

            PrepareNoteFromDB();

            noteForDeleting = (from i in NoteList
                               where i.Id == currentNote.Id
                               select i).FirstOrDefault();

            PrepareForDeleting(noteForDeleting);
            PrepareForCreating(currentNote);
        }
    }
}
