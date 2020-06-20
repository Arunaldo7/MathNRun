using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
class ShopItem
{
    public Text itemPrice;
    public Text itemQuantity;
}
public class ShopController : MonoBehaviour
{
    public static ShopController instance;

    [SerializeField] private ShopItem[] potionItemList;
    [SerializeField] private ShopItem[] makeItEasyItemList;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }



    private void PurchasePotion(int itemNumber, string potionType)
    {
        int itemQuantity;
        if (itemNumber != -1)
        {
            int itemPrice = int.Parse(potionItemList[itemNumber].itemPrice.text);
            string itemQuantityText = potionItemList[itemNumber].itemQuantity.text;
            itemQuantityText = itemQuantityText.Replace("x", "").Trim();
            itemQuantity = int.Parse(itemQuantityText);
            GameStateManager.instance.totalCoins = GameStateManager.instance.totalCoins - itemPrice;
        }else{
            itemQuantity = 1;
        }

        if (potionType == "Life Potion")
        {
            GameStateManager.instance.potionCount = GameStateManager.instance.potionCount + itemQuantity;
        }
        else
        {
            GameStateManager.instance.magicPotionCount = GameStateManager.instance.magicPotionCount + itemQuantity;
        }

        GameStateManager.instance.SaveData();
        MainmenuController.instance.DisplayGameState();
    }

    public void PurchasePotionFreeItem()
    {
        PurchasePotion(-1, "Life Potion");
    }

    public void PurchasePotionItemOne()
    {
        PurchasePotion(0, "Life Potion");
    }

    public void PurchasePotionItemTwo()
    {
        PurchasePotion(1, "Life Potion");
    }

    public void PurchasePotionItemThree()
    {
        PurchasePotion(2, "Life Potion");
    }

    public void PurchaseMagicPotionFreeItem()
    {
        PurchasePotion(-1, "Magic Potion");
    }

    public void PurchaseMagicPotionItemOne()
    {
        PurchasePotion(0, "Magic Potion");
    }

    public void PurchaseMagicPotionItemTwo()
    {
        PurchasePotion(1, "Magic Potion");
    }

    public void PurchaseMagicPotionItemThree()
    {
        PurchasePotion(2, "Magic Potion");
    }
}



