using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LOLAPI0217.Model
{
    public class Champion
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("info")]
        public ChampionInfo Info { get; set; }

        [JsonPropertyName("tags")]
        public List<string> Tags { get; set; }

    }
}
