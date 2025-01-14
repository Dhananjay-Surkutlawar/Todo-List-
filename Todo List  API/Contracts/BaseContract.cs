namespace Todo_List_API.Contracts
{
    public abstract class BaseContract
    {
        public Guid Id { get; set; } 

        public DateTime CreatedAt { get; set; } 

        public DateTime? UpdatedAt { get; set; }

    }
}
