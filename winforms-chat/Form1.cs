using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.Extensions.Configuration;
using Microsoft.SemanticKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            var config = new ConfigurationBuilder().AddUserSecrets<Form1>().Build();
            var builder = Kernel.CreateBuilder();
            builder.AddAzureOpenAIChatCompletion(
                config["AZURE_OPENAI_MODEL"],
                config["AZURE_OPENAI_ENDPOINT"],
                config["AZURE_OPENAI_APIKEY"]);
            kernel = builder.Build();
            chat = kernel.GetRequiredService<IChatCompletionService>();
            history = new ChatHistory();
        }

        private void Form1_Load(object sender, EventArgs e)
		{
			ChatForm.ChatboxInfo cbi = new ChatForm.ChatboxInfo();
			cbi.NamePlaceholder = "Intro to Semantic Kernel";
			cbi.PhonePlaceholder = "Azure OpenAI - GPT4o";

			var chat_panel = new ChatForm.Chatbox(cbi);
			chat_panel.Name = "chat_panel";
			chat_panel.Dock = DockStyle.Fill;
            chat_panel.kernel = kernel;
            chat_panel.chat = chat;
            chat_panel.history = history;

            this.Controls.Add(chat_panel);
		}
	}
}
