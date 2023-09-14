using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrameSkill : MonoBehaviour
{
    public ItemSkill CurrentSkill { private set; get; }

    [SerializeField] Image spriteSkill;
    [SerializeField] Text textSkill;
    [SerializeField] Text textPrice;

    public void SetupSkill(ItemSkill skill)
    {
        CurrentSkill = skill;

        if(textPrice != null)
            textPrice.text = "Price: " + skill.priceSkill.ToString();

        spriteSkill.sprite = skill.iconSkill;
        spriteSkill.color = new Color(255, 255, 255, 255);
        textSkill.text = skill.nameSkill;
    }
    public void ButSkill()
    {
        BuySkill.instnace.ShowBuyWindow(CurrentSkill.idSkill);
    }
    public void UseSkill()
    {
        GameManager.Instance.OnUseSkill(CurrentSkill.idSkill);
        Destroy(gameObject);
    }
}
