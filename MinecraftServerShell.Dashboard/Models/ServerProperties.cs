using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftServerShell.Dashboard.Models
{
    class ServerProperties
    {
        public bool EnableJmxMonitoring { get; set; }

        public int RconPort { get; set; }

        public ServerGamemode Gamemode { get; set; }

        public enum ServerGamemode
        {
            Survival,
            Creative,
            Spectator,
            Adventure
        }

        public bool EnableCommandBlock { get; set; }

        public bool EnableQuery { get; set; }

        public string LevelName { get; set; }

        public string Motd { get; set; }

        public int QueryPort { get; set; }

        public bool PvP { get; set; }

        public GameDifficulty Difficulty { get; set; }

        public enum GameDifficulty
        {
            Peaceful,
            Easy,
            Normal,
            Hard
        }

        public int NetworkCompressionThreshold { get; set; }

        public bool RequireResourcePack { get; set; }

        public int MaxTickTime { get; set; }

        public bool UseNativeTransport { get; set; }

        public int MaxPlayers { get; set; }

        public bool OnlineMode { get; set; }

        public bool EnableStatus { get; set; }

        public bool AllowFlight { get; set; }

        public bool BroadcastRconToOps { get; set; }

        public int ViewDistance { get; set; }

        public string ServerIp { get; set; }

        public string ResourcePackPrompt { get; set; }

        public bool AllowNether { get; set; }

        public int ServerPort { get; set; }

        public bool EnableRcon { get; set; }

        public bool SyncChunkWrites { get; set; }

        public int OpPermissionLevel { get; set; }

        public bool PreventProxyConnections { get; set; }

        public bool HideOnlinePlayers { get; set; }

        public string ResourcePack { get; set; }

        public int EntityBroadcastRangePercentage { get; set; }

        public int SimulationDistance { get; set; }

        public string RconPassword  { get; set; }

        public int PlayerIdleTimeout { get; set; }

        public bool ForceGamemode { get; set; }

        public int RateLimit { get; set; }

        public bool Hardcore { get; set; }

        public bool Whitelist { get; set; }

        public bool BroadcastConsoleToOps { get; set; }

        public bool SpawnNpcs { get; set; }

        public bool SpawnAnimals { get; set; }

        public int FunctionPermissionLevel { get; set; }

        public string TextFilteringConfig { get; set; }

        public bool SpawnMonsters { get; set; }

        public bool EnforceWhitelist { get; set; }

        public string ResourcepackSha1 { get; set; }

        public int SpawnProtection { get; set; }

        public long MaxWorldSize { get; set; }
    }
}
