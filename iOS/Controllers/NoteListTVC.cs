using Foundation;
using System;
using UIKit;
using XamarinNotesNew.Classes;
using System.Collections.Generic;
using System.Linq;
using XamarinNotesNew.iOS.Interfaces;

namespace XamarinNotesNew.iOS
{
    public partial class NoteListTVC : UITableViewController, ISaveChangesDelegate
    {
        public NoteListTVC (IntPtr handle) : base (handle)
        {
        }

        List<Note> dataToShow = new List<Note>();

        static Model blModel = new Model();

        Observer observer = new Observer(blModel);


        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            blModel.ToDo("Synchrinize", null);
        }

        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            base.PrepareForSegue(segue, sender);

            var destinationVC = segue.DestinationViewController;

            var navcon = destinationVC as UINavigationController;

            if (navcon != null)
            {
                destinationVC = navcon.VisibleViewController;

                var vc = destinationVC as NoteVC;

                if (vc != null)
                {
                    var identifier = segue.Identifier;

                    vc.Delegate = this;

                    if (identifier == "EditSegue")
                    {
                        vc.NavigationItem.Title = "Edit Note";
                        vc.NavigationItem.RightBarButtonItem.Title = "Save";

                        var source = TableView.Source as TableSourceForNotesList;
                        var rowPath = TableView.IndexPathForSelectedRow;
                        var item = source.GetItem(rowPath.Row);
                        vc.SetTask(this, item);
                    }

                    if (identifier == "AddSegue")
                    {
                        vc.NavigationItem.Title = "New Note";
                        vc.NavigationItem.RightBarButtonItem.Title = "Add";
                        vc.SetTask(this, new Note("", true, 0, new List<Picture>()));
                    }
                }
            }
        }

        public void Save(Note myNote)
        {
            blModel.ToDo("Change", myNote);
        }

        public void Add(Note myNote)
        {
            blModel.ToDo("Create", myNote);
        }

        public void Delete(Note myNote)
        {
            blModel.ToDo("Delete", myNote);
        }

        public override void ViewWillAppear(bool animated)
        {
            List<Note> fromDB = new List<Note>();

            fromDB = observer.GetAll();

            var activeNavCont = this.NavigationController;

            if (activeNavCont is ActiveNC)
            {
                dataToShow = fromDB.Where(i => i.Status == true).ToList();
            }
            else
            {
                dataToShow = fromDB.Where(i => i.Status == false).ToList();
            }

            TableSourceForNotesList noteSource = new TableSourceForNotesList(dataToShow);

            TableViewListNotes.Source = noteSource;

            noteSource.DeletingEvent += Delete;

            TableViewListNotes.RowHeight = UITableView.AutomaticDimension;
            TableViewListNotes.EstimatedRowHeight = 80f;
            TableViewListNotes.ReloadData();
        }
    }
}