using OneTimeSecretClone.Models;
namespace OneTimeSecretClone.Services
    {
    public class SecretServiceImpl : ISecretService


        {
        //private readonly SecretDbContext _context=new SecretDbContext();
        private readonly SecretDbContext _context;
        public SecretServiceImpl(SecretDbContext context)
            {
            this._context = context;
            }

        private DateTime ExpiryDateTimeHandler(DateTime creationDateTime, string expiryDelay)
            {
            DateTime expiryDateTime = new DateTime();

            switch (expiryDelay)
                {
                case "5s":
                    expiryDateTime = creationDateTime.AddSeconds(5);
                    break;
                case "1m":
                    expiryDateTime = creationDateTime.AddMinutes(1);
                    break;
                case "5m":
                    expiryDateTime = creationDateTime.AddMinutes(5);
                    break;
                case "1h":
                    expiryDateTime = creationDateTime.AddHours(1);
                    break;
                case "1d":
                    expiryDateTime = creationDateTime.AddDays(1);
                    break;
                case "7d":
                    expiryDateTime = creationDateTime.AddDays(7);
                    break;
                }

            return expiryDateTime;
            }
        public void CreateSecret(SecretModel secret)
            {
            secret.ExpiryDateTime = ExpiryDateTimeHandler(secret.CreationDateTime, secret.ExpiryDelay);
            try
                {

                this._context.Secrets.Add(secret);

                this._context.SaveChanges();
                }
            catch(Exception e){
                Console.WriteLine(e.Message);
                }

            }

        public SecretModel GetSecretModelById(int secretId)
            {
            SecretModel secret = this._context.Secrets.FirstOrDefault(s => s.Id == secretId);

            return secret;
            }

        public string GenerateLink(SecretModel secret)
            {
            CreateSecret(secret);
            SecretModel s= GetSecretModelById(secret.Id);
            int secretId = s.Id;
            string link = "https://localhost:7084/Secret/ViewMessage/" + secretId;

            return link;
            }

        public ResponseDto ViewMessageHandler(int secretId, string password)
            {
            SecretModel secret = GetSecretModelById(secretId);
            ResponseDto response= new ResponseDto();
            if (secret == null)
                {
                response.Message = "No Secret is Present with Given Link";
                return response;
                }

            if (!secret.Password.Equals(password))
                {
                response.Message = "Incorrect Password";
                return response;
                }

            if (IsMessageExpired(secret.ExpiryDateTime) || secret.IsMessageViewed)
                {
                response.Message = "Secret is either Expired or Already Viewed";
                return response;
                }

            secret.IsMessageViewed = true;
            this._context.SaveChanges();
            response.Data = secret.Message;
            response.Message = "Successful";
            
            return response;
            }

        private bool IsMessageExpired(DateTime expiryDateTimeUtc)
            {
            return DateTime.UtcNow > expiryDateTimeUtc;
            }

        }


    }
