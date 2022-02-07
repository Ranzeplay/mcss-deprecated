using MinecraftServerShell.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MinecraftServerShell.Core.Managers
{
    public class AppSettingsManager
    {
        public const string AppSettingsFileName = "settings.json";

        public static string SettingsFilePath() => Path.Combine(Environment.CurrentDirectory, AppSettingsFileName);

        public static void CreateDefaultIfNotExist()
        {
            var path = SettingsFilePath();

            if (!File.Exists(path))
            {
                File.WriteAllText(path, JsonSerializer.Serialize(new AppSettings()));
            }
        }

        public static void SaveNewSettings(AppSettings settings)
        {
            var path = SettingsFilePath();
            File.WriteAllText(path, JsonSerializer.Serialize(settings));
        }

        public static AppSettings ReadOrCreateSettings()
        {
            CreateDefaultIfNotExist();

            var content = File.ReadAllText(SettingsFilePath());
            return JsonSerializer.Deserialize<AppSettings>(content);
        }
    }
}
