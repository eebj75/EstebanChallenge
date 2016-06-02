using HPChallenge.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HPChallenge
{
    public partial class Default : Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        protected void GetTopScores()
        {
            int count = int.Parse(txtCount.Text);
            ScoreBoard sb = new ScoreBoard();
            List<Player> playerList = new List<Player>();
            playerList=  sb.GetTopPlayers(count);

            if (count > 0)
            {
                gvTopScores.PageSize = count;
                gvTopScores.DataSource = playerList;
                gvTopScores.DataBind();
            }
        }

        protected void GetWinner()
        {
            ScoreBoard sb = new ScoreBoard();
            Player player1 = new Player();
            Player player2 = new Player();


            var winner = 0;

            winner = sb.GetWinner("R", "S");

            if (winner == 1)
            {
                //player1 wins

                player1.playerName = "Juan";
                player1.points = 3;

                player2.playerName = "Ernesto";
                player2.points = 1;

                sb.InsertScoreBoard(player1);
                sb.InsertScoreBoard(player2);
            }
            else if (winner == 2)
            {
                //player2 wins

                player1.playerName = "Juan";
                player1.points = 1;

                player2.playerName = "Ernesto";
                player2.points = 3;

                sb.InsertScoreBoard(player1);
                sb.InsertScoreBoard(player2);
            }
            else
            {
                Response.Write("Cannot Determine Winner.");
            }
        }

        protected void btnGetTopScores_Click(object sender, EventArgs e)
        {
            GetTopScores();
        }

        protected void btnUploadFile_Click(object sender, EventArgs e)
        {
            if (FileUploadControl.HasFile)
            {
                try
                {
                    string fileName = FileUploadControl.PostedFile.FileName;
                    string FullPath = Path.Combine(@"c:\temp\", fileName);

                    FileUploadControl.SaveAs(FullPath);
                    
                    ProcessFile(FullPath);
                }
                catch (Exception ex)
                {
                }
            }
        }

        private void ProcessFile(string file)
        {
            try
            {   
                using (StreamReader sr = new StreamReader(file))
                {
                    string line = "";

                    while ((line = sr.ReadLine()) != null)
                    {
                        System.Console.WriteLine(line);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }
    }
}