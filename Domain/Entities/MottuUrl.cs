using Domain.DTO;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain
{
    public class MottuUrl
    {
        [Key]
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("hits")]
        public int Hits { get; set; }
        [Required(AllowEmptyStrings = false,ErrorMessage ="Invalid URL")]
        [MinLength(10)]
        [JsonPropertyName("url")]
        public string Url { get; set; }
        [JsonPropertyName("shortUrl")]
        public string ShortUrl { get; set; }

        public MottuUrl()
        {

        }

        public MottuUrl(MottuUrlDTO mottuUrlDTO)
        {
            Hits = mottuUrlDTO.Hits;
            Url = mottuUrlDTO.Url;
            ShortUrl = mottuUrlDTO.ShortUrl;
        }

        public MottuUrl(Dictionary<string,string> keyValuePairs,string id)
        {
            this.Id = id;
            Hits = 0;
            Url = keyValuePairs.Keys.FirstOrDefault() != default ? keyValuePairs.Keys.First() : throw new ArgumentNullException();
            ShortUrl = keyValuePairs.Values.FirstOrDefault() != default ? keyValuePairs.Values.First() : throw new ArgumentNullException();
        }
    }
}
