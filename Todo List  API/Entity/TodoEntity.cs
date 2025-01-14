namespace Todo_List__API.Entity
{
    public class TodoEntity : BaseEntity
    {
        public string ?Title { get; set; }
        public string? Description { get; set; }
        public bool IsCompleted { get; set; }
    }
}
