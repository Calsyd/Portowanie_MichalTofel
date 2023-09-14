using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Grid))]
public class FrameShop : MonoBehaviour
{
    [Header("Details")]
    [Tooltip("Script must by with gird")]
    [SerializeField] ItemSkill[] addToFrame;
    [SerializeField] FrameSkill prefabFrameSkill;

    private void Awake()
    {
        AddSkills(addToFrame);
    }

    public void AddSkill(ItemSkill skill)
    {
        if(transform.GetComponentInChildren<FrameSkill>() != null)
            if(transform.GetComponentsInChildren<FrameSkill>().Length >= 4)
                Destroy(transform.GetComponentsInChildren<FrameSkill>()[3].gameObject);

        Instantiate(prefabFrameSkill, transform).SetupSkill(skill);
    }
    public void AddSkills(ItemSkill[] skills)
    {
        if(transform.GetComponentInChildren<FrameSkill>() != null)
        {
            FrameSkill[] allFrameChildren = transform.GetComponentsInChildren<FrameSkill>();
            for (int i = 0; i < allFrameChildren.Length; i++)
            {
                Destroy(allFrameChildren[i].gameObject);
            }
        }

        for (int i = 0; i < skills.Length; i++)
        {
            Instantiate(prefabFrameSkill, transform).SetupSkill(skills[i]);
        }
    }
}
