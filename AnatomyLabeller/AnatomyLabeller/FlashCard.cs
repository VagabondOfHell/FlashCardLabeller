using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace AnatomyLabeller
{
    public class FlashCard
    {
        string cardName;
        List<Label> cardLabels;
        string cardBitmapFilepath;
        Size imageSize;

        public FlashCard()
        {
            cardName = "";
            cardBitmapFilepath = "";

            cardLabels = new List<Label>(8);
        }

        public FlashCard(string name, string bitmapFilepath = null)
        {
            cardName = name;
            cardBitmapFilepath = bitmapFilepath;

            cardLabels = new List<Label>(8);
        }

        public void AddLabel(Label newLabel)
        {
            if(!cardLabels.Contains(newLabel))
                cardLabels.Add(newLabel);
        }

        /// <summary>
        /// Checks if the specified ID is contained in the list
        /// </summary>
        /// <param name="id">The ID to test</param>
        /// <returns>True if it contains the id, false if not</returns>
        public bool ContainsID(int id)
        {
            foreach (Label label in cardLabels)
            {
                if (label.GetID() == id)
                    return true;
            }

            return false;
        }

        public string GetCardName()
        {
            return cardName;
        }

        public void SetCardName(string name)
        {
            cardName = name;
        }

        public string GetImageFilepath()
        {
            return cardBitmapFilepath;
        }

        public Size GetImageSize()
        {
            return imageSize;
        }

        /// <summary>
        /// Set the image that the flash card will use
        /// </summary>
        /// <param name="filePath">The path that directs to the image</param>
        /// <param name="size">The size of the image in the picturebox</param>
        public void SetImage(string filePath, Size size)
        {
            cardBitmapFilepath = filePath;
            imageSize = size;
        }

        public void SetFilePath(string imageFilePath)
        {
            cardBitmapFilepath = imageFilePath;
        }

        public void SetImageSize(Size size)
        {
            imageSize = size;
        }

        public Label[] GetLabels()
        {
            return cardLabels.ToArray();
        }

        /// <summary>
        /// Removes the specified label from the card
        /// </summary>
        /// <param name="label">The label to remove</param>
        /// <returns>True if successfully removed, false if not</returns>
        public bool RemoveLabel(Label label)
        {
            return cardLabels.Remove(label);
        }

        public override string ToString()
        {
            return cardName;
        }
    }
}
