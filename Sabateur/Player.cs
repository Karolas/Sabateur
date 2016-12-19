using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabateur
{
    class Player
    {
        public string Name { get; set; }
        public CardSet SelectedCard { get; set; }
        public CardSet[] OwnedCards { get; set; }

        public bool IsTurn { get; set; }

        public Player(string name)
        {
            Name = name;
        }

        public void SetOwnedCards()
        {
            OwnedCards = Game.Cards.Where(card => card.Owner == Name).ToArray();
        }
    }
}
