using MongoDB.Bson;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoruscantMarketplace.DataLayer.Modelos
{
    public class ModeloBase
    {
        [JsonIgnore]
        public ObjectId Id { get; set; }
        [JsonProperty("id")]
        public string IdString
        {
            get
            {
                return Id.ToString();
            }

            set
            {
                Id = new ObjectId(value);
            }
        }
    }
}
