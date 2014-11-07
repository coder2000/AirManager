using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Practices.Prism.Mvvm;

namespace AirManager.ViewModels
{
    public class CultureViewModel : BindableBase
    {
        public ImageSource Flag { get; set; }
        public string Language { get; set; }
        public string LanguageName { get; private set; }

        public static CultureViewModel Create(string flag, string language, string name)
        {
            return new CultureViewModel {Flag = new BitmapImage(new Uri(flag)), Language = language, LanguageName = name};
        }
    }
}
