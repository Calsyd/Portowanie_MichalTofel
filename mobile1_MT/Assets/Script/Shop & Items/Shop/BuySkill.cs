using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuySkill : MonoBehaviour
{
    public static BuySkill instnace;
    private ItemSkill skillToBuy;

    [SerializeField] GameObject windowSucessfull;
    [SerializeField] GameObject windowFailed;

    [Space]
    [SerializeField] Text textName;
    [SerializeField] Text textDesc;
    [SerializeField] Image imageIcon;

    private void Awake()
    {
        instnace = this;
    }

    public void ShowBuyWindow(int id)
    {
        CanvasGroup canv = GetComponent<CanvasGroup>();
        canv.interactable = true;
        canv.blocksRaycasts = true;
        canv.alpha = 1f;

        textName.text = Shop.GetSkillByID(id).nameSkill;
        textDesc.text = Shop.GetSkillByID(id).descriptionSkill;
        imageIcon.sprite = Shop.GetSkillByID(id).iconSkill;

        skillToBuy = Shop.GetSkillByID(id);
    }
    public void CloseBuyWindow()
    {
        CanvasGroup canv = GetComponent<CanvasGroup>();
        canv.interactable = false;
        canv.blocksRaycasts = false;
        canv.alpha = 0f;

        skillToBuy = null;
    }

    public void TryBuySkill()
    {
        if(Shop.instance.TryBuySkill(skillToBuy.idSkill))
        {
            windowSucessfull.SetActive(true);
        }else
        {
            windowFailed.SetActive(true);
        }

        CloseBuyWindow();
    }
}
