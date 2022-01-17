using System;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

namespace DiscordBot.Commands
{
	public class FunCommands : BaseCommandModule
	{
		[Command("ping")]
		[Description("Returns pong")]
		public async Task Ping(CommandContext ctx) 
		{
			await ctx.Channel.SendMessageAsync("pong").ConfigureAwait(false);
		}

		[Command("add")]
		[Description("Adds two numbers together")]
		public async Task Add(CommandContext ctx, int a, int b)
		{
			int result = a + b;
			await ctx.Channel.SendMessageAsync(result.ToString()).ConfigureAwait(false);
		}
	}
}

