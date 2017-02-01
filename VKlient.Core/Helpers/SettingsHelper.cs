using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
#if !ONEVK_SDK
using Windows.Storage;
#endif

namespace OneVK.Helpers
{
    /// <summary>
    /// Представляет собой статический класс для управления параметрами приложения.
    /// </summary>
    internal static class SettingsHelper
    {
        /// <summary>
        /// Записывает в локальное хранилище параметров указанный объект,
        /// сериализуя его в Json-строку.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="settingName">Имя параметра.</param>
        /// <param name="value">Объект для сериализации и записи.</param>
        public static void Set<T>(string settingName, T value) 
        {
            try
            {
                using (var writter = new StringWriter())
                {
                    var serializer = new JsonSerializer();
                    serializer.Serialize(writter, value);
#if !ONEVK_SDK
                    ApplicationData.Current.LocalSettings.Values[settingName] = 
                        writter.GetStringBuilder().ToString();
#endif
#if DEBUG
                    Debug.WriteLine("The {0} is {1} successfully saved.", 
                        settingName, value.GetType());
#endif
                }
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Возвращает десериализованный объект параметра.
        /// </summary>
        /// <typeparam name="T">Тип объекта параметра.</typeparam>
        /// <param name="settingName">Имя параметра.</param>
        public static T Get<T>(string settingName)
        {
#if !ONEVK_SDK
            object obj;
            if (ApplicationData.Current.LocalSettings.Values.TryGetValue(settingName, out obj))
            {
                try
                {
                    using (var reader = new StringReader((string)obj))
                    {
                        using (var jsonReader = new JsonTextReader(reader))
                        {
                            var serializer = new JsonSerializer();
                            var result = serializer.Deserialize<T>(jsonReader);
#if DEBUG
                            Debug.WriteLine("The {0} is {1} successfully readed.",
                                settingName, result.GetType());
#endif
                            return result;
                        }
                    }
                }
                catch (Exception) 
                {
                   return default(T);
                }
            }
#endif
            return default(T);
        }

        /// <summary>
        /// Возвращает значение, указывающее, существует ли объект параметра.
        /// </summary>
        /// <param name="settingName">Имя параметра.</param>
        public static bool ContainsSetting(string settingName)
        {
            return ApplicationData.Current.LocalSettings.Values.ContainsKey(settingName);
        }

        /// <summary>
        /// Возвращает объект параметра и удаляет его из хранилища параметров.
        /// </summary>
        /// <typeparam name="T">Тп объекта параметра.</typeparam>
        /// <param name="settingName">Имя параметра.</param>
        public static T GetReset<T>(string settingName)
        {
            T result = Get<T>(settingName);
#if !ONEVK_SDK
            if (ApplicationData.Current.LocalSettings.Values.ContainsKey(settingName))
                ApplicationData.Current.LocalSettings.Values.Remove(settingName);
#endif
#if DEBUG
            Debug.WriteLine("The {0} is {1} successfully readed and reseted.",
                settingName, result.GetType());
#endif
            return result;
        }
    }
}
