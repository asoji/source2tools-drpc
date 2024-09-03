using System.Diagnostics;
using System.Text.RegularExpressions;

namespace source2tools_drpc; 

internal class Source2ToolsDrpcProcessCheck {
    public static void ProcessCheck() {
        for (var i = 0;; i--) {
            var Source2ToolsName = new[] { "cs2", "csgocfg", "dota2", "dota2cfg", "hlvr", "hlvrcfg", "steamtourscfg", "steamtours" };
            // Counter Strike Source 2 is real boooooois! :D
            var Source2Tools = Source2ToolsName.SelectMany(name => Process.GetProcessesByName(name)).ToArray();

            var processes = Process.GetProcesses();
            switch (Source2Tools.Length) {
                case 0:
                    DiscordRichPresence.DiscordRpc.UpdateDetails("Source 2 Tools not running!");
                    DiscordRichPresence.DiscordRpc.UpdateState("Tool not detected!");
                    Source2ToolsDrpcMain.log.Error("None of the Source 2 Tools detected!");
                    Thread.Sleep(100);
                    break;
                case 1:
                    // DiscordRichPresence.DiscordRpc.UpdateDetails("Source 2 Tools running!");
                    Source2ToolsDrpcMain.log.Info("One of the Source 2 Tools detected! Updating DRPC...");
                    Source2ToolsDrpcMain.log.Info($"Window Title: {Source2Tools[0].MainWindowTitle}");
                    Source2ToolsDrpcMain.log.Info($"Running Tool Process: {Source2Tools[0].ProcessName}");

                    if (Source2Tools[0].ProcessName == "steamtourscfg" || Source2Tools[0].ProcessName == "steamtours") {
                        DiscordRichPresence.DiscordRpc.UpdateSmallAsset("steamtours");
                    }

                    if (Source2Tools[0].ProcessName == "csgocfg" || Source2Tools[0].ProcessName == "cs2") { // WHY... IS CS2S TOOL WINDOW... CSGOCFG??? WOULDNT IT MAKE MORE SENSE TO BE CS2CFG...??????????????????
                        DiscordRichPresence.DiscordRpc.UpdateSmallAsset("cs2");
                    }

                    if (Source2Tools[0].ProcessName == "hlvrcfg" || Source2Tools[0].ProcessName == "hlvr") {
                        DiscordRichPresence.DiscordRpc.UpdateSmallAsset("hla");
                    }

                    if (Source2Tools[0].ProcessName == "dota2cfg" || Source2Tools[0].ProcessName == "dota2") {
                        DiscordRichPresence.DiscordRpc.UpdateSmallAsset("dota2");
                    }

                    if (Source2Tools[0].MainWindowTitle.Contains(WindowTitle.WORKSHOP_TOOLS_WINDOW_TITLE)) {
                        if (Source2Tools[0].ProcessName == "steamtourscfg" || Source2Tools[0].ProcessName == "steamtours") {
                            DiscordRichPresence.DiscordRpc.UpdateLargeAsset("steamtours");
                        }

                        if (Source2Tools[0].ProcessName == "csgocfg" || Source2Tools[0].ProcessName == "cs2") {
                            DiscordRichPresence.DiscordRpc.UpdateLargeAsset("cs2");
                        }

                        if (Source2Tools[0].ProcessName == "hlvrcfg" || Source2Tools[0].ProcessName == "hlvr") {
                            DiscordRichPresence.DiscordRpc.UpdateLargeAsset("hla");
                        }

                        if (Source2Tools[0].ProcessName == "dota2cfg" || Source2Tools[0].ProcessName == "dota2") {
                            DiscordRichPresence.DiscordRpc.UpdateLargeAsset("dota2");
                        }

                        DiscordRichPresence.DiscordRpc.UpdateDetails("Workshop Tools launched");
                        DiscordRichPresence.DiscordRpc.UpdateState("Picking an addon to workshop");
                    }

                    if (Source2Tools[0].MainWindowTitle.Contains(WindowTitle.PREVIEW_WINDOW_TITLE)) {
                        if (Source2Tools[0].ProcessName == "steamtourscfg" || Source2Tools[0].ProcessName == "steamtours") {
                            DiscordRichPresence.DiscordRpc.UpdateLargeAsset("steamtours");
                        }

                        if (Source2Tools[0].ProcessName == "csgocfg" || Source2Tools[0].ProcessName == "cs2") {
                            DiscordRichPresence.DiscordRpc.UpdateLargeAsset("cs2");
                        }

                        if (Source2Tools[0].ProcessName == "hlvrcfg" || Source2Tools[0].ProcessName == "hlvr") {
                            DiscordRichPresence.DiscordRpc.UpdateLargeAsset("hla");
                        }

                        if (Source2Tools[0].ProcessName == "dota2cfg" || Source2Tools[0].ProcessName == "dota2") {
                            DiscordRichPresence.DiscordRpc.UpdateLargeAsset("dota2");
                        }

                        DiscordRichPresence.DiscordRpc.UpdateDetails("Previewing in SteamVR's Game Window");
                        DiscordRichPresence.DiscordRpc.UpdateState("Looking around");
                    }

                    if (Source2Tools[0].MainWindowTitle.Contains(WindowTitle.ASSETS_BROWSER_WINDOW_TITLE)) {
                        DiscordRichPresence.DiscordRpc.UpdateLargeAsset("asset_browser");
                        DiscordRichPresence.DiscordRpc.UpdateDetails("Browsing the Asset Browser");
                        DiscordRichPresence.DiscordRpc.UpdateState("That's a lot of assets");
                    }

                    if (Source2Tools[0].MainWindowTitle.Contains(WindowTitle.HAMMER_WINDOW_TITLE)) {
                        DiscordRichPresence.DiscordRpc.UpdateLargeAsset("hammer_editor");
                        DiscordRichPresence.DiscordRpc.UpdateDetails("Creating something in Hammer");
                        String StateText = Regex.Match(Source2Tools[0].MainWindowTitle, "\\[((.|\\n|\\r)*)\\]").Value;
                        DiscordRichPresence.DiscordRpc.UpdateState(
                            $"Editing {StateText}");
                    }

                    if (Source2Tools[0].MainWindowTitle.Contains(WindowTitle.MODEL_EDITOR_WINDOW_TITLE)) {
                        DiscordRichPresence.DiscordRpc.UpdateLargeAsset("model_browser");
                        DiscordRichPresence.DiscordRpc.UpdateDetails("Editing in the Model Editor");
                        DiscordRichPresence.DiscordRpc.UpdateState("not yet implemented");

                    }

                    if (Source2Tools[0].MainWindowTitle.Contains(WindowTitle.MATERIAL_EDITOR_WINDOW_TITLE)) {
                        DiscordRichPresence.DiscordRpc.UpdateLargeAsset("material_editor");
                        DiscordRichPresence.DiscordRpc.UpdateDetails("Editing in the Material Editor");
                        DiscordRichPresence.DiscordRpc.UpdateState(Source2Tools[0].MainWindowTitle.Remove(0, 17)); // assume that the window title for this always starts with `Material Editor - ` and trim that
                    }

                    if (Source2Tools[0].MainWindowTitle.Contains(WindowTitle.WORKSHOP_MANAGER_TITLE)) {
                        if (Source2Tools[0].ProcessName == "steamtourscfg" || Source2Tools[0].ProcessName == "steamtours") {
                            DiscordRichPresence.DiscordRpc.UpdateLargeAsset("steamtours");
                        }

                        if (Source2Tools[0].ProcessName == "csgocfg" || Source2Tools[0].ProcessName == "cs2") {
                            DiscordRichPresence.DiscordRpc.UpdateLargeAsset("cs2");
                        }

                        if (Source2Tools[0].ProcessName == "hlvrcfg" || Source2Tools[0].ProcessName == "hlvr") {
                            DiscordRichPresence.DiscordRpc.UpdateLargeAsset("hla");
                        }

                        if (Source2Tools[0].ProcessName == "dota2cfg" || Source2Tools[0].ProcessName == "dota2") {
                            DiscordRichPresence.DiscordRpc.UpdateLargeAsset("dota2");
                        }

                        DiscordRichPresence.DiscordRpc.UpdateDetails("Preparing Workshop Publishing");
                        DiscordRichPresence.DiscordRpc.UpdateState("Getting content ready");
                    }

                    if (Source2Tools[0].MainWindowTitle.Contains(WindowTitle.MODELDOC_SELECTOR_WINDOW_TITLE)) {
                        DiscordRichPresence.DiscordRpc.UpdateLargeAsset("noicon", "No high resolution enough icon exists for this tool");
                        DiscordRichPresence.DiscordRpc.UpdateDetails("In ModelDoc Selection");
                        DiscordRichPresence.DiscordRpc.UpdateState("they modeldoc on my poly til i gon");
                    }

                    if (Source2Tools[0].MainWindowTitle.Contains(WindowTitle.MODELDOC_EDITOR_WINDOW_TITLE)) {
                        DiscordRichPresence.DiscordRpc.UpdateLargeAsset("noicon", "No high resolution enough icon exists for this tool");
                        DiscordRichPresence.DiscordRpc.UpdateDetails("Editing in ModelDoc");
                        DiscordRichPresence.DiscordRpc.UpdateState(Source2Tools[0].MainWindowTitle.Remove(0, 11));
                    }

                    if (Source2Tools[0].MainWindowTitle.Contains(WindowTitle.POSTPROCESSING_EDITOR_WINDOW_TITLE)) {
                        DiscordRichPresence.DiscordRpc.UpdateLargeAsset("noicon", "No high resolution enough icon exists for this tool");
                        DiscordRichPresence.DiscordRpc.UpdateDetails("Editing in Postprocessing Editor");
                        DiscordRichPresence.DiscordRpc.UpdateState("shaders go glare");
                    }

                    if (!Source2Tools[0].MainWindowTitle.Contains(WindowTitle.WORKSHOP_TOOLS_WINDOW_TITLE) &&
                        !Source2Tools[0].MainWindowTitle.Contains(WindowTitle.HAMMER_WINDOW_TITLE) &&
                        !Source2Tools[0].MainWindowTitle.Contains(WindowTitle.MODEL_EDITOR_WINDOW_TITLE) &&
                        !Source2Tools[0].MainWindowTitle.Contains(WindowTitle.ASSETS_BROWSER_WINDOW_TITLE) &&
                        !Source2Tools[0].MainWindowTitle.Contains(WindowTitle.MATERIAL_EDITOR_WINDOW_TITLE) &&
                        !Source2Tools[0].MainWindowTitle.Contains(WindowTitle.PREVIEW_WINDOW_TITLE) &&
                        !Source2Tools[0].MainWindowTitle.Contains(WindowTitle.WORKSHOP_MANAGER_TITLE) &&
                        !Source2Tools[0].MainWindowTitle.Contains(WindowTitle.MODELDOC_SELECTOR_WINDOW_TITLE) &&
                        !Source2Tools[0].MainWindowTitle.Contains(WindowTitle.MODELDOC_EDITOR_WINDOW_TITLE) &&
                        !Source2Tools[0].MainWindowTitle.Contains(WindowTitle.POSTPROCESSING_EDITOR_WINDOW_TITLE)) {
                        DiscordRichPresence.DiscordRpc.UpdateDetails("Source 2 Tools is running, but...");
                        DiscordRichPresence.DiscordRpc.UpdateState("current tool not implemented, oops.");
                    }

                    // struggling to just get the initial window title and then anytihng after that
                    // switch (Source2Tools[0].MainWindowTitle) {
                    //     case WindowTitle.STEAMVR_ASSETS_BROWSER_WINDOW_TITLE when WindowTitle.STEAMVR_ASSETS_BROWSER_WINDOW_TITLE.Contains(WindowTitle.STEAMVR_ASSETS_BROWSER_WINDOW_TITLE):
                    //         DiscordRichPresence.DiscordRpc.UpdateDetails("Browsing the Asset Browser");
                    //         break;
                    //     case WindowTitle.STEAMVR_HAMMER_WINDOW_TITLE when WindowTitle.STEAMVR_HAMMER_WINDOW_TITLE.Contains(WindowTitle.STEAMVR_HAMMER_WINDOW_TITLE):
                    //         DiscordRichPresence.DiscordRpc.UpdateDetails("Creating something in Hammer");
                    //         break;
                    //     case WindowTitle.STEAMVR_MODEL_EDITOR_WINDOW_TITLE when WindowTitle.STEAMVR_MODEL_EDITOR_WINDOW_TITLE.Contains(WindowTitle.STEAMVR_MODEL_EDITOR_WINDOW_TITLE):
                    //         DiscordRichPresence.DiscordRpc.UpdateDetails("Editing in the Model Editor");
                    //         break;
                    //     case WindowTitle.STEAMVR_MATERIAL_EDITOR_WINDOW_TITLE when WindowTitle.STEAMVR_MATERIAL_EDITOR_WINDOW_TITLE.Contains(WindowTitle.STEAMVR_MATERIAL_EDITOR_WINDOW_TITLE):
                    //         DiscordRichPresence.DiscordRpc.UpdateDetails("Editing in the Material Editor");
                    //         break;
                    //     default:
                    //         DiscordRichPresence.DiscordRpc.UpdateDetails(
                    //             "Source 2 Tools is running, but...");
                    //         DiscordRichPresence.DiscordRpc.UpdateState("current window isn't implemented. oops.");
                    //         break;
                    // }
                    Thread.Sleep(5000);
                    break;
                default:
                    Source2ToolsDrpcMain.log.Critical("What the fuck did you do...?");
                    Environment.Exit(0);
                    break;
            }
        }
    }
}
