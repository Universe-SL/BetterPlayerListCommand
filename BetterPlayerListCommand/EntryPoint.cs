using HarmonyLib;
using PluginAPI.Core;
using PluginAPI.Core.Attributes;

namespace BetterPlayerListCommand;

public class EntryPoint
{
    [PluginEntryPoint("BetterPlayerListCommand", "1.0.0.0", "Improves the Players Command", "xNexusACS")]
    private void Load()
    {
        Log.Info("BetterPlayerListCommand Loaded");
        
        Harmony harmony = new("com.xnexusacs.patches");
        harmony.PatchAll();
    }
}