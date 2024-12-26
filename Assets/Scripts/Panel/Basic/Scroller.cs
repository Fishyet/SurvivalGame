using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Scripts.Panel.Basic
{
    public class Scroller : MonoBehaviour, IScrollHandler
    {
        public RectTransform content;
        public Scrollbar verticalScrollbar;
        public float scrollSpeed;
        
        private Vector2 _contentStartPosition;
        public RectTransform rectTransform;
        
        void Start()
        {
            _contentStartPosition = content.localPosition;
            verticalScrollbar.onValueChanged.AddListener(OnVerticalScroll);
            rectTransform = GetComponent<RectTransform>();
        }
        
        
        void OnVerticalScroll(float value)
        {
            content.localPosition = new Vector2(this._contentStartPosition.x,
                this._contentStartPosition.y + (content.rect.height - rectTransform.rect.height) * value);
        }
        
        void OnDestroy()
        {
            verticalScrollbar?.onValueChanged.RemoveListener(OnVerticalScroll);
        }
        
        public void OnScroll(PointerEventData eventData)
        {
            float newValue = verticalScrollbar.value - eventData.scrollDelta.y * scrollSpeed * 0.01f * Time.fixedTime;
            verticalScrollbar.value = Mathf.Clamp01(newValue);
        }
    }
}