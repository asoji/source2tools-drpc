using EasyLog;

namespace source2tools_drpc; 

internal class Source2ToolsDrpcMain {
    public static Logger log = new();
    public static Config cfg = new();

    private static void SetConfig() {
        cfg.ShowDate = true;
        cfg.Console = true;
    }

    private static void Main(string[] args) {
        log.cfg = cfg;
        SetConfig();
        log.InitLogger();

        DiscordRichPresence.Setup();
        Source2ToolsDrpcProcessCheck.ProcessCheck();
        Console.ReadKey(true);
    }
}