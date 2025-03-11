using Azure;
using Azure.AI.OpenAI;
using OpenAI.Chat;

namespace AzureAI.SpeechConcat.Services
{
    public class OpenAIService(string endpoint, string key, string deployment, string systemMessage)
    {
        private readonly string _endpoint = endpoint;
        private readonly string _key = key;
        private readonly string _deployment = deployment;
        private readonly string _systemMessage = systemMessage;

        public async Task<string> Chat(string userMessage)
        {
            var client = new AzureOpenAIClient(new Uri(_endpoint), new AzureKeyCredential(_key));

            ChatClient chatClient = client.GetChatClient(_deployment);            

            var messages = new List<ChatMessage>
            {
                new SystemChatMessage(_systemMessage),
                new UserChatMessage(userMessage)
            };

            var options = new ChatCompletionOptions
            {
                Temperature = 0,
                MaxOutputTokenCount = 4096,
                TopP = 0,
                FrequencyPenalty = 0,
                PresencePenalty = 0
            };

            var completion = await chatClient.CompleteChatAsync(messages, options);

            return completion.Value.Content.First().Text;
        }
    }
}
