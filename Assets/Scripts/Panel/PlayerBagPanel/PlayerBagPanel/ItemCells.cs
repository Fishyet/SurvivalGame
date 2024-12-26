/****************************************************************************
 * 2024.12 DESKTOP-STANAOI
 ****************************************************************************/

using System.Collections.Generic;
using QFramework;
using Scripts.ItemModule;
using Scripts.Panel.PlayerBagPanel.Component;
using UnityEngine;

namespace Scripts.Panel.PlayerBagPanel
{
    public partial class ItemCells : UIElement
    {
        [SerializeField] private GameObject itemCellPrefab;
        
        private void Awake()
        {
        }
        
        public void Init(List<Slot> slots)
        {
            for (int i = 0; i < slots.Count; i++)
            {
                var slot = slots[i];
                var itemCell = Instantiate(this.itemCellPrefab, this.CellContainer, false);
                BagItemCell c = itemCell.GetComponent<BagItemCell>();
                c.SetSlot(slot);
            }
        }
        
        protected override void OnBeforeDestroy()
        {
        }
    }
}