using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sabateur
{
    public partial class GameForm : Form
    {
        public GameForm()
        {
            InitializeComponent();

            AsignSelectDelegates();
            AsignTileSelectDelegates();
            InitializeFieldTiles();
            InitializeHandComponents();
        }

        private void GameForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'gameDBDataSet1.CardSet' table. You can move, or remove it, as needed.
            this.cardSetTableAdapter.Fill(this.gameDBDataSet1.CardSet);
            // TODO: This line of code loads data into the 'gameDBDataSet1.PlayerSet' table. You can move, or remove it, as needed.
            this.playerSetTableAdapter.Fill(this.gameDBDataSet1.PlayerSet);

            Game.SqlWorker.GetData();
            Game.ResetData();
            Game.SqlWorker.ReturnData();

            GameTimer.Start();
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            if(Game.Player.IsTurn == false)
            {
                if (Game.SqlWorker.IsTurn())
                {
                    Game.SqlWorker.GetData();
                    Game.InitiateControl();
                    ShowField();
                    ShowHand();
                }
            }
        }

        private void HideControls()
        {
            PictureBoxPathPos1.Hide();
            PictureBoxPathPos2.Hide();
            ListBoxItems.Items.Clear();
            ListBoxItems.Hide();
            ButtonUseCard.Hide();
            ButtonDiscardCard.Hide();
            ButtonEndTurn.Hide();
            LabelGoalCard.Text = "";

        }

        private void ButtonAddPlayer_Click(object sender, EventArgs e)
        {
            Game.SqlWorker.GetData();
            Game.AddPlayer(TextBoxName.Text);
            Game.SqlWorker.ReturnData();

            ListBoxPlayers.Items.Add(TextBoxName.Text);
        }

        private void ButtonNewGame_Click(object sender, EventArgs e)
        {
            Game.SqlWorker.GetData();
            PlayerSet[] players = Game.Players.Where(player => player.Name != "Field" &&
                                                               player.Name != "Deck" &&
                                                               player.Name != "Graveyard").ToArray();

            players[0].IsTurn = true;

            for(int i = 0; i < players.Count(); i++)
            {
                Game.AssignRandomCard(players[i].Name);
                Game.AssignRandomCard(players[i].Name);
                Game.AssignRandomCard(players[i].Name);
                Game.AssignRandomCard(players[i].Name);
                Game.AssignRandomCard(players[i].Name);
            }
            Game.SqlWorker.ReturnData();
        }

        private void PictureBoxPathPos1_Click(object sender, EventArgs e)
        {
            Game.Player.SelectedCard.IsPathUpside = false;
            ShowField();
            foreach (int[] spot in Game.AvailableFieldTiles(Game.Player.SelectedCard))
            {
                fieldTiles[spot[0] - 1, spot[1] - 1].Image = Properties.Resources.AvailableTile;
            }
        }

        private void PictureBoxPathPos2_Click(object sender, EventArgs e)
        {
            Game.Player.SelectedCard.IsPathUpside = true;
            ShowField();
            foreach (int[] spot in Game.AvailableFieldTiles(Game.Player.SelectedCard))
            {
                fieldTiles[spot[0] - 1, spot[1] - 1].Image = Properties.Resources.AvailableTile;
            }
        }

        private void ButtonDiscardCard_Click(object sender, EventArgs e)
        {
            Game.DiscardCard(Game.Player.SelectedCard);

            EndTurn();
        }

        private void ButtonUseCard_Click(object sender, EventArgs e)
        {
            if(Game.Player.SelectedCard.Type == CardType.Block)
            {
                if(ListBoxItems.SelectedItem != null)
                {
                    if (((PlayerSet)ListBoxItems.SelectedItem).Blocks.HasValue)
                    {
                        ((PlayerSet)ListBoxItems.SelectedItem).Blocks = ((PlayerSet)ListBoxItems.SelectedItem).Blocks | Game.Player.SelectedCard.BlockType;
                    }
                    else
                    {
                        ((PlayerSet)ListBoxItems.SelectedItem).Blocks = Game.Player.SelectedCard.BlockType;
                    }
                    EndTurn();
                }
            }
            if (Game.Player.SelectedCard.Type == CardType.AntiBlock)
            {
                if (ListBoxItems.SelectedItem != null)
                {
                    PlayerSet blockedPlayer = Game.Players.Where(player => player.Name == Game.Player.Name).First();
                    blockedPlayer.Blocks = blockedPlayer.Blocks ^ (BlockType)ListBoxItems.SelectedItem;
                    EndTurn();
                }
            }
        }

        private void ButtonChangeName_Click(object sender, EventArgs e)
        {
            Game.Player.Name = TextBoxName.Text;
            Game.Player.IsTurn = false;

            if(Game.SqlWorker.ConnectionStatus() == ConnectionState.Open)
            {
                Game.SqlWorker.ReturnData();
            }

            HideControls();
            HideHand();
        }

        private void EndTurn()
        {
            Game.EndTurn();
            ShowField();
            HideControls();
            HideHand();
        }

        private void ButtonEndTurn_Click(object sender, EventArgs e)
        {
            EndTurn();
        }
    }
}
