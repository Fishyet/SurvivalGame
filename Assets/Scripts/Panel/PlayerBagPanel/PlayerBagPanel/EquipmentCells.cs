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
    public partial class EquipmentCells : UIElement, IController
    {
        [SerializeField] private List<EquipmentCell> cells = new List<EquipmentCell>();
        
        public void Init()
        {
            PlayerInventory inventory = this.GetModel<PlayerInventory>();
            this.cells[0].SetSlot(inventory.LeftHandSlot);
            this.cells[1].SetSlot(inventory.HeadSlot);
            this.cells[2].SetSlot(inventory.BodySlot);
            this.cells[3].SetSlot(inventory.LegSlot);
            this.cells[4].SetSlot(inventory.FootSlot);
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