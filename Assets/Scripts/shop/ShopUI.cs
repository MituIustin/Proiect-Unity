using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    bool _select;
    BaseItem _selectedItem;
    HealhPotion _heal;
    StrengthBoost _damage;
    SpeedBoots _speed;
    int _price;
    int _playerMoney;

    public List<BaseItem> _items;

    Button _buyButton;
    TextMeshProUGUI _priceText;
    TextMeshProUGUI _numberOfKnives;
    TextMeshProUGUI _numberOfBombs;
    PlayerCombat _playerCombat;


    void Start()
    {
        _playerMoney = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombat>().GetCoins();
        _playerCombat = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombat>();
        _price = 0;
        _buyButton = GameObject.FindGameObjectWithTag("buyButton").GetComponent<Button>();
        _priceText = GameObject.FindGameObjectWithTag("cost").GetComponent<TextMeshProUGUI>();
        _buyButton.interactable = false;
        _numberOfKnives = GameObject.FindGameObjectWithTag("numberOfKnives").GetComponent<TextMeshProUGUI>();
        _numberOfBombs = GameObject.FindGameObjectWithTag("numberOfBombs").GetComponent<TextMeshProUGUI>();

    }


    void Update()
    {
        _priceText.text = _price.ToString();
        _numberOfKnives.text = _playerCombat.GetKnives().ToString();
        _numberOfBombs.text = _playerCombat.GetBombs().ToString();

    }

    public void ShopClose()
    {
        GameObject.FindGameObjectWithTag("Shop").GetComponent<Shop>().CloseShop();
        Destroy(gameObject);
    }

    void SelectItemFromVector(BaseItem item)
    {
        if (item.GetPrice() <= _playerMoney && !item.AlreadyHasThisBoost())
        {
            _selectedItem = item;
            _select = true;
            _price = item.GetPrice();
            _buyButton.interactable = true;
        }
    }

    public void BuyItem()
    {
        _selectedItem.UseEffect();
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombat>().SpendCoins(_price);
        _playerMoney -= _price;
        _price = 0;
        _select = false;
        _selectedItem = null;
        _buyButton.interactable = false;
    }

    public void SelectItem(int item)
    {
        SelectItemFromVector(_items[item]);
        
    }
}
