namespace ComputerStore.BusinessLogicLayer.Validation
{
    public static class RegexCollection
    {
        public const string EmailRegex = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
        public const string PhoneRegex = @"\(?\d{3}\)?[-\.]? *\d{3}[-\.]? *[-\.]?\d{4}";
    }
}