using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPF_LoginForm
{
    public static class ThemeManager
    {

        public static void ApplyTheme(FrameworkElement bg, FrameworkElement bgs)
        {
            string themeName = Properties.Settings.Default.ThemeName;

            if (themeName == "Dark")
            {
                ApplyDarkTheme(bg, bgs);
            }
            else if (themeName == "Light")
            {
                ApplyLightTheme(bg, bgs);
            }

            if(themeName == "")
            {
                Properties.Settings.Default.ThemeName = "Light";
            }
        }

        public static void ApplyDarkTheme(FrameworkElement bg, FrameworkElement bgs)
        {
            ResourceDictionary darkTheme = new ResourceDictionary();
            darkTheme.Source = new Uri("/Dark.xaml", UriKind.RelativeOrAbsolute);
            Application.Current.Resources.MergedDictionaries.Add(darkTheme);

            if (bg != null)
            {
                Style bgStyle = (Style)darkTheme["DarkTheme"];
                if (bgStyle != null)
                {
                    bg.Style = bgStyle;
                }
            }

            if (bgs != null)
            {
                Style bgsStyle = (Style)darkTheme["DarkThemeBG"];
                if (bgsStyle != null)
                {
                    bgs.Style = bgsStyle;
                }
            }
        }

        public static void ApplyLightTheme(FrameworkElement bg, FrameworkElement bgs)
        {
            ResourceDictionary lightTheme = new ResourceDictionary();
            lightTheme.Source = new Uri("/Light.xaml", UriKind.RelativeOrAbsolute);
            Application.Current.Resources.MergedDictionaries.Add(lightTheme);

            if (bg != null)
            {
                Style bgStyle = (Style)lightTheme["LightTheme"];
                if (bgStyle != null)
                {
                    bg.Style = bgStyle;
                }
            }

            if (bgs != null)
            {
                Style bgsStyle = (Style)lightTheme["LightThemeBG"];
                if (bgsStyle != null)
                {
                    bgs.Style = bgsStyle;
                }
            }
        }

        public static string GetCurrentThemeName()
        {
            return Properties.Settings.Default.ThemeName;
        }
        public static void ToggleTheme(FrameworkElement bg, FrameworkElement bgs)
        {
            string currentTheme = GetCurrentThemeName();
            string newTheme = (currentTheme == "Dark") ? "Light" : "Dark";

            Properties.Settings.Default.ThemeName = newTheme;
            Properties.Settings.Default.Save();

            ApplyTheme(bg, bgs); // Применить новую тему сразу после изменения
        }

    }
}
