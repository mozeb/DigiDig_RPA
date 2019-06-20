using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace FirstTest
{
    public class RecordsList : ObservableCollection<RecordData>
    {
        public RecordsList() : base()
        {

        }
    }

    public class RecordData
    {
        [JsonProperty(PropertyName = "recordName")]
        public string RecordName { get; set; }

        [JsonProperty(PropertyName = "artist")]
        public string Artist { get; set; }

        [JsonProperty(PropertyName = "releaseYear")]
        public string ReleaseYear { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "imageUrl")]
        public Uri RecordImage { get; set; }


        public RecordData()
        {

        }
    }
}
