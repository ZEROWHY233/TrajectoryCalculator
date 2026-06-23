using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace TrajectoryCalculator
{
    public partial class App : Application
    {
        public static bool IsDarkTheme { get; private set; } = true;

        private static readonly Dictionary<string, (string Dark, string Light)> ThemeColors;

        static App()
        {
            ThemeColors = new Dictionary<string, (string Dark, string Light)>
            {
                { "BackgroundBrush",      ("#23262e", "#eef1f5") },
                { "FrameBackgroundBrush",("#2d313a", "#e2e6ed") },
                { "TextBrush",            ("#eaeef2", "#1e222a") },
                { "EntryBackgroundBrush", ("#3b404b", "#ffffff") },
                { "EntryForegroundBrush", ("#f0f4f8", "#1e222a") },
                { "GridRowEvenBrush",     ("#2d313a", "#ffffff") },
                { "GridRowOddBrush",      ("#353a45", "#e6eaef") },
                { "GridHoverBrush",       ("#4c5463", "#d1d9e6") },
                { "GridHoverTextBrush",   ("#ffffff", "#1e222a") },
                { "GridRowHitBrush",      ("#2a5a3a", "#b8e0c8") },
                { "ButtonCalcBrush",      ("#3a7bd5", "#4a8de0") },
                { "ButtonDeleteBrush",    ("#6b6f7a", "#8a8f99") },
                { "ButtonClearBrush",     ("#b54141", "#d95353") },
                { "ButtonHitBrush",       ("#3a9b5e", "#4cb474") },
                { "ButtonSwitchBrush",    ("#7a5fc0", "#8f72c4") }
            };
        }

        public static void SwitchTheme()
        {
            IsDarkTheme = !IsDarkTheme;
            ApplyTheme(IsDarkTheme);
        }

        private static void ApplyTheme(bool darkMode)
        {
            ResourceDictionary resDict = Current.Resources;
            foreach (var pair in ThemeColors)
            {
                string hexColor = darkMode ? pair.Value.Dark : pair.Value.Light;
                try
                {
                    Color targetColor = (Color)ColorConverter.ConvertFromString(hexColor);
                    UpdateSolidBrush(resDict, pair.Key, targetColor);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"主题资源[{pair.Key}]颜色解析失败 {hexColor}：{ex.Message}");
                }
            }
        }

        private static void UpdateSolidBrush(ResourceDictionary resources, string resourceKey, Color color)
        {
            resources[resourceKey] = new SolidColorBrush(color);
        }

        public static void SetFontSize(double fontSize)
        {
            if (Current.Resources.Contains("FontSizeNormal"))
            {
                Current.Resources["FontSizeNormal"] = fontSize;
            }
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ApplyTheme(IsDarkTheme);
        }
    }
}