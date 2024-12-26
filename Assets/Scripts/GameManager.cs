using QFramework;
using Scripts.Panel.PlayerBagPanel;

namespace Scripts
{
    public class GameManager : MonoSingleton<GameManager>
    {
        private void Start()
        {
            _ = ResourceManager.Instance; //初始化资源管理器
            _ = Database.Instance; //初始化数据库
            UIKit.OpenPanel<PlayerBagPanel>();
        }
        
        protected override void OnDestroy()
        {
            base.OnDestroy();
            ResourceManager.Instance.Dispose();
            Database.Instance.Dispose();
        }
    }
}