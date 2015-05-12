using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace AnatomyLabeller
{
    public partial class frmMain : Form
    {
        Rectangle imageRect = new Rectangle(303, 26, 753, 597);
        Image currentImage;
        FlashCard currentCard;
        Label currentLabel;
        int currentLabelIndex;

        enum AnswerState { None, Correct, Incorrect };

        List<AnswerState> labelAnswerStates;

        string filePath;

        public frmMain()
        {
            InitializeComponent();
        }

        private void createNewFlashCardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CardCreator flashCardMaker = new CardCreator();

            if (flashCardMaker.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                lstCardList.Items.Add(flashCardMaker.newFlashCard);
            }
        }

        private void frmMain_Paint(object sender, PaintEventArgs e)
        {
            if (currentImage != null)
                e.Graphics.DrawImage(currentImage, imageRect);

            if (currentCard != null)
            {
                //Draw the labels
                Label[] labels = currentCard.GetLabels();

                for (int i = 0; i < labels.Length; i++)
                {
                    Rectangle drawRect = labels[i].GetDrawingRectangle();

                    drawRect.X += imageRect.X;
                    drawRect.Y += imageRect.Y;

                    DrawLabel(e, drawRect, labels[i].GetID().ToString(), labelAnswerStates[i]);
                }
            }
        }

        private void lstCardList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstCardList.SelectedIndex >= 0 && lstCardList.SelectedIndex < lstCardList.Items.Count)
            {
                currentCard = (FlashCard)lstCardList.Items[lstCardList.SelectedIndex];

                currentImage = Image.FromFile(currentCard.GetImageFilepath());
                imageRect.Size = currentCard.GetImageSize();

                labelAnswerStates = new List<AnswerState>();

                for (int i = 0; i < currentCard.GetLabels().Length; i++)
                {
                    labelAnswerStates.Add(AnswerState.None);
                }

                ResetAnswers();

                Invalidate();
            }
        }


        private void DrawLabel(PaintEventArgs e, Rectangle drawRect, string labelID, AnswerState answerState)
        {
            e.Graphics.DrawEllipse(System.Drawing.Pens.Black, drawRect);

            switch (answerState)
            {
                case AnswerState.None:
                    e.Graphics.FillEllipse(Brushes.Yellow, drawRect);
                    break;
                case AnswerState.Correct:
                    e.Graphics.FillEllipse(Brushes.Green, drawRect);
                    break;
                case AnswerState.Incorrect:
                    e.Graphics.FillEllipse(Brushes.Red, drawRect);
                    break;
                default:
                    break;
            }

            FontFamily fontFamily = new FontFamily("Arial");
            Font font = new Font(fontFamily, 16, FontStyle.Regular, GraphicsUnit.Pixel);
            e.Graphics.DrawString(labelID, font, Brushes.Black,
                new PointF(drawRect.X, drawRect.Y));
        }

        private void saveListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (dlgSave)
            {
                dlgSave.AddExtension = true;
                dlgSave.DefaultExt = ".xml";
                dlgSave.Filter = "XML Files (*.xml)|*.xml";
                dlgSave.OverwritePrompt = true;

                if (filePath != null)
                    SaveFile(filePath);
                else 
                {
                    if (dlgSave.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        SaveFile(dlgSave.FileName);
                    }
                }
            }
        }

        private void SaveFile(string filePath)
        {
            XmlTextWriter xmlWriter = new XmlTextWriter(filePath, null);

            xmlWriter.WriteStartDocument();

            xmlWriter.WriteStartElement("FlashSet");

            foreach (FlashCard card in lstCardList.Items)
            {
                xmlWriter.WriteStartElement("FlashCard");

                xmlWriter.WriteAttributeString("CardName", card.GetCardName());

                xmlWriter.WriteAttributeString("Image", card.GetImageFilepath());
                xmlWriter.WriteStartAttribute("ImageSizeW"); xmlWriter.WriteValue(card.GetImageSize().Width); xmlWriter.WriteEndAttribute();
                xmlWriter.WriteStartAttribute("ImageSizeH"); xmlWriter.WriteValue(card.GetImageSize().Height); xmlWriter.WriteEndAttribute();

                foreach (Label label in card.GetLabels())
                {
                    xmlWriter.WriteStartElement("Label");

                    xmlWriter.WriteStartAttribute("ID"); xmlWriter.WriteValue(label.GetID()); xmlWriter.WriteEndAttribute();
                    xmlWriter.WriteAttributeString("Name", label.GetName());
                    xmlWriter.WriteStartAttribute("OriginX"); xmlWriter.WriteValue(label.GetOrigin().X); xmlWriter.WriteEndAttribute();
                    xmlWriter.WriteStartAttribute("OriginY"); xmlWriter.WriteValue(label.GetOrigin().Y); xmlWriter.WriteEndAttribute();

                    xmlWriter.WriteEndElement();
                }

                xmlWriter.WriteEndElement();
                
            }

            xmlWriter.WriteEndElement();

            xmlWriter.WriteEndDocument();

            xmlWriter.Close();

            this.filePath = filePath;
            SetFormText();
        }

        private void loadListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (dlgOpen)
            {
                dlgOpen.CheckFileExists = true;
                dlgOpen.CheckPathExists = true;
                dlgOpen.DefaultExt = ".xml";
                dlgOpen.Filter = "XML Files (*.xml) | *.xml";
                dlgOpen.Multiselect = false;

                if (dlgOpen.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    LoadFile(dlgOpen.FileName);

                    this.filePath = dlgOpen.FileName;

                    SetFormText();
                }
            }
        }

        private void LoadFile(string filePath)
        {
            using (XmlReader reader = XmlReader.Create(filePath))
            {
                FlashCard flashCard = new FlashCard();
                Label label = new Label();

                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        if (reader.Name == "FlashCard")
                        {
                            flashCard = new FlashCard();
                            flashCard.SetCardName(reader.GetAttribute("CardName"));
                            Size size = new Size(Convert.ToInt32(reader.GetAttribute("ImageSizeW")), Convert.ToInt32(reader.GetAttribute("ImageSizeH")));
                            flashCard.SetImage(reader.GetAttribute("Image"), size);
                        }

                        if (reader.Name == "Label")
                        {
                            label = new Label();
                            label.SetID(Convert.ToUInt32(reader.GetAttribute("ID")));
                            label.SetName(reader.GetAttribute("Name"));
                            Point point = new Point();
                            point.X = Convert.ToInt32(reader.GetAttribute("OriginX"));
                            point.Y = Convert.ToInt32(reader.GetAttribute("OriginY"));

                            label.SetPosition(point);

                            flashCard.AddLabel(label);
                        }
                    }
                    else if (reader.NodeType == XmlNodeType.EndElement)
                    {
                        if(reader.Name != "FlashSet")
                            lstCardList.Items.Add(flashCard);
                    }
                }
            }
        }

        private bool ClickedWithinImage(int x, int y)
        {
            return imageRect.Contains(x, y);
        }

        private void frmMain_MouseClick(object sender, MouseEventArgs e)
        {
            if (ClickedWithinImage(e.X, e.Y))
            {
                if (currentCard != null)
                {
                    Label[] labels = currentCard.GetLabels();

                    for (int i = 0; i < labels.Length; i++)
                    {
                        if (labels[i].MouseClick(new Point(e.X - imageRect.X, e.Y - imageRect.Y)))
                        {
                            if (labelAnswerStates[i] == AnswerState.None)
                            {
                                currentLabel = labels[i];
                                lblLabelID.Text = "Label " + currentLabel.GetID() + ":";
                                txtAnswer.Text = string.Empty;
                                currentLabelIndex = i;

                                picResult.Image = Properties.Resources.Neutral;
                            }
                        }
                    }
                }
            }
        }

        private void btnSubmitAnswer_Click(object sender, EventArgs e)
        {
            if (currentLabel != null)
            {
                if (labelAnswerStates[currentLabelIndex] == AnswerState.None)
                {
                    if (txtAnswer.Text == string.Empty)
                        MessageBox.Show("Please input an answer in the text box");
                    else 
                    {
                        ProcessAnswer();
                        Invalidate();
                    }
                }
            }
        }

        void ProcessAnswer()
        {
            if (currentLabel.GetName().ToUpper() == txtAnswer.Text.ToUpper())
            {
                labelAnswerStates[currentLabelIndex] = AnswerState.Correct;
                picResult.Image = Properties.Resources.Correct;
            }
            else
            {
                labelAnswerStates[currentLabelIndex] = AnswerState.Incorrect;
                picResult.Image = Properties.Resources.Incorrect;
            }

            lblCorrectAnswer.Text = "Correct Answer: " + currentLabel.GetName();

            bool allAnswered = true;

            int correctAnswers = 0;

            for (int i = 0; i < labelAnswerStates.Count; i++)
            {
                if (labelAnswerStates[i] == AnswerState.None)
                {
                    allAnswered = false;
                    break;
                }
                else if (labelAnswerStates[i] == AnswerState.Correct)
                {
                    correctAnswers++;
                }
            }

            if (allAnswered)
            {
                if (MessageBox.Show("Your Results are: " + correctAnswers + "/" + labelAnswerStates.Count + 
                    "\nWould you like to reset your answers?", "Results", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    ResetAnswers();
                }
            }
            
        }

        void ResetAnswers()
        {
            for (int i = 0; i < labelAnswerStates.Count; i++)
            {
                labelAnswerStates[i] = AnswerState.None;
            }

            lblLabelID.Text = "Label: ";
            txtAnswer.Text = string.Empty;
            picResult.Image = Properties.Resources.Neutral;
            lblCorrectAnswer.Text = string.Empty;
        }

        private void btnResetAnswers_Click(object sender, EventArgs e)
        {
            ResetAnswers();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (dlgSave)
            {
                dlgSave.AddExtension = true;
                dlgSave.DefaultExt = ".xml";
                dlgSave.Filter = "XML Files (*.xml)|*.xml";
                dlgSave.OverwritePrompt = true;

                if (dlgSave.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    SaveFile(dlgSave.FileName);
                }
                
            }
        }

        private void lstCardList_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void lstCardList_KeyDown(object sender, KeyEventArgs e)
        {
            if (lstCardList.SelectedIndex >= 0)
            {
                if (e.KeyCode == Keys.Delete)
                {
                    FlashCard card = (FlashCard)lstCardList.Items[lstCardList.SelectedIndex];

                    if (card != null)
                    {
                        if (MessageBox.Show("Are you sure you want to delete " + card.GetCardName() + "?",
                            "Remove Flash Card?", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                        {
                            lstCardList.Items.RemoveAt(lstCardList.SelectedIndex);
                            currentCard = null;
                            currentImage = null;
                            currentLabelIndex = -1;
                            currentLabel = null;

                            ResetAnswers();

                            Invalidate();
                        }
                    }
                }
            }
        }

        private void SetFormText()
        {
            int lastIndex = filePath.LastIndexOf("\\") + 1;
            string newText = filePath.Substring(lastIndex , filePath.Length - lastIndex);
            this.Text = "Anatomy Labeller: " + newText;
        }

    }
}
