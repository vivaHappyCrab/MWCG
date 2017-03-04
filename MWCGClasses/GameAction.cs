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
            //todo:убрать из руки и вычесть ману на затраты
            switch (card.Type)
            {
                case CardType.Permanent:
                    {
                        //CreateAction(opponent,PlayAnswer);
                        GameObject perm = game.CreateObject(card.EntityId);
                        perm.onSummon?.Invoke(game, perm);
                        onObjectEnter(game, perm);
                        game.AddToBattleField(perm, card.Owner);
                        break;
                    }
                case CardType.Spell:
                    {
                        //CreateAction(opponent,PlayAnswer);
                        Spell spell = game.Factory.getObjectToPlayer(card.EntityId,game.Players[card.Owner])as Spell;
                        onSpellStartedCast(game, spell);
                        Event ev = game.Factory.getEventById(spell.Effect);
                        ev.Invoke(game, spell);
                        onSpellCompletedCast(game, spell);  
                        break;
                    }
            }
        }

        #region Event generators

        private static void onSpellCompletedCast(Game game, Spell spell)
        {
            foreach (Player p in game.Players)
            {
                foreach (GameObject un in p.Field.Units)
                    un.onSpellCastStart?.Invoke(game, spell);
                foreach (GameObject sup in p.Field.Supports)
                    sup.onSpellCastStart?.Invoke(game, spell);
                foreach (GameObject art in p.Field.Face.Arts)
                    art?.onSpellCastStart?.Invoke(game, spell);
            }
        }

        private static void onSpellStartedCast(Game game, Spell spell)
        {
            foreach (Player p in game.Players)
            {
                foreach (GameObject un in p.Field.Units)
                    un.onSpellCastCompleted?.Invoke(game, spell);
                foreach (GameObject sup in p.Field.Supports)
                    sup.onSpellCastCompleted?.Invoke(game, spell);
                foreach (GameObject art in p.Field.Face.Arts)
                    art?.onSpellCastCompleted?.Invoke(game, spell);
            }
        }

        public static void onObjectEnter(Game game, GameObject obj)
        {
            foreach (Player p in game.Players)
            {
                foreach (GameObject un in p.Field.Units)
                    un.onEnter?.Invoke(game, obj);
                foreach (GameObject sup in p.Field.Supports)
                    sup.onEnter?.Invoke(game, obj);
                foreach (GameObject art in p.Field.Face.Arts)
                    art?.onEnter?.Invoke(game, obj);
            }
        }

        public static void onObjectTakesDamage(Game game, GameObject obj)
        {
            foreach (Player p in game.Players)
            {
                foreach (GameObject un in p.Field.Units)
                    un.onTakeDamage?.Invoke(game, obj);
                foreach (GameObject sup in p.Field.Supports)
                    sup.onTakeDamage?.Invoke(game, obj);
                foreach (GameObject art in p.Field.Face.Arts)
                    art?.onEnter?.Invoke(game, obj);
            }
        }

        public static void onDeath(Game game, GameObject obj)
        {
            foreach (Player p in game.Players)
            {
                foreach (GameObject un in p.Field.Units)
                    un.onDeath?.Invoke(game, obj);
                foreach (GameObject sup in p.Field.Supports)
                    sup.onDeath?.Invoke(game, obj);
                foreach (GameObject art in p.Field.Face.Arts)
                    art?.onDeath?.Invoke(game, obj);
            }
        }
        #endregion
    }
}
