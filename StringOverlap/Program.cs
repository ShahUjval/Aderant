using System;
using System.Linq;

namespace StringOverlap
{
    class Program
    {

        static void Main(string[] args)
        {

            // Step 1 - intializes the String Fragments
            //Create and object of Reassemble (this intializes the String Fragments)
            Reassemble reassemble = new Reassemble();
                        
            // Only one or no string fragment present - Return as it is.
            if (!reassemble.isValidMinimumCount())
            {
                Console.WriteLine("reassemble requires minimum two String Fragments to proceed : Input string fragment count is : {0}" , reassemble.StringFragments.Count);
                return;
            }

            // Display the input string
            Console.WriteLine("Input String Fragments are : ");
            Console.WriteLine("-------------------------------------------------------------------------------------");
            foreach (var item in reassemble.StringFragments.ToList())
            {
                Console.WriteLine(item);
            }

            // Continue until you have only one item in collection
            while (reassemble.StringFragments.Count != 1)
            {
                //Step 2. Find the Longest Overlap in string fragments
                reassemble.FindLongestOverlap();

                //Step 3. Merge the found frangments with largest overlap and Contiue the process.    
                reassemble.MergeTheOverLapFragments();

                // - Refresh the Counter for Next round
                reassemble.RefreshCountersForNextRound();
            }

            Console.WriteLine("-------------------------------------------------------------------------------------");
            Console.WriteLine("Assembled String after Match and Merge : " + reassemble.StringFragments[0]);
            Console.ReadKey();

            reassemble.Dispose();
        }
       
    }
}
