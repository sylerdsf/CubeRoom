using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Sorumi.Util;

public enum StudioMode
{
    SelectItem,
    EditItem,
}
public class StudioPanel : MonoBehaviour
{

    private StudioMode mode;
    private DragItemCellView itemCellView;
    private Transform setView;
    private Transform editView;

    private Button resetButton;

    private Button placeButton;
    private Button deleteButton;
    private RotateButton rotateButton;

    private ItemPO[] itemCellViewData;

    #region delegate
    public Action<ItemPO, Vector2> OnItemBeginDrag
    {
        set
        {
            itemCellView.OnItemBeginDrag += (index, position) =>
            {
                value(itemCellViewData[index], position);
            };
        }
    }

    public Action<PointerEventData> OnResetClick
    {
        set
        {
            UIEventListener btnListener = resetButton.gameObject.AddComponent<UIEventListener>();

            btnListener.OnClick += value;
        }
    }
    public Action<PointerEventData> OnPlaceClick
    {
        set
        {
            UIEventListener btnListener = placeButton.gameObject.AddComponent<UIEventListener>();

            btnListener.OnClick += value;
        }
    }

    public Action<PointerEventData> OnDeleteClick
    {
        set
        {
            UIEventListener btnListener = deleteButton.gameObject.AddComponent<UIEventListener>();

            btnListener.OnClick += value;
        }
    }

    public Action<float> OnRotateChange
    {
        set
        {
            rotateButton.OnChange += value;
        }
    }

    #endregion
    public void Init()
    {
        itemCellView = transform.Find("DragItemScrollView").GetComponent<DragItemCellView>();
        itemCellView.DataSource = ItemCellViewDataSource;
        itemCellView.Init();

        setView = transform.Find("SetView");
        resetButton = setView.Find("ResetButton").GetComponent<Button>();
        editView = transform.Find("EditView");
        placeButton = editView.Find("PlaceButton").GetComponent<Button>();
        deleteButton = editView.Find("DeleteButton").GetComponent<Button>();
        rotateButton = editView.Find("RotateButton").GetComponent<RotateButton>();

        // TODO
        itemCellViewData = ItemData.GetAll();
        itemCellView.Refresh();

    }

    public ItemPO[] ItemCellViewDataSource()
    {
        return itemCellViewData;
    }

    #region Controller API

    public void SetMode(StudioMode mode)
    {
        this.mode = mode;
        SetItemCellViewActive(mode == StudioMode.SelectItem);
        SetEditViewActive(mode == StudioMode.EditItem);
    }
    public void SetRotateButtonValue(float degree)
    {
        rotateButton.SetValue(degree);
    }
    public void SetRotateButtonRotation(float degree)
    {
        rotateButton.SetRotation(degree);
    }

    private void SetItemCellViewActive(bool isActive)
    {
        itemCellView.gameObject.SetActive(isActive);
    }

    private void SetEditViewActive(bool isActive)
    {
        editView.gameObject.SetActive(isActive);
    }
    #endregion
}
