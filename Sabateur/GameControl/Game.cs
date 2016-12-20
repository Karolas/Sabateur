using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabateur
{
    class Game
    {
        public static SqlDataWorker SqlWorker = new SqlDataWorker();
        public static Player Player = new Player("Karolis");

        public static Random RandNumGen = new Random();
        public static DbSet<CardSet> Cards { get; set; }
        public static DbSet<PlayerSet> Players { get; set; }

        public static void ResetData()
        {
            DeleteCards();
            ResetPlayers();
            AddCards();

            SqlWorker.SaveData();
        }

        public static void InitiateControl()
        {
            Player.SetOwnedCards();
        }

        public static void EndTurn()
        {
            List<PlayerSet> players = Players.Where(player => player.Name != "Field" &&
                                                              player.Name != "Deck" &&
                                                              player.Name != "Graveyard").OrderByDescending(player => player.Name).ToList();
            for(int i = 0; i < players.Count; i++)
            {
                if(players[i].Name == Player.Name)
                {
                    AssignRandomCard(players[i].Name);
                    if (Player.SelectedCard.Owner == Player.Name) Player.SelectedCard.Owner = "Graveyard";
                    players[i].IsTurn = false;
                    if (i + 1 < players.Count) players[i + 1].IsTurn = true;
                    else players[0].IsTurn = true;
                    Player.IsTurn = false;
                    break;
                }
            }

            SqlWorker.ReturnData();
        }

        public static void AddPlayer(string name)
        {
            Players.Add(CratePlayer(name));

            SqlWorker.SaveData();
        }

        public static void AssignRandomCard(string playerName)
        {
            PlayerSet player = Players.Find(playerName);
            CardSet[] cards = Cards.Where(card => card.Owner == "Deck").ToArray();
            cards[RandNumGen.Next(0, cards.Count() - 1)].Owner = playerName;

            SqlWorker.SaveData();
        }

        public static void PutCardOnField(int id, CardType type, int col, int row)
        {
            CardSet card = Cards.Where(inHand => inHand.Id == id && inHand.Type == type).First();
            card.Owner = "Field";
            card.FieldCol = col;
            card.FieldRow = row;

            SqlWorker.SaveData();
        }

        private static void ResetPlayers()
        {
            var players = Players.ToList();

            foreach (PlayerSet player in players)
            {
                Players.Remove(player);
            }

            Players.Add(CratePlayer("Deck"));
            Players.Add(CratePlayer("Field"));
            Players.Add(CratePlayer("Graveyard"));
        }

        private static void AddCards()
        {
            Cards.AddRange(Deck.Cards);
        }

        private static void DeleteCards()
        {
            Cards.RemoveRange(Cards);
        }

        public static void AssignRandomSaboteur()
        {
            List<PlayerSet> players = Players.Where(player => player.Name != "Deck" &&
                                                              player.Name != "Field" &&
                                                              player.Name != "Graveyard" &&
                                                              player.IsSabateur == false).ToList();

            players[RandNumGen.Next(0, players.Count - 1)].IsSabateur = true;

            SqlWorker.SaveData();
        }

        private static PlayerSet CratePlayer(string name)
        {
            PlayerSet deck = new PlayerSet();
            deck.Name = name;
            deck.IsTurn = false;
            deck.IsSabateur = false;
            deck.Score = 0;

            return deck;
        }

        public static void DiscardCard(CardSet card)
        {
            card.Owner = "Graveyard";

            SqlWorker.SaveData();
        }

        public static List<int[]> AvailableFieldTiles(CardSet cardToPlace)
        {
            List<int[]> availableFieldTiles = new List<int[]>();
            CardSet[,] fieldTiles = new CardSet[9,5];
            var cardsOnField = Cards.Where(card => card.Owner == "Field" && card.Type != CardType.GoalPath).ToArray();

            foreach(CardSet card in cardsOnField)
            {
                fieldTiles[(int)card.FieldCol - 1, (int)card.FieldRow - 1] = card;
            }

            for(int i = 0; i < 9; i++)
            {
                for(int i1 = 0; i1 < 5; i1++)
                {
                    bool isAvailable = false;

                    if(fieldTiles[i, i1] == null)
                    {
                        if(i1 - 1 >= 0 && fieldTiles[i, i1 - 1] != null)
                        {
                            if (cardToPlace.Path != null)
                            {
                                if (DoesPathConnect(fieldTiles[i, i1 - 1], cardToPlace, i, i1)) isAvailable = true;
                            }
                            else isAvailable = true;
                        }
                        if (i1 + 1 < 5 && fieldTiles[i, i1 + 1] != null)
                        {
                            if (cardToPlace.Path != null)
                            {
                                if (DoesPathConnect(fieldTiles[i, i1 + 1], cardToPlace, i, i1)) isAvailable = true;
                            }
                            else isAvailable = true;
                        }
                        if (i - 1 >= 0 && fieldTiles[i - 1, i1] != null)
                        {
                            if (cardToPlace.Path != null)
                            {
                                if (DoesPathConnect(fieldTiles[i - 1, i1], cardToPlace, i, i1)) isAvailable = true;
                            }
                            else isAvailable = true;
                        }
                        if (i + 1 < 9 && fieldTiles[i + 1, i1] != null)
                        {
                            if (cardToPlace.Path != null)
                            {
                                if (DoesPathConnect(fieldTiles[i + 1, i1], cardToPlace, i, i1)) isAvailable = true;
                            }
                            else isAvailable = true;
                        }
                    }
                    if (isAvailable == true)
                    {
                        if (i1 - 1 >= 0 && fieldTiles[i, i1 - 1] != null)
                        {
                            if (!DoCardsJoin(fieldTiles[i, i1 - 1], cardToPlace, i, i1)) isAvailable = false;
                        }
                        if (i1 + 1 < 5 && fieldTiles[i, i1 + 1] != null)
                        {
                            if (!DoCardsJoin(fieldTiles[i, i1 + 1], cardToPlace, i, i1)) isAvailable = false;
                        }
                        if (i - 1 >= 0 && fieldTiles[i - 1, i1] != null)
                        {
                            if (!DoCardsJoin(fieldTiles[i - 1, i1], cardToPlace, i, i1)) isAvailable = false;
                        }
                        if (i + 1 < 9 && fieldTiles[i + 1, i1] != null)
                        {
                            if (!DoCardsJoin(fieldTiles[i + 1, i1], cardToPlace, i, i1)) isAvailable = false;
                        }
                    }

                    if(isAvailable == true) availableFieldTiles.Add(new int[]{ i + 1, i1 + 1});
                }
            }

            return availableFieldTiles;
        }

        private static bool DoCardsJoin(CardSet fieldCard, CardSet cardToPlace, int colToPlace, int rowToPlace)
        {
            int rowDif = (int)rowToPlace - ((int)fieldCard.FieldRow - 1);
            int colDif = (int)colToPlace - ((int)fieldCard.FieldCol - 1);

            if (!(bool)cardToPlace.IsPathUpside)
            {
                if (!(bool)fieldCard.IsPathUpside)
                {
                    if (rowDif == 0 && colDif == 1 &&
                        ((fieldCard.PathOpenings.Value.HasFlag(Direction.Right) &&
                        cardToPlace.PathOpenings.Value.HasFlag(Direction.Left)) ||
                        (!fieldCard.PathOpenings.Value.HasFlag(Direction.Right) &&
                        !cardToPlace.PathOpenings.Value.HasFlag(Direction.Left)))) return true;
                    if (rowDif == 0 && colDif == -1 &&
                        ((fieldCard.PathOpenings.Value.HasFlag(Direction.Left) &&
                        cardToPlace.PathOpenings.Value.HasFlag(Direction.Right)) ||
                        (!fieldCard.PathOpenings.Value.HasFlag(Direction.Left) &&
                        !cardToPlace.PathOpenings.Value.HasFlag(Direction.Right)))) return true;
                    if (rowDif == 1 && colDif == 0 &&
                        ((fieldCard.PathOpenings.Value.HasFlag(Direction.Down) &&
                        cardToPlace.PathOpenings.Value.HasFlag(Direction.Up)) ||
                        (!fieldCard.PathOpenings.Value.HasFlag(Direction.Down) &&
                        !cardToPlace.PathOpenings.Value.HasFlag(Direction.Up)))) return true;
                    if (rowDif == -1 && colDif == 0 &&
                        ((fieldCard.PathOpenings.Value.HasFlag(Direction.Up) &&
                        cardToPlace.PathOpenings.Value.HasFlag(Direction.Down)) ||
                        (!fieldCard.PathOpenings.Value.HasFlag(Direction.Up) &&
                        !cardToPlace.PathOpenings.Value.HasFlag(Direction.Down)))) return true;
                }
                else
                {
                    if (rowDif == 0 && colDif == 1 &&
                        ((fieldCard.PathOpenings.Value.HasFlag(Direction.Left) &&
                        cardToPlace.PathOpenings.Value.HasFlag(Direction.Left)) ||
                        (!fieldCard.PathOpenings.Value.HasFlag(Direction.Left) &&
                        !cardToPlace.PathOpenings.Value.HasFlag(Direction.Left)))) return true;
                    if (rowDif == 0 && colDif == -1 &&
                        ((fieldCard.PathOpenings.Value.HasFlag(Direction.Right) &&
                        cardToPlace.PathOpenings.Value.HasFlag(Direction.Right)) ||
                        (!fieldCard.PathOpenings.Value.HasFlag(Direction.Right) &&
                        !cardToPlace.PathOpenings.Value.HasFlag(Direction.Right)))) return true;
                    if (rowDif == 1 && colDif == 0 &&
                        ((fieldCard.PathOpenings.Value.HasFlag(Direction.Up) &&
                        cardToPlace.PathOpenings.Value.HasFlag(Direction.Up)) ||
                        (!fieldCard.PathOpenings.Value.HasFlag(Direction.Up) &&
                        !cardToPlace.PathOpenings.Value.HasFlag(Direction.Up)))) return true;
                    if (rowDif == -1 && colDif == 0 &&
                        ((fieldCard.PathOpenings.Value.HasFlag(Direction.Down) &&
                        cardToPlace.PathOpenings.Value.HasFlag(Direction.Down)) ||
                        (!fieldCard.PathOpenings.Value.HasFlag(Direction.Down) &&
                        !cardToPlace.PathOpenings.Value.HasFlag(Direction.Down)))) return true;
                }
            }
            else
            {
                if (!(bool)fieldCard.IsPathUpside)
                {
                    if (rowDif == 0 && colDif == 1 &&
                        ((fieldCard.PathOpenings.Value.HasFlag(Direction.Right) &&
                        cardToPlace.PathOpenings.Value.HasFlag(Direction.Right)) ||
                        (!fieldCard.PathOpenings.Value.HasFlag(Direction.Right) &&
                        !cardToPlace.PathOpenings.Value.HasFlag(Direction.Right)))) return true;
                    if (rowDif == 0 && colDif == -1 &&
                        ((fieldCard.PathOpenings.Value.HasFlag(Direction.Left) &&
                        cardToPlace.PathOpenings.Value.HasFlag(Direction.Left)) ||
                        (!fieldCard.PathOpenings.Value.HasFlag(Direction.Left) &&
                        !cardToPlace.PathOpenings.Value.HasFlag(Direction.Left)))) return true;
                    if (rowDif == 1 && colDif == 0 &&
                        ((fieldCard.PathOpenings.Value.HasFlag(Direction.Down) &&
                        cardToPlace.PathOpenings.Value.HasFlag(Direction.Down)) ||
                        (!fieldCard.PathOpenings.Value.HasFlag(Direction.Down) &&
                        !cardToPlace.PathOpenings.Value.HasFlag(Direction.Down)))) return true;
                    if (rowDif == -1 && colDif == 0 &&
                        ((fieldCard.PathOpenings.Value.HasFlag(Direction.Up) &&
                        cardToPlace.PathOpenings.Value.HasFlag(Direction.Up)) ||
                        (!fieldCard.PathOpenings.Value.HasFlag(Direction.Up) &&
                        !cardToPlace.PathOpenings.Value.HasFlag(Direction.Up)))) return true;
                }
                else
                {
                    if (rowDif == 0 && colDif == 1 &&
                        ((fieldCard.PathOpenings.Value.HasFlag(Direction.Left) &&
                        cardToPlace.PathOpenings.Value.HasFlag(Direction.Right)) ||
                        (!fieldCard.PathOpenings.Value.HasFlag(Direction.Left) &&
                        !cardToPlace.PathOpenings.Value.HasFlag(Direction.Right)))) return true;
                    if (rowDif == 0 && colDif == -1 &&
                        ((fieldCard.PathOpenings.Value.HasFlag(Direction.Right) &&
                        cardToPlace.PathOpenings.Value.HasFlag(Direction.Left)) ||
                        (!fieldCard.PathOpenings.Value.HasFlag(Direction.Right) &&
                        !cardToPlace.PathOpenings.Value.HasFlag(Direction.Left)))) return true;
                    if (rowDif == 1 && colDif == 0 &&
                        ((fieldCard.PathOpenings.Value.HasFlag(Direction.Up) &&
                        cardToPlace.PathOpenings.Value.HasFlag(Direction.Down)) ||
                        (!fieldCard.PathOpenings.Value.HasFlag(Direction.Up) &&
                        !cardToPlace.PathOpenings.Value.HasFlag(Direction.Down)))) return true;
                    if (rowDif == -1 && colDif == 0 &&
                        ((fieldCard.PathOpenings.Value.HasFlag(Direction.Down) &&
                        cardToPlace.PathOpenings.Value.HasFlag(Direction.Up)) ||
                        (!fieldCard.PathOpenings.Value.HasFlag(Direction.Down) &&
                        !cardToPlace.PathOpenings.Value.HasFlag(Direction.Up)))) return true;
                }
            }

            return false;
        }

        private static bool DoesPathConnect(CardSet fieldCard, CardSet cardToPlace, int colToPlace, int rowToPlace)
        {
            int rowDif = (int)rowToPlace - ((int)fieldCard.FieldRow - 1);
            int colDif = (int)colToPlace - ((int)fieldCard.FieldCol - 1);

            if (fieldCard.Path != null)
            {
                if (!(bool)cardToPlace.IsPathUpside)
                {
                    if (!(bool)fieldCard.IsPathUpside)
                    {
                        if (rowDif == 0 && colDif == 1 &&
                            fieldCard.Path.Value.HasFlag(Direction.Right) &&
                            cardToPlace.PathOpenings.Value.HasFlag(Direction.Left)) return true;
                        if (rowDif == 0 && colDif == -1 &&
                            fieldCard.Path.Value.HasFlag(Direction.Left) &&
                            cardToPlace.PathOpenings.Value.HasFlag(Direction.Right)) return true;
                        if (rowDif == 1 && colDif == 0 &&
                            fieldCard.Path.Value.HasFlag(Direction.Down) &&
                            cardToPlace.PathOpenings.Value.HasFlag(Direction.Up)) return true;
                        if (rowDif == -1 && colDif == 0 &&
                            fieldCard.Path.Value.HasFlag(Direction.Up) &&
                            cardToPlace.PathOpenings.Value.HasFlag(Direction.Down)) return true;
                    }
                    else
                    {
                        if (rowDif == 0 && colDif == 1 &&
                            fieldCard.Path.Value.HasFlag(Direction.Left) &&
                            cardToPlace.PathOpenings.Value.HasFlag(Direction.Left)) return true;
                        if (rowDif == 0 && colDif == -1 &&
                            fieldCard.Path.Value.HasFlag(Direction.Right) &&
                            cardToPlace.PathOpenings.Value.HasFlag(Direction.Right)) return true;
                        if (rowDif == 1 && colDif == 0 &&
                            fieldCard.Path.Value.HasFlag(Direction.Up) &&
                            cardToPlace.PathOpenings.Value.HasFlag(Direction.Up)) return true;
                        if (rowDif == -1 && colDif == 0 &&
                            fieldCard.Path.Value.HasFlag(Direction.Down) &&
                            cardToPlace.PathOpenings.Value.HasFlag(Direction.Down)) return true;
                    }

                }
                else
                {
                    if (!(bool)fieldCard.IsPathUpside)
                    {
                        if (rowDif == 0 && colDif == 1 &&
                            fieldCard.Path.Value.HasFlag(Direction.Right) &&
                            cardToPlace.PathOpenings.Value.HasFlag(Direction.Right)) return true;
                        if (rowDif == 0 && colDif == -1 &&
                            fieldCard.Path.Value.HasFlag(Direction.Left) &&
                            cardToPlace.PathOpenings.Value.HasFlag(Direction.Left)) return true;
                        if (rowDif == 1 && colDif == 0 &&
                            fieldCard.Path.Value.HasFlag(Direction.Down) &&
                            cardToPlace.PathOpenings.Value.HasFlag(Direction.Down)) return true;
                        if (rowDif == -1 && colDif == 0 &&
                            fieldCard.Path.Value.HasFlag(Direction.Up) &&
                            cardToPlace.PathOpenings.Value.HasFlag(Direction.Up)) return true;
                    }
                    else
                    {
                        if (rowDif == 0 && colDif == 1 &&
                            fieldCard.Path.Value.HasFlag(Direction.Left) &&
                            cardToPlace.PathOpenings.Value.HasFlag(Direction.Right)) return true;
                        if (rowDif == 0 && colDif == -1 &&
                            fieldCard.Path.Value.HasFlag(Direction.Right) &&
                            cardToPlace.PathOpenings.Value.HasFlag(Direction.Left)) return true;
                        if (rowDif == 1 && colDif == 0 &&
                            fieldCard.Path.Value.HasFlag(Direction.Up) &&
                            cardToPlace.PathOpenings.Value.HasFlag(Direction.Down)) return true;
                        if (rowDif == -1 && colDif == 0 &&
                            fieldCard.Path.Value.HasFlag(Direction.Down) &&
                            cardToPlace.PathOpenings.Value.HasFlag(Direction.Up)) return true;
                    }
                }
            }
            return false;
        }

        public static void CheckGoalCardReached(CardSet putCard)
        {
            CardSet[] goalCards = Game.Cards.Where(card => card.Type == CardType.GoalPath).ToArray();

            for (int i = 0; i < goalCards.Count(); i++)
            {
                if (IsCardNextToGoal(putCard, goalCards[i]))
                {
                    if (goalCards[i].Path == (Direction.Down | Direction.Left | Direction.Right | Direction.Up)) System.Windows.Forms.Application.Exit();
                    else
                    {
                        goalCards[i].Type = CardType.Path;
                    }
                    SqlWorker.SaveData();
                }
            }
        }

        private static bool IsCardNextToGoal(CardSet putCard, CardSet goalCard)
        {
            int colDif = (int)putCard.FieldCol - (int)goalCard.FieldCol;
            int rowDif = (int)putCard.FieldRow - (int)goalCard.FieldRow;
            if (Math.Abs(colDif) + Math.Abs(rowDif) == 1)
            {
                if(rowDif == 1)
                {
                    if (putCard.Path.Value.HasFlag(Direction.Up) && putCard.IsPathUpside == false) return true;
                    if (putCard.Path.Value.HasFlag(Direction.Down) && putCard.IsPathUpside == true) return true;
                }
                if (rowDif == -1)
                {
                    if (putCard.Path.Value.HasFlag(Direction.Down) && putCard.IsPathUpside == false) return true;
                    if (putCard.Path.Value.HasFlag(Direction.Up) && putCard.IsPathUpside == true) return true;
                }
                if (colDif == 1)
                {
                    if (putCard.Path.Value.HasFlag(Direction.Left) && putCard.IsPathUpside == false) return true;
                    if (putCard.Path.Value.HasFlag(Direction.Right) && putCard.IsPathUpside == true) return true;
                }
                if (colDif == -1)
                {
                    if (putCard.Path.Value.HasFlag(Direction.Right) && putCard.IsPathUpside == false) return true;
                    if (putCard.Path.Value.HasFlag(Direction.Left) && putCard.IsPathUpside == true) return true;
                }
            }
            return false;
        }

        private bool IsCardOnTile(IQueryable<CardSet> fieldCards, int row, int col)
        {
            return fieldCards.Where(card => card.FieldCol == row && card.FieldRow == col).Count() > 0;
        }
    }
}
