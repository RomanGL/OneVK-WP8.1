using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OneVK.Enums.Wall;

namespace OneVK.Model.Wall
{
    public sealed class VKPostSource
    {
        private const string _vk = "vk";
        private const string _widget = "widget";
        private const string _api = "api";
        private const string _rss = "rss";
        private const string _sms = "sms";

        private const string _android = "android";
        private const string _ios = "ios";
        private const string _wphone = "wphone";

        /// <summary>
        /// Тип источника записи в виде строки.
        /// </summary>
        [JsonProperty("type")]
        private string _type { get; set; }

        /// <summary>
        /// Платформа, с которой была опубликована запись
        /// в виде строки.
        /// </summary>
        [JsonProperty("platform")]
        private string _platform { get; set; }

        /// <summary>
        /// Тип источника записи.
        /// </summary>
        public VKPostSourceType Type
        {
            get
            {
                switch (_type)
                {
                    case _widget:
                        return VKPostSourceType.Widget;
                    case _api:
                        return VKPostSourceType.API;
                    case _rss:
                        return VKPostSourceType.RSS;
                    case _sms:
                        return VKPostSourceType.SMS;
                    default:
                        return VKPostSourceType.VK;
                }
            }
            set
            {
                switch (value)
                {
                    case VKPostSourceType.Widget:
                        _type = _widget;
                        break;
                    case VKPostSourceType.API:
                        _type = _api;
                        break;
                    case VKPostSourceType.RSS:
                        _type = _rss;
                        break;
                    case VKPostSourceType.SMS:
                        _type = _sms;
                        break;
                    case VKPostSourceType.VK:
                        _type = _vk;
                        break;
                }
            }
        }

        /// <summary>
        /// Платформа, с которой была опубликована запись.
        /// </summary>
        public VKPostSourcePlatform Platform
        {
            get
            {
                switch (_platform)
                {
                    case _android:
                        return VKPostSourcePlatform.Android;
                    case _ios:
                        return VKPostSourcePlatform.iOS;
                    case _wphone:
                        return VKPostSourcePlatform.WinPhone;
                    default:
                        return VKPostSourcePlatform.NotSpecified;
                }
            }
            set
            {
                switch (value)
                {
                    case VKPostSourcePlatform.NotSpecified:
                        _platform = "";
                        break;
                    case VKPostSourcePlatform.Android:
                        _platform = _android;
                        break;
                    case VKPostSourcePlatform.iOS:
                        _platform = _ios;
                        break;
                    case VKPostSourcePlatform.WinPhone:
                        _platform = _wphone;
                        break;
                }
            }
        }

        // date -- https://vk.com/dev/post_source

        /// <summary>
        /// Ссылка на ресурс, с которого была опубликована запись.
        /// </summary>
        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
