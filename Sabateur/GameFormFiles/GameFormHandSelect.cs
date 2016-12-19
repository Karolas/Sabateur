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
        private delegate void SelectDelegate();
        private Dictionary<CardType, Delegate> CardSelectMethods;

        private void AsignSelectDelegates()
        {
            CardSelectMethods = new Dictionary<CardType, Delegate>()
            {
                { CardType.Path,  new SelectDelegate(PathCardSelected)},
                { CardType.Map,  new SelectDelegate(MapCardSelect)},
                { CardType.RemovePath,  new SelectDelegate(RemovePathCardSelect)},
                { CardType.Block,  new SelectDelegate(BlockCardSelected)},
                { CardType.AntiBlock,  new SelectDelegate(AntiblockCardSelected)},
            };
        }

        private void PathCardSelected()
        {
            PictureBoxPathPos1.Show();
            PictureBoxPathPos2.Show();
            ButtonDiscardCard.Show();

            Game.Player.SelectedCard.IsPathUpside = false;
            PictureBoxPathPos1.Image = new Bitmap(CardSpriteGenerator.GetApropriateSprite(Game.Player.SelectedCard), new Size(85, 111));
            CardSet flipedCard = new CardSet();
            flipedCard.Path = Game.Player.SelectedCard.Path;
            flipedCard.PathOpenings = Game.Player.SelectedCard.PathOpenings;
            flipedCard.Type = CardType.Path;
            flipedCard.IsPathUpside = true;
            PictureBoxPathPos2.Image = new Bitmap(CardSpriteGenerator.GetApropriateSprite(flipedCard), new Size(85, 111));
        }

        private void MapCardSelect()
        {
            ButtonDiscardCard.Show();
        }
        private void RemovePathCardSelect()
        {
            ButtonDiscardCard.Show();
        }

        private void BlockCardSelected()
        {
            ListBoxItems.Show();
            ButtonUseCard.Show();
            ButtonDiscardCard.Show();

            PlayerSet[] players = Game.Players.Where(player => player.Name != "Field" &&
                                                               player.Name != "Deck" &&
                                                               player.Name != "Graveyard" &&
                                                               player.Name != Game.Player.Name &&
                                                               !player.Blocks.Value.HasFlag(Game.Player.SelectedCard.BlockType)).ToArray();
            ListBoxItems.Items.AddRange(players);
        }

        private void AntiblockCardSelected()
        {
            ListBoxItems.Show();
            ButtonUseCard.Show();
            ButtonDiscardCard.Show();

            PlayerSet myPlayer = Game.Players.Where(player => player.Name == Game.Player.Name).First();

            if (myPlayer.Blocks.HasValue)
            {
                if (Game.Player.SelectedCard.BlockType.Value.HasFlag(BlockType.Cart))
                {
                    if (myPlayer.Blocks.Value.HasFlag(BlockType.Cart)) ListBoxItems.Items.Add(BlockType.Cart);
                }
                if (Game.Player.SelectedCard.BlockType.Value.HasFlag(BlockType.Lantern))
                {
                    if (myPlayer.Blocks.Value.HasFlag(BlockType.Lantern)) ListBoxItems.Items.Add(BlockType.Lantern);
                }
                if (Game.Player.SelectedCard.BlockType.Value.HasFlag(BlockType.Pickaxe))
                {
                    if (myPlayer.Blocks.Value.HasFlag(BlockType.Pickaxe)) ListBoxItems.Items.Add(BlockType.Pickaxe);
                }
            }
        }
    }
}
