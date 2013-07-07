/*
 * Copyright (C) 2012-2013 Arctium <http://arctium.org>
 * 
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

using Framework.Console.Commands;
using Framework.Database;
using Framework.Logging;

namespace WorldServer.Console.Commands
{
    public class AccountCommands : CommandParser
    {
        [Command("caccount", "")]
        public static void CreateAccount(string[] args)
        {
            string name = Read<string>(args, 0);
            string password = Read<string>(args, 1);

            if (name == null || password == null)
                return;

            name = name.ToUpper();
            password = password.ToUpper();

            SQLResult result = DB.Realms.Select("SELECT * FROM accounts WHERE name = ?", name);
            if (result.Count == 0)
            {
                if (DB.Realms.Execute("INSERT INTO accounts (name, password, language) VALUES (?, ?, '')", name, password))
                    Log.Message(LogType.Normal, "Account {0} successfully created", name);
            }
            else
                Log.Message(LogType.Error, "Account {0} already in database", name);
        }

        [Command("daccount", "")]
        public static void DeleteAccount(string[] args)
        {
            string AccountName = Read<string>(args, 0);

            if (AccountName == null)
                return;

            AccountName = AccountName.ToUpper();

            SQLResult result = DB.Realms.Select("SELECT * FROM accounts WHERE name = ?", AccountName);
            if (result.Count == 1)
            {
                if(DB.Realms.Execute("DELETE FROM accounts WHERE name = ?", AccountName))
                    Log.Message(LogType.Normal, "Account {0} Deleted Auccessfully", AccountName);

            }
            else
            {
                Log.Message(LogType.Error, "Account {0} Does not exist", AccountName);
            }

        }

        [Command("setgmlevel", "")]
        public static void Set_gm_level(string[] args)
        {
            string AccountName = Read<string>(args, 0);
            string gmlevel = Read<string>(args, 1);

            AccountName = AccountName.ToUpper();

            if (AccountName == null || gmlevel == null)
                return;
            SQLResult result = DB.Realms.Select("SELECT * FROM Accounts WHERE name = ?", AccountName);
            if (result.Count == 1)
            {
                if (DB.Realms.Execute("UPDATE Accounts SET gmlevel = ? WHERE name = ?", gmlevel, AccountName))
                    Log.Message(LogType.Normal, "Sucessfully updated GM level {0} to the account {1}", gmlevel, AccountName);
            }
            else
            {
                Log.Message(LogType.Error, "Error, Account does not exist");
            }
        }
    }
}
