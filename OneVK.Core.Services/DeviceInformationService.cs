using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.System.Profile;

namespace OneVK.Core.Services
{
    /// <summary>
    /// Представляет сервис для получения информации об устройстве.
    /// </summary>
    public sealed class DeviceInformationService : IDeviceInformationService
    {
        /// <summary>
        /// Возвращает идентификатор оборудования.
        /// </summary>
        public string GetHardwareID()
        {
            var token = HardwareIdentification.GetPackageSpecificToken(null);
            var hardwareId = token.Id;
            var dataReader = DataReader.FromBuffer(hardwareId);

            byte[] bytes = new byte[hardwareId.Length];
            dataReader.ReadBytes(bytes);

            return Convert.ToBase64String(bytes).Replace("=", "");
        }
    }
}
