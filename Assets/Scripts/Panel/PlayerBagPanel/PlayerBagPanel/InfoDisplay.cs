/****************************************************************************
 * 2024.12 DESKTOP-STANAOI
 ****************************************************************************/

using QFramework;
using Scripts.ItemModule;
using Scripts.Panel.Basic;

namespace Scripts.Panel.PlayerBagPanel
{
    public partial class InfoDisplay : UIElement, IController
    {
        private Slot Slot => this.DisplayCell.Slot;
        
        public void Start()
        {
            this.DisplayCell.SetSlot(this.GetModel<PlayerInventory>().Slots[0]);
            this.GetSystem<ItemCellSystem>().SelectedCellUpdate.Register(this.OnSelectChange)
                .UnRegisterWhenGameObjectDestroyed(this);
            this.DisplayCell.Slot.ItemChange.Register(this.SetInfoDisplay).UnRegisterWhenGameObjectDestroyed(this);
            this.ItemName.text = this.Slot.Item.ItemName;
            this.ItemDescription.text = this.Slot.Item.Description;
            this.ItemType.text = this.Slot.Item.ItemType.ToString();
            this.ItemQuality.text = this.Slot.Item.Quality.ToString();
            this.ItemValue.text = this.Slot.Item.Value.ToString("F1");
            ;
            this.ItemWeight.text = this.Slot.Item.Weight.ToString("F1");
        }
        
        public void OnSelectChange(ItemCell cell)
        {
            this.DisplayCell.Slot.ItemChange.UnRegister(this.SetInfoDisplay);
            if (cell == null)
            {
                this.DisplayCell.SetSlot(Slot.NullSlot);
                this.SetInfoDisplay(new SlotChange());
                return;
            }
            
            this.DisplayCell.SetSlot(cell.Slot);
            this.SetInfoDisplay(new SlotChange());
            this.DisplayCell.Slot.ItemChange.Register(this.SetInfoDisplay).UnRegisterWhenGameObjectDestroyed(this);
        }
        
        private void SetInfoDisplay(SlotChange obj)
        {
            if (this.Slot.IsEmpty)
            {
                this.ItemName.text = "";
                this.ItemDescription.text = "";
                this.ItemType.text = "";
                this.ItemQuality.text = "";
                this.ItemValue.text = "";
                this.ItemWeight.text = "";
                return;
            }
            
            this.ItemName.text = this.Slot.Item.ItemName;
            this.ItemDescription.text = this.Slot.Item.Description;
            this.ItemType.text = this.Slot.Item.ItemType.ToString();
            this.ItemQuality.text = this.Slot.Item.Quality.ToString();
            this.ItemValue.text = this.Slot.Item.Value.ToString("F1");
            ;
            this.ItemWeight.text = this.Slot.Item.Weight.ToString("F1");
        }
        
        
        protected override void OnBeforeDestroy()
        {
        }
        
        public IArchitecture GetArchitecture()
        {
            return SurvivalGameArch.Interface;
        }
    }
}