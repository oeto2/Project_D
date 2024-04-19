using UnityEngine;
[System.Serializable] //반드시 필요
public class Row //행에 해당되는 이름
{
    public Slot[] row;
}

public class TetrisInventory : MonoBehaviour
{
    public Row[] column; //열에 해당되는 이름

    //private void Start()
    //{
    //    for(int i=0;i<column.Length; i++)
    //    {
    //        for(int j = 0; j < column[i].row.Length; j++)
    //        {
    //            column[i].row[j].itemOriginPosX = j;
    //            column[i].row[j].itemOriginPosY = i;
    //        }
    //    }
    //}

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Q))
        {
            Debug.Log("Q누름");
            AcquireItem(Database.Item.Get(20000001));
            AcquireItem(Database.Item.Get(30000002));
        }
    }
    public void AcquireItem(ItemData item_, int count_ = 1)
    {
        if (item_.itemType == Constants.ItemType.Consume || item_.itemType == Constants.ItemType.Material)
        {
            for (int i = 0; i < column.Length; i++)
            {
                for (int j = 0; j < column[i].row.Length; j++)
                {
                    if (column[i].row[j].item != null)
                    {
                        if (column[i].row[j].item.itemName == item_.itemName)
                        {
                            column[i].row[j].SetSlotCount(count_);
                            //InformationManager.Instance.SaveInformation(i, _item.id, _count);
                            return;
                        }
                    }

                }
            }
        }
        for (int i = 0; i < column.Length; i++)
        {
            for(int j =0; j < column[i].row.Length; j++)
            {
                if (column[i].row[j].item == null)
                {
                    if (CheckenoughSpace(item_, i, j))
                    {
                        for (int k = i; k < i + item_.height; k++)
                        {
                            for (int o = j; o < j + item_.width; o++)
                            {
                                column[k].row[o].AddItem(item_, count_);
                            }
                        }
                        return;
                    }
                }

                //if (column[i].row[j].item == null)
                //{
                //    column[i].row[j].AddItem(_item, _count);
                //    //InformationManager.Instance.SaveInformation(i, _item.id, _count);
                //    return;
                //}

            }
        }
    }

    private bool CheckenoughSpace(ItemData item_,int i,int j)
    {
        bool enoughSpace = false;
        if (i + item_.height > column.Length)
            return false;
        for(int k = i; k < i+item_.height; k++)
        {
            if (j + item_.width > column[k].row.Length)
                return false;
            if (column[k].row[j].item == null)
            {
                for (int o = j; o < j + item_.width; o++)
                {
                    if (column[k].row[o].item == null)
                        enoughSpace = true;
                    else
                        return false;
                }
            }
        }
        return enoughSpace;
    }
}