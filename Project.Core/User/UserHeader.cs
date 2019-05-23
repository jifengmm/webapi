
namespace Project.Core.User
{
    public interface IUserHeader
    {
        int Id { get; set; }

        string Name { get; set; }

        string CompanyId { get; set; }
    }

    public class UserHeader : IUserHeader
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string CompanyId { get; set; }
    }
}
