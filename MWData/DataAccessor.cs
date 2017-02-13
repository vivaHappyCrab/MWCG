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
                cn.Dispose();
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
                cn.Dispose();
            }
            return result;
        }
        static public List<GameObject> getObjectList()
        {
            List<GameObject> result = new List<GameObject>();
            using (SqlConnection cn = new SqlConnection(connector))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("dbo.getGameObject", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader res = cmd.ExecuteReader();
                while (res.Read())
                {
                    GameObject o;
                    switch ((ObjectType)(res["OType"]))
                    {
                        case ObjectType.creature: {
                                o = new Unit((int)(res["BackCard"]), (int)(res["ObjectNum"]), res["Name"].ToString(), res["Description"].ToString(), (int)(res["Attack"]), (int)(res["Health"])); 
                                break;
                            }
                        case ObjectType.support:
                            {
                                o = new Support((int)(res["BackCard"]), (int)(res["ObjectNum"]), res["Name"].ToString(), res["Description"].ToString(), (int)(res["Health"]));
                                break;
                            }
                        case ObjectType.hero:
                            {
                                o = new Hero((int)(res["BackCard"]), (int)(res["ObjectNum"]), res["Name"].ToString(), res["Description"].ToString(), (int)(res["Health"]), (int)(res["Default"]));
                                break;
                            }
                        case ObjectType.spell:
                            {
                                o = new Spell((int)(res["BackCard"]), (int)(res["ObjectNum"]), (int)(res["Default"]), res["Name"].ToString(), res["Description"].ToString());
                                break;
                            }
                        case ObjectType.artifact:
                            {
                                o = new Artifact((int)(res["BackCard"]), (int)(res["ObjectNum"]), res["Name"].ToString(), res["Description"].ToString(), (ArtType)(res["Default"]), (int)(res["Health"]));
                                break;
                            }
                        case ObjectType.ability:
                            {
                                o = new Ability((int)(res["BackCard"]), (int)(res["ObjectNum"]), (int)(res["Default"]), (int)(res["Cost"]), (bool)(res["Attack"]), res["Name"].ToString(), res["Description"].ToString());
                                break;
                            }
                        default: { o = null;break; }
                    }
                    result.Add(o);
                }
                cn.Dispose();
            }
            return result;
        }
        static public GameObject getObject(int id)
        {
            GameObject o;
            using (SqlConnection cn = new SqlConnection(connector))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("dbo.getGameObject", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("id", id));
                SqlDataReader res = cmd.ExecuteReader();
                res.Read();
                    switch ((ObjectType)(res["OType"]))
                    {
                        case ObjectType.creature:
                            {
                                o = new Unit((int)(res["BackCard"]), (int)(res["ObjectNum"]), res["Name"].ToString(), res["Description"].ToString(), (int)(res["Attack"]), (int)(res["Health"]));
                                break;
                            }
                        case ObjectType.support:
                            {
                                o = new Support((int)(res["BackCard"]), (int)(res["ObjectNum"]), res["Name"].ToString(), res["Description"].ToString(), (int)(res["Health"]));
                                break;
                            }
                        case ObjectType.hero:
                            {
                                o = new Hero((int)(res["BackCard"]), (int)(res["ObjectNum"]), res["Name"].ToString(), res["Description"].ToString(), (int)(res["Health"]), (int)(res["Default"]));
                                break;
                            }
                        case ObjectType.spell:
                            {
                                o = new Spell((int)(res["BackCard"]), (int)(res["ObjectNum"]), (int)(res["Default"]), res["Name"].ToString(), res["Description"].ToString());
                                break;
                            }
                        case ObjectType.artifact:
                            {
                                o = new Artifact((int)(res["BackCard"]), (int)(res["ObjectNum"]), res["Name"].ToString(), res["Description"].ToString(), (ArtType)(res["Default"]), (int)(res["Health"]));
                                break;
                            }
                        case ObjectType.ability:
                            {
                                o = new Ability((int)(res["BackCard"]), (int)(res["ObjectNum"]), (int)(res["Default"]), (int)(res["Cost"]), (bool)(res["Attack"]), res["Name"].ToString(), res["Description"].ToString());
                                break;
                            }
                        default: { o = null; break; }
                    }
                cn.Dispose();
            }
            return o;
        }
    }
}
