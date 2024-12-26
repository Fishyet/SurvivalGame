using System.Collections.Generic;
using System.Linq;
using QFramework;
using Scripts.ItemModule;

namespace Scripts.Panel.Basic
{
    public class ItemCellSystem : AbstractSystem
    {
        private ItemCell _selectedCell;
        private bool _isDragging;
        private HashSet<ItemCell> _draggingCells;
        private ItemCell _distributeCell;
        private int _distributeNumber;
        private List<Dictionary<string, LinkedList<Slot>>> _queryDic = new List<Dictionary<string, LinkedList<Slot>>>();
        
        public ItemCell SelectedCell
        {
            get => this._selectedCell;
            set
            {
                this._selectedCell?.SetOutline(false);
                this._selectedCell = value;
                this._selectedCell?.SetOutline(true);
                SelectedCellUpdate.Trigger(SelectedCell);
            }
        }
        
        public readonly EasyEvent<ItemCell> SelectedCellUpdate = new EasyEvent<ItemCell>();
        public HashSet<ItemCell> ManagedCells = new HashSet<ItemCell>();
        
        protected override void OnInit()
        {
            this._draggingCells = new HashSet<ItemCell>();
        }
        
        public void Register(ItemCell cell)
        {
            this.ManagedCells.Add(cell);
            cell.Click.Register(this.OnCellClick);
            cell.RightClick.Register(this.OnCellRightClick);
            cell.DoubleClick.Register(this.OnCellDoubleClick);
            cell.Enter.Register(this.OnCellEnter);
            cell.DragStart.Register(this.OnCellDragStart);
            cell.DragEnd.Register(this.OnCellDragEnd);
        }
        
        public void RegisterInventory(Inventory inventory)
        {
            this._queryDic.Add(inventory.SlotDic);
        }
        
        public void UnRegisterInventory(Inventory inventory)
        {
            this._queryDic.Remove(inventory.SlotDic);
        }
        
        private void OnCellDragEnd(ItemCell obj)
        {
            this._isDragging = false;
            this._draggingCells.Clear();
            this._distributeCell = null;
        }
        
        private void OnCellDragStart(ItemCell obj)
        {
            if (this._draggingCells.Count != 0) this._draggingCells.Clear();
            this._isDragging = true;
            this._distributeCell = obj;
            this._distributeNumber = obj.Slot.Number;
        }
        
        private void OnCellRightClick(ItemCell obj)
        {
            if (this._selectedCell != null)
            {
                if (this._selectedCell == obj)
                {
                    return;
                }
                else
                {
                    if (!obj.AddWith(this._selectedCell)) // 如果不能合并
                    {
                        obj.SwapWith(this._selectedCell); // 交换
                    }
                    
                    this.SelectedCell = obj;
                }
            }
        }
        
        public void UnRegister(ItemCell cell)
        {
            this.ManagedCells.Remove(cell);
            cell.Click.UnRegister(this.OnCellClick);
            cell.RightClick.UnRegister(this.OnCellRightClick);
            cell.DoubleClick.UnRegister(this.OnCellDoubleClick);
            cell.Enter.UnRegister(this.OnCellEnter);
        }
        
        private void OnCellEnter(ItemCell obj)
        {
            if (this._isDragging)
            {
                if (this._distributeCell == obj)
                {
                    return;
                }
                
                if (!obj.Slot.IsEmpty)
                {
                    return;
                }
                
                if (this._distributeNumber <= this._draggingCells.Count + 1)
                {
                    return;
                }
                
                if (!this._draggingCells.Add(obj))
                {
                    return;
                }
                
                this.Distribute();
            }
        }
        
        private void Distribute()
        {
            int n = this._distributeNumber / (this._draggingCells.Count + 1);
            int m = this._distributeNumber % (this._draggingCells.Count + 1);
            foreach (var cell in this._draggingCells)
            {
                if (cell.Slot.IsEmpty)
                {
                    cell.Slot.Set(this._distributeCell.Slot.Item, n);
                }
                else
                {
                    cell.Slot.SetNumReturnOutRange(n);
                }
            }
            
            this._distributeCell.Slot.SetNumReturnOutRange(m + n);
        }
        
        private void OnCellDoubleClick(ItemCell obj)
        {
            if (obj.Slot.IsEmpty || obj.Slot.IsFull)
            {
                return;
            }
            
            foreach (var dic in this._queryDic)
            {
                dic.TryGetValue(obj.Slot.Item.Uid, out var slots);
                if (slots == null)
                {
                    continue;
                }
                
                foreach (var slot in slots.ToArray())
                {
                    if (slot.IsFull || obj.Slot == slot)
                    {
                        continue;
                    }
                    
                    slot.AddEvent.Trigger(obj);
                    if (obj.Slot.IsFull) return;
                }
            }
        }
        
        private void OnCellClick(ItemCell obj)
        {
            if (this.SelectedCell == null)
            {
                this.SelectedCell = obj;
            }
            else
            {
                if (this.SelectedCell == obj)
                {
                    this.SelectedCell = null;
                }
                else
                {
                    this.SelectedCell = obj;
                }
            }
        }
    }
}