using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DandDBot.Modules
{
    public class Time : ModuleBase<SocketCommandContext>
    {
        [Command("time")]
        public async Task TimeAsync()
        {
            EmbedBuilder builder = new EmbedBuilder();

            builder.WithTitle("Current Time")
                    .WithDescription(DateTime.Now.ToString())
                    .WithColor(Color.Green);

            await ReplyAsync("", false, builder.Build());
        }
    }
}
