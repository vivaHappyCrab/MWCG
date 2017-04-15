using MWCGClasses.InGame;
using MWCGClasses.GameObjects;
using System;
using MWCGClasses.Enums;

namespace MWCGClasses
{
    public delegate void Event(Game g, GameObject obj);

    public delegate void PlayerEvent(Game g, Player player);

    public delegate void CardEvent(Game g, Card card);

    public class GameAction
    {
        public static void PlayCard(Game game, Card card)
        {
            if (card == null)
                return;
            if (game == null)
                throw new ArgumentNullException(nameof(game));
            if (card.ManaCost > game.Players[card.Owner].Mana)
                return;
            if (!game.Players[card.Owner].Hand.Contains(card))
                return;

            game.Players[card.Owner].Mana -= card.ManaCost;
            game.Players[card.Owner].Hand.Remove(card);

            switch (card.Type)
            {
                case CardType.Permanent:
                    {
                        //CreateAction(opponent,PlayAnswer);
                        GameObject perm = game.CreateObject(card.EntityId);
                        perm.OnSummon?.Invoke(game, perm);
                        OnObjectEnter(game, perm);
                        game.AddToBattleField(perm, card.Owner);
                        break;
                    }
                case CardType.Spell:
                    {
                        //CreateAction(opponent,PlayAnswer);
                        Spell spell = game.Factory.GetObjectToPlayer(card.EntityId, game.Players[card.Owner]) as Spell;
                        OnSpellStartedCast(game, spell);
                        if (spell == null) return;
                        Event ev = game.Factory.GetEventById(spell.Effect);
                        ev.Invoke(game, spell);
                        OnSpellCompletedCast(game, spell);
                        spell.Owner.Graves.Graves.Add(spell);
                        break;
                    }
            }
        }

        #region Event generators

        private static void OnSpellCompletedCast(Game game, Spell spell)
        {
            foreach (Player p in game.Players)
            {
                foreach (Unit un in p.Field.Units)
                    un.OnSpellCastStart?.Invoke(game, spell);
                foreach (Support sup in p.Field.Supports)
                    sup.OnSpellCastStart?.Invoke(game, spell);
                foreach (Artifact art in p.Field.Face.Arts)
                    art?.OnSpellCastStart?.Invoke(game, spell);
            }
        }

        public static void OnDrawCard(Game game, Card card)
        {
            foreach (Player p in game.Players)
            {
                foreach (Unit un in p.Field.Units)
                    un.OnDrawCard?.Invoke(game, card);
                foreach (Support sup in p.Field.Supports)
                    sup.OnDrawCard?.Invoke(game, card);
                foreach (Artifact art in p.Field.Face.Arts)
                    art?.OnDrawCard?.Invoke(game, card);
            }
        }

        private static void OnSpellStartedCast(Game game, Spell spell)
        {
            foreach (Player p in game.Players)
            {
                foreach (Unit un in p.Field.Units)
                    un.OnSpellCastCompleted?.Invoke(game, spell);
                foreach (Support sup in p.Field.Supports)
                    sup.OnSpellCastCompleted?.Invoke(game, spell);
                foreach (Artifact art in p.Field.Face.Arts)
                    art?.OnSpellCastCompleted?.Invoke(game, spell);
            }
        }

        public static void OnObjectEnter(Game game, GameObject obj)
        {
            foreach (Player p in game.Players)
            {
                foreach (Unit un in p.Field.Units)
                    un.OnEnter?.Invoke(game, obj);
                foreach (Support sup in p.Field.Supports)
                    sup.OnEnter?.Invoke(game, obj);
                foreach (Artifact art in p.Field.Face.Arts)
                    art?.OnEnter?.Invoke(game, obj);
            }
        }

        public static void OnObjectTakesDamage(Game game, GameObject obj)
        {
            foreach (Player p in game.Players)
            {
                foreach (Unit un in p.Field.Units)
                    un.OnTakeDamage?.Invoke(game, obj);
                foreach (Support sup in p.Field.Supports)
                    sup.OnTakeDamage?.Invoke(game, obj);
                foreach (Artifact art in p.Field.Face.Arts)
                    art?.OnTakeDamage?.Invoke(game, obj);
            }
        }

        public static void OnObjectDealsDamage(Game game, GameObject obj)
        {
            foreach (Player p in game.Players)
            {
                foreach (Unit un in p.Field.Units)
                    un.OnDealDamage?.Invoke(game, obj);
                foreach (Support sup in p.Field.Supports)
                    sup.OnDealDamage?.Invoke(game, obj);
                foreach (Artifact art in p.Field.Face.Arts)
                    art?.OnDealDamage?.Invoke(game, obj);
            }
        }

        public static void OnTurnStart(Game game, Player player)
        {
            foreach (Player p in game.Players)
            {
                foreach (Unit un in p.Field.Units)
                    un.OnTurnStart?.Invoke(game, player);
                foreach (Support sup in p.Field.Supports)
                    sup.OnTurnStart?.Invoke(game, player);
                foreach (Artifact art in p.Field.Face.Arts)
                    art?.OnTurnStart?.Invoke(game, player);
            }
        }

        public static void OnDeath(Game game, GameObject obj)
        {
            foreach (Player p in game.Players)
            {
                foreach (Unit un in p.Field.Units)
                    un.OnPermDeath?.Invoke(game, obj);
                foreach (Support sup in p.Field.Supports)
                    sup.OnPermDeath?.Invoke(game, obj);
                foreach (Artifact art in p.Field.Face.Arts)
                    art?.OnPermDeath?.Invoke(game, obj);
            }
        }
        #endregion
    }
}
