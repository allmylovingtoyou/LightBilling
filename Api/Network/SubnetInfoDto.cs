namespace Api.Network
{
    public class SubnetInfoDto
    {
        public int Id { get; set; }
        public string Net { get; set; }
        public int Mask { get; set; }
        public string Gateway { get; set; }
    }
}