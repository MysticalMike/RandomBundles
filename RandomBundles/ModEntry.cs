using Harmony;
using RandomBundles.Commands;
using RandomBundles.CustomBundles.Patches;
using StardewModdingAPI;

namespace RandomBundles
{
    public class ModEntry : Mod
    {
        public static bool Debug = true;

        public static string stream;

        public override void Entry(IModHelper helper)
        {
            stream = helper.DirectoryPath;

            InitializeClasses(helper);

            var harmony = HarmonyInstance.Create(this.ModManifest.UniqueID);
            harmony.PatchAll();

            RegisterCommands(helper);
        }

        private void InitializeClasses(IModHelper helper)
        {
            GenerateBundlesPatch.Initialize(this);
            PopulateOptionsPatch.Initialize(this);
            BundlePatch.Initialize(this, stream);

            SetBundleType.Initialize(Monitor);
            Bundle.Initialize(Monitor);
        }

        private void RegisterCommands(IModHelper helper)
        {
            helper.ConsoleCommands.Add("setbundletype", SetBundleType.CommandInfo, new SetBundleType().RunCommand);
            helper.ConsoleCommands.Add("bundle", Bundle.CommandInfo, new Bundle().RunCommand);
        }

        public void DebugMessage(string msg)
        {
            if (Debug)
                Monitor.Log(msg, LogLevel.Debug);
        }
    }
}
 