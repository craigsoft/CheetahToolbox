/// ======================================================================
///		CheetahToolbox: (https://github.com/CraigCraig/CheetahToolbox)
///				Project:  Craig's CheetahToolbox a Swiss Army Knife
///
///
///			Author: Craig Craig (https://github.com/CraigCraig)
///		License:     MIT License (http://opensource.org/licenses/MIT)
/// ======================================================================
#if WINDOWS
namespace CheetahToolbox.Registry;

using Exceptions;
using Microsoft.Win32;

public readonly struct RegistryLocation
{
    /// <summary>
    /// <see cref="RegistryTarget"/> of the key
    /// </summary>
    public readonly RegistryTarget Target;

    /// <summary>
    /// Name of the key
    /// </summary>
    public readonly string Key;

    /// <summary>
    /// Name of the value
    /// </summary>
    public readonly string Value;

    public readonly RegistryValueKind Kind;

    public RegistryLocation(string key, string value, RegistryTarget target = RegistryTarget.HKCU)
    {
        Target = target;
        Key = key;
        Value = value;
        Kind = RegistryKey?.GetValueKind(Value) ?? RegistryValueKind.Unknown;
    }

    public void SetString(string data) => RegistryKey?.SetValue(Value, data, Kind);

    public string? GetString()
    {
        if (RegistryKey?.GetValue(Value) is string data)
        {
            return data;
        }
        return null;
    }

    public void SetBool(bool data) => RegistryKey?.SetValue(Value, data, Kind);

    public bool? GetBool()
    {
        if (RegistryKey?.GetValue(Value) is bool data)
        {
            return data;
        }
        if (RegistryKey?.GetValue(Value) is int data2)
        {
            if (data2 == 0)
            {
                return false;
            }
            else if (data2 == 1)
            {
                return true;
            }
        }
        return null;
    }

    public void SetInt(int data) => RegistryKey?.SetValue(Value, data, Kind);

    public int GetInt()
    {
        int result = 0;
        if (RegistryKey?.GetValue(Value) is int data)
        {
            result = data;
        }
        return result;
    }

    public RegistryKey? RegistryKey
    {
        get
        {
            RegistryKey? key = null;
            switch (Target)
            {
                case RegistryTarget.HKLM:
                    key = Registry.LocalMachine.OpenSubKey(Key);
                    break;
                case RegistryTarget.HKCU:
                    key = Registry.CurrentUser.OpenSubKey(Key);
                    break;
                default:
                    break;
            }
            if (key == null) throw new RegistryException();
            return key;
        }
    }
}
#endif