using QFramework;
using Scripts.ItemModule;
using Scripts.Panel.Basic;

namespace Scripts.Panel.PlayerBagPanel.Component
{
    public class BagItemCell : ItemCell
    {
        public override IArchitecture GetArchitecture()
        {
            return SurvivalGameArch.Interface;
        }
        
        public override void SetSlot(Slot value)
        {
            base.SetSlot(value);
            this._slot.AddEvent.Register((mainCell) => mainCell.AddWith(this)).UnRegisterWhenGameObjectDestroyed(this);
        }
    }
}