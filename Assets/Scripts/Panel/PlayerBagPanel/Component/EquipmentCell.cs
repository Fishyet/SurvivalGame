using QFramework;
using Scripts.ItemModule;
using Scripts.Panel.Basic;
using UnityEngine.EventSystems;

namespace Scripts.Panel.PlayerBagPanel.Component
{
    public class EquipmentCell : ItemCell
    {
        public override IArchitecture GetArchitecture()
        {
            return SurvivalGameArch.Interface;
        }
        
        public override bool AddWith(ItemCell other)
        {
            return false;
        }
        
        public override bool IsCanSwap(ItemCell other)
        {
            return other is not EquipmentCell && (other.Slot.IsEmpty || other.Slot.Item.ItemType == ItemType.装备);
        }
        
        public override void OnDrag(PointerEventData eventData)
        {
        }
    }
}