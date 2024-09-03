using System.Diagnostics;
using System.Text.RegularExpressions;

namespace source2tools_drpc; 

internal class Source2ToolsDrpcProcessCheck {
    static string[] Source2ToolsName = new[] { "cs2", "csgocfg", "dota2", "dota2cfg", "hlvr", "hlvrcfg", "steamtourscfg", "steamtours" };
    public static bool TryGetSource2Process(out Process? toolProcess)
    {
        // Counter Strike Source 2 is real boooooois! :D
        var Source2Tools = Source2ToolsName.SelectMany(name => Process.GetProcessesByName(name)).ToArray();

        if (!Source2Tools.Any())
        {
            toolProcess = null;
            return false;
        }
        
        // More than one tool is running. Oh no.
        // Let's just take the first one.
        else if (Source2Tools.Length > 1)
        {
            Source2ToolsDrpcMain.log.Warning($"Uh-oh! More than one tool open. Taking the first found tool into account. ({Source2Tools[0].ProcessName})");
        }
        toolProcess = Source2Tools.First();
        return true;

    }

    public static void SetAsset(string processName, bool isLarge = false)
    {
        string asset = "";
        switch (processName)
        {
            case "steamtourscfg":
            case "steamtours":
                asset = "steamtours";
                break;
            case "csgocfg":
            case "cs2":
                // WHY... IS CS2S TOOL WINDOW... CSGOCFG??? WOULDNT IT MAKE MORE SENSE TO BE CS2CFG...??????????????????
                asset = "cs2";
                break;
            case "hlvrcfg":
            case "hlvr":
                asset = "hlvr";
                break;
            case "dota2cfg":
            case "dota2":
                asset = "dota2";
                break;
        }

        if (isLarge)
        {
            DiscordRichPresence.UpdateLargeAsset(asset);
        }
        DiscordRichPresence.UpdateSmallAsset(asset);
    }
    
    public static void ProcessCheck() {
        for (var i = 0;; i++)
        {
            DiscordRichPresence.UpdatePresence();
            if(i != 0) Thread.Sleep(5000);
            Process? ToolProcess;
            bool success = TryGetSource2Process(out ToolProcess);
            
            // No tools are running.
            if (!success)
            {
                DiscordRichPresence.UpdateDetails("Source 2 Tools not running!");
                DiscordRichPresence.UpdateState("Tool not detected!");
                DiscordRichPresence.UpdateLargeAsset("bendy");
                Source2ToolsDrpcMain.log.Warning("None of the Source 2 Tools detected!");
                continue;
            }
            
            Source2ToolsDrpcMain.log.Info("One or more of the Source 2 Tools detected! Updating DRPC...");
            Source2ToolsDrpcMain.log.Info($"Window Title: {ToolProcess.MainWindowTitle}");
            Source2ToolsDrpcMain.log.Info($"Running Tool Process: {ToolProcess.ProcessName}");

            if (ToolProcess.MainWindowTitle == ToolProcess.ProcessName)
            {
                Source2ToolsDrpcMain.log.Info($"Windows Title matches the Process Name; we're skipping this loop.");
                continue;
            }

            bool isLarge = ToolProcess.MainWindowTitle.Contains(WindowTitle.WORKSHOP_TOOLS_WINDOW_TITLE) ||
                           ToolProcess.MainWindowTitle.Contains(WindowTitle.PREVIEW_WINDOW_TITLE);

            SetAsset(ToolProcess.ProcessName, isLarge);
            
            if (ToolProcess.MainWindowTitle.Contains(WindowTitle.WORKSHOP_TOOLS_WINDOW_TITLE))
            {
                DiscordRichPresence.UpdateDetails("Workshop Tools launched");
                DiscordRichPresence.UpdateState("Picking an addon to workshop");
                continue;
            }

            if (ToolProcess.MainWindowTitle.Contains(WindowTitle.PREVIEW_WINDOW_TITLE))
            {
                DiscordRichPresence.UpdateDetails("Previewing in SteamVR's Game Window");
                DiscordRichPresence.UpdateState("Looking around");
                continue;
            }

            if (ToolProcess.MainWindowTitle.Contains(WindowTitle.ASSETS_BROWSER_WINDOW_TITLE))
            {
                DiscordRichPresence.UpdateLargeAsset("asset_browser");
                DiscordRichPresence.UpdateDetails("Browsing the Asset Browser");
                DiscordRichPresence.UpdateState("That's a lot of assets");
                continue;
            }

            if (ToolProcess.MainWindowTitle.Contains(WindowTitle.HAMMER_WINDOW_TITLE))
            {
                DiscordRichPresence.UpdateLargeAsset("hammer_editor");
                DiscordRichPresence.UpdateDetails("Creating something in Hammer");
                String StateText = ToolProcess.MainWindowTitle.Replace("Hammer - [", "").Replace("*", "").Replace("]", "");
                if (StateText == "Hammer")
                {
                    DiscordRichPresence.UpdateState("No map file open");
                    continue;
                }
                DiscordRichPresence.UpdateState(
                    $"Editing {StateText}");
                continue;
            }

            if (ToolProcess.MainWindowTitle.Contains(WindowTitle.MODEL_EDITOR_WINDOW_TITLE))
            {
                DiscordRichPresence.UpdateLargeAsset("model_browser");
                DiscordRichPresence.UpdateDetails("Editing in the Model Editor");
                DiscordRichPresence.UpdateState("not yet implemented");
                continue;
            }

            if (ToolProcess.MainWindowTitle.Contains(WindowTitle.MATERIAL_EDITOR_WINDOW_TITLE))
            {
                DiscordRichPresence.UpdateLargeAsset("material_editor");
                DiscordRichPresence.UpdateDetails("Editing in the Material Editor");
                // assume that the window title for this always starts with `Material Editor - ` and trim that
                DiscordRichPresence.UpdateState(ToolProcess.MainWindowTitle.Replace("Material Editor - ", "")); 
                continue;
            }

            if (ToolProcess.MainWindowTitle.Contains(WindowTitle.WORKSHOP_MANAGER_TITLE))
            {
                DiscordRichPresence.UpdateDetails("Preparing Workshop Publishing");
                DiscordRichPresence.UpdateState("Getting content ready");
                continue;
            }

            if (ToolProcess.MainWindowTitle.Contains(WindowTitle.MODELDOC_SELECTOR_WINDOW_TITLE))
            {
                DiscordRichPresence.UpdateLargeAsset("noicon",
                    "No high resolution enough icon exists for this tool");
                DiscordRichPresence.UpdateDetails("In ModelDoc Selection");
                DiscordRichPresence.UpdateState("they modeldoc on my poly til i gon");
                continue;
            }

            if (ToolProcess.MainWindowTitle.Contains(WindowTitle.MODELDOC_EDITOR_WINDOW_TITLE))
            {
                DiscordRichPresence.UpdateLargeAsset("noicon",
                    "No high resolution enough icon exists for this tool");
                DiscordRichPresence.UpdateDetails("Editing in ModelDoc");
                DiscordRichPresence.UpdateState(ToolProcess.MainWindowTitle.Replace("ModelDoc :: ", ""));
                continue;
            }

            if (ToolProcess.MainWindowTitle.Contains(WindowTitle.POSTPROCESSING_EDITOR_WINDOW_TITLE))
            {
                DiscordRichPresence.UpdateLargeAsset("noicon",
                    "No high resolution enough icon exists for this tool");
                DiscordRichPresence.UpdateDetails("Editing in Postprocessing Editor");
                DiscordRichPresence.UpdateState("shaders go glare");
                continue;
            }

            if (ToolProcess.MainWindowTitle.Contains(WindowTitle.PARTICLE_EDITOR_WINDOW_TITLE) ||
                ToolProcess.MainWindowTitle.Contains(WindowTitle.PARTICLE_EDITOR_FUCKIN_AAAAAAAAAAAA))
            {
                DiscordRichPresence.UpdateDetails("Tinkering inside the Particle Editor");
                DiscordRichPresence.UpdateLargeAsset("particle_editor");
                if (ToolProcess.MainWindowTitle == WindowTitle.PARTICLE_EDITOR_WINDOW_TITLE)
                {
                       DiscordRichPresence.UpdateState("No file loaded");
                       continue;
                }
                string StateText = ToolProcess.MainWindowTitle.Replace(WindowTitle.PARTICLE_EDITOR_FUCKIN_AAAAAAAAAAAA, "");
                DiscordRichPresence.UpdateState($"Editing {StateText}");
                continue;
            }

            // if (!ToolProcess.MainWindowTitle.Contains(WindowTitle.WORKSHOP_TOOLS_WINDOW_TITLE) &&
            //     !ToolProcess.MainWindowTitle.Contains(WindowTitle.HAMMER_WINDOW_TITLE) &&
            //     !ToolProcess.MainWindowTitle.Contains(WindowTitle.MODEL_EDITOR_WINDOW_TITLE) &&
            //     !ToolProcess.MainWindowTitle.Contains(WindowTitle.ASSETS_BROWSER_WINDOW_TITLE) &&
            //     !ToolProcess.MainWindowTitle.Contains(WindowTitle.MATERIAL_EDITOR_WINDOW_TITLE) &&
            //     !ToolProcess.MainWindowTitle.Contains(WindowTitle.PREVIEW_WINDOW_TITLE) &&
            //     !ToolProcess.MainWindowTitle.Contains(WindowTitle.WORKSHOP_MANAGER_TITLE) &&
            //     !ToolProcess.MainWindowTitle.Contains(WindowTitle.MODELDOC_SELECTOR_WINDOW_TITLE) &&
            //     !ToolProcess.MainWindowTitle.Contains(WindowTitle.MODELDOC_EDITOR_WINDOW_TITLE) &&
            //     !ToolProcess.MainWindowTitle.Contains(WindowTitle.POSTPROCESSING_EDITOR_WINDOW_TITLE))
            // {
            // }
            DiscordRichPresence.UpdateDetails("Source 2 Tools is running, but...");
            DiscordRichPresence.UpdateState("Current tool not implemented, oops.");
            continue;
        }
    }
}
