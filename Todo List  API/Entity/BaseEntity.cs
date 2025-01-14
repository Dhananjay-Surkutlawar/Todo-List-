namespace Todo_List__API.Entity
{
    public class BaseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid(); 

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        public bool IsCompleted { get; set; } = false;
    }
}
