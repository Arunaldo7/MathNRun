using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class PurchaseController : MonoBehaviour
{

    private const string coinBundle1 = "com.patheticlabs.mathrun.coinbundle1";
    private const string coinBundle2 = "com.patheticlabs.mathrun.coinbundle2";
    private const string coinBundle3 = "com.patheticlabs.mathrun.coinbundle3";

    public void OnPurchaseComplete1(Product product)
    {
        int addCoinCount = 0;

        if (product.definition.id == coinBundle1)
        {
            addCoinCount = 3000;

            Debug.Log("Total Coins : " + GameStateManager.instance.totalCoins);
            Debug.Log("Add Coins : " + addCoinCount);

            GameStateManager.instance.totalCoins = GameStateManager.instance.totalCoins + addCoinCount;

            GameStateManager.instance.SaveData();
            MainmenuController.instance.DisplayGameState();
        }
    }

    public void OnPurchaseComplete2(Product product)
    {
        int addCoinCount = 0;

        if (product.definition.id == coinBundle2)
        {
            addCoinCount = 8000;

            Debug.Log("Total Coins : " + GameStateManager.instance.totalCoins);
            Debug.Log("Add Coins : " + addCoinCount);

            GameStateManager.instance.totalCoins = GameStateManager.instance.totalCoins + addCoinCount;

            GameStateManager.instance.SaveData();
            MainmenuController.instance.DisplayGameState();
        }
    }

    public void OnPurchaseComplete3(Product product)
    {
        int addCoinCount = 0;

        if (product.definition.id == coinBundle3)
        {
            addCoinCount = 20000;

            Debug.Log("Total Coins : " + GameStateManager.instance.totalCoins);
            Debug.Log("Add Coins : " + addCoinCount);

            GameStateManager.instance.totalCoins = GameStateManager.instance.totalCoins + addCoinCount;

            GameStateManager.instance.SaveData();
            MainmenuController.instance.DisplayGameState();
        }
    }



    public void OnPurchaseFailure(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log("Purchase of Product - " + product.definition.id + " failed due to " + failureReason);
    }
}
