using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MWCGClasses;
using MWCGClasses.GameObjects;
using MWCGClasses.InGame;
using System.Data.SqlClient;
using System.Data;

namespace MWData
{
    public class DataAccessor
    {
        public static string connector = @"Server=CRAB\SQLDB; DataBase=MagicWar;Integrated Security=SSPI; ";

        static public Card getCard(int id)
        {
            Card card = new Card();
            using (SqlConnection cn=new SqlConnection(connector))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("dbo.getCard",cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("id", id));
                SqlDataReader res=cmd.ExecuteReader();
                res.Read();
                card.CardId = (int)res["CardId"];
                card.Collectable = (bool)res["Collectable"];
                card.Description=(string)res["Description"];
                card.EntityId=(int)res["EntityId"];
                card.ManaCost=(int)res["ManaCost"];
                card.Name=(string)res["Name"];
                card.Rarity=(RareType)res["Rarity"];
                card.Type=(CardType)res["Type"];
                cn.Close();
            }
            return card;
        }
        static public List<Card> getCardList()
        {
            List<Card> result = new List<Card>();
            using (SqlConnection cn = new SqlConnection(connector))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("dbo.getCard", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader res = cmd.ExecuteReader();
                while (res.Read()){
                    Card card = new Card();
                    card.CardId = (int)res["CardId"];
                    card.Collectable = (bool)res["Collectable"];
                    card.Description = (string)res["Description"];
                    card.EntityId = (int)res["EntityId"];
                    card.ManaCost = (int)res["ManaCost"];
                    card.Name = (string)res["Name"];
                    card.Rarity = (RareType)res["Rarity"];
                    card.Type = (CardType)res["Type"];
                    result.Add(card);
                }
                cn.Close();
            }
            return result;
        }
    }
}
