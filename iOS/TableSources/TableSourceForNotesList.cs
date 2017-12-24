using System;
using UIKit;
using Foundation;
using XamarinNotesNew.Classes;
using System.Collections.Generic;
using System.IO;
using XamarinNotesNew.iOS.Classes;

namespace XamarinNotesNew.iOS
{
    public class TableSourceForNotesList: UITableViewSource
    {
        string cellID = "NoteCell";

        SourceOfNotes source = new SourceOfNotes();

        public delegate void DeletingDelegate(Note item);

        public event DeletingDelegate DeletingEvent;

        public TableSourceForNotesList(List<Note> items)
        {
            source.Source = items;
        }

        public override UITableViewCell GetCell(UITableView tableView, Foundation.NSIndexPath indexPath)
        {
            ListNoteCell myCell = (ListNoteCell)tableView.DequeueReusableCell(cellID);

            if (source.Source[indexPath.Row] != null)
            {
                myCell.Attachment = "";

                if (source.Source[indexPath.Row].Attachments.Count > 0) 
                {
                    foreach(var item in source.Source[indexPath.Row].Attachments)
                    {
                        if (myCell.Attachment == "")
                        {
                            myCell.Attachment = item.Name;
                        }
                        else 
                        {
                            myCell.Attachment = myCell.Attachment + ", " + item.Name;   
                        }
                    }
                }
               
                myCell.Date = $"{source.Source[indexPath.Row].DateOfCreation}";
                myCell.Text = source.Source[indexPath.Row].Text;
            }

            return myCell;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            if (source.Source != null)
            {
                return source.Source.Count;
            }
            return 0;
        }

        public override void CommitEditingStyle(UITableView tableView, UITableViewCellEditingStyle editingStyle, Foundation.NSIndexPath indexPath)
        {
            switch (editingStyle)
            {
                case UITableViewCellEditingStyle.Delete:
                    // remove the item from the underlying data source
                    Note noteForDeleting = new Note();
                    noteForDeleting = source.Source[indexPath.Row];

                    source.DeleteFromSource(indexPath.Row);

                    // delete the row from the table
                    tableView.DeleteRows(new NSIndexPath[] { indexPath }, UITableViewRowAnimation.Fade);


                    if (DeletingEvent != null)
                    {
                        DeletingEvent.Invoke(noteForDeleting);
                    }

                    break;
                case UITableViewCellEditingStyle.None:
                    Console.WriteLine("CommitEditingStyle:None called");
                    break;
            }
        }

        public override bool CanEditRow(UITableView tableView, NSIndexPath indexPath)
        {
            return true; // return false if you wish to disable editing for a specific indexPath or for all rows
        }
        public override string TitleForDeleteConfirmation(UITableView tableView, NSIndexPath indexPath)
        {   // Optional - default text is 'Delete'
            return "Trash ("+ source.Source[indexPath.Row].Text + ")";
        }

        public Note GetItem(int id)
        {
            return source.Source[id];
        }
    }
}
