namespace MWCGClasses.Enums
{
    /// <summary>
    /// Тип наносимого урона.
    /// </summary>
    public enum DamageType
    {
        /// <summary>
        /// Чистый
        /// </summary>
        /// <remarks>Не режется ничем, не усиливается ни от чего.</remarks>
        Pure =0,

        /// <summary>
        /// Физический
        /// </summary>
        /// <remarks>Наносится юнитами и оружием при атаке.</remarks>
        Physical = 1,

        /// <summary>
        /// Магический
        /// </summary>
        /// <remarks>Наносится заклинаниями, усиливается силой магии. </remarks>
        Magical = 2
    }
}