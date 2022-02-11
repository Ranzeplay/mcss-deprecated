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
        internal static AppSettings AppSettings { get; set; } = null!;

        public static string SettingsFilePath() => Path.Combine(Environment.CurrentDirectory, AppSettingsFileName);

        private static void CreateDefaultIfNotExist()
        {
            var path = SettingsFilePath();

            if (!File.Exists(path))
            {
                File.WriteAllText(path, JsonSerializer.Serialize(new AppSettings()));
            }
        }

        public static void SaveNewSettings(AppSettings settings)
        {
            AppSettings = settings;

            var path = SettingsFilePath();
            File.WriteAllText(path, JsonSerializer.Serialize(AppSettings));
        }

        public static AppSettings ReadOrCreateSettings()
        {
            CreateDefaultIfNotExist();

            var content = File.ReadAllText(SettingsFilePath());

            if (AppSettings == null)
            {
                AppSettings = JsonSerializer.Deserialize<AppSettings>(content);
            }
           
            return AppSettings;
        }
    }
}
