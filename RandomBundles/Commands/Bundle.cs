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
                int id;
                int id2;

                if (args[0].Contains("-"))
                {
                    id = Convert.ToInt32(args[0].Split('-')[0]);
                    id2 = Convert.ToInt32(args[0].Split('-')[1]);
                }
                else
                {
                    id = Convert.ToInt32(args[0]);
                    id2 = id;
                }

                if (id < 0 || id2 < 0 || id > 36 || id2 > 36)
                {
                    Monitor.Log($"Bundle id must be in 0-36", LogLevel.Info);
                    return;
                }

                if (id2 < id)
                {
                    Monitor.Log($"Range end cannot be less than range start.", LogLevel.Info);
                    return;
                }

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
                    int changed = 0;

                    for (int i = id; i <= id2; i++)
                    {
                        if (!(Game1.getLocationFromName("CommunityCenter") as CommunityCenter).bundles.ContainsKey(i))
                        {
                            Monitor.Log($"Bundle {i} doesn't exsist, skipping.", LogLevel.Info);
                            continue;
                        }

                        for (int index = 0; index < (Game1.getLocationFromName("CommunityCenter") as CommunityCenter).bundles[i].Length; ++index)
                        {
                            (Game1.getLocationFromName("CommunityCenter") as CommunityCenter).bundles.FieldDict[i][index] = completed;
                            changed++;
                        }
                    }

                    if (id2 != 0 && changed > 0)
                    {
                        Monitor.Log($"Bundles {id}-{id2} have been {state}.", LogLevel.Info);
                    }
                    else if (changed > 0)
                    {
                        Monitor.Log($"Bundle {id} has been {state}.", LogLevel.Info);
                    }
                    else
                    {
                        Monitor.Log($"No bundles were changed.", LogLevel.Info);
                    }
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
