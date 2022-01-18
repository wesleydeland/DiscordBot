using System;
using System.Linq;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Extensions;

namespace DiscordBot.Commands
{
	public class TeamCommands : BaseCommandModule
	{
		[Command("join")]
		public async Task Join(CommandContext ctx)
		{
			//get member role
			var role = ctx.Guild.GetRole(932798695429242960);

			var joinEmbed = new DiscordEmbedBuilder
			{
				Title = "Would you like to join?",
				Color = DiscordColor.Blue
			};

			var joinMessage = await ctx.Channel.SendMessageAsync(embed: joinEmbed).ConfigureAwait(false);

			var thumbsUpEmoji = DiscordEmoji.FromName(ctx.Client, ":+1:");
			var thumbsDownEmoji = DiscordEmoji.FromName(ctx.Client, ":-1:");


			await joinMessage.CreateReactionAsync(thumbsUpEmoji).ConfigureAwait(false);
			await joinMessage.CreateReactionAsync(thumbsDownEmoji).ConfigureAwait(false);

			var interactivity = ctx.Client.GetInteractivity();

			var result = await interactivity.WaitForReactionAsync(x =>
				x.Message == joinMessage &&
				x.User == ctx.User &&
				(x.Emoji == thumbsUpEmoji ||
				x.Emoji == thumbsDownEmoji)).ConfigureAwait(false);

			if (result.Result.Emoji == thumbsUpEmoji)
            {
				await ctx.Member.GrantRoleAsync(role).ConfigureAwait(false);
            }
            else if (result.Result.Emoji == thumbsDownEmoji)
            {
				await ctx.Member.RevokeRoleAsync(role).ConfigureAwait(false);
            }
            else
            {
                //something is wrong
            }

			await joinMessage.DeleteAsync().ConfigureAwait(false);
		}
	}
}

