using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] public static int soulsCount = 0;
    [SerializeField] int soulsTotal = 0;
    [SerializeField] TextMeshProUGUI soulsText;
    [SerializeField] TextMeshProUGUI bankText;
    [SerializeField] TextMeshProUGUI timerText;
    public float timeValue = 300f;

    void Awake()
    {
        //int numGameSessions = FindObjectsOfType<GameSession>().Length;
        //if (numGameSessions > 1)
        //{
        //    Destroy(gameObject);
        //}
        //else
        //{
        //    DontDestroyOnLoad(gameObject);
        //}
    }
    void Start()
    {
        soulsText.text = soulsCount.ToString();
        bankText.text = soulsTotal.ToString();
    }

    public void AddToSouls(int soulsToAdd)
    {
        soulsCount += soulsToAdd;
        soulsText.text = soulsCount.ToString();
    }

    public void TakeSouls(int soulsToTake)
    {
        soulsCount -= soulsToTake;
        if(soulsCount <= 0)
        {
            soulsCount = 0;
        }
        soulsText.text = soulsCount.ToString();
    }

    public void BankSouls(int soulTotal)
    {
        soulsTotal += soulTotal;
        soulsCount -= soulsCount;
        bankText.text = soulsTotal.ToString();
        soulsText.text = soulsCount.ToString();
    }

    void Update()
    {
        if (timeValue >0)
        {
            timeValue -= Time.deltaTime;
        }
        else
        {
            timeValue = 0;
        }

        DisplayTime(timeValue);

    }

    void DisplayTime(float timeToDisplay)
    {
        if(timeToDisplay <= 0)
        {
            timeToDisplay = 0;
            SceneManager.LoadScene(2);
        }

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
