using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;

    private float gameTimer;

    [SerializeField]
    private int timeToStartFire;
    private bool fireStarted;

    [SerializeField]
    private Text topText;
    [SerializeField]
    private Text bottomText;
    [SerializeField]
    private Text seedsText;

    private GameObject startStem;

    public int seedsAmount = 20;
    private int plantAmount = 1;
    private int fireAmount = 0;
    private int seconds = 6;
    public bool gameActive = false;
    private bool gameOver = false;
    private bool gameStarted = false;
    public bool canPlant = false;
    private int gameState = 0;
    private bool addingSeeds = false;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        topText.text = "Press Space to Play";
        topText.color = Color.white;
        bottomText.text = "";
        seedsText.text = "";
    }

    private void StartGame()
    {
        startStem = Instantiate(Prefabs.stemObject, new Vector3(0, 0, 0), Quaternion.identity);
        startStem.GetComponent<Stem>().SetLifespan(Random.Range(5,7));
        gameActive = true;
        seconds = 0;

        topText.text = "Keep your plants alive!";
        topText.color = Color.white;
        bottomText.text = "Score: " + 0;
    }

    void Update()
    {
        seedsText.transform.position = Input.mousePosition + new Vector3(25f, -15f, 0f);

        CountSeconds();

        if (gameOver && seconds >= 8)
        {
            topText.text = "Press Space to restart";
            topText.color = Color.yellow;

            if (Input.GetButtonDown("Jump"))
            {
                SceneManager.LoadScene("GameScene");
            }
        }

        if (!gameActive && !gameOver)
        {
            if (!gameStarted)
            {
                if (Input.GetButtonDown("Jump"))
                {
                    gameStarted = true;
                }
                return;
            }
            if (seconds >= 5)
            {
                switch (gameState)
                {
                    case 0: topText.text = "Right click and drag to look around";
                        topText.color = Color.white;
                        break;
                    case 1: topText.text = "This number is your remaining seeds";
                        seedsText.text = "" + seedsAmount;
                        seedsText.color = Color.yellow;
                        topText.color = Color.yellow;
                        break;
                    case 2: topText.text = "You will gain more seeds over time";
                        addingSeeds = true;
                        seedsText.color = Color.white;
                        topText.color = Color.white;
                        break;
                    case 3: topText.text = "Left click to plant a seed";
                        topText.color = Color.yellow;
                        canPlant = true;
                        break;
                    case 4: StartGame(); break;
                    default: break;
                }
                seconds = 0;
                gameState++;
            }
            return;
        }

        if (!fireStarted)
        {
            bottomText.text = timeToStartFire - seconds + 1 + "";

            if (seconds >= timeToStartFire - 2)
            {
                bottomText.color = Color.red;
            }

            if (seconds > timeToStartFire)
            {
                StartFire();
            }
        }
    }

    private void GameEnd()
    {
        gameActive = false;
        gameOver = true;
        bottomText.text = "Final Score: " + seconds;
        topText.color = Color.red;
        seconds = 0;
    }

    private void CountSeconds()
    {
        gameTimer += Time.deltaTime;

        if (gameTimer >= 1)
        {
            gameTimer--;
            seconds++;

            if (gameActive)
            {
                bottomText.text = "Score: " + seconds;
            }

            if (addingSeeds)
            {
                ModifySeedAmount(1);
            }
        }
    }

    private void StartFire()
    {
        topText.text = "Keep the fire alive too!";
        topText.color = Color.yellow;
        bottomText.color = Color.white;
        fireStarted = true;
        seconds = 0;
        ModifyFireCount(1);
        GameObject fire = Instantiate(Prefabs.fireObject, new Vector3(0f, 0f, 0f), Quaternion.identity);
        fire.GetComponent<Fire>().stem = startStem.GetComponent<Stem>();
        startStem.GetComponent<Stem>().LightOnFire(fire);
        startStem.GetComponent<Stem>().burnEndTime = 15f;
    }

    public static void ModifySeedAmount(int amount)
    {
        _instance.seedsAmount += amount;
        _instance.seedsText.text = "" + _instance.seedsAmount;
    }

    public static void ModifyPlantCount(int amount)
    {
        _instance.plantAmount += amount;

        if (_instance.plantAmount <= 0)
        {
            _instance.topText.text = "You let the plants die!";
            _instance.GameEnd();
        }
    }

    public static void ModifyFireCount(int amount)
    {
        _instance.fireAmount += amount;

        if (_instance.fireAmount <= 0)
        {
            _instance.topText.text = "You let the fire die!";
            _instance.GameEnd();
        }
    }
}