using Foundation;
using System;
using UIKit;

namespace XamarinNotesNew.iOS
{
    public partial class AttachmentListCell : UITableViewCell
    {
        public string Name
        {
            get { return FileName.Text; }
            set { FileName.Text = value; }
        }

        public string Extension
        {
            get { return AttachmentExtension.Text; }
            set { AttachmentExtension.Text = value; }
        }

        public AttachmentListCell (IntPtr handle) : base (handle)
        {
        }


    }
}