using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    public ItemSlot[] _slots;
    public GameObject _inventoryWindow;
    public Transform _slotPanel;
    public Transform _dropPosition;

    [Header("Select Item")]
    public TextMeshProUGUI _selectedItemName;
    public TextMeshProUGUI _selectedItemDescription;
    public TextMeshProUGUI _selectedStatName;
    public TextMeshProUGUI _selectedStatValue;
    public GameObject _useBtn;
    public GameObject _equipBtn;
    public GameObject _unEquipBtn;
    public GameObject _dropBtn;

    private PlayerController _controller;
    private PlayerCondition _condition;

    private ItemData _selectedItem;
    private int _selectedItemIdx;

    int _curEquipIdx;

    // Start is called before the first frame update
    void Start()
    {
        _controller = CharacterManager.Instance.Player.Controller;
        _condition = CharacterManager.Instance.Player.Condition;
        _dropPosition = CharacterManager.Instance.Player._dropPosition;

        _controller.Inventory += Toggle;
        CharacterManager.Instance.Player.AddItem += AddItem;

        _inventoryWindow.SetActive(false);
        _slots = new ItemSlot[_slotPanel.childCount];

        for (int i = 0; i < _slots.Length; ++i)
        {
            _slots[i] = _slotPanel.GetChild(i).GetComponent<ItemSlot>();
            _slots[i]._index = i;
            _slots[i]._inventory = this;
        }

        ClearSelectedItemWindow();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void ClearSelectedItemWindow()
    {
        _selectedItemName.text = string.Empty;
        _selectedItemDescription.text = string.Empty;
        _selectedStatName.text = string.Empty;
        _selectedStatValue.text = string.Empty;

        _useBtn.SetActive(false);
        _equipBtn.SetActive(false);
        _unEquipBtn.SetActive(false);
        _dropBtn.SetActive(false);
    }

    public void Toggle()
    {
        if (IsOpen())
        {
            _inventoryWindow.SetActive(false);
        }
        else
        {
            _inventoryWindow.SetActive(true);
        }
    }

    public bool IsOpen()
    {
        return _inventoryWindow.activeInHierarchy;
    }

    void AddItem()
    {
        ItemData data = CharacterManager.Instance.Player._itemData;

        // �������� �ߺ� �������� canStack
        if (data._canStack)
        {
            ItemSlot slot = GetItemStack(data);
            if (slot != null)
            {
                slot._quantity++;
                UpdateUI();
                CharacterManager.Instance.Player._itemData = null;
                return;
            }
        }

        // ����ִ� ���� �����´�
        ItemSlot emptySlot = GetEmptySlot();

        // �ִٸ�
        if (emptySlot != null)
        {
            emptySlot._item = data;
            emptySlot._quantity = 1;
            UpdateUI();
            CharacterManager.Instance.Player._itemData = null;
            return;
        }

        // ���ٸ�
        ThrowItem(data);
        CharacterManager.Instance.Player._itemData = null;
    }

    void UpdateUI()
    {
        for (int i = 0; i < _slots.Length; ++i)
        {
            if (_slots[i]._item != null)
                _slots[i].Setting();
            else
                _slots[i].Clear();
        }
    }

    ItemSlot GetItemStack(ItemData data)
    {
        for (int i = 0; i < _slots.Length; ++i)
        {
            if (_slots[i]._item == data &&
                _slots[i]._quantity < data._maxStackAmount)
            {
                return _slots[i];
            }
        }
        return null;
    }

    ItemSlot GetEmptySlot()
    {
        for (int i = 0; i < _slots.Length; ++i)
        {
            if (_slots[i]._item == null)
            {
                return _slots[i];
            }
        }
        return null;
    }

    void ThrowItem(ItemData data)
    {
        var item = Resources.Load<GameObject>($"Prefabs/{data._itemCode}");
        Instantiate(item, _dropPosition.position,
            Quaternion.Euler(Vector3.one * Random.value * 360));
    }

    public void SelectItem(int index)
    {
        if (_slots[index]._item == null) return;

        _selectedItem = _slots[index]._item;
        _selectedItemIdx = index;

        _selectedItemName.text = _selectedItem._displayName;
        _selectedItemDescription.text = _selectedItem._description;

        _selectedStatName.text = string.Empty;
        _selectedStatValue.text = string.Empty;

        for (int i = 0; i < _selectedItem._consumables.Length; ++i)
        {
            _selectedStatName.text += _selectedItem._consumables[i]._type.ToString() + "\n";
            _selectedStatValue.text += _selectedItem._consumables[i]._value.ToString() + "\n";
        }

        _useBtn.SetActive(_selectedItem._type == ItemType.Consumable);
        _equipBtn.SetActive(_selectedItem._type == ItemType.Equipable && !_slots[index]._equipped);
        _unEquipBtn.SetActive(_selectedItem._type == ItemType.Equipable && _slots[index]._equipped);
        _dropBtn.SetActive(true);
    }

    public void OnUseButton()
    {
        if (_selectedItem._type == ItemType.Consumable)
        {
            for (int i = 0; i < _selectedItem._consumables.Length; ++i)
            {
                switch (_selectedItem._consumables[i]._type)
                {
                    case ConsumableType.Health:
                        _condition.Heal(_selectedItem._consumables[i]._value);
                        break;
                    case ConsumableType.MagicPoint:
                        _condition.RecoveryMP(_selectedItem._consumables[i]._value);
                        break;
                }
            }
            RemoveSelectedItem();
        }
    }

    public void OnDropButton()
    {
        ThrowItem(_selectedItem);
        RemoveSelectedItem();
    }

    private void RemoveSelectedItem()
    {
        _slots[_selectedItemIdx]._quantity--;

        if (_slots[_selectedItemIdx]._quantity <= 0)
        {
            _selectedItem = null;
            _slots[_selectedItemIdx]._item = null;
            _selectedItemIdx = -1;
            ClearSelectedItemWindow();
        }

        UpdateUI();
    }

    public void OnEquipButton()
    {
        if (_slots[_curEquipIdx]._equipped)
        {
            UnEquip(_curEquipIdx);
        }

        _slots[_selectedItemIdx]._equipped = true;
        _curEquipIdx = _selectedItemIdx;
        CharacterManager.Instance.Player._equipment.EquipNew(_selectedItem);
        UpdateUI();

        SelectItem(_selectedItemIdx);
    }

    public void UnEquipButton()
    {
        UnEquip(_selectedItemIdx);
    }

    private void UnEquip(int index)
    {
        _slots[index]._equipped = false;
        CharacterManager.Instance.Player._equipment.UnEquip();
        UpdateUI();

        if (_selectedItemIdx == index)
        {
            SelectItem(_selectedItemIdx);
        }
    }
}
