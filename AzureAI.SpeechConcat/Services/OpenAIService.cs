using Azure;
using Azure.AI.OpenAI;
using OpenAI.Chat;

namespace AzureAI.SpeechConcat.Services
{
    public class OpenAIService
    {
        private readonly ChatClient _chatClient;
        private readonly string _systemMessage;

        public OpenAIService(string endpoint, string key, string deployment, string systemMessage)
        {
            var client = new AzureOpenAIClient(new Uri(endpoint), new AzureKeyCredential(key));
            _chatClient = client.GetChatClient(deployment);

            _systemMessage = systemMessage;
        }

        public async Task<string> Chat(string userMessage)
        {
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

            try
            {
                var completion = await _chatClient.CompleteChatAsync(messages, options);

                return completion.Value.Content.First().Text;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("429"))
                {
                    Thread.Sleep(5000);
                    return await Chat(userMessage);
                }

                throw;
            }
        }
    }
}
