using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DandDBot.Modules
{
    public class UserHelp : ModuleBase<SocketCommandContext>
    {
        [Command("help")]
        public async Task UserHelpAsync(string userSelection = "general")
        {
            EmbedBuilder builder = new EmbedBuilder();

            switch (userSelection)
            {
                case "general":
                    builder.WithTitle("General Help")
                    .WithDescription("To get help using a command, type '!help <command name>'.")
                    .WithColor(Color.LighterGrey);

                    await ReplyAsync("", false, builder.Build());
                    break;
                case "roll":
                    builder.WithTitle("Roll (d)")
                    .WithDescription("Rolls dice.\n Usage 1: '!d <number of sides> <amount of dice> <modifier>'\n Usage 2: '!d <number of sides> <amount of dice> <stat> <player>'")
                    .WithColor(Color.LighterGrey);

                    await ReplyAsync("", false, builder.Build());
                    break;
                default:
                    builder.WithTitle("Error")
                    .WithDescription("That is not a command. Try typing: '!help <command name>'.")
                    .WithColor(Color.Red);

                    await ReplyAsync("", false, builder.Build());
                    break;
            }
        }
    }
}
