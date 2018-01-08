using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace DandDBot.Modules
{
    public class SetProfile : ModuleBase<SocketCommandContext>
    {
        /*
         * ID
         * strength
         * dexterity
         * constitution
         * inteligence
         * wisdom
         * charisma
         * proficiency bonus
         */

        [Command("setstats")]
        public async Task ProfileAsync(string userName, int strMod = 0, int dexMod = 0, int conMod = 0, int intMod = 0, int wisMod = 0, int chaMod = 0, int proMod = 0)
        {
            string errorCode = ""; // 1: a modifer is out of range 2: no perms
            EmbedBuilder builder = new EmbedBuilder();

            string path = @"D:\\GoogleDrive\\Discord_Bot\\DandD_Profiles\\" + userName + ".txt";

            int[] userProf = new int[7];
            userProf[0] = 0;
            userProf[1] = strMod;
            userProf[2] = dexMod;
            userProf[3] = conMod;
            userProf[4] = intMod;
            userProf[5] = wisMod;
            userProf[6] = chaMod;
            userProf[7] = proMod;

            bool perm = false;

            switch (Context.User.Id)
            {
                case 176815679599542274: // Jared
                case 398989970523553806: // Jeffrey
                case 334096740250681345: // Devon
                    perm = true;
                    break;
            }

            if (!perm)
            {
                errorCode = "2";
            }
            else
            {

                for (int i = 1; i < userProf.Length; i++)
                {
                    if (userProf[i] <= -20 || userProf[i] >= 20)
                        errorCode = "1";
                }

                using (StreamWriter File = new StreamWriter(path))
                {
                    System.IO.File.WriteAllText(path, String.Empty);
                    for (int i = 0; i < userProf.Length; i++)
                    {
                        File.WriteLine(userProf[i]);
                    }
                    File.Close();
                }

                builder.WithTitle("Success")
                        .WithDescription($"This user's stats have been set")
                        .WithColor(Color.Green);

                await ReplyAsync("", false, builder.Build());
            }

            switch (errorCode)
            {
                case "1":
                    builder.WithTitle("Error")
                        .WithDescription($"Your entered modifier was either too large")
                        .WithColor(Color.Red);

                    await ReplyAsync("", false, builder.Build());
                    break;
                case "2":
                    builder.WithTitle("Error")
                        .WithDescription($"You lack the permisions")
                        .WithColor(Color.Red);

                    await ReplyAsync("", false, builder.Build());
                    break;
            }
        }
    }
}