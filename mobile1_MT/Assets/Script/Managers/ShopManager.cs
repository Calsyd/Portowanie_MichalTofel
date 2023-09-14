using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager instance;
    public InventoryShop currentInventory;

    public int Coin { private set; get; }

    private void Awake()
    {
        instance = this;
    }

    public void AddCoin() => Coin++;
    public void DeleteCoin(int coin) => Coin -= coin;
    public void SetCoin(int coin) => Coin = coin;
}
