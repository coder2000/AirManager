using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Practices.Prism.Mvvm;

namespace AirManager.ViewModels
{
    public class CultureViewModel : BindableBase
    {
        /// <summary>
        /// Gets or sets the flag.
        /// </summary>
        /// <value>
        /// The flag.
        /// </value>
        public ImageSource Flag { get; set; }
        /// <summary>
        /// Gets or sets the language.
        /// </summary>
        /// <value>
        /// The language.
        /// </value>
        public string Language { get; set; }
        /// <summary>
        /// Gets the name of the language.
        /// </summary>
        /// <value>
        /// The name of the language.
        /// </value>
        public string LanguageName { get; private set; }

        /// <summary>
        /// Creates the specified flag.
        /// </summary>
        /// <param name="flag">The flag.</param>
        /// <param name="language">The language.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public static CultureViewModel Create(string flag, string language, string name)
        {
            return new CultureViewModel {Flag = new BitmapImage(new Uri(flag)), Language = language, LanguageName = name};
        }
    }
}
