namespace Redis.Cache.Application.Models
{
    public class Like
    {
        public Guid Id { get; private set; }
        public string Name { get; set; }

        public Like(string name)
        {
            Id= Guid.NewGuid();
            Name= name;
        }
    }
}
