﻿using MWCGClasses.GameObjects;
using MWCGClasses.InGame;

namespace MWData
{
    public static class EnterBf
    {
        public static void YouthBerserkBc(Game g, GameObject obj)
        {
            foreach (Unit unit in obj.Owner.Field.Units)
            {
                unit.TakeDamage(1, g);
            }
        }
    }
}