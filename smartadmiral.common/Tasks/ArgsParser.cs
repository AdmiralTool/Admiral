using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace smartadmiral.common.Tasks
{
    public class ArgsParser
    {
        public static Dictionary<string, string> Parse(string args)
        {
            var result = new Dictionary<string, string>();

            var parameters = Regex.Matches(args, @"[^=]+=[\""'].+?[\""']|[^ ]+")
                            .Cast<Match>()
                            .Select(m => m.Value)
                            .ToList();

            foreach (var parameter in parameters)
            {
                var splitParameter = parameter.Split('=');
                var key = splitParameter[0].Trim();
                var value = splitParameter[1].Trim();

                result.Add(key, value);
            }

            return result;
        }

        public static bool IsFeatureAllowed(Dictionary<string, string> dictionary, string featureName)
        {
            return dictionary.ContainsKey(featureName) &&
                   (dictionary[featureName] == "true" || dictionary[featureName] == "yes");
        }
    }
}