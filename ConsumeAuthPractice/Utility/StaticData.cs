namespace ConsumeAuthPractice.Utility
{
    public class StaticData
    {
        public static string CrudApiUrl {get; set;}

        public enum ApiType
        {
            Put, Post, Delete, get
        }

        public static string RoleAdmin = "ADMIN";
        public static string RoleCustomer = "CUSTOMER";
        public static string RoleOperator = "OPERATOR";
        public static string RoleAccount = "ACCOUNT";
        public static string RoleTransport = "TRANSPORT";
        public static string TokenValue = "JwtTokenInitialValues";
    }
}
