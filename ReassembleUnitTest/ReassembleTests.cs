using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StringOverlap;

namespace ReassembleUnitTest
{
    [TestClass]
    public class ReassembleTests
    {
        [TestMethod]
        public void TestReassembleWithDataSetExample1()
        {

            List<string> stringFragmentsTest = new List<string>(new string[] { "all is well", "ell that en", "hat end", "t ends well" }); // all is well that ends well

            string expectedFinalMergedString = "all is well that ends well";

            // Step 1 - intializes the String Fragments
            //Create and object of Reassemble (this intializes the String Fragments)
            Reassemble reassemble = new Reassemble();
            reassemble.StringFragments = stringFragmentsTest;

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

            Assert.AreEqual(expectedFinalMergedString, reassemble.StringFragments[0]);
            reassemble.Dispose();
        }

        [TestMethod]
        public void TestReassembleWithDataSetExample2()
        {

            List<string> stringFragmentsTest = new List<string>(new string[] { "Bucks -- Beat", "Go Bucks", "o Bucks -- B", "Beat Mich", "ichigan~", "Bucks", "Michigan~" });  // Go Bucks -- Beat Michigan~

            string expectedFinalMergedString = "Go Bucks -- Beat Michigan~";

            // Step 1 - intializes the String Fragments
            //Create and object of Reassemble (this intializes the String Fragments)
            Reassemble reassemble = new Reassemble();
            reassemble.StringFragments = stringFragmentsTest;

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

            Assert.AreEqual(expectedFinalMergedString, reassemble.StringFragments[0]);
            reassemble.Dispose();
        }

        /// <summary>
        /// Test With 2 fragments with no overlap
        /// </summary>
        [TestMethod]
        public void TestReassembleWithDataSetExample3()
        {

            List<string> stringFragmentsTest = new List<string>(new string[] { "Go Bucks", "Beat Michigan"});  // Go Bucks Beat Michigan

            string expectedFinalMergedString = "Go BucksBeat Michigan";

            // Step 1 - intializes the String Fragments
            //Create and object of Reassemble (this intializes the String Fragments)
            Reassemble reassemble = new Reassemble();
            reassemble.StringFragments = stringFragmentsTest;

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

            Assert.AreEqual(expectedFinalMergedString, reassemble.StringFragments[0]);
            reassemble.Dispose();
        }
        /// <summary>
        /// Test With More than 3 fragments with no overlap
        /// </summary>
        [TestMethod]
        public void TestReassembleWithDataSetExample4()
        {

            List<string> stringFragmentsTest = new List<string>(new string[] { "Go Bucks", "Beat Michigan" , " alaska" , " miami"});  // Go Bucks Beat Michigan

            string expectedFinalMergedString = "Go BucksBeat Michigan alaska miami";

            // Step 1 - intializes the String Fragments
            //Create and object of Reassemble (this intializes the String Fragments)
            Reassemble reassemble = new Reassemble();
            reassemble.StringFragments = stringFragmentsTest;

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

            Assert.AreEqual(expectedFinalMergedString, reassemble.StringFragments[0]);
            reassemble.Dispose();
        }

        [TestMethod]
        public void NullStringTest()
        {
            // Step 1 - intializes the String Fragments
            //Create and object of Reassemble (this intializes the String Fragments)
            Reassemble reassemble = new Reassemble();
            reassemble.StringFragments = null;
            // Only one or no string fragment present - Return as it is.
            if (!reassemble.isValidMinimumCount())
            {
                Assert.AreEqual(null, reassemble.StringFragments);
            }
            reassemble.Dispose();
        }
    }
}
