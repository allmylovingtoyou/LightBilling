using Api.Client.Status;
using Api.House;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Api.Client
{
    public class ClientInfoDto
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string Login { get; set; }

        public string CompositeAddress => GetCompositeAddress();

        public double Balance { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public ClientStatus Status { get; set; }

        public bool IsActive { get; set; }

        [JsonIgnore]
        public HouseDto House { get; set; }

        private string GetCompositeAddress()
        {
            if (House == null)
            {
                return null;
            }

            return House?.Address + " " + House?.Number + " " + House?.AdditionalNumber + " " + House?.Porch;
        }
    }
}