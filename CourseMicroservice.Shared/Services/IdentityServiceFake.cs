
namespace CourseMicroservice.Shared.Services
{
    public class IdentityServiceFake : IIdentityService
    {
        public Guid GetUserId => Guid.Parse("1f336e13-29c4-4c60-85c5-c5ad748e2b3b");

        public string UserName => "ayumayev";
    }
}
