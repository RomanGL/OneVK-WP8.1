using Microsoft.Practices.ServiceLocation;
using OneVK.Enums.App;
using OneVK.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace OneVK.Controls
{
    /// <summary>
    /// Представляет методы расширения для <see cref="RichTextBlock"/>.
    /// </summary>
    public static class RichTextBlockExtensions
    {
        private static readonly Regex linksRegex = new Regex(@"(\[.*?\|.*?\])|(https?://\S+)");
        //private static readonly Regex linksRegex = new Regex(@"(https?|ftp:\/\/)?([\w\.]+)\.([a-z]{2,6}\.?)(\/[\w\.]*)*\/?");
        private static readonly Regex emojiRegex = new Regex(@"(\ud83c[\udf00-\udfff])|(\ud83d[\udc00-\ude4f])|(\ud83d[\ude80-\udeff])|([\u2600-\u26ff])|([\u2702-\u27b0])");

        public static string GetText(DependencyObject obj)
        {
            return (string)obj.GetValue(TextProperty);
        }

        public static void SetText(DependencyObject obj, string value)
        {
            obj.SetValue(TextProperty, value);
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.RegisterAttached("Text", typeof(string), typeof(RichTextBlockExtensions), 
                new PropertyMetadata(default(string), OnTextChanged));

        private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var tb = d as RichTextBlock;
            if (tb == null) return;

            tb.Blocks.Clear();

            var text = e.NewValue as string;
            if (string.IsNullOrWhiteSpace(text))
            {
                return;
            }

            tb.Blocks.Add(CreateParagraph(text));
        }

        /// <summary>
        /// Создает параграф на снове переданного текста.
        /// </summary>
        /// <param name="text">Текст для обработки.</param>
        private static Paragraph CreateParagraph(string text)
        {
            var settings = ServiceLocator.Current.GetInstance<SettingsService>();
            var paragraph = new Paragraph();

            string[] splitResult = linksRegex.Split(text);
            foreach (string block in splitResult)
            {
                if (String.IsNullOrEmpty(block)) continue;
                if (block.StartsWith("http", StringComparison.OrdinalIgnoreCase))
                {
                    var hp = new Hyperlink { NavigateUri = new Uri(block) };
                    hp.Inlines.Add(new Run { Text = block });
                    paragraph.Inlines.Add(hp);
                }
                else if (block.StartsWith("[", StringComparison.OrdinalIgnoreCase) && 
                    block.EndsWith("]", StringComparison.OrdinalIgnoreCase))
                {
                    string part = block.Replace("[", "").Replace("]", "");
                    paragraph.Inlines.Add(new Run { Text = part.Split(new char[] { '|' })[1] });
                }
                else
                {
                    switch (settings.EmojiType)
                    {
                        case EmojiType.Twitter:
                            SetTwitterEmoji(paragraph, block);
                            continue;
                        case EmojiType.Apple:
                            SetAppleEmoji(paragraph, block);
                            continue;
                        default:
                            paragraph.Inlines.Add(new Run { Text = block });
                            continue;
                    }
                }
            }
            
            return paragraph;
        }

        /// <summary>
        /// Вставляет в параграф Twitter Emoji.
        /// </summary>
        /// <param name="paragraph">Параграф для добавления.</param>
        /// <param name="block">Блок текста для добавления.</param>
        private static void SetTwitterEmoji(Paragraph paragraph, string block)
        {
            var r = emojiRegex.Split(block);
            foreach (string s in r)
            {
                if (emojiRegex.IsMatch(s))
                {
                    string path = null;
                    for (int i = 0; i < s.Length; i += Char.IsSurrogatePair(s, i) ? 2 : 1)
                    {
                        try
                        {
                            int x = Char.ConvertToUtf32(s, i);
                            path = String.Format("ms-appx:///Assets/TwitterEmoji/{0:x}.png", x);
                        }
                        catch (Exception) { }
                    }

                    if (path == null)
                    {
                        paragraph.Inlines.Add(new Run { Text = s });
                        continue;
                    }

                    var cont = new InlineUIContainer();
                    var img = new Image() { Stretch = Stretch.Uniform, Source = new BitmapImage(new Uri(path)) };
                    img.Height = 20;
                    img.Margin = new Thickness(0, 0, 0, -2);
                    cont.Child = img;

                    paragraph.Inlines.Add(cont);
                }
                else
                    paragraph.Inlines.Add(new Run { Text = s });
            }
        }

        /// <summary>
        /// Вставляет в параграф Apple Emoji.
        /// </summary>
        /// <param name="paragraph">Параграф для добавления.</param>
        /// <param name="block">Блок текста для добавления.</param>
        private static void SetAppleEmoji(Paragraph paragraph, string block)
        {
            var r = emojiRegex.Split(block);
            foreach (string s in r)
            {
                if (emojiRegex.IsMatch(s))
                {
                    string path = null;
                    for (int i = 0; i < s.Length; i += Char.IsSurrogatePair(s, i) ? 2 : 1)
                    {
                        try
                        {
                            int x = Char.ConvertToUtf32(s, i);
                            path = String.Format("ms-appx:///Assets/AppleEmoji/{0:x}.png", x);
                        }
                        catch (Exception) { }
                    }

                    if (path == null) continue;

                    var cont = new InlineUIContainer();
                    var img = new Image() { Stretch = Stretch.Uniform, Source = new BitmapImage(new Uri(path)) };
                    img.Height = 20;
                    img.Margin = new Thickness(0, 0, 0, -2);
                    cont.Child = img;

                    paragraph.Inlines.Add(cont);
                }
                else
                    paragraph.Inlines.Add(new Run { Text = s });
            }
        }
    }
}
