using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Item to buy in shop
// Path Create> Shop> Item
[CreateAssetMenu(menuName = "Shop/Item")]
public class ItemSkill : ScriptableObject
{
    public int idSkill;

    [Space]
    public int priceSkill;
    public string nameSkill;
    public string descriptionSkill;

    public Sprite iconSkill;
}
