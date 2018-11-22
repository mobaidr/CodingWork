using System;
using System.Linq;
using System.Reactive.Linq;

namespace BasicExample
{
    public static class ExampleOne
    {
        public static void BasicExample()
        {
            var query = from number in Enumerable.Range(1, 5) select number;

            var oQuery = query.ToObservable();
            oQuery.Subscribe(Console.WriteLine, ImDone);
            Console.ReadKey();

            void ImDone() { Console.WriteLine("I'm Done"); }
        }
    }
}
