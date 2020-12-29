using RandomBundles.CustomBundles.Patches;
using StardewModdingAPI;
using System;

namespace RandomBundles.Commands
{

    // Sets bundle type through SMAPI console
    class SetBundleType
    {
        public static string CommandInfo = "Sets the bundle type of the community center.\n" + CommandUsage;
        public static string CommandUsage = "Usage: setBundleType <S:type> <B:use_seed>\n- type: bundle type 'normal', 'remixed', or 'randomized'\n- use_seed: boolean use level seed";

        private static IMonitor Monitor;

        public static void Initialize(IMonitor monitor)
        {
            Monitor = monitor;
        }

        public void RunCommand(string command, string[] args)
        {
            try
            {
                bool use_seed = Convert.ToBoolean(args[1]);

                switch (args[0])
                {
                    case "normal":
                        {
                            GenerateBundlesPatch.SetBundleData(0, use_seed);
                            break;
                        }
                    case "remixed":
                        {
                            GenerateBundlesPatch.SetBundleData(1, use_seed);
                            break;
                        }
                    case "randomized":
                        {
                            GenerateBundlesPatch.SetBundleData(2, use_seed);
                            break;
                        }
                    default: Monitor.Log("Invalid bundle type", LogLevel.Info); return;
                }
                Monitor.Log($"Set bundle type to {args[0]}", LogLevel.Info);
            }
            catch (Exception ex)
            {
                Monitor.Log(ex.StackTrace, LogLevel.Error);
            }
        }
    }
}
