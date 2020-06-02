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



    private void PurchasePotion(int itemNumber){
        int itemPrice =  int.Parse(potionItemList[itemNumber].itemPrice.text);
        string itemQuantityText = potionItemList[itemNumber].itemQuantity.text;
        itemQuantityText = itemQuantityText.Replace("x", "").Trim();
        int itemQuantity =  int.Parse(itemQuantityText);

        GameStateManager.instance.totalCoins = GameStateManager.instance.totalCoins - itemPrice;
        GameStateManager.instance.potionCount = GameStateManager.instance.potionCount + itemQuantity;

        GameStateManager.instance.SaveData();
        MainmenuController.instance.DisplayGameState();
    }

    public void PurchasePotionItemOne()
    {
        PurchasePotion(0);
    }

    public void PurchasePotionItemTwo()
    {
        PurchasePotion(1);
    }

    public void PurchasePotionItemThree()
    {
        PurchasePotion(2);
    }
}



