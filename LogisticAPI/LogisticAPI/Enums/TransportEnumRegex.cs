namespace LogisticAPI.Enums
{
    public static class TransportEnumRegex 
    {
        public static readonly string GROUND_TRANSPORT = "^[A-Za-z]{3}\\d{3}$";
        public static readonly string MARINE_TRANSPORT = "^[A-Za-z]{3}\\d{4}[A-Za-z]$";
    }
}
