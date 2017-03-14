using System;
using System.Collections.Generic;
using System.Linq;

namespace MWCGClasses.ClientInterface
{
    public class LocalConsoleClient : IClient
    {
        public Answer CreateAction(ActionType type, List<int> targets)
        {
            foreach (int target in targets)
                Console.Write("{0};", target);
            Console.WriteLine();
            while(true)
            {
                int n;
                if(!int.TryParse(Console.ReadLine(),out n))n=-1;
                if (!targets.Select(x => x == n).Any()) continue;
                Answer a = new Answer
                {
                    ActionType = type,
                    Target = n
                };
                return a;
            }
        }
    }
}
