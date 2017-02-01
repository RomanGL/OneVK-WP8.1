using System.Threading.Tasks;

namespace OneVK.Core.Services
{
    /// <summary>
    /// Предсталяет сервис для проигрывания звуковых эффектов в формате WAV.
    /// </summary>
    public interface ISoundService
    {
        /// <summary>
        /// Воспроизвести звуковой эффект.
        /// </summary>
        /// <param name="source">Путь к WAV файлу.</param>
        /// <param name="volume">Громкость.</param>
        Task PlaySound(string source, float volume = 1);
    }
}
