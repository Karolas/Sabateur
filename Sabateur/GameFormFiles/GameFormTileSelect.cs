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
        private delegate void TileSelectDelegate(object sender);
        private Dictionary<CardType, Delegate> TileSelectMethods;

        private void AsignTileSelectDelegates()
        {
            TileSelectMethods = new Dictionary<CardType, Delegate>()
            {
                {CardType.Path, new TileSelectDelegate(TileSelectPathCard)},
                {CardType.Map, new TileSelectDelegate(TileSelectMap)},
                {CardType.RemovePath, new TileSelectDelegate(TileSelectRemovePath)},
            };
        }

        private void TileSelectPathCard(object sender)
        {
            BlockType? playerBlocks = Game.Players.Where(player => player.Name == Game.Player.Name).First().Blocks;
            if (playerBlocks == null || playerBlocks == 0)
            {
                List<int[]> availableSpots = Game.AvailableFieldTiles(Game.Player.SelectedCard);
                for (int i = 0; i < availableSpots.Count; i++)
                {
                    if (fieldTiles[availableSpots[i][0] - 1, availableSpots[i][1] - 1] == sender)
                    {
                        fieldTiles[availableSpots[i][0] - 1, availableSpots[i][1] - 1].Image = new Bitmap(CardSpriteGenerator.GetApropriateSprite(Game.Player.SelectedCard), new Size(85, 111));
                        Game.Player.SelectedCard.FieldCol = availableSpots[i][0];
                        Game.Player.SelectedCard.FieldRow = availableSpots[i][1];
                        Game.Player.SelectedCard.Owner = "Field";
                        Game.CheckGoalCardReached(Game.Player.SelectedCard);
                        EndTurn();
                        break;
                    }
                }
                ShowField();
            }
        }

        private void TileSelectMap(object sender)
        {
            CardSet[] goalCards = Game.Cards.Where(card => card.Type == CardType.GoalPath).ToArray();

            for (int i = 0; i < goalCards.Count(); i++)
            {
                if (fieldTiles[(int)goalCards[i].FieldCol - 1, (int)goalCards[i].FieldRow - 1] == sender)
                {
                    if (goalCards[i].Path == (Direction.Down | Direction.Left | Direction.Right | Direction.Up))
                    {
                        LabelGoalCard.Text = "Gold";
                    }
                    else
                    {
                        LabelGoalCard.Text = "Coal";
                    }
                    HideHand();
                    Game.DiscardCard(Game.Player.SelectedCard);
                    ButtonEndTurn.Show();
                    ButtonDiscardCard.Hide();
                    break;
                }
            }
        }

        private void TileSelectRemovePath(object sender)
        {
            List<CardSet> cards = Game.Cards.Where(card => card.Owner == "Field" && card.Type == CardType.Path).ToList();
            for (int i = 0; i < cards.Count(); i++)
            {
                if (fieldTiles[(int)cards[i].FieldCol - 1, (int)cards[i].FieldRow - 1] == sender)
                {
                    Game.DiscardCard(cards[i]);
                    EndTurn();
                    break;
                }
            }
        }
    }
}
