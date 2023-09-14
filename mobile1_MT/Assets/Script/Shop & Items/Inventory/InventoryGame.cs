using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryGame : MonoBehaviour
{
    public static InventoryGame instance;

    public InventoryShop CurrentInventorySave;
    [SerializeField] private FrameSkill prefabFrameItem;

    private void Awake()
    {
        instance = this;
    }
    public void RefreshItem()
    {
        FrameSkill[] af = GetComponentsInChildren<FrameSkill>();
        for (int i = 0; i < af.Length; i++)
        {
            Destroy(af[i].gameObject);
        }

        for (int i = 0; i < CurrentInventorySave.AllSkills.Count; i++)
        {
            Instantiate(prefabFrameItem, transform).SetupSkill(CurrentInventorySave.AllSkills[i]);
        }

        Debug.Log("Spawn ");
    }
}
