using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Azure.Monitor.OpenTelemetry.Exporter;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using System;
using System.Windows.Forms;

namespace winforms_chat
{
    public partial class Form1 : Form
	{
		Kernel kernel;
        IChatCompletionService chat;
        ChatHistory history;

        public Form1()
		{
			InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
		{
            var config = new ConfigurationBuilder().AddUserSecrets<Form1>().Build();
            var builder = Kernel.CreateBuilder();
            builder.AddAzureOpenAIChatCompletion(
                config["AZURE_OPENAI_MODEL"],
                config["AZURE_OPENAI_ENDPOINT"],
                config["AZURE_OPENAI_APIKEY"]);

            builder.Services.AddLogging(
                b => b.AddConsole().SetMinimumLevel(LogLevel.Trace)
                );

            chat = kernel.GetRequiredService<IChatCompletionService>();
            history = new ChatHistory();

            ChatForm.ChatboxInfo cbi = new ChatForm.ChatboxInfo();
			cbi.NamePlaceholder = "Semantic Kernel - Winforms Chat";
			cbi.PhonePlaceholder = "Azure OpenAI - GPT4o";

			var chat_panel = new ChatForm.Chatbox(cbi);
			chat_panel.Name = "chat_panel";
			chat_panel.Dock = DockStyle.Fill;
            chat_panel.kernel = kernel;
            chat_panel.chat = chat;
            chat_panel.history = history;

            Controls.Add(chat_panel);
		}
	}
}
