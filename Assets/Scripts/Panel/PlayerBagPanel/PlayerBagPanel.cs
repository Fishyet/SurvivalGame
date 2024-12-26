using QFramework;
using Scripts.ItemModule;
using Scripts.Panel.Basic;

namespace Scripts.Panel.PlayerBagPanel
{
    public class PlayerBagPanelData : UIPanelData
    {
    }
    
    public partial class PlayerBagPanel : UIPanel, IController
    {
        private PlayerInventory _playerInventory;
        
        protected override void OnInit(IUIData uiData = null)
        {
            mData = uiData as PlayerBagPanelData ?? new PlayerBagPanelData();
            // please add init code here
            this._playerInventory = this.GetModel<PlayerInventory>();
            this.ItemCells.Init(this._playerInventory.Slots);
            this.EquipmentCells.Init();
        }
        
        public void UpdateUI()
        {
        }
        
        protected override void OnOpen(IUIData uiData = null)
        {
        }
        
        protected override void OnShow()
        {
            this.GetSystem<ItemCellSystem>().RegisterInventory(this._playerInventory);
        }
        
        protected override void OnHide()
        {
            this.GetSystem<ItemCellSystem>().UnRegisterInventory(this._playerInventory);
        }
        
        protected override void OnClose()
        {
        }
        
        public IArchitecture GetArchitecture()
        {
            return SurvivalGameArch.Interface;
        }
    }
}