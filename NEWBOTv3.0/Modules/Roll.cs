using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DandDBot.Modules
{
    public class Roll : ModuleBase<SocketCommandContext>
    {
        [Command("d")]
        public async Task RollAsync(int numberOfSides = 0, int amountOfDice = 1, string modifierOrStat = "0", string userName = "-")
        {
            string errorCode = "default"; //1+2 = Toolarge, 3 = badusername, 4 = badmodifier
            EmbedBuilder builder = new EmbedBuilder();
            string asterikNatural = "";
            int rollAddition = 0;
            List<string> fileData = new List<string>();
            int finalRoll = 0;
            char profileAccess = 'n';
            int modifierValue = 0;
            if (numberOfSides > 100 || numberOfSides < 1)
            {
                errorCode = "1";
            }
            if (amountOfDice > 20 || amountOfDice < 1)
            {
                errorCode = "2";
            }

            if (modifierOrStat != "0")
            {
                try
                {
                    modifierValue = Convert.ToInt32(modifierOrStat);
                }
                catch (FormatException)
                {
                    profileAccess = 'y';
                }
            }

            if (profileAccess == 'y' && userName == "-")
            {
                errorCode = "3";
            }

            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            StreamReader sr;
            int i = 0;
            int cumulativeRoll = 0;
            byte[] singleByteBuf = new byte[1];
            int max = Byte.MaxValue - Byte.MaxValue % numberOfSides;
            while (i < amountOfDice)
            {
                rng.GetBytes(singleByteBuf);
                int b = singleByteBuf[0];
                if (b < max)
                {
                    int currentRoll = b % numberOfSides + 1;
                    cumulativeRoll += currentRoll;
                    i += 1;
                }
            }

            if ((cumulativeRoll == 1 || cumulativeRoll == 20) && (amountOfDice == 1) && (numberOfSides == 20))
            {
                asterikNatural = "*";
            }

            if (profileAccess == 'n')
            {
                rollAddition = Convert.ToInt32(modifierOrStat);
            }
            else
            {
                try
                {
                    sr = new StreamReader("D:\\GoogleDrive\\Discord_Bot\\DandD_Profiles\\" + userName + ".txt");
                    sr.Close();
                }
                catch (IOException)
                {
                    errorCode = "3";
                }

                if (errorCode != "3")
                {
                    sr = new StreamReader("D:\\GoogleDrive\\Discord_Bot\\DandD_Profiles\\" + userName + ".txt");
                    fileData.Add(sr.ReadLine()); //Discord ID
                    fileData.Add(sr.ReadLine()); //Strength
                    fileData.Add(sr.ReadLine()); //Dexterity
                    fileData.Add(sr.ReadLine()); //Constitution
                    fileData.Add(sr.ReadLine()); //Intelligence
                    fileData.Add(sr.ReadLine()); //Wisdom
                    fileData.Add(sr.ReadLine()); //Charisma
                    fileData.Add(sr.ReadLine()); //Proficiency
                    sr.Close();


                    switch (modifierOrStat)
                    {
                        case "s":
                            rollAddition = Convert.ToInt32(fileData[1]);
                            break;
                        case "d":
                            rollAddition = Convert.ToInt32(fileData[2]);
                            break;
                        case "n":
                            rollAddition = Convert.ToInt32(fileData[3]);
                            break;
                        case "i":
                            rollAddition = Convert.ToInt32(fileData[4]);
                            break;
                        case "w":
                            rollAddition = Convert.ToInt32(fileData[5]);
                            break;
                        case "c":
                            rollAddition = Convert.ToInt32(fileData[6]);
                            break;
                        case "sp":
                            rollAddition = Convert.ToInt32(fileData[1]) + Convert.ToInt32(fileData[7]);
                            break;
                        case "dp":
                            rollAddition = Convert.ToInt32(fileData[2]) + Convert.ToInt32(fileData[7]);
                            break;
                        case "np":
                            rollAddition = Convert.ToInt32(fileData[3]) + Convert.ToInt32(fileData[7]);
                            break;
                        case "ip":
                            rollAddition = Convert.ToInt32(fileData[4]) + Convert.ToInt32(fileData[7]);
                            break;
                        case "wp":
                            rollAddition = Convert.ToInt32(fileData[5]) + Convert.ToInt32(fileData[7]);
                            break;
                        case "cp":
                            rollAddition = Convert.ToInt32(fileData[6]) + Convert.ToInt32(fileData[7]);
                            break;
                        default:
                            errorCode = "4";
                            break;
                    }
                }
            }

            finalRoll = cumulativeRoll + rollAddition;

            switch (errorCode)
            {
                case "1":
                    builder.WithTitle("Error")
                    .WithDescription($"The number of sides you entered was too large or too small.")
                    .WithColor(Color.Red);

                    await ReplyAsync("", false, builder.Build());
                    break;
                case "2":
                    builder.WithTitle("Error")
                    .WithDescription($"The number of dice you entered was too large or too small.")
                    .WithColor(Color.Red);

                    await ReplyAsync("", false, builder.Build());
                    break;
                case "3":
                    builder.WithTitle("Error")
                    .WithDescription($"You either didn't enter a username or didn't enter a valid username.")
                    .WithColor(Color.Red);

                    await ReplyAsync("", false, builder.Build());
                    break;
                case "4":
                    builder.WithTitle("Error")
                    .WithDescription($"You entered an invalid modifier or stat.")
                    .WithColor(Color.Red);

                    await ReplyAsync("", false, builder.Build());
                    break;
                default:
                    builder.WithTitle("Roll Results")
                    .WithDescription($"You rolled a {finalRoll}{asterikNatural}.")
                    .WithColor(Color.LighterGrey);

                    await ReplyAsync("", false, builder.Build());
                    break;
            }
        }
    }
}
