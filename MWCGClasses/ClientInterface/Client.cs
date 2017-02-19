using System;
using System.Collections.Generic;
using System.Text;

namespace MWCGClasses.ClientInterface
{
    public enum ActionType{
        FieldObjects,
        GraveYardObjects,
        HandCard,
        LibCard,
        OppOpenCard,
        OppCloseCard
    }
    public interface Client
    {
        Answer CreateAction(ActionType type, List<int> targets);
    }
}
