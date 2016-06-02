using System;
using HPChallenge.Models;

namespace HPChallenge
{
    public partial class Settings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnDeleteScores_Click(object sender, EventArgs e)
        {
            ScoreBoard sb = new ScoreBoard();
            sb.DeleteScores();
        }
    }
}