using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecuringAppSecrets.Helper
{
    public class KeyVaultHelper
    {
        public static string GetValue(string Guid)
        {
            string value = null;
            return value;
        }
        public static readonly string clientId = "8a605fae-cd90-4081-946e-18957824e620";
        public static readonly string clientSecret = "q8nH696jDSupMsXBF8/FI2kTGrxeDACu5P/v9D1/314=";
        public static async Task<string> GetKeyVaultValueAsync(string KeyVaultName)
        {
            //var secretId = KeyVaultVariables.GetValue(KeyVaultName);
            var secretUri = $"https://appsecretkeyvault.vault.azure.net/secrets/AuthorizationKey/44308889c85f4c6999c6640fd792a765";
            var azureServiceTokenProvider = new AzureServiceTokenProvider();
            var kv = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(azureServiceTokenProvider.KeyVaultTokenCallback));// GetToken));
            var sec = await kv.GetSecretAsync(secretUri);

            return sec.Value;
        }
        private static async Task<string> GetToken(string authority, string resource, string scope)
        {
            var authContext = new AuthenticationContext(authority);
            ClientCredential clientCred = new ClientCredential(Encryption.Decrypt(clientId, false), Encryption.Decrypt(clientSecret, false));
            AuthenticationResult result = await authContext.AcquireTokenAsync(resource, clientCred);

            if (result == null)
                throw new InvalidOperationException("Failed to obtain the JWT token");

            return result.AccessToken;
        }
    }
}
