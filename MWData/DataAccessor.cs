using System;
using System.Collections.Generic;
using MWCGClasses;
using MWCGClasses.GameObjects;
using System.Data.SqlClient;
using System.Data;
using MWCGClasses.Enums;

namespace MWData
{
    public class DataAccessor
    {
        public static string Connector = @"Server=CRAB\SQLDB; DataBase=MagicWar;Integrated Security=SSPI; ";

        public static Card GetCard(int id)
        {
            Card card = new Card();
            using (SqlConnection cn=new SqlConnection(Connector))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("dbo.getCard", cn) {CommandType = CommandType.StoredProcedure};
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
        public static List<Card> GetCardList()
        {
            List<Card> result = new List<Card>();
            using (SqlConnection cn = new SqlConnection(Connector))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("dbo.getCard", cn) {CommandType = CommandType.StoredProcedure};
                SqlDataReader res = cmd.ExecuteReader();
                while (res.Read())
                {
                    Card card = new Card
                    {
                        CardId = (int) res["CardId"],
                        Collectable = (bool) res["Collectable"],
                        Description = (string) res["Description"],
                        EntityId = (int) res["EntityId"],
                        ManaCost = (int) res["ManaCost"],
                        Name = (string) res["Name"],
                        Rarity = (RareType) res["Rarity"],
                        Type = (CardType) res["Type"]
                    };
                    result.Add(card);
                }
                cn.Dispose();
            }
            return result;
        }
        public static List<GameObject> GetObjectList()
        {
            List<GameObject> result = new List<GameObject>();
            using (SqlConnection cn = new SqlConnection(Connector))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("dbo.getGameObject", cn) {CommandType = CommandType.StoredProcedure};
                SqlDataReader res = cmd.ExecuteReader();
                while (res.Read())
                {
                    GameObject o;
                    switch ((ObjectType)res["OType"])
                    {
                        case ObjectType.Creature: {
                                o = new Unit((int)res["BackCard"], (int)res["ObjectNum"], res["Name"].ToString(), res["Description"].ToString(), (int)res["Attack"], (int)res["Health"]); 
                                break;
                            }
                        case ObjectType.Support:
                            {
                                o = new Support((int)res["BackCard"], (int)res["ObjectNum"], res["Name"].ToString(), res["Description"].ToString(), (int)res["Health"]);
                                break;
                            }
                        case ObjectType.Hero:
                            {
                                o = new Hero((int)res["BackCard"], (int)res["ObjectNum"], res["Name"].ToString(), res["Description"].ToString(), (int)res["Health"], (int)res["Default"]);
                                break;
                            }
                        case ObjectType.Spell:
                            {
                                o = new Spell((int)res["BackCard"], (int)res["ObjectNum"], (int)res["Default"], res["Name"].ToString(), res["Description"].ToString());
                                break;
                            }
                        case ObjectType.Artifact:
                            {
                                o = new Artifact((int)res["BackCard"], (int)res["ObjectNum"], res["Name"].ToString(), res["Description"].ToString(), (ArtType)res["Default"], (int)res["Health"]);
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
        public static GameObject GetObject(int id)
        {
            GameObject o;
            using (SqlConnection cn = new SqlConnection(Connector))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("dbo.getGameObject", cn) {CommandType = CommandType.StoredProcedure};
                cmd.Parameters.Add(new SqlParameter("id", id));
                SqlDataReader res = cmd.ExecuteReader();
                res.Read();
                    switch ((ObjectType)res["OType"])
                    {
                        case ObjectType.Creature:
                            {
                                o = new Unit((int)res["BackCard"], (int)res["ObjectNum"], res["Name"].ToString(), res["Description"].ToString(), (int)res["Attack"], (int)res["Health"]);
                                break;
                            }
                        case ObjectType.Support:
                            {
                                o = new Support((int)res["BackCard"], (int)res["ObjectNum"], res["Name"].ToString(), res["Description"].ToString(), (int)res["Health"]);
                                break;
                            }
                        case ObjectType.Hero:
                            {
                                o = new Hero((int)res["BackCard"], (int)res["ObjectNum"], res["Name"].ToString(), res["Description"].ToString(), (int)res["Health"], (int)res["Default"]);
                                break;
                            }
                        case ObjectType.Spell:
                            {
                                o = new Spell((int)res["BackCard"], (int)res["ObjectNum"], (int)res["Default"], res["Name"].ToString(), res["Description"].ToString());
                                break;
                            }
                        case ObjectType.Artifact:
                            {
                                o = new Artifact((int)res["BackCard"], (int)res["ObjectNum"], res["Name"].ToString(), res["Description"].ToString(), (ArtType)res["Default"], (int)res["Health"]);
                                break;
                            }
                        default: { o = null; break; }
                    }
                cn.Dispose();
            }
            return o;
        }
        public static Race GetRace(int id)
        {
            Race race = new Race();
            using (SqlConnection cn = new SqlConnection(Connector))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("dbo.getRace", cn) {CommandType = CommandType.StoredProcedure};
                cmd.Parameters.Add(new SqlParameter("id", id));
                SqlDataReader res = cmd.ExecuteReader();
                res.Read();
                race.RaceId = (int)res["RaceId"];
                race.Name = res["Name"].ToString();
                race.HeroId = (int)res["HeroId"];
            }
            return race;
        }
        public static List<Race> GetRaceList()
        {
            List<Race> result = new List<Race>();
            using (SqlConnection cn = new SqlConnection(Connector))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("dbo.getRace", cn) {CommandType = CommandType.StoredProcedure};
                SqlDataReader res = cmd.ExecuteReader();
                while (res.Read())
                {
                    Race race = new Race
                    {
                        RaceId = (int) res["RaceId"],
                        Name = res["Name"].ToString(),
                        HeroId = (int) res["HeroId"]
                    };
                    result.Add(race);
                }
                cn.Dispose();
            }
            return result;
        }
        public static List<Tuple<int,int?>> GetEventList()
        {
            List<Tuple<int, int?>> result = new List<Tuple<int, int?>>();
            using (SqlConnection cn = new SqlConnection(Connector))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("dbo.getEventList", cn) { CommandType = CommandType.StoredProcedure };
                SqlDataReader res = cmd.ExecuteReader();
                while (res.Read())
                {
                    int? enter = res["EnterEvent"] == DBNull.Value ? null : (int?) res["EnterEvent"];
                    result.Add(new Tuple<int, int?>((int)res["Id"],enter));
                }
                cn.Dispose();
            }
            return result;
        }
    }
}
