namespace OneVK.Request
{
    /// <summary>
    /// Представляет запрос на вызов каптчи.
    /// </summary>
    public sealed class CaptchaForceRequest : BaseVKRequest<int>
    {
        public override string GetMethod() { return VKMethodsConstants.CaptchaForce; }
    }
}
