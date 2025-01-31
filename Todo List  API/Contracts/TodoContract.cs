using Todo_List_API.Contracts;

namespace Todo_List__API.Contracts
{
    public class TodoContract : BaseContract
    {
        public string ?Title { get; set; }
        public string? Description { get; set; }
        public bool IsCompleted { get; set; }
        public string? Email { get; set; }
    }
}
