using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabateur
{
    class CardSpriteGenerator
    {
        public static Bitmap GenerateCardSprite(CardSet card)
        {
            Bitmap sprite = new Bitmap(100, 131);
            Graphics spritePainter = Graphics.FromImage(sprite);

            spritePainter.FillRectangle(Brushes.DarkGray, new Rectangle(0, 0, sprite.Width, sprite.Height));
            spritePainter.FillRectangle(Brushes.DarkGray, new Rectangle(0, 0, sprite.Width, sprite.Height));

            if (card.Path.HasValue)
            {
                if (card.Path.Value.HasFlag(Direction.Down))
                {
                    if (!card.IsPathUpside.Value) spritePainter.FillRectangle(Brushes.PeachPuff, new Rectangle(38, 53, 24, 78));
                    else spritePainter.FillRectangle(Brushes.PeachPuff, new Rectangle(38, 0, 24, 78));
                }

                if (card.Path.Value.HasFlag(Direction.Right))
                {
                    if (!card.IsPathUpside.Value) spritePainter.FillRectangle(Brushes.PeachPuff, new Rectangle(38, 53, 62, 25));
                    else spritePainter.FillRectangle(Brushes.PeachPuff, new Rectangle(0, 53, 62, 25));
                }

                if (card.Path.Value.HasFlag(Direction.Left))
                {
                    if (!card.IsPathUpside.Value) spritePainter.FillRectangle(Brushes.PeachPuff, new Rectangle(0, 53, 62, 25));
                    else spritePainter.FillRectangle(Brushes.PeachPuff, new Rectangle(38, 53, 62, 25));
                }

                if (card.Path.Value.HasFlag(Direction.Up))
                {
                    if (!card.IsPathUpside.Value) spritePainter.FillRectangle(Brushes.PeachPuff, new Rectangle(38, 0, 24, 78));
                    else spritePainter.FillRectangle(Brushes.PeachPuff, new Rectangle(38, 53, 24, 78));
                }
            }

            if (card.PathOpenings.HasValue)
            {
                if (card.PathOpenings.Value.HasFlag(Direction.Up))
                {
                    if (!card.IsPathUpside.Value) spritePainter.FillRectangle(Brushes.PeachPuff, new Rectangle(38, 0, 24, 40));
                    else spritePainter.FillRectangle(Brushes.PeachPuff, new Rectangle(38, 91, 24, 40));
                }

                if (card.PathOpenings.Value.HasFlag(Direction.Right))
                {
                    if (!card.IsPathUpside.Value) spritePainter.FillRectangle(Brushes.PeachPuff, new Rectangle(70, 53, 30, 24));
                    else spritePainter.FillRectangle(Brushes.PeachPuff, new Rectangle(0, 53, 30, 24));
                }

                if (card.PathOpenings.Value.HasFlag(Direction.Down))
                {
                    if (!card.IsPathUpside.Value) spritePainter.FillRectangle(Brushes.PeachPuff, new Rectangle(38, 91, 24, 40));
                    else spritePainter.FillRectangle(Brushes.PeachPuff, new Rectangle(38, 0, 24, 40));
                }

                if (card.PathOpenings.Value.HasFlag(Direction.Left))
                {
                    if (!card.IsPathUpside.Value) spritePainter.FillRectangle(Brushes.PeachPuff, new Rectangle(0, 53, 30, 24));
                    else spritePainter.FillRectangle(Brushes.PeachPuff, new Rectangle(70, 53, 30, 24));
                }
            }

            return sprite;
        }

        public static Bitmap GetApropriateSprite(CardSet card)
        {
            if (card.Type == CardType.Path) return GenerateCardSprite(card);
            if (card.Type == CardType.Map) return Properties.Resources.Map;
            if (card.Type == CardType.RemovePath) return Properties.Resources.RemovePath;
            if (card.Type == CardType.Block)
            {
                if (card.BlockType == BlockType.Cart) return Properties.Resources.BlockCart;
                if (card.BlockType == BlockType.Lantern) return Properties.Resources.BlockLantern;
                if (card.BlockType == BlockType.Pickaxe) return Properties.Resources.BlockPickaxe;
                else return Properties.Resources.EmptyTile;
            }
            else if (card.Type == CardType.AntiBlock)
            {
                if (card.BlockType == BlockType.Cart) return Properties.Resources.AntiBlockCart;
                if (card.BlockType == BlockType.Lantern) return Properties.Resources.AntiBlockLantern;
                if (card.BlockType == BlockType.Pickaxe) return Properties.Resources.AntiBlockPickaxe;
                if (card.BlockType == (BlockType.Cart | BlockType.Lantern)) return Properties.Resources.AntiBlockLanternCart;
                if (card.BlockType == (BlockType.Lantern | BlockType.Pickaxe)) return Properties.Resources.AntiBlockLanternPickaxe;
                if (card.BlockType == (BlockType.Pickaxe | BlockType.Cart)) return Properties.Resources.AntiBlockPickaxeCart;
                else return Properties.Resources.EmptyTile;
            }
            else return Properties.Resources.EmptyTile;
        }
    }
}
