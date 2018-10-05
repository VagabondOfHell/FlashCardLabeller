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
		private const int labelCapacity = 8;

        private string cardName = "";

		private string cardBitmapFilepath = "";

		private List<Label> cardLabels = new List<Label> ( labelCapacity );

        private Size imageSize;

        public FlashCard()
        {
            
        }

		/// <summary>
		/// Class constructor
		/// </summary>
		/// <param name="name">Card name</param>
		/// <param name="bitmapFilepath">File path to image</param>
        public FlashCard(string name, string bitmapFilepath = null)
        {
            CardName = name;

			ImageFilepath = bitmapFilepath;
        }

		public string CardName
		{
			get
			{
				return cardName;
			}
			set
			{
				cardName = value;
			}
		}

		public string ImageFilepath
		{
			get
			{
				return cardBitmapFilepath;
			}
			set
			{
				cardBitmapFilepath = value;
			}
		}

		public Size ImageSize
		{
			get
			{
				return imageSize;
			}
			set
			{
				imageSize = value;
			}
		}

		public Label [ ] Labels
		{
			get
			{
				return cardLabels.ToArray ( );
			}
		}

		/// <summary>
		/// Adds a label
		/// </summary>
		/// <param name="newLabel"Label to be added></param>
		public void AddLabel(Label newLabel)
        {
			if ( !cardLabels.Contains(newLabel))
			{
				cardLabels.Add(newLabel);
			}   
        }

        /// <summary>
        /// Checks if the specified ID is contained in the list
        /// </summary>
        /// <param name="id">The ID to test</param>
        /// <returns>True if it contains the id, false if not</returns>
        public bool ContainsID(int id)
        {
			var label = cardLabels.Where ( card => card.ID == id ).FirstOrDefault();

			return label == null ? false : true;
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
