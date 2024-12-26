using DG.Tweening;
using QFramework;
using Scripts.ItemModule;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

namespace Scripts.Panel.Basic
{
    public abstract class ItemCell : MonoBehaviour, IController, IPointerClickHandler, IPointerEnterHandler,
        IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        [SerializeField] private Outline outline;
        [SerializeField] private Text numberText;
        [SerializeField] private GameObject textBg;
        [SerializeField] private Image iconImage;
        public Transform IconTransform => this.iconImage.transform;
        public readonly EasyEvent<ItemCell> Click = new EasyEvent<ItemCell>();
        public readonly EasyEvent<ItemCell> RightClick = new EasyEvent<ItemCell>();
        public readonly EasyEvent<ItemCell> DoubleClick = new EasyEvent<ItemCell>();
        public readonly EasyEvent<ItemCell> Enter = new EasyEvent<ItemCell>();
        public readonly EasyEvent<ItemCell> DragStart = new EasyEvent<ItemCell>();
        public readonly EasyEvent<ItemCell> DragEnd = new EasyEvent<ItemCell>();
        public bool IsEmpty => this._slot.IsEmpty;
        protected Slot _slot;
        public Slot Slot => this._slot;
        
        public virtual bool IsCanSwap(ItemCell other)
        {
            return true;
        }
        
        public virtual void SetSlot(Slot value)
        {
            this._slot = value;
            this._slot.ItemChange.Register(this.SetIcon).UnRegisterWhenGameObjectDestroyed(this);
            this._slot.NumberChange.Register(this.SetNumber).UnRegisterWhenGameObjectDestroyed(this);
            UpdateUI();
        }
        
        public virtual bool AddWith(ItemCell other)
        {
            if (this._slot.IsSameItem(other._slot))
            {
                int number = this._slot.SetNumReturnOutRange(this._slot.Number + other._slot.Number); //返回多余的数量
                if (number != other._slot.Number) //如果多余的数量不等于添加的数量
                {
                    Vector3 otherPosition = other.IconTransform.position;
                    other._clonedObject = Instantiate(other.iconImage.gameObject, other.transform); //为了保持原来的大小
                    other._clonedObject.transform.SetParent(other.ParentParent);
                    Sequence sequence = DOTween.Sequence();
                    sequence.Append(other._clonedObject.transform.DOMove(this.IconTransform.position, 0.3f));
                    other._slot.SetNumReturnOutRange(number);
                    sequence.Play().OnComplete(() => Destroy(other._clonedObject));
                }
                
                //如果多余的数量等于添加的数量，那么就不用做任何事情
                return true;
            }
            
            return false;
        }
        
        public virtual bool SwapWith(ItemCell other)
        {
            if (!this.IsCanSwap(other)) return false;
            if (!other.IsCanSwap(this)) return false;
            Vector3 thisPosition = this.IconTransform.position;
            Vector3 otherPosition = other.IconTransform.position;
            this.numberText.enabled = false;
            this.textBg.LocalScale(0); //隐藏
            other.numberText.enabled = false;
            other.textBg.LocalScale(0); //隐藏
            ActionKit.Sequence()
                .DOTween(() =>
                {
                    this.IconTransform.DOMove(otherPosition, 0.3f);
                    return other.IconTransform.DOMove(thisPosition, 0.3f);
                })
                .Callback(() =>
                {
                    this.numberText.enabled = true;
                    this.textBg.LocalScale(1);
                    other.numberText.enabled = true;
                    other.textBg.LocalScale(1);
                    this.IconTransform.Position(thisPosition);
                    other.IconTransform.Position(otherPosition);
                    Item tempItem = this._slot.Item;
                    int tempNumber = this._slot.Number;
                    this._slot.Set(other._slot.Item, other._slot.Number);
                    other._slot.Set(tempItem, tempNumber);
                }).Start(this);
            return true;
        }
        
        
        public virtual void Start()
        {
            this.GetSystem<ItemCellSystem>().Register(this);
            this.outline.enabled = false;
        }
        
        public abstract IArchitecture GetArchitecture();
        
        protected virtual void UpdateUI()
        {
            if (this._slot.IsEmpty)
            {
                this.iconImage.enabled = false;
                this.numberText.gameObject.SetActive(false);
                this.textBg.SetActive(false);
            }
            else
            {
                this.SetIcon(this._slot.Item.ID);
                this.SetNumber(this._slot.Number);
            }
        }
        
        public virtual void SetOutline(bool isShow)
        {
            this.outline.enabled = isShow;
        }
        
        /// <summary>
        /// 但凡有变化就调用
        /// </summary>
        /// <param name="slotChange"></param>
        protected virtual void SetIcon(SlotChange slotChange)
        {
            if (this._slot.IsEmpty)
            {
                this.iconImage.enabled = false;
                return;
            }
            
            this.iconImage.enabled = true;
            this.iconImage.sprite = ResourceManager.LoadSync<Sprite>($"item{this._slot.Item.ID}");
        }
        
        
        /// <summary>
        /// 仅第一次设置时调用
        /// </summary>
        /// <param name="id"></param>
        protected virtual void SetIcon(int id)
        {
            this.iconImage.enabled = true;
            this.iconImage.sprite = ResourceManager.LoadSync<Sprite>($"item{id}");
        }
        
        /// <summary>
        /// 仅第一次设置时调用
        /// </summary>
        /// <param name="number"></param>
        protected virtual void SetNumber(int number)
        {
            this.numberText.text = number.ToString();
            this.numberText.gameObject.SetActive(number > 0);
            this.textBg.SetActive(number > 0);
        }
        
        /// <summary>
        /// 但凡有变化就调用
        /// </summary>
        /// <param name="slotChange"></param>
        protected virtual void SetNumber(SlotChange slotChange)
        {
            this.numberText.text = slotChange.CurSlot.Number.ToString();
            this.numberText.gameObject.SetActive(!slotChange.CurSlot.IsEmpty);
            this.textBg.SetActive(!slotChange.CurSlot.IsEmpty);
        }
        
        private bool _lock = false;
        private bool _isCheckingDoubleClick = false;
        
        public virtual void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                this.RightClick.Trigger(this);
                return;
            }
            
            if (eventData.clickCount == 2)
            {
                this.DoubleClick.Trigger(this);
                this._isCheckingDoubleClick = false;
            }
            else
            {
                if (this._isCheckingDoubleClick || this._lock) return;
                this._isCheckingDoubleClick = true;
                this._lock = true;
                ActionKit.Delay(0.2f, () =>
                {
                    if (_isCheckingDoubleClick)
                    {
                        this.Click.Trigger(this);
                    }
                    
                    _isCheckingDoubleClick = false;
                    this._lock = false;
                }).Start(this);
            }
        }
        
        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            this.Enter.Trigger(this);
        }
        
        
        protected virtual void OnDestroy()
        {
            this.GetSystem<ItemCellSystem>().UnRegister(this);
        }
        
        public virtual void OnDrag(PointerEventData eventData)
        {
            if (this._slot.IsEmpty) return;
            this._clonedObject.transform.position = eventData.position;
        }
        
        private Transform ParentParent => this.transform.parent.parent;
        private GameObject _clonedObject;
        
        
        public void OnBeginDrag(PointerEventData eventData)
        {
            if (this._slot.IsEmpty) return;
            this._clonedObject = Instantiate(this.iconImage.gameObject, this.transform); //为了保持原来的大小
            this._clonedObject.transform.SetParent(this.ParentParent);
            this.DragStart.Trigger(this);
        }
        
        public void OnEndDrag(PointerEventData eventData)
        {
            if (this._slot.IsEmpty) return;
            this._clonedObject.transform.DOMove(this.IconTransform.position, 0.3f)
                .OnComplete(() => Destroy(this._clonedObject));
            this.DragEnd.Trigger(this);
        }
    }
}