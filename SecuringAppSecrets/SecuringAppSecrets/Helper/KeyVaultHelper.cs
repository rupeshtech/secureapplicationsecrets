using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecuringAppSecrets.Helper
{
    public class KeyVaultHelper
    {
        public static readonly string clientId = "8a605fae-cd90-4081-946e-18957824e620";
        public static readonly string clientSecret = "q8nH696jDSupMsXBF8/FI2kTGrxeDACu5P/v9D1/314=";
        public static async Task<string> GetValueAsync(string Guid)
        {
            string value = null;
            var kv = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(GetToken));
            var sec = await kv.GetSecretAsync($"https://appsecretkeyvault.vault.azure.net/secrets/AuthorizationKey/bb008eba16ce4b96a275f35b1eaed0e1");
            value = sec.Value;
            return value;
        }
        
        public static async Task<string> GetToken(string authority, string resource, string scope)
        {
            var authContext = new AuthenticationContext(authority);
            ClientCredential clientCred = new ClientCredential(clientId,clientSecret);
            AuthenticationResult result = await authContext.AcquireTokenAsync(resource, clientCred);

            if (result == null)
                throw new InvalidOperationException("Failed to obtain the JWT token");

            return result.AccessToken;
        }

        public static async Task<string> GetKeyVaultValueAsync(string KeyVaultName)
        {
            //var secretId = KeyVaultVariables.GetValue(KeyVaultName);
            var secretUri = $"https://appsecretkeyvault.vault.azure.net/secrets/ApiKey/834027a4e821407f96c7370a1d7113ee";
            var azureServiceTokenProvider = new AzureServiceTokenProvider();
            var kv = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(azureServiceTokenProvider.KeyVaultTokenCallback));// GetToken));
            var sec = await kv.GetSecretAsync(secretUri);

            return sec.Value;
        }
    }
}
