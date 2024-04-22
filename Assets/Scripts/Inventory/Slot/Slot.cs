using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Slot : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{

    public ItemData item; // 획득한 아이템.
    public int itemCount; // 획득한 아이템의 개수.
    public Image itemImage; // 아이템의 이미지.
    //public int itemOriginPosX;
    //public int itemOriginPosY;

    // 필요한 컴포넌트.
    [SerializeField]
    private TextMeshProUGUI _textCount;
    [SerializeField]
    private GameObject _countImage;

    private void Awake()
    {
        ClearSlot();
    }

    // 이미지의 투명도 조절.
    private void SetColor(float alpha_)
    {
        Color color = itemImage.color;
        color.a = alpha_;
        itemImage.color = color;
    }

    // 아이템 획득
    public void AddItem(ItemData item_, int count_ = 1)
    {
        item = item_;
        itemCount = count_;
        if(itemCount == 0)
        {
            ClearSlot();
            return;
        }
        itemImage.sprite = item.Sprite;

        if (item.itemType == Constants.ItemType.Material || item.itemType == Constants.ItemType.Consume)
        {
            _countImage.SetActive(true);
            _textCount.text = itemCount.ToString();
        }
        else
        {
            _textCount.text = "0";
            _countImage.SetActive(false);
        }

        SetColor(1);
    }

    // 아이템 개수 조정.
    public void SetSlotCount(int count_)
    {
        if(itemCount + count_ > item.itemMax_Stack)
        {
            itemCount = item.itemMax_Stack;
            _textCount.text = itemCount.ToString();
            return;
        }
        itemCount += count_;
        _textCount.text = itemCount.ToString();

        if (itemCount <= 0)
            ClearSlot();
    }

    // 슬롯 초기화.
    public void ClearSlot()
    {
        item = null;
        itemCount = 0;
        itemImage.sprite = null;
        SetColor(0);

        _textCount.text = "0";
        _countImage.SetActive(false);
    }

    public void UseItem()
    {
        if (item.itemType == Constants.ItemType.Consume)
        {
            GameManager.Instance.UsePotion(item);
            SetSlotCount(-1);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (item != null)
            {
                //if (item.itemType == Constants.ItemType.Weapon)
                //{
                //    //무기 장착
                //}
                //else if(item.itemType == Constants.ItemType.Equip)
                //{
                //    //장비장착
                //}
                UseItem();
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(item != null)
        {
            ItemDescription.instance.SetColor(item);
            ItemDescription.instance.gameObject.SetActive(true);
            ItemDescription.instance.transform.position = eventData.position;
            ItemDescription.instance.itemName.text = item.itemName;
            ItemDescription.instance.itemDescription.text = item.itemDescription;
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        ItemDescription.instance.gameObject.SetActive(false);
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            SetColor(0.5f);
            DragSlot.instance.dragSlot = this;
            DragSlot.instance.DragSetImage(itemImage);
            DragSlot.instance.dragItem = item;
            DragSlot.instance.transform.position = eventData.position;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            DragSlot.instance.transform.position = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(item != null)
        {
            SetColor(1);
        }
        DragSlot.instance.SetColor(0);
        DragSlot.instance.dragItem = null;
        DragSlot.instance.dragSlot = null;
        DragSlot.instance.equipmentSlot = null;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (DragSlot.instance.dragSlot != null|| DragSlot.instance.equipmentSlot != null)
            ChangeSlot();
    }

    private void ChangeSlot()
    {
        ItemData _tempItem = item;
        int _tempItemCount = itemCount;
        if (DragSlot.instance.dragSlot == this)
        {
            return;  // 변경 없이 메서드 종료
        }
        if (DragSlot.instance.dragSlot != null)
        {
            if (item == DragSlot.instance.dragSlot.item && (item.itemType == Constants.ItemType.Material||item.itemType==Constants.ItemType.Consume))
            {
                if(itemCount + DragSlot.instance.dragSlot.itemCount > item.itemMax_Stack)
                {
                    SetSlotCount(DragSlot.instance.dragSlot.itemCount);
                    DragSlot.instance.dragSlot.SetSlotCount(_tempItemCount - item.itemMax_Stack);
                    return;
                }
                SetSlotCount(DragSlot.instance.dragSlot.itemCount);
                DragSlot.instance.dragSlot.ClearSlot();
                return;
            }
            AddItem(DragSlot.instance.dragSlot.item, DragSlot.instance.dragSlot.itemCount);
            if (_tempItem != null)
            {
                DragSlot.instance.dragSlot.AddItem(_tempItem, _tempItemCount);
            }
            else
                DragSlot.instance.dragSlot.ClearSlot();

            if(UIManager.Instance.ExistPopup(nameof(RewardPopup)))
            {
                GameManager.Instance.CallGetRewardItemEvent(GameManager.Instance.CallSetRewardItemEvent());
                GameManager.Instance.CallUpdateRewardCountEvent();
            }
        }
            
        else
        {
            
            if (_tempItem != null)
            {
                //아이템 타입이 같은 경우에만 교환가능
                if(DragSlot.instance.equipmentSlot.item.itemType == _tempItem.itemType)
                {
                    AddItem(DragSlot.instance.equipmentSlot.item);
                    DragSlot.instance.equipmentSlot.AddItem(_tempItem);
                }
                else
                {
                    //착용할수없음
                    Debug.Log("바꿀수없습니다.");
                    return;
                }
            }
            else
            {
                AddItem(DragSlot.instance.equipmentSlot.item);
                DragSlot.instance.equipmentSlot.ClearSlot();
            }

            if (UIManager.Instance.ExistPopup(nameof(RewardPopup)))
            {
                GameManager.Instance.CallGetRewardItemEvent(GameManager.Instance.CallSetRewardItemEvent());
                GameManager.Instance.CallUpdateRewardCountEvent();
            }
        }
    }
}
