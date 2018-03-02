using System;

namespace EncryptedMessageBoard.Models
{
    public class EncryptedMessage
    {
        public long Id { get; set; }
        public string Author { get; set; }
        private string _Message;
        public string Message
        {
            get { return _Message; }
            set { _Message = StringEncryptionUtil.Encrypt(value, EncryptionIv); }
        }
        public string TimeStamp { get; set; }
        public byte[] EncryptionIv { get; set; }

        public EncryptedMessage()
        {
            EncryptionIv = StringEncryptionUtil.GenerateInitializationVector();
            TimeStamp = DateTime.Now.ToString().TrimEnd();
        }
        public EncryptedMessage(string author, string message)
        {
            EncryptionIv = StringEncryptionUtil.GenerateInitializationVector();
            TimeStamp = DateTime.Now.ToString().TrimEnd();
            Author = author;
            Message = message;
        }

        public String DecryptMessage()
        {
            return StringEncryptionUtil.Decrypt(Message, EncryptionIv);
        }
    }
}
