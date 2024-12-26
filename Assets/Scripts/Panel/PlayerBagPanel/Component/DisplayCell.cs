using QFramework;
using Scripts.ItemModule;
using Scripts.Panel.Basic;
using UnityEngine.EventSystems;

namespace Scripts.Panel.PlayerBagPanel.Component
{
    public class DisplayCell : ItemCell
    {
        public override IArchitecture GetArchitecture()
        {
            return SurvivalGameArch.Interface;
        }
        
        protected override void SetNumber(int number)
        {
        }
        
        protected override void SetNumber(SlotChange slotChange)
        {
        }
        
        public override bool AddWith(ItemCell other)
        {
            return false;
        }
        
        public override bool SwapWith(ItemCell other)
        {
            return false;
        }
        
        public override void Start()
        {
        }
        
        public override void OnPointerClick(PointerEventData eventData)
        {
        }
        
        public override void OnPointerEnter(PointerEventData eventData)
        {
        }
        
        public override void OnDrag(PointerEventData eventData)
        {
        }
        
        protected override void OnDestroy()
        {
        }
    }
}