

namespace bMovieTracker.App
{
    public class UserVM
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public UserVM() { }

        public UserVM(int id)
        {
            Id = id;
        }

        public string FullName => $"{FirstName} {LastName}";
    }
}
