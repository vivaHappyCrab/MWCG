using System;
using System.Collections.Generic;
using MWCGClasses;
using MWCGClasses.GameObjects;

namespace MWData
{
    public interface IDataAccessor
    {
        Card GetCard(int id);
        List<Card> GetCardList();
        List<Tuple<int, int>> GetEventList();
        GameObject GetObject(int id);
        List<GameObject> GetObjectList();
        Race GetRace(int id);
        List<Race> GetRaceList();
    }
}