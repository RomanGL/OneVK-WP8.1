using OneVK.Enums.Common;
using OneVK.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;

namespace TileUpdaterTask
{
    /// <summary>
    /// Представляет фоновую задачу обновления тайла приложения.
    /// </summary>
    public sealed class TileUpdaterTask : IBackgroundTask
    {
        private BackgroundTaskDeferral _deferral;

        /// <summary>
        /// Запускает фоновую задачу.
        /// </summary>
        /// <param name="taskInstance">Экземпляр фоновой задачи.</param>
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            _deferral = taskInstance.GetDeferral();
            StartUpdate();
        }

        /// <summary>
        /// Запускает операцию обновления тайла.
        /// </summary>
        private async void StartUpdate()
        {
            var request = new GetCountersRequest();

        }
    }
}
