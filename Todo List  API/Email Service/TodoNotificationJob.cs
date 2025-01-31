
namespace Todo_List__API.Email_Service
{
    public class TodoNotificationJob
    {
        private readonly IEmailService _emailService;

        public TodoNotificationJob(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task SendTodoNotificationAsync(string userEmail, string todoTitle)
        {
            var subject = "Todo Task Notification";
            var message = $"Your Todo task '{todoTitle}' has been successfully added!";

            // Send the email using IEmailService
            await _emailService.SendEmailAsync(userEmail, subject, message);
        }
    }
}