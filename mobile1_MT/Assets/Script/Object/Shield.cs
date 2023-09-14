using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Shield : MonoBehaviour
{
    public Type CurrentType
    {
        private set;
        get;
    }
    [SerializeField] Vector3 targetScale;
    [SerializeField] Sprite[] listShields;
    private bool isScaled = false;
    public int tapShield = 0; //ile razy kliknales w tarcze (od 3 do 0)

    private void Awake()
    {
        Invoke(nameof(begin), 0.5f);//delay
        SwitchType((Type)Random.Range(0, listShields.Length));
    }

    private void SwitchType(Shield.Type type)
    {
        switch (type)
        {
            case Type.woodenShield:
                GetComponent<SpriteRenderer>().sprite = listShields[0];
                tapShield = 1;
                break;
            case Type.silverShield:
                GetComponent<SpriteRenderer>().sprite = listShields[1];
                tapShield = 2;
                break;
            case Type.diamondShield:
                tapShield = 3;
                GetComponent<SpriteRenderer>().sprite = listShields[2];
                break;
        }
    }
    private void begin()
    {
        isScaled = true;    
    }
    public void Click()
    {
        if (tapShield > 1)
        {
            tapShield--;
            return;
        }

        FastClick();
    }
    public void FastClick()
    {
        GameManager.Instance.SetExplosion(transform.position);
        if (transform.localScale.x > 0.75f)//add 2 points
        {
            GameManager.Instance.AddPoint(2);
        }
        if (transform.localScale.x > 0.50f)//add 3 points
        {
            GameManager.Instance.AddPoint(1);
        }
        if (transform.localScale.x > 0.25f)//add 4 points
        {
            GameManager.Instance.AddPoint(2);
        }
        Destroy(gameObject);
        ShopManager.instance.AddCoin();
    }
    private void FixedUpdate()
    {
        if (isScaled)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, GameManager.Instance.CurrentLevelDetails.targetDelay * Time.deltaTime);
            if (transform.localScale.x <= targetScale.x + 0.1f)
                Destroy(gameObject);
        }
    }

    public enum Type
    {
        woodenShield,
        silverShield,
        diamondShield
    }
}
