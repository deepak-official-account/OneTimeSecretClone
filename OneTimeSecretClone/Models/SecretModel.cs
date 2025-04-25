using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OneTimeSecretClone.Models
    {
    public class SecretModel
        {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Message { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public DateTime CreationDateTime { get; set; } = DateTime.UtcNow;
        [NotMapped]
        public string ExpiryDelay { get; set; }
        [Required]
        public DateTime ExpiryDateTime { get; set; }
      
        [Required]
        public bool IsMessageViewed { get; set; } = false;


        public SecretModel() { }
        public SecretModel(string message, string password, DateTime creationDateTime, string expiryDelay, DateTime expiryDateTime, bool isMessageViewed)
            {
            Message = message;
            Password = password;
            CreationDateTime = creationDateTime;
            ExpiryDelay = expiryDelay;
            ExpiryDateTime = expiryDateTime;
            IsMessageViewed = isMessageViewed;
            }
        }
    }
