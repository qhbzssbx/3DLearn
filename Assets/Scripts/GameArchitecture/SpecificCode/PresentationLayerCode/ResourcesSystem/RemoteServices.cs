using YooAsset;

namespace GF
{
    public class RemoteServices : IRemoteServices
    {
        private readonly string _defaultHostServer;
        private readonly string _fallbackHostServer;

        public RemoteServices(string defaultHostServer, string fallbackHostServer)
        {
            _defaultHostServer = defaultHostServer;
            _fallbackHostServer = fallbackHostServer;
        }

        public string GetRemoteFallbackURL(string fileName)
        {
            return $"{_defaultHostServer}/{fileName}";
        }

        public string GetRemoteMainURL(string fileName)
        {
            return $"{_fallbackHostServer}/{fileName}";
        }
    }
}

