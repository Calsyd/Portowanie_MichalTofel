using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public static Shop instance;
    public InventoryShop CurrentInventory;

    private void Awake()
    {
        instance = this;
    }
    public bool TryBuySkill(int id)
    {
        if (CurrentInventory.AllSkills.Count > 3)
            return false;
        if (ShopManager.instance.Coin < GetSkillByID(id).priceSkill)
            return false;

        BuySkill(id);
        ShopManager.instance.DeleteCoin(GetSkillByID(id).priceSkill);
        return true;
    }
    private void BuySkill(int id) => CurrentInventory.AllSkills.Add(GetSkillByID(id));

    public static ItemSkill GetSkillByID(int id)
    {
        foreach (ItemSkill item in Resources.LoadAll<ItemSkill>("Skills"))
            if (item.idSkill == id)
                return item;

        return null;
    }
    public static ItemSkill GetSkillByName(string name)
    {
        foreach (ItemSkill item in Resources.LoadAll<ItemSkill>("Skills"))
            if (item.nameSkill == name)
                return item;

        return null;
    }
}
