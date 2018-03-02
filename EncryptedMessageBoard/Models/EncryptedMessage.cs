namespace EncryptedMessageBoard.Models
{
    public class EncryptedMessage
    {
        public long Id { get; set; }
        public string Author { get; set; }
        private string _Message;
        public string Message
        {
            get { return StringEncryptionUtil.Decrypt(_Message); }
            set { _Message = StringEncryptionUtil.Encrypt(value); }
        }
    }
}
