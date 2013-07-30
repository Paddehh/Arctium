using Framework.Console.Commands;
using Framework.Constants;
using Framework.ObjectDefines;
using WorldServer.Game.Packets.PacketHandler;
using WorldServer.Game.Spawns;
using WorldServer.Game.WorldEntities;
using WorldServer.Network;
using Framework.Database;
using Framework.Constants.Authentication;
namespace WorldServer.Game.Chat.Commands
{
    public class GMCommands : Globals
    {
        
        [ChatCommand("ban", "")]
        public static void Ban(string[] args, WorldClass session)
        {
            var pName = CommandParser.Read<string>(args, 1);
            ChatMessageValues ChatMessage = new ChatMessageValues(0, "");

            SQLResult result = DB.Characters.Select("SELECT * FROM characters WHERE name = ?", pName);
            if (result.Count == 0)
            {

                ChatMessage.Message = "Could not find character with that name";
                ChatHandler.SendMessage(ref session, ChatMessage);
            }
            else
            {
                var AccId = result.Read<string>(0, "accountid");
                DB.Realms.Execute("UPDATE accounts SET banned = 1 WHERE id = ?", AccId);
   
                ChatMessage.Message = "Successfully banned account";
                ChatHandler.SendMessage(ref session, ChatMessage);
            }


        }
        [ChatCommand("unban", "")]
        public static void Unban(string[] args, WorldClass session)
        {
            ChatMessageValues ChatMessage = new ChatMessageValues(0, "");
            ChatMessage.Message = "Command not yet implemented";
            ChatHandler.SendMessage(ref session, ChatMessage);
            
        }
    }
}
