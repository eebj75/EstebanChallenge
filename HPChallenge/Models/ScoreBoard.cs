using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;


namespace HPChallenge.Models
{
    
    public class ScoreBoard
    {
        
        public ScoreBoard()
        {

        }

        public List<Player> GetTopPlayers(int count)
        {
            List<Player> playerList = new List<Player>();

            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["HPDBConnectionString"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[dbo].[sp_ListTopScores]";
                cmd.Parameters.AddWithValue("@count", count);
                cmd.Connection = conn;

                conn.Open();
                //cmd.ExecuteNonQuery();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var player = new Player();
                    player.playerName = reader["PlayerName"].ToString();
                    player.points = (int)reader["Points"];
                    playerList.Add(player);
                }
            }

            return playerList;
        }

        public void DeleteScores()
        {
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["HPDBConnectionString"].ConnectionString);
            SqlCommand command = new SqlCommand("", conn);

            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[dbo].[DeleteData]";
            command.Connection.Open();
            command.ExecuteNonQuery();
            command.Connection.Close();
        }

        public int InsertScoreBoard(Player player)
        {
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["HPDBConnectionString"].ConnectionString);
            SqlCommand command = new SqlCommand("",conn);

            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[dbo].[sp_InsertScore]";
            command.Parameters.AddWithValue("@PlayerName", player.playerName);
            command.Parameters.AddWithValue("@Points", player.points);
            command.Connection.Open();
            command.ExecuteNonQuery();
            command.Connection.Close();

            return 0;
        }

        public int GetWinner(string strategyP1, string strategyP2)
        {
            int winner;
            strategyP1 = strategyP1.ToUpper();
            strategyP2 = strategyP2.ToUpper();

            if (strategyP1 == strategyP2)
            {
                winner = 1;
            }
            else if (strategyP1 == ("R") && strategyP2 == ("S"))
            {
                winner = 1;
            }
            // Paper covers rock...
            else if (strategyP1 == ("P") && strategyP2 == ("R"))
            {
                winner = 1;
            }
            // Scissors cut paper...
            else if (strategyP1 == ("S") && strategyP2 == ("P"))
            {
                winner = 1;
            }
            else
            {
                winner = 2;
            }

            return winner;
        }


    }
}