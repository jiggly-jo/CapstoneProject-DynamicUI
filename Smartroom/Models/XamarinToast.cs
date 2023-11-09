namespace Smartroom.Models
{
    /// <summary>
    /// Interface for Toast settings on both ios and Android Platforms
    /// </summary>
    public interface IXamarinToast
    {
        void LongAlert(string message);
        void ShortAlert(string message);
    }
}