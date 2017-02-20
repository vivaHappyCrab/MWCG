using System;
using System.Collections.Generic;
using System.Text;
using MWCGClasses.InGame;
using MWCGClasses.GameObjects;
using MWData;

namespace MWCGClasses
{
    public delegate void Event(Game g, GameObject obj);
    public class GameAction
    {
        public static void PlayCard(Game game, Card card)
        {
            switch (card.Type)
            {
                case CardType.Permanent:
                    {
                        //CreateAction(opponent,PlayAnswer);
                        GameObject perm = game.Factory.getObjectById(card.EntityId);
                        perm.onSummon?.Invoke(game, perm);
                        onObjectEnter(game, perm);
                        game.AddToBattleField(perm, card.Owner);
                        break;
                    }
            }
        }

        public static void onObjectEnter(Game game, GameObject obj)
        {
            foreach (Player p in game.Players)
            {
                foreach (GameObject un in p.Field.Units)
                    un.onEnter(game, obj);
                foreach (GameObject sup in p.Field.Supports)
                    sup.onEnter(game, obj);
                foreach (GameObject art in p.Field.Face.Arts)
                    art.onEnter(game, obj);
            }
        }

        public static void onObjectTakesDamage(Game game, GameObject obj)
        {
            foreach (Player p in game.Players)
            {
                foreach (GameObject un in p.Field.Units)
                    un.onTakeDamage(game, obj);
                foreach (GameObject sup in p.Field.Supports)
                    sup.onTakeDamage(game, obj);
                foreach (GameObject art in p.Field.Face.Arts)
                    art.onEnter(game, obj);
            }
        }
    }
}
