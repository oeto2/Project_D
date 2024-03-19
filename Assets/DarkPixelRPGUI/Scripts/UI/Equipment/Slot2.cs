using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DarkPixelRPGUI.Scripts.UI.Equipment
{
    public class Slot2 : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
    {
        public Item item;
        public int itemCount;
        public Image itemImage;
        public Image placedItemImage;

        [SerializeField]
        private TextMeshProUGUI _itemCountText;
        [SerializeField]
        private GameObject _countImage;

        public void Additem(Item _item,int _count =1)
        {
            item = _item;
            itemCount = _count;
            itemImage.sprite = item.Sprite;

            ////장비아이템이 아닐경우
            //_countImage.SetActive(true);
            //_itemCountText.text = itemCount.ToString();

            //장비아이템이 아닐경우
            //_itemCountText.text = "0";
            //_countImage.SetActive(false);

            placedItemImage.enabled = true;
        }

        public void SetSlotCount(int _count)
        {
            itemCount += _count;
            _itemCountText.text = itemCount.ToString();

            if (itemCount <= 0)
                ClearSlot();
        }

        public virtual void ClearSlot()
        {
            itemImage.sprite = null;
            itemImage.color = Color.white;
            itemImage.enabled = false;
            item = null;
            _itemCountText.text = "0";
            _countImage.SetActive(false);
            if (placedItemImage)
            {
                placedItemImage.enabled = false;
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if(eventData.button == PointerEventData.InputButton.Right)
            {
                if(item != null)
                {
                    //아이템을 장착
                }
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            
        }

        public void OnDrag(PointerEventData eventData)
        {
           
        }

        public void OnEndDrag(PointerEventData eventData)
        {
           
        }

        public void OnDrop(PointerEventData eventData)
        {
          
        }
    }

}