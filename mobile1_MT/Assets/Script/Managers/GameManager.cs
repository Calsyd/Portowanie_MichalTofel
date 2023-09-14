using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int Points { private set; get; }
    public int PointsLevel { private set; get; }
    public LevelDetails CurrentLevelDetails
    {
        get { return levelDetails[CurrentLevel]; }
    }
    public int CurrentLevel { private set; get; }
    public bool CurrenUseSkill { private set; get; }
    private float currentTimer { set; get; }
    private ItemSkill currentItem { set; get; }
    private const string savePoints = "Save points";
    private const string saveLevel = "Save level";
    private const string saveCoin = "Save coin";

    [SerializeField] ShopManager shopManager;
    [SerializeField] LevelDetails[] levelDetails;
    [SerializeField] WaypointShield[] pointTarget;
    [SerializeField] GameObject targetObject;
    [SerializeField] GameObject particleExplosionEffects;
    [SerializeField] Text pointinLevel;
    [SerializeField] Text allPoint;
    [SerializeField] Text levelText;
    [SerializeField] Text pointText;
    [SerializeField] Text coinText;
    [SerializeField] Text powerText;
    private GameStatus gameStatus = new GameStatus();
    private float targetTimer;
    private float polingTimer = 0;
    private Vector2 awakePositionEffects;

    private bool oneTapShield = false;

    [SerializeField] UnityEvent inGameEvent;
    [SerializeField] UnityEvent inAfkEvent;
    [SerializeField] UnityEvent onWinEvent;

    private void Awake()
    {
        for (int i = 0; i < pointTarget.Length; i++)
        {
            pointTarget[i].GetComponent<SpriteRenderer>().enabled = false;
        }
        Load();
        Instance = this;
        CurrentLevel = 0;
        ChangeStatus(GameStatus.afk);
    }
    private void Update()
    {
        if (currentTimer != 0)
        {
            if (Time.time >= targetTimer)
            {
                //win
                Debug.Log("end time");
                currentTimer = 0;
                targetTimer = 0;
                ChangeStatus(GameStatus.win);
            }
        }
        if (CurrenUseSkill)
        {
            if (powerText.text != null)
            {
                powerText.text = currentItem.nameSkill;
            }
            if (oneTapShield)
            {
                Shield[] allS = FindObjectsOfType<Shield>();
                for (int i = 0; i < allS.Length; i++)
                {
                    allS[i].tapShield = 1;
                }
            }
        }
        else if (powerText != null)
            powerText.text = "";

        if (coinText != null)
            coinText.text = "Coin: " + ShopManager.instance.Coin;
        if (levelText != null)
            levelText.text = "Your Level: " + CurrentLevel;
        if (pointText != null)
            pointText.text = "Points: " + Points;
    }
    public void AddPoint(int _point)
    {
        Points += _point;
        PointsLevel += _point;
        Save();
    }
    public void AddCoin()
    {
        ShopManager.instance.AddCoin();
        Save();
    }
    public void AddLevel()
    {
        CurrentLevel++;
        Save();
    }
    public void NextLevel()
    {
        ChangeStatus(GameStatus.inGame);
    }
    public void Finish()
    {
        ChangeStatus(GameStatus.afk);
    }
    public void Save()
    {
        PlayerPrefs.SetInt(savePoints, Points);
        PlayerPrefs.SetInt(saveLevel, CurrentLevel);
        PlayerPrefs.SetInt(saveCoin, ShopManager.instance.Coin);
        PlayerPrefs.Save();
    }
    public void Load()
    {
        if (PlayerPrefs.HasKey(savePoints))
            Points = PlayerPrefs.GetInt(savePoints);

        if (PlayerPrefs.HasKey(saveLevel))
            CurrentLevel = PlayerPrefs.GetInt(saveLevel);

        if(PlayerPrefs.HasKey(saveCoin))
            shopManager.SetCoin(PlayerPrefs.GetInt(saveCoin));
    }
    public void ClearData()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Shop.instance.CurrentInventory.AllSkills.Clear();
    }
    public void ChangeStatus(GameStatus status)
    {
        gameStatus = status;
        switch (status)
        {
            case GameStatus.afk:
                inAfkEvent?.Invoke();
                PointsLevel = 0;
                break;
            case GameStatus.inGame:
                InvokeRepeating(nameof(CreateShield), 1f, CurrentLevelDetails.howfastCreate);
                currentTimer = CurrentLevelDetails.timeLevel;
                targetTimer = Time.time + CurrentLevelDetails.timeLevel;
                inGameEvent?.Invoke();
                InventoryGame.instance.RefreshItem();
                break;
            case GameStatus.win:
                CancelInvoke(nameof(CreateShield));
                Shield[] allTarget = FindObjectsOfType<Shield>();
                for (int i = 0; i < allTarget.Length; i++)
                {
                    Destroy(allTarget[i].gameObject);
                }
                onWinEvent?.Invoke();
                AddLevel();
                pointinLevel.text = "Point in level: " + PointsLevel;
                allPoint.text = "All points: " + Points;
                OnStopSkill();
                break;
        }
    }
    private WaypointShield GetRandomPosition()
    {
        foreach (WaypointShield target in pointTarget)
        {
            if (target.IsFree())
                return target;
        }
        return pointTarget[UnityEngine.Random.Range(0, pointTarget.Length)];
    }
    private void CreateShield()
    {
        StartCoroutine(SearchCreateWaypoint());
    }
    private IEnumerator SearchCreateWaypoint()
    {
        WaypointShield ws = GetRandomPosition();
      
        
        ws.ImputShield(Instantiate(targetObject, ws.transform.position, Quaternion.identity).GetComponent<Shield>());
        yield return null;
    }
    public void SetExplosion(Vector2 explosionPosition)
    {
        //polingEffects.transform.position = explosionPosition;
        //polingTimer = 2 + Time.time;
        GameObject effects = Instantiate(particleExplosionEffects, explosionPosition, Quaternion.identity);
        Destroy(effects, 5f);
    }

    public void OnUseSkill(int id)
    {
        if (CurrenUseSkill)
            return;

        CurrenUseSkill = true;
        currentItem = Shop.GetSkillByID(id);
        Shield[] allS = FindObjectsOfType<Shield>();
        if (id == 1)
        {
            // Use Item With ID 1

            for (int i = 0; i < allS.Length; i++)
            {
                allS[i].FastClick();
            }

            OnStopSkill();
        }
        if(id == 2)
        {
            // Use Item With ID 2

            StartCoroutine(ChangeTapShield(5));
        }
        if(id == 3)
        {
            StartCoroutine(ChangeTime(5f,0.25f));
        }
        if(id == 4)
        {
            StartCoroutine(ChangeTime(5f, 1.75f));
        }

        ShopManager.instance.currentInventory.AllSkills.Remove(Shop.GetSkillByID(id));
    }
    public void OnStopSkill()
    {
        if (!CurrenUseSkill)
            return;

        currentItem = null;
        CurrenUseSkill = false;
        oneTapShield = false;
        Time.timeScale = 1f;
    }

    public IEnumerator ChangeTime(float secnods, float timeS)
    {
        Time.timeScale = timeS;
        yield return new WaitForSeconds(secnods);
        Time.timeScale = 1f;
        OnStopSkill();
    }
    public IEnumerator ChangeTapShield(float seconds)
    {
        oneTapShield = true;
        yield return new WaitForSeconds(seconds);
        //oneTapShield = false;
    }

    public void ExitAppliaction()
        => Application.Quit();
}
[Serializable]
public class LevelDetails
{
    public float timeLevel;
    public float targetDelay;
    public float howfastCreate;
}
public enum GameStatus
{
    afk,
    inGame,
    win
}
