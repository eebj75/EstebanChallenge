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
using System;
using System.Collections.Specialized;
using System.Net;

namespace HPChallenge
{
    public partial class Default : Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            paragraphMessage.InnerText = "";
            InsertScoreBoard();
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
                    
                    if (ProcessFile(FullPath))
                    {
                        paragraphMessage.InnerText = "upload completed";
                    }
                }
                catch (Exception ex)
                {
                    paragraphMessage.InnerText = "Error processing file. " + ex.Message;
                }
            }
        }

        private bool isValidStrategy(string strategy)
        {
            if (strategy == "P" || strategy == "R" || strategy == "S")
            {
                return true;
            }
            else
                return false;
        }

        private bool ProcessFile(string file)
        {
            try
            {
                using (StreamReader sr = new StreamReader(file))
                {
                    string line = "";
                    List<Player> listA = new List<Player>();
                   
                    while ((line = sr.ReadLine()) != null)
                    {
                        line = line.Trim();
                        if (line.Length > 20)
                        {
                            line = line.Replace(@"""", string.Empty);
                            line = line.Replace("[", string.Empty);
                            line = line.Replace("]", string.Empty).Trim();
                            string[] arrPlayer = line.Split(',');

                            if (arrPlayer.Length >= 4)
                            {
                                Player player1 = new Player();
                                Player player2 = new Player();

                                var player1Strategy = arrPlayer[1].Trim().ToUpper();
                                var player2Strategy = arrPlayer[3].Trim().ToUpper();

                                if (isValidStrategy(player1Strategy) && isValidStrategy(player2Strategy))
                                {
                                    player1.playerName = arrPlayer[0];
                                    player1.strategy = player1Strategy;
                                    listA.Add(player1);
                                    player2.playerName = arrPlayer[2];
                                    player2.strategy = player2Strategy;
                                    listA.Add(player2);
                                }
                                else
                                {
                                    paragraphMessage.InnerText = ("Invalid Strategy.");
                                    return false;
                                }                          
                            }
                            else
                            {
                                paragraphMessage.InnerText = "At least 2 players are required.";
                                return false;
                            }
                        }
                    }

                    List<Player> listB = new List<Player>();

                    while (listA.Count != 0 || listB.Count != 0)
                    {
                        ScoreBoard sb = new ScoreBoard();
                        int winner = sb.GetWinner(listA[0].strategy, listA[1].strategy);

                        if (winner == 1)
                        {
                            if (listA.Count == 2 && listB.Count == 0)
                            {
                                listA[0].points = 3;
                                sb.InsertScoreBoard(listA[0]);
                                listA[1].points = 1;
                                sb.InsertScoreBoard(listA[1]);
                                break;
                            }
                            else
                            {
                                listB.Add(listA[0]);
                                listA.RemoveAt(1);
                                listA.RemoveAt(0);
                            }
                        }
                        else if (winner == 2)
                        {
                            if (listA.Count == 2 && listB.Count == 0)
                            {
                                listA[1].points = 3;
                                sb.InsertScoreBoard(listA[1]);
                                listA[0].points = 1;
                                sb.InsertScoreBoard(listA[0]);
                                break;
                            }
                            else
                            { 
                                listB.Add(listA[1]);
                                listA.RemoveAt(1);
                                listA.RemoveAt(0);
                            }
                        }

                        if (listA.Count == 0)
                        {
                            var listBdata = from item in listB
                                           select item;

                            listA.AddRange(listBdata);

                            listB.Clear();
                        }
                    }   
                }
                
            }
            catch (Exception e)
            {
                paragraphMessage.InnerText = ("The file could not be read:") + (e.Message);
            }
            return true;
        }

        private void InsertScoreBoard()
        {
            Player player = new Player();

            StringContent content = new StringContent("name:juan");


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:58257/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.PostAsync("http://localhost:58257/api/game/post", content);
              
                //HttpResponseMessage response = client.PostAsync("api/game/post/" , content ).Result;
               
            }
            //string DATA = @"{""object"":{""name"":""Name""}}";

            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost:58257/api/game");
            //request.Method = "POST";
            //request.ContentType = "application/json";
            //request.ContentLength = DATA.Length;
            //using (Stream webStream = request.GetRequestStream())

            //using (StreamWriter requestWriter = new StreamWriter(webStream, System.Text.Encoding.ASCII))
            //{
            //    requestWriter.Write(DATA);
            //}
        }

        protected void gvTopScores_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.Cells.Count > 1)
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    e.Row.Cells[0].Text = "Player Name";
                    e.Row.Cells[1].Text = "Points";
                }

                e.Row.Cells[2].Visible = false;
            }
        }
    }
}