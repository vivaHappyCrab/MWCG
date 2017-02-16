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
        public static void PlayCard(Game game, Card card) {
            switch (card.Type) { 
                case CardType.Permanent:{
                        //CreateAction(opponent,PlayAnswer);
                        GameObject perm=Factory.getObjectById(card.EntityId);
                        perm.onSummon?.Invoke(game, perm);
                        //onObjectEnter(game, perm);must be in game.AddToBF
                        game.AddToBattleField(perm,card.Owner);
                        break;
                }
            }
        }

        public static void onObjectEnter(Game game, GameObject obj) {
            foreach(Unit u in game.Players[obj.Owner.Num].Field.Units)
            {
                u.onEnter(game, obj);
            }
            foreach (Unit u in game.Players[obj.Owner.Num].Field.Units)
            {
                u.onEnter(game, obj);
            }
        }

    }
}
