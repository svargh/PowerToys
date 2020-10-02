// Copyright (c) Microsoft Corporation
// The Microsoft Corporation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

// Code forked from Betsegaw Tadele's https://github.com/betsegaw/windowwalker/
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Microsoft.Plugin.WindowWalker.Components
{
    /// <summary>
    /// Class housing fuzzy matching methods
    /// </summary>
    public static class SimpleMatching
    {
        /// <summary>
        /// Finds the best match (the one with the most
        /// number of letters adjacent to each other) and
        /// returns the index location of each of the letters
        /// of the matches
        /// </summary>
        /// <param name="text">The text to search inside of</param>
        /// <param name="searchText">the text to search for</param>
        /// <returns>returns the index location of each of the letters of the matches</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1814:Prefer jagged arrays over multidimensional", Justification = "matches does not waste space with the current implementation, however this could probably be optimized to store the indices of matches instead of boolean values.  Currently there are no unit tests for this, but we could refactor if memory/perf becomes an issue. ")]
        public static List<int> FindMatch(string text, string searchText)
        {
            if (searchText == null)
            {
                throw new ArgumentNullException(nameof(searchText));
            }

            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }

            searchText = searchText.ToLower(CultureInfo.CurrentCulture);
            text = text.ToLower(CultureInfo.CurrentCulture);

            List<string> searchStrings = new List<string>();
            if (searchText.Contains("|", StringComparison.CurrentCulture))
            {
                string[] vs = searchText.Split("|");
                foreach (string s in vs)
                {
                    searchStrings.Add(s);
                }
            }
            else
            {
                searchStrings.Add(searchText);
            }

            List<int> bestMatch = new List<int>();
            foreach (string splittedSearchString in searchStrings)
            {
                if (text.Contains(splittedSearchString, StringComparison.CurrentCulture))
                {
                    bestMatch.Add(1);
                    break;
                }
            }

            return bestMatch;
        }
    }
}
