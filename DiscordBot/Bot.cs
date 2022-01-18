﻿using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using DiscordBot.Commands;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.EventArgs;
using Newtonsoft.Json;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Extensions;

namespace DiscordBot
{
	public class Bot
	{
		public DiscordClient Client { get; private set; }
		public CommandsNextExtension Commands { get; private set; }

		public Bot()
		{
		}

		public async Task RunAsync()
        {
			var json = string.Empty;

			using (var fs = File.OpenRead("config.json"))
			using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
			{
				json = await sr.ReadToEndAsync().ConfigureAwait(false);
			}

			var configJson = JsonConvert.DeserializeObject<ConfigJson>(json);

			var discordConfig = new DiscordConfiguration
			{
				Token = configJson.Token,
				TokenType = TokenType.Bot,
				AutoReconnect = true,
				MinimumLogLevel = Microsoft.Extensions.Logging.LogLevel.Debug

			};

			Client = new DiscordClient(discordConfig);
			Client.Ready += OnClientReady;

			var commandsConfig = new CommandsNextConfiguration
			{
				StringPrefixes = new string[] {configJson.Prefix},
				EnableMentionPrefix = true,
				EnableDms = false,
				DmHelp = true
			};

			Client.UseInteractivity(new InteractivityConfiguration
            {

            });

			Commands = Client.UseCommandsNext(commandsConfig);
			Commands.RegisterCommands<FunCommands>();
			Commands.RegisterCommands<AdminCommands>();
			Commands.RegisterCommands<TeamCommands>();

			await Client.ConnectAsync();
			await Task.Delay(-1);
        }

		private Task OnClientReady(DiscordClient client, ReadyEventArgs args)
		{
			return Task.CompletedTask;
		}
	}
}

