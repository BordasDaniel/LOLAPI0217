using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LOLAPI0217.Model
{
    internal class ChampionInfo
    {
        [JsonPropertyName("attack")]
        public int Attack { get; set; }

        [JsonPropertyName("defense")]
        public int Defense { get; set; }
    }
}
