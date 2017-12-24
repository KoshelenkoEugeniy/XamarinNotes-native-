using System;
using System.Collections.Generic;
using XamarinNotesNew.Classes;
using UIKit;
using Foundation;
using System.IO;
using XamarinNotesNew.iOS.Classes;
using XamarinNotesNew.iOS.Interfaces;

namespace XamarinNotesNew.iOS.TableSources
{
    class TableSourceForAttachmentsList : UITableViewSource
    {
        string cellID = "AttachmentCell";

        SourceOfAttachments source = new SourceOfAttachments();


        public TableSourceForAttachmentsList() { }

        public TableSourceForAttachmentsList(List<Picture> items)
        {
            source.Source = items;
        }

        public override nint RowsInSection(UITableView tableView, nint section)
        {
            if (source.Source != null)
            {
                return source.Source.Count;
            }
            return 0;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {

            AttachmentListCell myCell = (AttachmentListCell)tableView.DequeueReusableCell(cellID);

            if (source.Source[indexPath.Row] != null)
            {
                myCell.Name = source.Source[indexPath.Row].Name;
                myCell.Extension = source.Source[indexPath.Row].Extension;
            }

            return myCell;
        }

        public override void CommitEditingStyle(UITableView tableView, UITableViewCellEditingStyle editingStyle, Foundation.NSIndexPath indexPath)
        {
            switch (editingStyle)
            {
                case UITableViewCellEditingStyle.Delete:
                    // remove the item from the underlying data source
                    source.DeleteFromSource(indexPath.Row);

                    // delete the row from the table
                    tableView.DeleteRows(new NSIndexPath[] { indexPath }, UITableViewRowAnimation.Fade);
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
            return "Trash (" + source.Source[indexPath.Row].Name + ")";
        }

        public Picture GetItem(int id)
        {
            return source.Source[id];
        }
    }
}

