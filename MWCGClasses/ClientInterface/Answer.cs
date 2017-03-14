using System.Collections.Generic;

namespace MWCGClasses.ClientInterface
{
    public class Answer
    {
        public int Target { get; set; }

        public List<int> Targets { get; set; }
        
        public ActionType ActionType { get; set; }
    }
}
