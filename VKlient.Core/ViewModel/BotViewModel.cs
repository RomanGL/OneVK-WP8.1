using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using OneVK.Helpers;
using OneVK.Core;
using OneVK.Model.LongPoll;
using OneVK.Request;
using System.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Net.Http;
using OneVK.Response.Bot;
using OneVK.Enums.LongPoll;

namespace OneVK.ViewModel
{
    /// <summary>
    /// Представляет модель представления страницы бота ВКонтакте.
    /// </summary>
    public class BotViewModel : BaseViewModel
    {
        private readonly Random random = new Random(Environment.TickCount);
        private static readonly char[] wordSeparators = new char[] { ' ', ',', ';', ':', '.', '!', '?' };
        private readonly ObservableCollection<string> _lastHistory = new ObservableCollection<string>();
        private const string beginMonitoringText = "запустить";
        private const string stopMonitoringText = "остановить";
        private const string iiiRequestURL = "http://iii.ru/api/2.0/json/Chat.request";

        #region Конструкторы
        /// <summary>
        /// Конструктор по умолчанию.
        /// </summary>
        public BotViewModel()
        {
            BeginStopMonitoringCommand = new RelayCommand(() =>
            {
                if (_isWorking) Stop();
                else Begin();
            });
        }
        #endregion

        #region Приватные поля
        private bool _isWorking;
        private uint _repliesCount;
        private byte _retriesCount;
        private string _cuid;
        #endregion

        #region Свойства
        /// <summary>
        /// Текст на кнопке запуска/остановки мониторинга.
        /// </summary>
        public string BeginStopButtonText
        {
            get { return _isWorking ? stopMonitoringText : beginMonitoringText; }
        }
        /// <summary>
        /// Возвращает коллекцию последних 40-ка событий.
        /// </summary>
        public ObservableCollection<string> LastHistory
        {
            get { return _lastHistory; }
        }
        /// <summary>
        /// Количество ответов бота.
        /// </summary>
        public uint RepliesCount
        {
            get { return _repliesCount; }
            private set { Set<uint>(() => RepliesCount, ref _repliesCount, value); }
        }
        #endregion

        #region Команды
        public RelayCommand BeginStopMonitoringCommand { get; private set; }
        #endregion

        #region Публичные методы
        #endregion

        #region Приватные методы
        /// <summary>
        /// Запустить мониторинг.
        /// </summary>
        private async void Begin()
        {
            _isWorking = true;                      

            string request = String.Format("http://iii.ru/api/2.0/json/Chat.init/{0}/{1}",
                "970c8b3d-2e25-471d-8aab-efc87bcb7155", "onevk");
            string response = await GetAsync(request);
            if (String.IsNullOrEmpty(response))
            {
                Stop();
                return;
            }
            _cuid = JsonConvert.DeserializeObject<BotResponse<BotInitResponse>>(response).Response.Cuid;

            RaisePropertyChanged(() => BeginStopButtonText);  
            ServiceHelper.VKLongPollService.MessagesReceived += MessagesReceived;
        }        

        /// <summary>
        /// Остановить мониторинг.
        /// </summary>
        private void Stop()
        {
            _isWorking = false;
            RaisePropertyChanged(() => BeginStopButtonText);
            ServiceHelper.VKLongPollService.MessagesReceived -= MessagesReceived;
        }

        /// <summary>
        /// Вызывается при получении новых сообщений через LongPoll.
        /// </summary>
        private async void MessagesReceived(object sender, MessagesReceivedEventArgs e)
        {
            if (String.IsNullOrEmpty(_cuid)) return;
            for (int i = 0; i < e.Messages.Count; i++)
            {
                await Task.Delay(2000);
                await WorkOnMessage(e.Messages[i]);
            }
        }

        /// <summary>
        /// Обработать сообщение.
        /// </summary>
        /// <param name="msg">Информация о полученном сообщении.</param>
        private async Task WorkOnMessage(MessageInfo msg)
        {
            if ((msg.Flags & VKMessageFlags.Outbox) == VKMessageFlags.Outbox || String.IsNullOrWhiteSpace(msg.Text))
                return;

            var arr = new string[] { _cuid, msg.Text };
            string json = JsonConvert.SerializeObject(arr);
            string result = await PostAsync(iiiRequestURL, EncodeTo64(EncodeXOR(EncodeTo64(json))));

            if (String.IsNullOrEmpty(result))
            {
                Stop();
                return;
            }

            string ans = JsonConvert.DeserializeObject<BotResponse<BotChatResponse>>(result).Response.Text.Value;

            if (msg.IsChatMessage)
                await SendMessage(ans, msg.MessageID, chatID: msg.ChatID);
            else
                await SendMessage(ans, msg.MessageID, userID: msg.UserID);

            _lastHistory.Insert(0, String.Format("Отправлено сообщение: {0}\nНа сообщение: {1}",
                ans, msg.Text));

            if (_lastHistory.Count == 41)
                _lastHistory.RemoveAt(40);            

            //if (String.IsNullOrEmpty(result))
            //{
            //    if (_retriesCount == 5)
            //        return;

            //    _retriesCount++;
            //    await Task.Delay(TimeSpan.FromSeconds(2 * _retriesCount));
            //    await WorkOnMessage(msg);
            //}
            //_retriesCount = 0;
        }

        private static string EncodeTo64(string toEncode)
        {
            byte[] toEncodeAsBytes
                  = Encoding.UTF8.GetBytes(toEncode);
            string returnValue
                  = Convert.ToBase64String(toEncodeAsBytes);
            return returnValue;
        }

        private static string DecodeFrom64(string encodedData)
        {
            byte[] encodedDataAsBytes
                = Convert.FromBase64String(encodedData);
            string returnValue =
               Encoding.UTF8.GetString(encodedDataAsBytes, 0, encodedDataAsBytes.Length);
            return returnValue;
        }

        /// <summary>
        /// Декодировать ответ по алгоритму XOR.
        /// </summary>
        /// <param name="message">Сообщение для декодирования.</param>
        public static string DecodeXOR(string message)
        {
            string key = "some very-very long string without any non-latin characters due to different string representations inside of variable programming languages";
            byte[] arr = Encoding.UTF8.GetBytes(message);
            byte[] keyarr = Encoding.UTF8.GetBytes(key);
            byte[] result = new byte[arr.Length];
            for (int i = 0; i < arr.Length; i++)
            {
                result[i] = (byte)(arr[i] ^ keyarr[i % keyarr.Length]);
            }
            return Encoding.UTF8.GetString(result, 0, result.Length);
        }

        /// <summary>
        /// Кодировать ответ по алгоритму XOR.
        /// </summary>
        /// <param name="message">Сообщение для кодирования.</param>
        public static string EncodeXOR(string message)
        {
            string key = "some very-very long string without any non-latin characters due to different string representations inside of variable programming languages";

            byte[] text = Encoding.UTF8.GetBytes(message);
            byte[] result = new byte[text.Length];
            byte[] keyarr = Encoding.UTF8.GetBytes(key);
            for (int i = 0; i < text.Length; i++)
            {
                result[i] = (byte)(text[i] ^ keyarr[i % keyarr.Length]);
            }
            return Encoding.UTF8.GetString(result, 0, result.Length);
        }

        /// <summary>
        /// Отправить указанное сообщение.
        /// </summary>
        /// <param name="text">Текст сообщения.</param>
        /// <param name="userID">Идентификатор пользователя.</param>
        /// <param name="chatID">Идентификатор чата.</param>
        private async Task SendMessage(string text, ulong replyToMessageID, ulong userID = 0, uint chatID = 0)
        {
            var request = userID == 0 ? new SendMessageRequest(chatID) : new SendMessageRequest(userID);
            request.Message = text;
            request.ForwardMessages = new List<ulong> { replyToMessageID };

            var response = await request.ExecuteAsync();   
#if DEBUG
            Debug.WriteLine("Message send: " + response.Response + ". Text: " + text);
#endif
            RepliesCount++;
        }

        private async Task<string> PostAsync(string requestURL, string parameters)
        {
            string result = String.Empty;
            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.PostAsync(requestURL, new StringContent(parameters));
                    var bytes = await response.Content.ReadAsByteArrayAsync();
                    result = DecodeFrom64(DecodeXOR(DecodeFrom64(Encoding.UTF8.GetString(bytes, 0, bytes.Length))));
                }
            }
            catch (Exception) { }
            return result;
        }

        private static async Task<string> GetAsync(string query)
        {
            string result = String.Empty;
            using (var client = new HttpClient())
            {
                try
                {
                    var response = await client.GetAsync(query);
                    result = result = DecodeFrom64(DecodeXOR(DecodeFrom64(await response.Content.ReadAsStringAsync())));
                }
                catch (Exception) { }
            }
            return result;
        }
        #endregion        
    }
}
