using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveScript : MonoBehaviour
{
    private int moneySave;
    private int scoreSave;
    private int difScoreSave;
    private int planetNumSave;
    private int multiplayerScoreSave=1;
    private Dictionary<int, int> enemyList;
    private int firstGameSave;
    public GameObject gm;
    // Start is called before the first frame update
    public void Awake()
    {
      // PlayerPrefs.DeleteAll(); //for testing save and load
        gm = GameObject.FindGameObjectWithTag("Player");
        LoadBtn();
    }

    public void Start()
    {
       StartCoroutine("SaveMethod");
    }
    public void SaveBtn()
    {
        moneySave = gm.GetComponent<GameManager>().money;
        scoreSave= gm.GetComponent<GameManager>().score;
        difScoreSave = gm.GetComponent<GameManager>().difScore;
        planetNumSave = gm.GetComponent<GameManager>().planetNum;
        multiplayerScoreSave = gm.GetComponent<GameManager>().multiplayerScore;
        enemyList = gm.GetComponent<GameManager>().enemyList;
        firstGameSave = gm.GetComponent<GameManager>().firstGame;

        PlayerPrefs.SetInt("Money", moneySave);
        PlayerPrefs.SetInt("Score", scoreSave);
        PlayerPrefs.SetInt("difScore", difScoreSave);
        PlayerPrefs.SetInt("planetNum", planetNumSave);
        PlayerPrefs.SetInt("Multi", multiplayerScoreSave);
        PlayerPrefs.SetInt("firstGame", firstGameSave);
       for (int i=0;i<10;i++)
        {
            PlayerPrefs.SetInt("enemy" + i, enemyList[i]);
        }
        Debug.Log("f " + firstGameSave);
    }
    public void LoadBtn()
    {
        gm.GetComponent<GameManager>().money = PlayerPrefs.GetInt("Money");
        gm.GetComponent<GameManager>().score = PlayerPrefs.GetInt("Score");
        gm.GetComponent<GameManager>().difScore = PlayerPrefs.GetInt("difScore");
        gm.GetComponent<GameManager>().planetNum = PlayerPrefs.GetInt("planetNum");
        //gm.GetComponent<GameManager>().multiplayerScore = PlayerPrefs.GetInt("Multi");
        gm.GetComponent<GameManager>().firstGame = PlayerPrefs.GetInt("firstGameSave");
        gm.GetComponent<GameManager>().planet.GetComponent<SpriteRenderer>().sprite = 
            gm.GetComponent<GameManager>().planets[gm.GetComponent<GameManager>().planetNum];
        gm.GetComponent<GameManager>().planetNumText.text = "Planet: " + (gm.GetComponent<GameManager>().planetNum + 1);

       // Debug.Log("end load");
    }

    public void TestLoadFirst()
    {
        for (int i = 0; i < 10; i++)
        {
            gm.GetComponent<GameManager>().enemyList[i] = PlayerPrefs.GetInt("enemy" + i);
        }
        gm.GetComponent<GameManager>().TextChanger();
        //Debug.Log("end load 2");
    }

    public IEnumerator SaveMethod()
    {
        yield return new WaitForSeconds(1f);
        this.gameObject.GetComponent<SaveScript>().SaveBtn();
        StartCoroutine("SaveMethod");
    }
}
