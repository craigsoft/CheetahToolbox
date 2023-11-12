﻿namespace WinToolbox.Registry;

using System.Runtime.Versioning;
using Microsoft.Win32;

internal static class Scanner
{
    [SupportedOSPlatform("windows")]
    public static void DoScan()
    {
        var uninstallKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall")
            ?? throw new Exception("Uninstall key not found.");

        foreach (var subKeyName in uninstallKey.GetSubKeyNames())
        {
            var subKey = uninstallKey.OpenSubKey(subKeyName);
            if (subKey is not null)
            {
                if (CheckUninstallEntry(subKey))
                {
                    uninstallKey.DeleteSubKey(subKeyName, true);
                }
            }
        }
    }

    /// <summary>
    /// Checks if the given uninstall entry is a ghost app.
    /// </summary>
    /// <param name="subKey"></param>
    /// <returns>true if the entry is a ghost app, otherwise false</returns>
    [SupportedOSPlatform("windows")]
    private static bool CheckUninstallEntry(RegistryKey subKey)
    {
        // TODO: Check if the entry is a ghost app

        var displayName = subKey.GetValue("DisplayName")?.ToString();
        if (!string.IsNullOrEmpty(displayName))
        {
            //Log.WriteLine(displayName);
        }

        var displayIcon = subKey.GetValue("DisplayIcon")?.ToString();
        if (!string.IsNullOrEmpty(displayIcon))
        {
            //Log.WriteLine($"\t{displayIcon}");
        }

        var installLocation = subKey.GetValue("InstallLocation")?.ToString();
        if (!string.IsNullOrEmpty(installLocation))
        {
            //Log.WriteLine($"\t{installLocation}");
        }

        var uninstallString = subKey.GetValue("UninstallString")?.ToString();
        if (!string.IsNullOrEmpty(uninstallString))
        {
            //Log.WriteLine($"\t{uninstallString}");
        }

        return false; // TODO: Return true if the entry is a ghost app
    }
}