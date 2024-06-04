using System;
using System.Collections.Generic;
using System.Text;
using CentralAuth;
using CommandSystem;
using CommandSystem.Commands.Console;
using HarmonyLib;

namespace BetterPlayerListCommand;

[HarmonyPatch(typeof(PlayersCommand), nameof(PlayersCommand.Execute))]
public static class PlayerListCommandPatch
{
    [HarmonyPrefix]
    public static bool OnExecute(PlayersCommand __instance, ref bool __result, ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        if (ReferenceHub.LocalHub == null)
        {
            response = "The Dedicated Server is not connected or the server crashed.";
            return false;
        }

        HashSet<ReferenceHub> allHubs = ReferenceHub.AllHubs;
        StringBuilder stringBuilder = new StringBuilder();

        stringBuilder.AppendLine(allHubs.Count is 0
            ? "<color=red>No players found.</color>"
            : $"<color=red>All Players: ({(ServerStatic.IsDedicated ? allHubs.Count - 1 : allHubs.Count)})</color>");

        foreach (ReferenceHub referenceHub in allHubs)
        {
            if (referenceHub.Mode is ClientInstanceMode.DedicatedServer)
                continue;
            
            stringBuilder.AppendLine($"<color=yellow>{(referenceHub.nicknameSync.Network_myNickSync ?? "(no nickname)")}</color>\n <color=cyan>User Id: {(referenceHub.authManager.UserId ?? "(no User ID)")} Ip Address: ({(referenceHub.queryProcessor._ipAddress ?? "no Ip Address")}) Player Id: [{referenceHub.PlayerId}]</color>\n");
        }
        
        response = stringBuilder.ToString();
        __result = true;
        return false;
    }
}
