using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrystalReport
{
    public class Language
    {
        public static string TransliterateToHindi(string inputText)
        {
            Dictionary<string, string> transliterationMap = GetTransliterationMap();

            StringBuilder transliteratedText = new StringBuilder();

            // Convert input text to lowercase for simplicity (you might need more sophisticated handling)
            inputText = inputText.ToLower();

            for (int i = 0; i < inputText.Length; i++)
            {
                string currentChar = inputText[i].ToString();

                // Handling cases like "sh", "ch", etc., if required
                if (i + 1 < inputText.Length)
                {
                    string doubleChar = inputText.Substring(i, 2);
                    if (transliterationMap.ContainsKey(doubleChar))
                    {
                        transliteratedText.Append(transliterationMap[doubleChar]);
                        i++; // Skip the next character as it has been considered
                        continue;
                    }
                }

                if (transliterationMap.ContainsKey(currentChar))
                {
                    transliteratedText.Append(transliterationMap[currentChar]);
                }
                else
                {
                    // If the character is not found in the map, retain the original character
                    transliteratedText.Append(currentChar);
                }
            }

            return transliteratedText.ToString();
        }

        static Dictionary<string, string> GetTransliterationMap()
        {
            // Mapping between Romanized English characters and Hindi Devanagari characters
            Dictionary<string, string> map = new Dictionary<string, string>
            {
                {"a", "अ"},
                {"b", "ब"},
                {"c", "क"},
                {"d", "ड"},
                {"e", "ए"},
                {"f", "फ"},
                {"g", "ग"},
                {"h", "ह"},
                {"i", "इ"},
                {"j", "ज"},
                {"k", "क"},
                {"l", "ल"},
                {"m", "म"},
                {"n", "न"},
                {"o", "ओ"},
                {"p", "प"},
                {"q", "क़"}, // Not a standard mapping; used for demonstration purposes
                {"r", "र"},
                {"s", "स"},
                {"t", "ट"},
                {"u", "उ"},
                {"v", "व"},
                {"w", "व"}, // Not a standard mapping; used for demonstration purposes
                {"x", "क्ष"},
                {"y", "य"},
                {"z", "ज़"},
                {"sh", "श"}, // Additional mappings for compound characters
                {"ch", "च"},
                // Add more mappings as needed...
                {" ", " "} // Keep space as is
            };

            return map;
        }
    
}
}
