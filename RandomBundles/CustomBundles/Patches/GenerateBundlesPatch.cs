using System;
using System.Collections.Generic;
using System.IO;
using Harmony;
using StardewValley;

namespace RandomBundles.CustomBundles.Patches
{
    [HarmonyPatch(typeof(Game1), "GenerateBundles")]
    class GenerateBundlesPatch
    {
        private static ModEntry Main;

        public static void Initialize(ModEntry main)
        {
            Main = main;
        }

        [HarmonyPrefix]
        public static bool Prefix(ref int bundle_type, ref bool use_seed)
        {
            Main.DebugMessage("Generating bundle...");

            SetBundleData(bundle_type, use_seed);

            switch (bundle_type)
            {
                case 0: Main.DebugMessage("'Normal' bundle generated"); break;
                case 1: Main.DebugMessage("'Remixed' bundle generated"); break;
                default: Main.DebugMessage("'Unknown' bundle generated with id of " + bundle_type); break;
            }
            return false;
        }

        public static void SetBundleData(int bundle_type, bool use_seed)
        {
            Random r = null;
            r = ((!use_seed) ? new Random() : new Random((int)Game1.uniqueIDForThisGame * 9));

            switch (bundle_type)
            {
                default:
                    {
                        Game1.netWorldState.Value.SetBundleData(Game1.content.LoadBase<Dictionary<string, string>>("Data\\Bundles"));
                        break;
                    }
                case 1:
                    {
                        Dictionary<string, string> bundle_data = new BundleGenerator().Generate("Data\\RandomBundles", r);
                        Game1.netWorldState.Value.SetBundleData(bundle_data);
                        break;
                    }
                case 2:
                    {
                        string path = Path.Combine(Main.Helper.DirectoryPath, "Data\\RandomizedBundles.json");
                        Dictionary<string, string> bundle_data = new CustomBundleGenerator().Generate(path, r);
                        Game1.netWorldState.Value.SetBundleData(bundle_data);
                        break;
                    }
            }
        }
    }
}
