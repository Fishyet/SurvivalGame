using QFramework;

namespace Scripts
{
    public class ResourceManager : Singleton<ResourceManager>
    {
        private ResLoader _resLoader;
        
        private ResourceManager()
        {
            // 注册资源加载器
            ResKit.Init();
            this._resLoader = ResLoader.Allocate();
        }
        
        public static T LoadSync<T>(string assetName) where T : UnityEngine.Object
        {
            return Instance._resLoader.LoadSync<T>(assetName);
        }
        
        public override void Dispose()
        {
            base.Dispose();
            this._resLoader.Recycle2Cache();
        }
    }
}