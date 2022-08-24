using NetDiscordRpc;
using NetDiscordRpc.RPC;

namespace source2tools_drpc; 

public class DiscordRichPresence {
    public static DiscordRPC DiscordRpc;

    public static void Setup() {
        DiscordRpc = new DiscordRPC("1012024516467236965");

        DiscordRpc.Initialize();

        DiscordRpc.SetPresence(new RichPresence {
            Details = "Source 2 Tools not running!"
        });

        DiscordRpc.Invoke();
    }
}