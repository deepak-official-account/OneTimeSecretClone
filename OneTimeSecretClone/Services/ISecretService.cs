using OneTimeSecretClone.Models;
namespace OneTimeSecretClone.Services
    {
    public interface ISecretService
        {
         void CreateSecret(SecretModel secret);
    SecretModel GetSecretModelById(int secretId);
    string GenerateLink(SecretModel secret);

    ResponseDto ViewMessageHandler(int secretId, string password);

    }
    }
