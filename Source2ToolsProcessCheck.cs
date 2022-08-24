using System.Diagnostics;

namespace source2tools_drpc; 

internal class Source2ToolsDrpcProcessCheck {
    public static void ProcessCheck() {
        for (var i = 0;; i--) {
            var Source2ToolsName = new[] { /*"csgocfg",*/ "dota2cfg", "hlvrcfg", "steamtourscfg" }; // one can wish
            // for CS:GO Source 2
            var Source2Tools = Source2ToolsName.SelectMany(name => Process.GetProcessesByName(name)).ToArray();

            var processes = Process.GetProcesses();

            switch (Source2Tools.Length) {
                case 0:
                    DiscordRichPresence.DiscordRpc.UpdateDetails("Source 2 Tools not running!");
                    Source2ToolsDrpcMain.log.Error("None of the Source 2 Hammer tools detected!");
                    Thread.Sleep(1000);
                    break;
                case 1:
                    DiscordRichPresence.DiscordRpc.UpdateDetails("Source 2 Tools running!");
                    Source2ToolsDrpcMain.log.Info("One of the Source 2 Tools detected! Updating DRPC...");
                    Thread.Sleep(1000);
                    break;
                default:
                    Console.WriteLine("What the fuck did you do?");
                    Environment.Exit(0);
                    break;
            }
        }
    }
}