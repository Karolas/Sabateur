using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sabateur
{
    public partial class GameForm : Form
    {
        PictureBox[] HandCardPictureBoxes = new PictureBox[5];
        private void HandCardSelected(object sender, EventArgs e)
        {
            HideControls();
            ShowField();

            if(CardSelectMethods.ContainsKey(Game.Player.SelectedCard.Type))
            {
                Invoke(CardSelectMethods[Game.Player.SelectedCard.Type]);
            }
        }

        private void ShowHand()
        {
            for(int i = 0; i < HandCardPictureBoxes.Count(); i++)
            {
                HandCardPictureBoxes[i].Show();
                HandCardPictureBoxes[i].Image = CardSpriteGenerator.GetApropriateSprite(Game.Player.OwnedCards[i]);
            }
        }

        private void HideHand()
        {
            foreach(PictureBox pictureBox in HandCardPictureBoxes)
            {
                pictureBox.Hide();
            }
        }
        private void PictureBoxCard1_Click(object sender, EventArgs e)
        {
            Game.Player.SelectedCard = Game.Player.OwnedCards[0];
        }

        private void PictureBoxCard2_Click(object sender, EventArgs e)
        {
            Game.Player.SelectedCard = Game.Player.OwnedCards[1];
        }

        private void PictureBoxCard3_Click(object sender, EventArgs e)
        {
            Game.Player.SelectedCard = Game.Player.OwnedCards[2];
        }

        private void PictureBoxCard4_Click(object sender, EventArgs e)
        {
            Game.Player.SelectedCard = Game.Player.OwnedCards[3];
        }

        private void PictureBoxCard5_Click(object sender, EventArgs e)
        {
            Game.Player.SelectedCard = Game.Player.OwnedCards[4];
        }
        private void InitializeHandComponents()
        {
            HandCardPictureBoxes[0] = PictureBoxCard1;
            HandCardPictureBoxes[1] = PictureBoxCard2;
            HandCardPictureBoxes[2] = PictureBoxCard3;
            HandCardPictureBoxes[3] = PictureBoxCard4;
            HandCardPictureBoxes[4] = PictureBoxCard5;

            foreach(PictureBox pictureBox in HandCardPictureBoxes)
            {
                pictureBox.Click += HandCardSelected;
            }
        }
    }
}
