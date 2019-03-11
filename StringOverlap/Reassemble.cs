using System;
using System.Collections.Generic;
using System.Linq;

namespace StringOverlap
{
    public class Reassemble : IDisposable
    {
        #region Private Variables
        /// <summary>
        /// Collection which holds the String Fragments
        /// </summary>
        private List<string> stringFragments = null;
        /// <summary>
        ///  String Fragment Index 1  - Stores the string index found out of Longest Overlap match
        /// </summary>
        private int stringFragmentIndex1 = 0;
        /// <summary>
        /// String Fragment Index 2 - Stores the string index found out of Longest Overlap match
        /// </summary>
        private int stringFragmentIndex2 = 0;
        /// <summary>
        /// Stores the Longest overlap - after each pass
        /// </summary>
        private int longestOverlap = 0;
        /// <summary>
        /// Flag: Has Dispose already been called?
        /// </summary>
        private bool disposed = false;
        #endregion

        #region Public Properties
        public List<string> StringFragments
        {
            get
            {
                return stringFragments;
            }
            set
            {
                stringFragments = value;
            }
        }
        #endregion

        #region Constructor
        public Reassemble()
        {
            PopulateStringFragmentsWithRandomCollection();
        }
        #endregion

        #region Public Methods
        public void RefreshCountersForNextRound()
        {
            longestOverlap = 0;
            stringFragmentIndex1 = 0;
            stringFragmentIndex2 = 0;
        }

        /// <summary>
        /// Helper function to Merge the Overlap Fragments found after each pass.
        /// Each pass our collection of fragments will be reduced by one.
        /// </summary>
        public void MergeTheOverLapFragments()
        {

            if (stringFragments.Count <= 2 && longestOverlap == 0)
            {
                // we got only two fragments which are not overlapping : return s1 + s2
                stringFragments[0] = stringFragments[0] + stringFragments[1];
                stringFragments.RemoveAt(1);
            }

            else
            {
                // Get the Two Fragments
                var s1 = stringFragments[stringFragmentIndex1];
                var s2 = stringFragments[stringFragmentIndex2];
                string mergeString = string.Empty;

                if (s1.Contains(s2))
                {
                    stringFragments.RemoveAt(stringFragmentIndex2);
                    return;
                }
                if (s2.Contains(s1))
                {
                    stringFragments.RemoveAt(stringFragmentIndex1);
                    return;
                }

                mergeString = s1 + s2.Substring(longestOverlap);

                if (stringFragmentIndex1 < stringFragmentIndex2)
                {
                    stringFragments.RemoveAt(stringFragmentIndex1); // O(n)
                    stringFragments.RemoveAt(stringFragmentIndex2 - 1); // O(n) - shifts one places
                }
                else
                {
                    stringFragments.RemoveAt(stringFragmentIndex2); // O(n)
                    stringFragments.RemoveAt(stringFragmentIndex1 - 1); // O(n) - shifts one places
                }

                stringFragments.Add(mergeString); // O(1) adding at the last
            }
        }

        /// <summary>
        /// Main Loop for the Greedy Macth and Merge. 
        /// We will Scan all the string fragments to find the Longest Overlap between any two fragments. 
        /// Once found - we Merge them and we repeat this process until we have only one fragment left with us.
        /// this loops runs in O(n^2)
        /// </summary>
        public void FindLongestOverlap()
        {
            for (int i = 0; i < stringFragments.Count; ++i)
            {
                for (int j = i + 1; j < stringFragments.Count; ++j)
                {
                    var overLap = FindLongestOverlap(stringFragments[i], stringFragments[j]);
                    /*
                     *   checking if s2 overlaps s1  
                     *   s1 = all is well
                     *   s2 =         ell that ends well
                     *   in this case s2 overlaps s1 
                     */
                    if (overLap > longestOverlap)  // found the longest Overlap : update the Current Longest Overlap and store the indices as well for Merge Purpose
                    {
                        UpdateLongestOverlapWithFragments(i, j, overLap);
                    }

                    if (overLap == 0)
                    {
                        /* check if s1 overlaps s2
                         * s1 =         ell that ends well
                         * s2 = all is well
                         * here s1 overlaps on s2
                         */
                        overLap = FindLongestOverlap(stringFragments[j], stringFragments[i]);
                        if (overLap > longestOverlap)  // found the longest Overlap : update the Current Longest Overlap and store the indixes as well for Merge Purpose
                        {
                            UpdateLongestOverlapWithFragments(j, i, overLap);
                        }
                    }

                }
            }
        }
        /// <summary>
        /// Helper function to populate the string Fragments with some random collection
        /// </summary>
        public void PopulateStringFragmentsWithRandomCollection()
        {
            //stringFragments = new List<string>(new string[] { "all is well", "ell that en", "hat end" , "t ends well" }); // all is well that ends well
            stringFragments = new List<string>(new string[] { "Bucks -- Beat", "Go Bucks", "o Bucks -- B", "Beat Mich", "ichigan~", "Bucks", "Michigan~" });  // Go Bucks -- Beat Michigan~
        }
        /// <summary>
        /// Helper function to check if we have valid (minimum) counts to proceed 
        /// </summary>
        /// <returns></returns>
        public bool isValidMinimumCount()
        {
            if (stringFragments != null)
            {
                return stringFragments.Count >= 2 ? true : false;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region Private Methods
        /// <summary>
        /// Helper Function to update the Longest Overlap and the String Fragment Indexes (s1,s2)
        /// </summary>
        /// <param name="fragmentIndex1">Frangment Index of the String 1 (s1)</param>
        /// <param name="fragmentIndex2">Fragment Index of the String 2 (s2)</param>
        /// <param name="overLap"></param>
        private void UpdateLongestOverlapWithFragments(int fragmentIndex1, int fragmentIndex2, int overLap)
        {
            longestOverlap = overLap;
            stringFragmentIndex1 = fragmentIndex1;
            stringFragmentIndex2 = fragmentIndex2;
        }
        /// <summary>
        /// Check Overlap function checks if the "String fragment 1" and "string fragment 2" are matching for given Overlap Length
        /// </summary>
        /// <param name="s1">String Fragment 1</param>
        /// <param name="s2">String Fragment 2</param>
        /// <param name="overlapLength">Overlap Length</param>
        /// <returns>returns True - if macth is succesfull else false</returns>
        private bool CheckOverlap(string s1, string s2, int overlapLength)
        {
            if (overlapLength <= 0) return false;
            if (overlapLength <= s1.Length && overlapLength <= s2.Length)
            {
                return (s1.Substring(s1.Length - overlapLength) == s2.Substring(0, overlapLength)) ? true : false;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Find Longest Overlap - finds the Longest overlap from the given string fragments (s1 and s2)
        /// </summary>
        /// <param name="s1">String Fragment 1</param>
        /// <param name="s2">String fragment 2</param>
        /// <returns>returns a longest overlap found between string fragments s1 and s2 (int)</returns>
        private int FindLongestOverlap(string s1, string s2)
        {
            if (s1.Contains(s2)) return s2.Length;
            if (s2.Contains(s1)) return s1.Length;

            char endChar = s1.ToCharArray().Last();
            char startChar = s2.ToCharArray().First();

            int s1FirstIndexOfStartChar = s1.IndexOf(startChar);
            int overlapLength = s1.Length - s1FirstIndexOfStartChar;

            while (overlapLength >= 0 && s1FirstIndexOfStartChar >= 0)
            {
                if (CheckOverlap(s1, s2, overlapLength))
                {
                    return overlapLength;
                }

                s1FirstIndexOfStartChar = s1.IndexOf(startChar, s1FirstIndexOfStartChar + 1);
                overlapLength = s1.Length - s1FirstIndexOfStartChar;
            }
            return 0;
        }


        #endregion

        #region IDisposable Implementation
        /// <summary>
        /// Public implementation of Dispose pattern callable by consumers.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);              // I am calling you from Dispose, it's safe
            GC.SuppressFinalize(this);  // Hey GC: don't bother calling finalize later
        }

        /// <summary>
        /// Protected implementation of Dispose pattern.
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;
            /*  Free managed resources too, but only if I'm being called from Dispose
             *  (If I'm being called from Finalize then the objects might not exist anymore
             */
            if (disposing)
            {
                // Free any other managed objects here.
                if (stringFragments != null)
                {
                    stringFragments = null;
                }
            }
            // Free any unmanaged objects here.
            disposed = true;
        }

        #endregion


    }
}
