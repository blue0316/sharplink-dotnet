namespace CBS_Sports.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string EliasId { get; set; }
        public int Efod { get; set;}
        public string ProTeam { get;}
        public string Jersey { get; set; }
        public string? ProStatus { get; set; }
        public string Position { get; set; }
        public string Photo { get; set; }
        public int Age { get; set;}
        public string? icons { get; set;}
    }
}
