using HPChallenge.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:58257/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                if (string.IsNullOrEmpty(txtCount.Text))
                    txtCount.Text = "10";

                int count = int.Parse(txtCount.Text);
                HttpResponseMessage response = client.GetAsync("api/game/" + count).Result;

                if (response.IsSuccessStatusCode)
                {
                    var dataObjects = response.Content.ReadAsAsync<IEnumerable<Player>>().Result;

                    if (count > 0)
                    {
                        gvTopScores.PageSize = count;
                        gvTopScores.DataSource = dataObjects;
                        gvTopScores.DataBind();
                    }
                }
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
                        line = line.Trim();
                        if (line.Length > 20)
                        {
                            line = line.Replace(@"""", string.Empty);
                            line = line.Replace("[", string.Empty);
                            line = line.Replace("]", string.Empty);
                            string[] arrPlayer = line.Split(',');

                            if (arrPlayer.Length >= 4)
                            {
                                ScoreBoard sb = new ScoreBoard();
                                int winner = sb.GetWinner(arrPlayer[1], arrPlayer[3]);
                                Player player1 = new Player();
                                Player player2 = new Player();

                                if (winner == 1)
                                {
                                    player1.playerName = arrPlayer[0];
                                    player1.points = 3;
                                    sb.InsertScoreBoard(player1);

                                    player2.playerName = arrPlayer[2];
                                    player2.points = 1;
                                    sb.InsertScoreBoard(player2);

                                }
                                else if (winner == 2)
                                {
                                    player1.playerName = arrPlayer[0];
                                    player1.points = 1;
                                    sb.InsertScoreBoard(player1);

                                    player2.playerName = arrPlayer[2];
                                    player2.points = 3;
                                    sb.InsertScoreBoard(player2);
                                }
                                else
                                {
                                    Response.Write("Could not get a winner.");
                                }
                            }
                            else
                            {
                                Response.Write("At least 2 players are required.");
                            }
                        }
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