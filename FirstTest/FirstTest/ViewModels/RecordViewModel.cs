using System;
using Newtonsoft.Json;
namespace FirstTest
{
    public class RecordViewModel : ObservableObject
    {

        /*[JsonProperty(PropertyName = "recordName")]
        private string _recordName;

        public string RecordName { 
            get
            {
                if (string.IsNullOrEmpty(_recordName))
                    return ("Unknown");

                return _recordName;

            }
            set{
                _recordName = value;
                OnPropertychanged("RecordName");
            } 
        }

        [JsonProperty(PropertyName = "artist")]
        private string _artist;

        public string Artist
        {
            get
            {
                if (string.IsNullOrEmpty(_recordName))
                    return ("Unknown");

                return _artist;

            }
            set
            {
                _artist = value;
                OnPropertychanged("Artist");
            }
        }*/

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
    }
}
