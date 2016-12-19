using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sabateur
{
    public partial class GameForm : Form
    {
        private const int FieldWidth = 9;
        private const int FieldHeight = 5;
        private PictureBox[,] fieldTiles = new PictureBox[FieldWidth, FieldHeight];

        private void ShowField()
        {
            foreach (PictureBox fieldTile in fieldTiles)
            {
                fieldTile.Image = Properties.Resources.EmptyTile;
            }
            foreach (CardSet card in Game.Cards.Where(card => card.Owner == "Field"))
            {
                if (card.Type == CardType.Path) fieldTiles[(int)card.FieldCol - 1, (int)card.FieldRow - 1].Image = new Bitmap(CardSpriteGenerator.GenerateCardSprite(card), new Size(85, 111));
                if (card.Type == CardType.GoalPath) fieldTiles[(int)card.FieldCol - 1, (int)card.FieldRow - 1].Image = Properties.Resources.BackCard;
            }
        }
        private void FieldTile_Click(object sender, EventArgs e)
        {
            Invoke(TileSelectMethods[Game.Player.SelectedCard.Type], sender);
        }
        private void InitializeFieldTiles()
        {
            fieldTiles[0, 0] = PictureBoxField1x1;
            fieldTiles[0, 1] = PictureBoxField1x2;
            fieldTiles[0, 2] = PictureBoxField1x3;
            fieldTiles[0, 3] = PictureBoxField1x4;
            fieldTiles[0, 4] = PictureBoxField1x5;
            fieldTiles[1, 0] = PictureBoxField2x1;
            fieldTiles[1, 1] = PictureBoxField2x2;
            fieldTiles[1, 2] = PictureBoxField2x3;
            fieldTiles[1, 3] = PictureBoxField2x4;
            fieldTiles[1, 4] = PictureBoxField2x5;
            fieldTiles[2, 0] = PictureBoxField3x1;
            fieldTiles[2, 1] = PictureBoxField3x2;
            fieldTiles[2, 2] = PictureBoxField3x3;
            fieldTiles[2, 3] = PictureBoxField3x4;
            fieldTiles[2, 4] = PictureBoxField3x5;
            fieldTiles[3, 0] = PictureBoxField4x1;
            fieldTiles[3, 1] = PictureBoxField4x2;
            fieldTiles[3, 2] = PictureBoxField4x3;
            fieldTiles[3, 3] = PictureBoxField4x4;
            fieldTiles[3, 4] = PictureBoxField4x5;
            fieldTiles[4, 0] = PictureBoxField5x1;
            fieldTiles[4, 1] = PictureBoxField5x2;
            fieldTiles[4, 2] = PictureBoxField5x3;
            fieldTiles[4, 3] = PictureBoxField5x4;
            fieldTiles[4, 4] = PictureBoxField5x5;
            fieldTiles[5, 0] = PictureBoxField6x1;
            fieldTiles[5, 1] = PictureBoxField6x2;
            fieldTiles[5, 2] = PictureBoxField6x3;
            fieldTiles[5, 3] = PictureBoxField6x4;
            fieldTiles[5, 4] = PictureBoxField6x5;
            fieldTiles[6, 0] = PictureBoxField7x1;
            fieldTiles[6, 1] = PictureBoxField7x2;
            fieldTiles[6, 2] = PictureBoxField7x3;
            fieldTiles[6, 3] = PictureBoxField7x4;
            fieldTiles[6, 4] = PictureBoxField7x5;
            fieldTiles[7, 0] = PictureBoxField8x1;
            fieldTiles[7, 1] = PictureBoxField8x2;
            fieldTiles[7, 2] = PictureBoxField8x3;
            fieldTiles[7, 3] = PictureBoxField8x4;
            fieldTiles[7, 4] = PictureBoxField8x5;
            fieldTiles[8, 0] = PictureBoxField9x1;
            fieldTiles[8, 1] = PictureBoxField9x2;
            fieldTiles[8, 2] = PictureBoxField9x3;
            fieldTiles[8, 3] = PictureBoxField9x4;
            fieldTiles[8, 4] = PictureBoxField9x5;

            foreach (PictureBox fieldTile in fieldTiles)
            {
                fieldTile.Click += FieldTile_Click;
            }
        }
    }
}
