using System.Collections.Generic;

namespace MWCGClasses.ClientInterface
{
    public enum ActionType{
        FieldObjects,
        GraveYardObjects,
        HandCard,
        LibCard,
        OppOpenCard,
        OppCloseCard,
        System 
    }
    public interface IClient
    {
        Answer CreateAction(ActionType type, List<int> targets);
    }
}
