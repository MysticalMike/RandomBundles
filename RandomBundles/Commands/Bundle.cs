using Netcode;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Locations;
using System;
using System.Collections.Generic;

namespace RandomBundles.Commands
{
    class Bundle
    {
        public static string CommandInfo = "Complete or uncomplete community center bundle.\n" + CommandUsage;
        public static string CommandUsage = "Usage: bundle <I:ID> <B:completed>\n- ID: interger bundle id\n- locked: boolean set completed";

        private static IMonitor Monitor;

        public static void Initialize(IMonitor monitor)
        {
            Monitor = monitor;
        }

        public void RunCommand(string command, string[] args)
        {
            try
            {
                int id = Convert.ToInt32(args[0]);
                bool completed = Convert.ToBoolean(args[1]);
                string state = completed ? "completed" : "uncompleted";

                if (id == -1)
                {
                    foreach (KeyValuePair<int, NetArray<bool, NetBool>> fieldPair in (Game1.getLocationFromName("CommunityCenter") as CommunityCenter).bundles.FieldDict)
                    {
                        for (int index = 0; index < fieldPair.Value.Count; ++index)
                        {
                            fieldPair.Value[index] = completed;
                        }
                    }
                    Monitor.Log($"All bundles have been {state}", LogLevel.Info);
                }
                else
                {
                    for (int index = 0; index < (Game1.getLocationFromName("CommunityCenter") as CommunityCenter).bundles[id].Length; ++index)
                    {
                        (Game1.getLocationFromName("CommunityCenter") as CommunityCenter).bundles.FieldDict[id][index] = completed;
                    }
                    Monitor.Log($"Bundle {id} has been {state}.", LogLevel.Info);
                }
                Game1.playSound("crystal");
            }
            catch (Exception ex)
            {
                Monitor.Log(ex.Message, LogLevel.Info);
            }
        }
    }
}
