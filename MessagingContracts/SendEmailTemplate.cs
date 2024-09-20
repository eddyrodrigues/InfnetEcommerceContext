namespace MessagingContracts
{
    public class SendEmailTemplate
    {
        public string to { get; set; }
        public string subject { get; set; }
        public string body { get; set; }
        public string attachmentPath { get; set; }
    }
}
