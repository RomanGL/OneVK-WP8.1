using OneVK.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Core;

namespace OneVK.Core.Collections
{
    public class TestUpDownCollection : ObservableCollection<string>, ISupportUpDownIncrementalLoading
    {
        private int _upCount;
        private int _downCount;

        public bool HasMoreDownItems { get { return _downCount < 300; } }
        
        public bool HasMoreUpItems { get { return _upCount < 300; } }

        public Task<object> LoadDownAsync(uint count)
        {
            return Task.Run<object>(async () =>
            {
                await CoreHelper.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    for (int i = 0; i < count; i++)
                    {
                        this.Add("Эта строка добавлена в конец");
                    }
                });

                _downCount += (int)count;
                Debug.WriteLine("Added down " + count + " items.");
                return null;
            });               
        }

        public Task<object> LoadUpAsync(uint count)
        {
            return Task.Run<object>(async () =>
            {
                await CoreHelper.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    for (int i = 0; i < count; i++)
                    {
                        this.Insert(0, "Эта строка добавлена в начало");
                    }
                });

                _upCount += (int)count;
                Debug.WriteLine("Added up " + count + " items.");
                return null;
            });
        }
    }
}
