using System;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

namespace DiscordBot.Commands
{
	public class AdminCommands : BaseCommandModule
	{
		[Command("members")]
		[Description("Lists all members in server")]
		[RequireRoles(RoleCheckMode.Any, new string[] { "admin" })]
		public async Task Members(CommandContext ctx)
		{
			foreach (var member in ctx.Guild.Members)
			{
				await ctx.Member.SendMessageAsync(member.ToString()).ConfigureAwait(false);
			}

		}
	}
}

