using System.Diagnostics;

namespace source2tools_drpc; 

internal class Source2ToolsDrpcProcessCheck {
    public static void ProcessCheck() {
        for (var i = 0;; i--) {
            var Source2ToolsName = new[] { /*"cs2cfg",*/ "dota2cfg", "hlvrcfg", "steamtourscfg" };
            // Counter Strike Source 2 is real boooooois! :D
            var Source2Tools = Source2ToolsName.SelectMany(name => Process.GetProcessesByName(name)).ToArray();

            var processes = Process.GetProcesses();
            switch (Source2Tools.Length) {
                case 0:
                    DiscordRichPresence.DiscordRpc.UpdateDetails("Source 2 Tools not running!");
                    Source2ToolsDrpcMain.log.Error("None of the Source 2 Tools detected!");
                    Thread.Sleep(1000);
                    break;
                case 1:
                    DiscordRichPresence.DiscordRpc.UpdateDetails("Source 2 Tools running!");
                    Source2ToolsDrpcMain.log.Info("One of the Source 2 Tools detected! Updating DRPC...");
                    Thread.Sleep(1000);
                    break;
                default:
                    Source2ToolsDrpcMain.log.Critical("What the fuck did you do...?");
                    Environment.Exit(0);
                    break;
            }
        }
    }
}
