using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGrid : MonoBehaviour
{
    const float tileSizeWidth = 44;
    const float tileSizeHeight = 44;

    InventoryItem[,] inventoryItemsSlot;

    RectTransform rectTransform;

    [SerializeField] int gridSizeWidth = 10;
    [SerializeField] int gridSizeHeight = 13;

    [SerializeField] GameObject inventoryItemPrefab;
    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        Init(gridSizeWidth, gridSizeHeight);

        InventoryItem inventoryItem = Instantiate(inventoryItemPrefab).GetComponent<InventoryItem>();
    }

    private void Init(int width, int height)
    {
        inventoryItemsSlot = new InventoryItem[width, height];
        Vector2 size = new Vector2(width*tileSizeWidth, height*tileSizeHeight);
        rectTransform.sizeDelta = size;
    }

    Vector2 positionOnTheGrid = new Vector2();
    Vector2Int tileGridPosition = new Vector2Int();
    public Vector2Int GetTileGridPosition(Vector2 mousePosition)
    {
        positionOnTheGrid.x = mousePosition.x - rectTransform.position.x;
        positionOnTheGrid.y = rectTransform.position.y - mousePosition.y;

        tileGridPosition.x = (int)(positionOnTheGrid.x/tileSizeWidth);
        tileGridPosition.y = (int)(positionOnTheGrid.y/tileSizeHeight);

        return tileGridPosition;
    }

    public void PlaceItem(InventoryItem inventoryItem, int posX, int posY)
    {
        RectTransform rectTransform = inventoryItem.GetComponent<RectTransform>();
        rectTransform.parent = rectTransform;
        inventoryItemsSlot[posX,posY] = inventoryItem;

        Vector2 position = new Vector2();
        position.x = posX * gridSizeWidth + gridSizeWidth / 2;
        position.x = posY * gridSizeHeight + gridSizeHeight / 2;

        rectTransform.localPosition = position;
    }
}
