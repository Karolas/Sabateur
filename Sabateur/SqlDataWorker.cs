using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabateur
{
    class SqlDataWorker : IDataWorker
    {
        private string ConnectionString { get; } = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\GameDB.mdf;Integrated Security=True";
        private SqlConnection Connection { get; }
        private GameDBEntities db { get; } = new GameDBEntities();

        public System.Data.ConnectionState ConnectionStatus()
        {
            return Connection.State;
        }
        public SqlDataWorker()
        {
            Connection = new SqlConnection(ConnectionString);
        }

        public bool IsTurn()
        {
            if(Connection.State == System.Data.ConnectionState.Closed)
            {
                Connection.Open();
                PlayerSet curPlayer = db.PlayerSet.Find(Game.Player.Name);
                if (curPlayer != null && curPlayer.IsTurn == true)
                {
                    Connection.Close();
                    Game.Player.IsTurn = true;
                    return true;
                }
                else
                {
                    Connection.Close();
                    return false;
                }
            }
            else
            {
                return false;
            }
            
        }

        public void GetData()
        {
            Connection.Open();
            Game.Cards = db.CardSet;
            Game.Players = db.PlayerSet;
        }

        public void SaveData()
        {
            db.CardSet = Game.Cards;
            db.PlayerSet = Game.Players;

            db.SaveChanges();

            Game.Cards = db.CardSet;
            Game.Players = db.PlayerSet;
        }

        public void SaveDataCards()
        {
            db.CardSet = Game.Cards;

            db.SaveChanges();

            Game.Cards = db.CardSet;
        }

        public void SaveDataPlayers()
        {
            db.PlayerSet = Game.Players;

            db.SaveChanges();

            Game.Players = db.PlayerSet;
        }

        public void ReturnData()
        {
            db.CardSet = Game.Cards;
            db.PlayerSet = Game.Players;

            db.SaveChanges();
            Connection.Close();
        }
    }
}
