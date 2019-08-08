using System;
using Api.Network;
using Newtonsoft.Json;

namespace Api.House
{
    /// <summary>
    /// Сущность дома
    /// </summary>
    public class HouseInfoDto
    {
        public int? Id { get; set; }
        public string Address { get; set; }
        public string Number { get; set; }
        public string AdditionalNumber { get; set; }
        public string Comment { get; set; }

        public string Porch { get; set; }

        [JsonIgnore]
        public SubnetDto Subnet { get; set; }
        public string SubnetString => getSubnetString(Subnet);

        private String getSubnetString(SubnetDto subnetDto)
        {
            if (subnetDto == null)
            {
                return null;
            }

            return subnetDto.Net + "/" + subnetDto.Mask;
        }
    }
}