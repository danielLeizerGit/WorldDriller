using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameStatus
    {
    play,win
    }

public class GameManager : MonoBehaviour
{
    //Game elemnts
    private GameStatus gameStatus = GameStatus.play;
    public int score = 0;
    public int money = 0;
    public int difScore = 0;
    public int planetNum = 0;
    public int multiplayerScore = 1;
    private Vector3 origonalSize;
    float size = 1;
    public Dictionary<int, int> enemyList = new Dictionary<int, int>(); // key is int for each enemy . value is the amount of this enemy in the game
    [SerializeField] public List<GameObject> enemiesPrefabs;
    [SerializeField] public List<Sprite> planets;
    [SerializeField] public GameObject planet;
    private bool sinkPlanet = false;
    public int firstGame=1;

    //UI
    private bool menuOpen = false;
    [SerializeField] GameObject staticsPanel;
    [SerializeField] public Text scoreText;
    [SerializeField] public Text moneyText;
    [SerializeField] public Text amountOfEnemies;
    [SerializeField] public Text planetNumText;

    
    void Start()
    {

        StartCoroutine("start"); // delay the start function



        StartCoroutine("enemiesPoints");
       
    }

    // Update is called once per frame
    void Update()
    {
        Cliker();
        PointSystem();
        MoneySystem();
    }

    private void Cliker()
    {
        if (Input.GetMouseButtonDown(0))
        {
          
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
         
            if (Physics.Raycast(ray, out hit)) // need normal box collider (not 2d)!
            {
                if (hit.collider.gameObject.tag == "Earth")
                {
                    score += 1*multiplayerScore;
              
                }
              
                scoreText.text = "Score: " + score;
            }
        }
    }
        private void PointSystem()
        {
        scoreText.text = "Score: " + score;

        float sinkFloat= 1 - score/((planetNum + 1) * Mathf.Pow(10, planetNum + 1));
        if (sinkFloat <= 0.5f)
            sinkFloat = 0.5f;
        if(size!= sinkFloat)
        {
            planet.transform.localScale = new Vector3(sinkFloat, sinkFloat, sinkFloat);
            size = sinkFloat;
        }
       

            if ((planetNum+1) *Mathf.Pow(10,planetNum+1) <= score)
            SwitchPlanet();
        }
        private void SwitchPlanet()
        {
        if (planets.Count > planetNum)
        {
            size = 1f;
            planet.transform.localScale = origonalSize;
            sinkPlanet = false;
            planetNum++;
            planet.GetComponent<SpriteRenderer>().sprite = planets[planetNum];
            planetNumText.text = "Planet: " + (planetNum +1);
        }
        else
            gameStatus = GameStatus.win;
           
        }

        IEnumerator enemiesPoints()
        {
            for(int i=0;i<enemyList.Count;i++)
            {
               score += enemyList[i] * (int)Mathf.Pow(10, i);
        }
         yield return new WaitForSeconds(1f);
         StartCoroutine("enemiesPoints");
        }

    IEnumerator start()
    {
        yield return new WaitForSeconds(1f);
        this.gameObject.GetComponent<SaveScript>().TestLoadFirst();
        origonalSize = planet.transform.localScale;
        planet.GetComponent<SpriteRenderer>().sprite = planets[planetNum];
        //if (firstGame == 1)
        //{
        //    for (int i = 0; i < 10; i++)
        //        enemyList.Add(i, 0);
        //    firstGame = 2;
        //    Debug.Log("s2");
        //}
        //else
        //{
        //    this.gameObject.GetComponent<SaveScript>().TestLoadFirst();
        //    Debug.Log("s3");
        //}


      
      
    }
    private void MoneySystem()
        {
            if(difScore+5<=score)
            {
            int moneyToGive = (score - difScore) / 5;
                difScore = score;
                money += moneyToGive;
            }
         moneyText.text = "Money: " + money;
        }
    public void TextChanger()
    {
        amountOfEnemies.text = "Laser: " + enemyList[0]  + "\n" + "Drillers: " + enemyList[1] + "\n" + "Energy remover: "+ enemyList[2];
    }
    //btn methods
    public void BuyLaser()
    {
        if (money >= 10)
        {
            money -= 10;
            enemyList[0] += 1;
            TextChanger();
        }
    }
    public void BuyDriller()
    {
        if (money >= 100)
        {
            money -= 100;
            enemyList[1] += 1;
            TextChanger();
        }
    }
    public void BuyEnergyRemover()
    {
        if (money >= 500)
        {
            money -= 500;
            enemyList[2] += 1;
            TextChanger();
        }
    }

    public void StaticsPanelBtn()
    {
        staticsPanel.SetActive(!menuOpen);
        menuOpen = !menuOpen;
    }

   
}


