using System;

namespace MWCGClasses.Enums
{
    [Flags]
    public enum Keywords
    {
        /// <summary>
        /// No keywords.
        /// </summary>
        None =0,
        //--------Main Types-------------------

        /// <summary>
        /// Activated when enter the battlefield.
        /// </summary>
        Haste =0x1,

        /// <summary>
        /// When attacks, heals himself.
        /// </summary>
        Vampirism=0x2,

        /// <summary>
        /// Attack faster then opponent.
        /// </summary>
        FirstStrike=0x4,

        /// <summary>
        /// Attack with opponent and then one more time.
        /// </summary>
        DoubleDamage=0x8,

        /// <summary>
        /// Don't used in attack phase.
        /// </summary>
        CantAttack=0x10,

        /// <summary>
        /// Don't targeted in attack phase.
        /// </summary>
        CantBeAttacked=0x20,

        /// <summary>
        /// Don't used in defender phase.
        /// </summary>
        CantDefend=0x40,

        /// <summary>
        /// Don't targeted in defender phase.
        /// </summary>
        CantBeDefended=0x80,

        /// <summary>
        /// Any amount of damage enough to kill unit.
        /// </summary>
        CorrosiveAttack=0x100,

        /// <summary>
        /// Unit can't be targeted by skills.
        /// </summary>
        Shroud=0x200,

        /// <summary>
        /// Unit don't get magical damage.
        /// </summary>
        SpellImmune=0x400,

        /// <summary>
        /// Unit don't get physical damage.
        /// </summary>
        Protected=0x800,

        //------------------Combo parameters

        /// <summary>
        /// Unit can only defend.
        /// </summary>
        Defender=0x30,

        /// <summary>
        /// Unit attacks first and then attacks together one more time.
        /// </summary>
        FirstDoubleDamage=0xc
    }
}
