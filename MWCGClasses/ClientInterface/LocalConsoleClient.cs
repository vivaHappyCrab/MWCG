using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MWCGClasses.ClientInterface
{
    public class LocalConsoleClient : Client
    {
        public Answer CreateAction(ActionType type, List<int> targets)
        {
            foreach (int target in targets)
                Console.Write(string.Format("{0};",target));
            Console.WriteLine();
            while(true){
                int n = int.Parse(Console.ReadLine());
                if (targets.Select(x => x == n).Any())
                {
                    Answer a = new Answer();
                    a.ActionType = type;
                    a.Target = n;
                    return a;
                }
            }
        }
    }
}
