using MWCGClasses.InGame;
using MWCGClasses.GameObjects;

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
                        perm.OnSummon?.Invoke(game, perm);
                        OnObjectEnter(game, perm);
                        game.AddToBattleField(perm, card.Owner);
                        break;
                    }
                case CardType.Spell:
                    {
                        //CreateAction(opponent,PlayAnswer);
                        Spell spell = game.Factory.GetObjectToPlayer(card.EntityId,game.Players[card.Owner])as Spell;
                        OnSpellStartedCast(game, spell);
                        if(spell==null)return;
                        Event ev = game.Factory.GetEventById(spell.Effect);
                        ev.Invoke(game, spell);
                        OnSpellCompletedCast(game, spell);  
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
                    art?.OnEnter?.Invoke(game, obj);
            }
        }

        public static void OnDeath(Game game, GameObject obj)
        {
            foreach (Player p in game.Players)
            {
                foreach (Unit un in p.Field.Units)
                    un.OnDeath?.Invoke(game, obj);
                foreach (Support sup in p.Field.Supports)
                    sup.OnDeath?.Invoke(game, obj);
                foreach (Artifact art in p.Field.Face.Arts)
                    art?.OnDeath?.Invoke(game, obj);
            }
        }
        #endregion
    }
}
