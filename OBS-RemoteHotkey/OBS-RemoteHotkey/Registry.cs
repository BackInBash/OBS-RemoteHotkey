using Microsoft.Win32;
using System;

namespace OBS_RemoteHotkey
{
    public static class AppSettingsManager
    {
        // Einstellungen werden in der Registry gespeichert...

        private static RegistryKey GetAppSettingsKey()
        {
            RegistryKey registryKey = Registry.CurrentUser.CreateSubKey("SOFTWARE\\OBS-RemoteHotkey", RegistryKeyPermissionCheck.ReadWriteSubTree);
            return registryKey;
        }

        public static void SetValueString(string name, string value)
        {
            try
            {
                GetAppSettingsKey().SetValue(name, value, RegistryValueKind.String);
            }
            catch (Exception ex)
            {
            }
        }

        public static string GetValueString(string name, string defaultV = "")
        {
            string r1 = null;
            try
            {
                r1 = (string)GetAppSettingsKey().GetValue(name, null);
            }
            catch (Exception ex)
            {
            }

            if (r1 == null)
            {
                SetValueString(name, defaultV);
                return defaultV;
            }
            return r1;
        }

        public static void SetValueInt32(string name, int value)
        {
            try
            {
                GetAppSettingsKey().SetValue(name, value, RegistryValueKind.DWord);
            }
            catch (Exception ex)
            {
            }
        }

        public static int GetValueInt32(string name, int defaultV = 0)
        {
            int r1 = defaultV;
            try
            {
                var k = GetAppSettingsKey();
                r1 = (int)(k.GetValue(name, defaultV));
            }
            catch (Exception ex)
            {
            }

            if (r1 == defaultV)
            {
                SetValueInt32(name, defaultV);
                return defaultV;
            }
            return r1;
        }

        public static void SetValueBool(string name, bool value)
        {
            SetValueInt32(name, value ? 1 : 0);
        }

        public static bool GetValueBool(string name, bool defaultV = false)
        {
            return (GetValueInt32(name, (defaultV ? 1 : 0))) == 1;
        }
    }
}