namespace LogisticAPI.Enums
{
    public class ErrorTypeEnum : Enumeration
    {
        public ErrorTypeEnum(string idString, string name) : base(idString, name) { }

        public static readonly ErrorTypeEnum IdFormatTransportGroundIsRequired = new("SE0001", "Id is no valid, it has to be 3 leters amd 3 numbers. An example is 'ABC123'");
        public static readonly ErrorTypeEnum IdFormatTransportMaritimeIsRequired = new("SE0002", "Id is no valid, it has to be 3 leters amd 4 numbers and 1 letters at the end. An example is 'ABC1234D'");
        public static readonly ErrorTypeEnum TypeTransportIsRequired = new("SE0003", "Type transport is not valid");

    }
}
