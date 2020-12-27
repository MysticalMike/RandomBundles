using Harmony;
using StardewModdingAPI;

namespace RandomBundles
{
    public class ModEntry : Mod
    {
        public static bool Debug = true;

        public override void Entry(IModHelper helper)
        {
            InitializeClasses(helper);

            var harmony = HarmonyInstance.Create(this.ModManifest.UniqueID);
            harmony.PatchAll();

            RegisterCommands(helper);
        }

        private void InitializeClasses(IModHelper helper)
        {
            CustomBundles.Patches.GenerateBundlesPatch.Initialize(this);
            CustomBundles.Patches.PopulateOptionsPatch.Initialize(this);

            Commands.SetBundleType.Initialize(Monitor);
            Commands.Bundle.Initialize(Monitor);
        }

        private void RegisterCommands(IModHelper helper)
        {
            helper.ConsoleCommands.Add("setbundletype", Commands.SetBundleType.CommandInfo, new Commands.SetBundleType().RunCommand);
            helper.ConsoleCommands.Add("bundle", Commands.Bundle.CommandInfo, new Commands.Bundle().RunCommand);
        }

        public void DebugMessage(string msg)
        {
            if (Debug)
                Monitor.Log(msg, LogLevel.Debug);
        }
    }
}