namespace Contracts.Models
{

    public enum Roles { Admin = 1,Consumer = 2,Deliverer = 3};

    public static class RolesConstants
    {
        public const string Admin = "Admin";
        public const string Consumer = "Consumer";
        public const string Deliverer = "Deliverer";

        public const string ConsumerDeliverer = Consumer + "," + Deliverer;

        public const string AdminConsumer = Admin + "," + Consumer;

        public const string All = Admin + "," + Consumer + "," + Deliverer;
    }
}
