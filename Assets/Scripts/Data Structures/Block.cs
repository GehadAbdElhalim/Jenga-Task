using Newtonsoft.Json;

namespace JengaTask
{
    [System.Serializable]
    public class Block
    {
        [JsonProperty]
        public int Id { get; private set; }
        [JsonProperty]
        public string Subject { get; private set; }
        [JsonProperty]
        public string Grade { get; private set; }
        [JsonProperty]
        public MasteryType Mastery { get; private set; }
        [JsonProperty]
        public string DomainId { get; private set; }
        [JsonProperty]
        public string Domain { get; private set; }
        [JsonProperty]
        public string Cluster { get; private set; }
        [JsonProperty]
        public string StandardId { get; private set; }
        [JsonProperty]
        public string StandardDescription { get; private set; }

        public enum MasteryType
        {
            GLASS = 0,
            WOOD = 1,
            STONE = 2
        }
    }
}