using System;
using System.Collections.Generic;
using System.Diagnostics;
using My;

namespace mRemoteNC
{
    public class SupportedCultures : Dictionary<string, string>
    {
        private SupportedCultures()
        {
            System.Globalization.CultureInfo CultureInfo;
            foreach (string CultureName in Settings.Default.SupportedUICultures.Split(','))
            {
                try
                {
                    CultureInfo = new System.Globalization.CultureInfo(CultureName.Trim());
                    Add(CultureInfo.Name, CultureInfo.TextInfo.ToTitleCase(CultureInfo.NativeName));
                }
                catch (Exception ex)
                {
                    Debug.Print(
                        string.Format(
                            "An exception occurred while adding the culture \'{0}\' to the list of supported cultures. {1}",
                            CultureName, ex.ToString()));
                }
            }
        }

        private static SupportedCultures _Instance = new SupportedCultures();

        public static void InstantiateSingleton()
        {
            if (_Instance == null)
            {
                _Instance = new SupportedCultures();
            }
        }

        public static bool IsNameSupported(string CultureName)
        {
            return _Instance.ContainsKey(CultureName);
        }

        public static bool IsNativeNameSupported(string CultureNativeName)
        {
            return _Instance.ContainsValue(CultureNativeName);
        }

        public static string CultureName(string CultureNativeName)
        {
            string[] Names = new string[_Instance.Count + 1];
            string[] NativeNames = new string[_Instance.Count + 1];

            _Instance.Keys.CopyTo(Names, 0);
            _Instance.Values.CopyTo(NativeNames, 0);

            for (int Index = 0; Index <= _Instance.Count; Index++)
            {
                if (NativeNames[Index] == CultureNativeName)
                {
                    return Names[Index];
                }
            }

            throw (new System.Collections.Generic.KeyNotFoundException());
        }

        public static string CultureNativeName(string CultureName)
        {
            return _Instance[CultureName];
        }

        public static List<string> CultureNativeNames
        {
            get
            {
                List<string> ValueList = new List<string>();
                foreach (string Value in _Instance.Values)
                {
                    ValueList.Add(Value);
                }
                return ValueList;
            }
        }
    }
}