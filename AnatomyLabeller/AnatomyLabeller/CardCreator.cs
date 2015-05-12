using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnatomyLabeller
{
    public partial class CardCreator : Form
    {
        private bool isFirstClick = false;
        private int milliseconds = 0;
        private Timer doubleClickTimer = new Timer();
        MouseEventArgs mouseEventForFirstClick;

        public FlashCard newFlashCard;

        const string ADD_LABEL_ADD_TEXT = "Add Label";
        const string ADD_LABEL_EDIT_TEXT = "Edit Label";


        /// <summary>
        /// The current modification state
        /// Clear - No actions being performed
        /// Placement - Placing the currently selected label
        /// </summary>
        enum State { Clear, Placement };

        State currentState = State.Clear;

        Label currentLabel;
        Point originalLabelPosition;

        //The position that the image is drawn at
        Point imagePosition = new Point(416, 32);

        Image image;

        Point labelMousePosition;


        public CardCreator()
        {
            InitializeComponent();
            newFlashCard = new FlashCard("");

            doubleClickTimer.Interval = 10;
            doubleClickTimer.Tick +=
                new EventHandler(doubleClickTimer_Tick);

        }

        private void btnBrowseFolders_Click(object sender, EventArgs e)
        {
            using (dlgOpen)
            {
                dlgOpen.CheckFileExists = true;
                dlgOpen.CheckPathExists = true;
                dlgOpen.Filter = "Image Files (*.bmp, *.jpg, *.gif, *.png, *.tiff)|*.bmp;*.jpg;*.gif;*.png;*.tiff;";
                dlgOpen.Multiselect = false;

                //Show the dialog and check for acceptance
                if (dlgOpen.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (!string.IsNullOrWhiteSpace(dlgOpen.FileName))
                    {
                        //Set the file path and image
                        txtFilePath.Text = dlgOpen.FileName;

                        image = Image.FromFile(dlgOpen.FileName);
                        
                        //Get image size
                        Size imageSize = image.Size;

                        //Set the Number Box values for current size
                        numWidth.Value = imageSize.Width;
                        numHeight.Value = imageSize.Height;

                        this.Invalidate();
                    }
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCardName.Text))
            {
                MessageBox.Show("Please enter a valid name for this flash card", "Invalid Entry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (image == null)
            {
                MessageBox.Show("Please use a valid image for this flash card", "Invalid Entry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (newFlashCard.GetLabels().GetLength(0) == 0)
            {
                MessageBox.Show("Please create at least one label for this flash card", "Invalid Entry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //Set card properties
            newFlashCard.SetCardName(txtCardName.Text);
            newFlashCard.SetImage(txtFilePath.Text, new Size((int)numWidth.Value, (int)numHeight.Value));
            
            btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void CardCreator_Paint(object sender, PaintEventArgs e)
        {
            //Draw the image
            if (image != null)
            {
                e.Graphics.DrawImage(image, new Rectangle(imagePosition.X, imagePosition.Y, (int)numWidth.Value, (int)numHeight.Value));
            }

            //Draw the labels
            Label[] labels = newFlashCard.GetLabels();

            foreach (Label label in labels)
            {
                Rectangle drawRect = label.GetDrawingRectangle();

                if (label != currentLabel)
                {
                    //Draw with the offset of our image position
                    drawRect.X += imagePosition.X;
                    drawRect.Y += imagePosition.Y;
                }
                else
                {
                    if (currentState == State.Placement)
                    {
                        drawRect.X = labelMousePosition.X;
                        drawRect.Y = labelMousePosition.Y;
                    }
                    else
                    {
                        //Draw with the offset of our image position
                        drawRect.X += imagePosition.X;
                        drawRect.Y += imagePosition.Y;
                        
                    }
                }
                DrawLabel(e, drawRect, label.GetID().ToString());
            }

        }

        private void DrawLabel(PaintEventArgs e, Rectangle drawRect, string labelID)
        {
            e.Graphics.DrawEllipse(System.Drawing.Pens.Black, drawRect);
            e.Graphics.FillEllipse(Brushes.Yellow, drawRect);

            FontFamily fontFamily = new FontFamily("Arial");
            Font font = new Font(fontFamily, 16, FontStyle.Regular, GraphicsUnit.Pixel);
            e.Graphics.DrawString(labelID, font, Brushes.Black, 
                new PointF(drawRect.X, drawRect.Y));
 
        }

        private void numWidth_ValueChanged(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        /// <summary>
        /// Adds a label to the flash card or edits the selected label's name
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddLabel_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLabelName.Text))
            {
                MessageBox.Show("Please enter a valid name for the label", "Invalid Entry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //Check for valid image
            if (image == null)
            {
                MessageBox.Show("Please use a valid image for this flash card before creating labels",
                    "Invalid Entry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (newFlashCard.ContainsID((int)numLabelID.Value))
            {
                MessageBox.Show("That Label ID is already in use. Please use a valid ID for this label",
                   "Invalid Entry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (btnAddLabel.Text == ADD_LABEL_ADD_TEXT)
            {
                //If we are currently manipulating a label, reset it
                ResetCurrentLabel(true);

                currentLabel = new Label((int)numLabelID.Value, txtLabelName.Text);
                currentState = State.Placement;
            }
            else if (btnAddLabel.Text == ADD_LABEL_EDIT_TEXT)
            {
                if (currentLabel != null)
                {
                    newFlashCard.RemoveLabel(currentLabel);
                    currentLabel.SetName(txtLabelName.Text);
                    currentLabel.SetID((uint)numLabelID.Value);
                    newFlashCard.AddLabel(currentLabel);
                    
                    ResetCurrentLabel(true);
                    btnAddLabel.Text = ADD_LABEL_ADD_TEXT;
                    txtLabelName.Clear();

                    Invalidate();
                }
            }
        }

        /// <summary>
        /// Resets the current label to null, along with Original Label Position
        /// </summary>
        /// <param name="useOriginalPosition">True to apply original position before nulling</param>
        private void ResetCurrentLabel(bool useOriginalPosition = false)
        {
            if (currentLabel != null)
            {
                if (useOriginalPosition)
                {
                    currentLabel.SetPosition(originalLabelPosition);
                }

                currentLabel = null;
                originalLabelPosition = new Point(-1, -1);
            }
        }

        /// <summary>
        /// Checks if the mouse is within image bounds
        /// </summary>
        /// <param name="x">X coordinate of the mouse</param>
        /// <param name="y">Y coordinate of the mouse</param>
        /// <returns>True if within bounds, false if not</returns>
        private bool ClickedWithinImage(int x, int y)
        { 
            if(image != null)
            {
                Size imageSize = image.Size;

                if (x >= imagePosition.X && x <= imagePosition.X + imageSize.Width &&
                    y >= imagePosition.Y && y <= imagePosition.Y + imageSize.Height)
                    return true;
            }

            return false;
        }

        private void CardCreator_MouseClick(object sender, MouseEventArgs e)
        {
            // This is the first mouse click. 
            if (!isFirstClick)
            {
                isFirstClick = true;
                mouseEventForFirstClick = e;

                // Start the double click timer.
                doubleClickTimer.Start();
            }
            // This is the second mouse click. 
            else
            {
                if (milliseconds < SystemInformation.DoubleClickTime)
                {
                    doubleClickTimer.Stop();

                    // Allow the MouseDown event handler to process clicks again.
                    isFirstClick = false;
                    milliseconds = 0;

                    ProcessDoubleClick(e);
                }
            }
 
        }

        private void CardCreator_MouseMove(object sender, MouseEventArgs e)
        {
            if (currentState == State.Placement)
            {
                if (currentLabel != null)
                {
                    labelMousePosition = new Point(e.X, e.Y);
                    this.Invalidate();
                }
            }
        }

        private void CardCreator_MouseDown(object sender, MouseEventArgs e)
        {

        }

        void doubleClickTimer_Tick(object sender, EventArgs e)
        {
            milliseconds += doubleClickTimer.Interval;

            // The timer has reached the double click time limit. 
            if (milliseconds >= SystemInformation.DoubleClickTime)
            {
                doubleClickTimer.Stop();

                // Allow the MouseDown event handler to process clicks again.
                isFirstClick = false;
                milliseconds = 0;

                ProcessSingleClick(mouseEventForFirstClick);                
            }
        }

        void ProcessDoubleClick(MouseEventArgs e)
        {
            if (ClickedWithinImage(e.X, e.Y))
            {
                switch (currentState)
                {
                    case State.Clear:
                        //Make sure that we are editting
                        if (currentLabel == null)
                        {
                            Label[] labels = newFlashCard.GetLabels();

                            foreach (Label label in labels)
                            {
                                //Check if we clicked on the label
                                if (label.MouseClick(new Point(e.X - imagePosition.X, e.Y - imagePosition.Y)))
                                {
                                    //Get the label
                                    originalLabelPosition = label.GetOrigin();
                                    currentLabel = label;
                                    lblCurrentLabelName.Text = currentLabel.GetName();

                                    currentState = State.Placement;
                                    break;
                                }
                            }
                        }
                        break;
                    case State.Placement:
                        break;
                }
            }
        }

        void ProcessSingleClick(MouseEventArgs e)
        {
            if (ClickedWithinImage(e.X, e.Y))
            {
                switch (currentState)
                {
                    case State.Clear:
                        //Make sure that we are editting
                        if (currentLabel == null)
                        {
                            Label[] labels = newFlashCard.GetLabels();

                            foreach (Label label in labels)
                            {
                                if (label.MouseClick(new Point(e.X - imagePosition.X, e.Y - imagePosition.Y)))
                                {
                                    originalLabelPosition = label.GetOrigin();
                                    currentLabel = label;

                                    txtLabelName.Text = currentLabel.GetName();
                                    btnAddLabel.Text = "Edit Label";
                                    break;
                                }
                            }
                        }

                        break;

                    case State.Placement:
                        if (currentLabel != null)
                        {
                            //Check if there is an identical entry to be removed
                            newFlashCard.RemoveLabel(currentLabel);

                            //Set the position to be the mouse position minus the image position
                            //so that labels only store relative position
                            currentLabel.SetPosition(new Point(e.X - imagePosition.X, e.Y - imagePosition.Y));

                            newFlashCard.AddLabel(currentLabel);

                            ResetCurrentLabel(false);
                            currentState = State.Clear;

                            txtLabelName.Text = string.Empty;

                            this.Invalidate();
                        }
                        else
                            //We can't place a null label, so set status to clear
                            currentState = State.Clear;
                        break;
                }
            }
        }

    }
}
