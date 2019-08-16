namespace Api.Network
{
    public class GreyAddressDto
    {
        public int? Id { get; set; }
        public string Address { get; set; }
        public string CompositeAddress => GetCompositeAddress();
        public WhiteAddressDto White { get; set; }
        
        private string GetCompositeAddress()
        {
            if (Address == null || White == null)
            {
                return null;
            }

            return Address + " - " + White.Address;
        }  
        
        
    }
}