using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NEWBOTv3.Modules
{
    public class SetNick: ModuleBase<SocketCommandContext>
    {
        [Command("setnick")]
        public async Task SetNickAsync(string userName)
        {
            /*
            switch (Context.User.Id)
            {
                case 176815679599542274: //Jared
                case 170671108675076108: //Thomas
                case 175755733395046400: //Colin
                    var user = Context.Channel.GetUsersAsync(Context.ar).FirstOrDefault();
                    string newname = e.GetArg("newname");
                    await user.Edit(nickname: newname).ConfigureAwait(false);
                    break;
                default:
                    await ReplyAsync("You do not have permission to use this command.");
                    break;
            }
            */
        }
    }
}
