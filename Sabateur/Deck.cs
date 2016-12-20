using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabateur
{
    static class Deck
    {
        private static CardSet startCard = SetStartCard(71);
        private static CardSet[] goalCards = SetGoalCards(68, 69, 70);
        public static CardSet[] Cards { get; private set; } =
        {
            PrepareCard(1, CardType.Path, null, Direction.Up | Direction.Down, Direction.Up | Direction.Down),
            PrepareCard(2, CardType.Path, null, Direction.Up | Direction.Down, Direction.Up | Direction.Down),
            PrepareCard(3, CardType.Path, null, Direction.Up | Direction.Down, Direction.Up | Direction.Down),
            PrepareCard(4, CardType.Path, null, Direction.Up | Direction.Down, Direction.Up | Direction.Down),
            PrepareCard(5, CardType.Path, null, Direction.Up | Direction.Down | Direction.Right, Direction.Up | Direction.Down | Direction.Right),
            PrepareCard(6, CardType.Path, null, Direction.Up | Direction.Down | Direction.Right, Direction.Up | Direction.Down | Direction.Right),
            PrepareCard(7, CardType.Path, null, Direction.Up | Direction.Down | Direction.Right, Direction.Up | Direction.Down | Direction.Right),
            PrepareCard(8, CardType.Path, null, Direction.Up | Direction.Down | Direction.Right, Direction.Up | Direction.Down | Direction.Right),
            PrepareCard(9, CardType.Path, null, Direction.Up | Direction.Down | Direction.Right, Direction.Up | Direction.Down | Direction.Right),
            PrepareCard(10, CardType.Path, null, Direction.Up | Direction.Down | Direction.Left | Direction.Right, Direction.Up | Direction.Down | Direction.Left | Direction.Right),
            PrepareCard(11, CardType.Path, null, Direction.Up | Direction.Down | Direction.Left | Direction.Right, Direction.Up | Direction.Down | Direction.Left | Direction.Right),
            PrepareCard(12, CardType.Path, null, Direction.Up | Direction.Down | Direction.Left | Direction.Right, Direction.Up | Direction.Down | Direction.Left | Direction.Right),
            PrepareCard(13, CardType.Path, null, Direction.Up | Direction.Down | Direction.Left | Direction.Right, Direction.Up | Direction.Down | Direction.Left | Direction.Right),
            PrepareCard(14, CardType.Path, null, Direction.Up | Direction.Down | Direction.Left | Direction.Right, Direction.Up | Direction.Down | Direction.Left | Direction.Right),
            PrepareCard(15, CardType.Path, null, Direction.Down | Direction.Right, Direction.Down | Direction.Right),
            PrepareCard(16, CardType.Path, null, Direction.Down | Direction.Right, Direction.Down | Direction.Right),
            PrepareCard(17, CardType.Path, null, Direction.Down | Direction.Right, Direction.Down | Direction.Right),
            PrepareCard(18, CardType.Path, null, Direction.Down | Direction.Right, Direction.Down | Direction.Right),
            PrepareCard(19, CardType.Path, null, Direction.Down | Direction.Left, Direction.Down | Direction.Left),
            PrepareCard(20, CardType.Path, null, Direction.Down | Direction.Left, Direction.Down | Direction.Left),
            PrepareCard(21, CardType.Path, null, Direction.Down | Direction.Left, Direction.Down | Direction.Left),
            PrepareCard(22, CardType.Path, null, Direction.Down | Direction.Left, Direction.Down | Direction.Left),
            PrepareCard(23, CardType.Path, null, Direction.Down | Direction.Left, Direction.Down | Direction.Left),
            PrepareCard(24, CardType.Path, null, Direction.Down, Direction.Down),
            PrepareCard(25, CardType.Path, null, Direction.Down | Direction.Left | Direction.Up),
            PrepareCard(26, CardType.Path, null, Direction.Down | Direction.Left | Direction.Up | Direction.Right),
            PrepareCard(27, CardType.Path, null, Direction.Down | Direction.Left),
            PrepareCard(28, CardType.Path, null, Direction.Down | Direction.Right),
            PrepareCard(29, CardType.Path, null, Direction.Right),
            PrepareCard(30, CardType.Path, null, Direction.Up | Direction.Right | Direction.Left, Direction.Up | Direction.Right | Direction.Left),
            PrepareCard(31, CardType.Path, null, Direction.Up | Direction.Right | Direction.Left, Direction.Up | Direction.Right | Direction.Left),
            PrepareCard(32, CardType.Path, null, Direction.Up | Direction.Right | Direction.Left, Direction.Up | Direction.Right | Direction.Left),
            PrepareCard(33, CardType.Path, null, Direction.Up | Direction.Right | Direction.Left, Direction.Up | Direction.Right | Direction.Left),
            PrepareCard(34, CardType.Path, null, Direction.Up | Direction.Right | Direction.Left, Direction.Up | Direction.Right | Direction.Left),
            PrepareCard(35, CardType.Path, null, Direction.Right | Direction.Left, Direction.Right | Direction.Left),
            PrepareCard(36, CardType.Path, null, Direction.Right | Direction.Left, Direction.Right | Direction.Left),
            PrepareCard(37, CardType.Path, null, Direction.Right | Direction.Left, Direction.Right | Direction.Left),
            PrepareCard(38, CardType.Path, null, Direction.Up | Direction.Down),
            PrepareCard(39, CardType.Path, null, Direction.Up | Direction.Left | Direction.Right),
            PrepareCard(40, CardType.Path, null, Direction.Left | Direction.Right),
            startCard,
            goalCards[0],
            goalCards[1],
            goalCards[2],
            PrepareCard(41, CardType.Block, BlockType.Cart),
            PrepareCard(42, CardType.Block, BlockType.Cart),
            PrepareCard(43, CardType.Block, BlockType.Cart),
            PrepareCard(44, CardType.Block, BlockType.Pickaxe),
            PrepareCard(45, CardType.Block, BlockType.Pickaxe),
            PrepareCard(46, CardType.Block, BlockType.Pickaxe),
            PrepareCard(47, CardType.Block, BlockType.Lantern),
            PrepareCard(48, CardType.Block, BlockType.Lantern),
            PrepareCard(49, CardType.Block, BlockType.Lantern),
            PrepareCard(50, CardType.AntiBlock, BlockType.Cart),
            PrepareCard(51, CardType.AntiBlock, BlockType.Cart),
            PrepareCard(52, CardType.AntiBlock, BlockType.Cart | BlockType.Pickaxe),
            PrepareCard(53, CardType.AntiBlock, BlockType.Pickaxe),
            PrepareCard(54, CardType.AntiBlock, BlockType.Pickaxe),
            PrepareCard(55, CardType.AntiBlock, BlockType.Pickaxe | BlockType.Lantern),
            PrepareCard(56, CardType.AntiBlock, BlockType.Lantern),
            PrepareCard(57, CardType.AntiBlock, BlockType.Lantern),
            PrepareCard(58, CardType.AntiBlock, BlockType.Lantern | BlockType.Cart),
            PrepareCard(59, CardType.Map),
            PrepareCard(60, CardType.Map),
            PrepareCard(61, CardType.Map),
            PrepareCard(62, CardType.Map),
            PrepareCard(63, CardType.Map),
            PrepareCard(64, CardType.Map),
            PrepareCard(65, CardType.RemovePath),
            PrepareCard(66, CardType.RemovePath),
            PrepareCard(67, CardType.RemovePath),
        };

        private static CardSet PrepareCard(int id, CardType type, BlockType? blockType = null, Direction? opening = null, Direction? path = null)
        {
            CardSet card = new CardSet();
            card.Id = id;
            card.Type = type;
            card.Owner = "Deck";
            card.BlockType = blockType;
            card.PathOpenings = opening;
            card.IsPathUpside = false;
            card.Path = path;

            return card;
        }

        private static CardSet SetStartCard(int id)
        {
            CardSet card = new CardSet();
            card.Id = id;
            card.Type = CardType.Path;
            card.Owner = "Field";
            card.PathOpenings = Direction.Down | Direction.Left | Direction.Right | Direction.Up;
            card.Path = Direction.Down | Direction.Left | Direction.Right | Direction.Up;
            card.IsPathUpside = false;
            card.FieldCol = 9;
            card.FieldRow = 3;
         
            return card;
        }

        private static CardSet[] SetGoalCards(int id1, int id2, int id3)
        {
            CardSet[] cards = { new CardSet(), new CardSet(), new CardSet()};

            int[,] fieldPos = { { 1, 1 }, { 1, 3 }, { 1, 5 }, };
            List<int> indexes = new List<int>();
            indexes.Add(0);
            indexes.Add(1);
            indexes.Add(2);
            int[] randomizedIndexes = new int[3];
            randomizedIndexes[0] = indexes[Game.RandNumGen.Next(0, 2)];
            indexes.Remove(randomizedIndexes[0]);
            randomizedIndexes[1] = indexes[Game.RandNumGen.Next(0, 1)];
            indexes.Remove(randomizedIndexes[1]);
            randomizedIndexes[2] = indexes.First();


            cards[0].Id = id1;
            cards[1].Id = id2;
            cards[2].Id = id3;
            cards[0].Type = CardType.GoalPath;
            cards[1].Type = CardType.GoalPath;
            cards[2].Type = CardType.GoalPath;
            cards[0].IsPathUpside = false;
            cards[1].IsPathUpside = false;
            cards[2].IsPathUpside = false;
            cards[0].Owner = "Field";
            cards[1].Owner = "Field";
            cards[2].Owner = "Field";
            cards[0].PathOpenings = Direction.Up | Direction.Down | Direction.Right | Direction.Left;
            cards[1].PathOpenings = Direction.Down | Direction.Right;
            cards[2].PathOpenings = Direction.Up | Direction.Left;
            cards[0].Path = Direction.Up | Direction.Down | Direction.Right | Direction.Left;
            cards[1].Path = Direction.Down | Direction.Right;
            cards[2].Path = Direction.Up | Direction.Left;
            cards[0].FieldCol = fieldPos[randomizedIndexes[0], 0];
            cards[0].FieldRow = fieldPos[randomizedIndexes[0], 1];
            cards[1].FieldCol = fieldPos[randomizedIndexes[1], 0];
            cards[1].FieldRow = fieldPos[randomizedIndexes[1], 1];
            cards[2].FieldCol = fieldPos[randomizedIndexes[2], 0];
            cards[2].FieldRow = fieldPos[randomizedIndexes[2], 1];

            return cards;
        }
    }
}
