using NetDiscordRpc;
using NetDiscordRpc.RPC;

namespace source2tools_drpc;

public class DiscordRichPresence {
    public static DiscordRPC DiscordRpc;
    public static RichPresence RichPresence = new()
    {
        Details = "Loading...",
        State = null,
        Party = null,
        Timestamps = Timestamps.Now,
        Assets = new Assets()
        {
            LargeImageKey = "noicon",
        }
    };
    
    public static void UpdatePresence() => DiscordRpc.SetPresence(RichPresence);
    public static void UpdateDetails(string text) => RichPresence.Details = text;
    public static void UpdateState(string text) => RichPresence.State = text;
    public static void UpdateLargeAsset(string asset, string? text = null)
    {
        RichPresence.Assets.LargeImageKey = asset;
        RichPresence.Assets.LargeImageText = text;
    } 
    public static void UpdateSmallAsset(string asset, string? text = null)
    {
        RichPresence.Assets.SmallImageKey = asset;
        RichPresence.Assets.SmallImageText = text;
    } 
    
    public static void Setup() {
        DiscordRpc = new DiscordRPC("1012024516467236965");

        DiscordRpc.Initialize();

        DiscordRpc.SetPresence(new RichPresence {
            Buttons = new Button[] {
                new() { Label = "DRPC Git Repo", Url = "https://github.com/asoji/source2tools-drpc" }
            },
            Timestamps = Timestamps.Now
        });

        DiscordRpc.Invoke();
    }
}