using System;
using System.Collections.Generic;

namespace MWCGClasses.ClientInterface
{
    public class Answer
    {
        public int Target { get; set; } = -1;

        public List<int> Targets { get; set; } = null;

        public Tuple<int, int> Pair { get; set; } = null;
        
        public ActionType ActionType { get; set; } = ActionType.Skip;
    }
}
