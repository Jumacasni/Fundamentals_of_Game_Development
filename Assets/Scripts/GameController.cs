using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    // Spawning platforms
    private float lengthGround;
    private Vector3 groundLastPosition;
    public GameObject groundPlatform;
    public GameObject waveBackground;
    private Vector3 pickUpLastPosition;
    private Vector3 waveLastPosition;
    private float lengthWave;
    private Vector3 hurdleLastPosition;
    public GameObject[] hurdle;
    public GameObject enemyObject;
    private float lastLengthEnemy;
    private Vector3 enemyLastPosition;
    public GameObject invincibilityPower;
    private float lastLengthInvincibility;
    private Vector3 invincibilityLastPosition;
    public GameObject scoreMultiplierPower;
    private float lastLengthScoreMultiplier;
    private Vector3 scoreMultiplierLastPosition;
    private bool restart;
    private AudioSource audioSource;
    public GameObject groundIslandObject;
    private Vector3 groundIslandLastPosition;
    private float lengthGroundIsland;
    public GameObject grassObject;
    private Vector3 grassLastPosition;
    public GameObject player;
    private float lengthHurdle;

    // Text
    public Text countText;
    private int count;
    private int points;
    public Text gameOver;
    public Text tryAgain;

    void Start () {
        points = 0;
        audioSource = GetComponent<AudioSource>();
        groundLastPosition = new Vector3(0.0f, 0.0f, 0.0f);
        groundIslandLastPosition = new Vector3(0.0f, 0.0f, 0.0f);
        grassLastPosition = new Vector3(0.0f, 0.0f, 0.0f);
        waveLastPosition = new Vector3(0.0f, 0.0f, 0.0f);
        enemyLastPosition = new Vector3(0.0f, 0.0f, 0.0f);
        invincibilityLastPosition = new Vector3(0.0f, 0.0f, 0.0f);
        scoreMultiplierLastPosition = new Vector3(0.0f, 0.0f, 0.0f);
        lastLengthInvincibility = 50f;
        lastLengthScoreMultiplier = 50f;
        lengthGroundIsland = 6f;
        lastLengthEnemy = 10f;
        lengthGround = GameObject.Find("Ground").GetComponent<MeshRenderer>().bounds.size.z;
        lengthHurdle = lengthGround;
        hurdleLastPosition = new Vector3(0.0f, 0.0f, -lengthGround / 2);
        lengthWave = GameObject.Find("Wave").GetComponent<MeshRenderer>().bounds.size.z - 10;

       for (int i = 0; i < 15; i++)
        {
            SpawningGround();
        }

        InvokeRepeating("SpawningWave", 0f, 20f);
        count = 0;
        InvokeRepeating("SpawningEnemy", 0f, 5f);
        InvokeRepeating("SpawningInvincibility", 0f, 5f);
        InvokeRepeating("SpawningScoreMultiplier", 0f, 5f);
        InvokeRepeating("SetCountText", 0f, 0.05f);
        gameOver.text = "";
        tryAgain.text = "";
        restart = false;
    }
	
	void Update () {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

		if(points >= 400)
        {
            lengthHurdle += 0.5f;
            player.GetComponent<PlayerController>().AddSpeed();
            points = 0;
        }

        if(player.transform.position.z + 50f > groundLastPosition.z)
        {
            SpawningGround();
        }

        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                Application.LoadLevel(Application.loadedLevel);
            }
        }
    }

    private void SpawningGround()
    {
        GameObject ground;

        ground = Instantiate(groundPlatform) as GameObject;
        SpawningHurdle();

        ground.transform.position = groundLastPosition + new Vector3(0f, 0f, lengthGround);
        groundLastPosition = ground.transform.position;

        SpawningGroundIsland();
        SpawningGrass();
        SpawningGroundIsland();
        SpawningGrass();
        SpawningGroundIsland();
        SpawningGrass();
    }

    private void SpawningGroundIsland()
    {
        GameObject groundIsland;

        groundIsland = Instantiate(groundIslandObject) as GameObject;
        groundIsland.transform.position = groundIslandLastPosition + new Vector3(3.5f, 0f, lengthGroundIsland);

        groundIsland = Instantiate(groundIslandObject) as GameObject;
        groundIsland.transform.position = groundIslandLastPosition + new Vector3(-3.5f, 0f, lengthGroundIsland);

        groundIslandLastPosition = new Vector3(0f, 0f, groundIsland.transform.position.z);
    }

    private void SpawningGrass()
    {
        GameObject grass;

        float randomNumber = Random.Range(player.transform.position.z + 20f, player.transform.position.z + 100f);

        grass = Instantiate(grassObject) as GameObject;
        grass.transform.position = new Vector3(-1.2f, 0.1f, randomNumber);

        randomNumber = Random.Range(player.transform.position.z + 20f, player.transform.position.z + 100f);

        grass = Instantiate(grassObject) as GameObject;
        grass.transform.position = new Vector3(0f, 0.1f, randomNumber);

        randomNumber = Random.Range(player.transform.position.z + 20f, player.transform.position.z + 100f);

        grass = Instantiate(grassObject) as GameObject;
        grass.transform.position = new Vector3(1.2f, 0.1f, randomNumber);

    }

    private void SpawningHurdle()
    {
        GameObject randomHurdle;
        int randomIndex = Random.Range(0, hurdle.Length);

        randomHurdle = Instantiate(hurdle[randomIndex]) as GameObject;

        randomHurdle.transform.position = randomHurdle.transform.position + hurdleLastPosition + new Vector3(0f, 0f, lengthHurdle);
        hurdleLastPosition += new Vector3(0f, 0f, lengthHurdle);
    }

    private void SpawningEnemy()
    {
        GameObject enemy;

        enemy = Instantiate(enemyObject) as GameObject;

        float lengthEnemy = Random.Range(lastLengthEnemy, lastLengthEnemy + 50f);
        enemy.transform.position = enemyLastPosition + new Vector3(0f, 0f, lengthEnemy);
        enemyLastPosition = enemy.transform.position;
        lastLengthEnemy = lengthEnemy;
    }

    private void SpawningInvincibility()
    {
        GameObject invincibility;

        invincibility = Instantiate(invincibilityPower) as GameObject;

        float lengthInvincibility = Random.Range(lastLengthEnemy, lastLengthEnemy + 100f);
        invincibility.transform.position = invincibilityLastPosition + new Vector3(0f, 0f, lengthInvincibility);
        invincibilityLastPosition = invincibility.transform.position;
        int intRandomPosition = Random.Range(-1, 1);
        float randomPosition = intRandomPosition;
        invincibility.transform.position = new Vector3(randomPosition, 0f, invincibility.transform.position.z);
        lastLengthInvincibility = lengthInvincibility;
    }

    private void SpawningScoreMultiplier()
    {
        GameObject scoreMultiplier;

        scoreMultiplier = Instantiate(scoreMultiplierPower) as GameObject;

        float lengthScoreMultiplier = Random.Range(lastLengthScoreMultiplier, lastLengthScoreMultiplier + 100f);
        scoreMultiplier.transform.position = scoreMultiplierLastPosition + new Vector3(0f, 0f, lengthScoreMultiplier);
        scoreMultiplierLastPosition = scoreMultiplier.transform.position;
        int intRandomPosition = Random.Range(-1, 1);
        float randomPosition = intRandomPosition;
        scoreMultiplier.transform.position = new Vector3(randomPosition, 0f, scoreMultiplier.transform.position.z);
        lastLengthScoreMultiplier = lengthScoreMultiplier;
    }

    private void SpawningWave()
    {
        GameObject wave = Instantiate(waveBackground) as GameObject;
        wave.transform.position = waveLastPosition + new Vector3(0f, -5f, lengthWave);
        waveLastPosition = wave.transform.position;
    }

    public void AddScore(int n)
    {
        count += n;
        SetCountText();
    }

    public int GetPoints()
    {
        return points;
    }

    public int GetScore()
    {
        return count;
    }

    private void SetCountText()
    {
        countText.text = "Score: " + count.ToString();
        count += 1;
        points += 1;
    }

    public void GameOver()
    {
        audioSource.Stop();
        gameOver.text = "GAME OVER!!";
        tryAgain.text = "Press 'k' to try again";
        restart = true;
        CancelInvoke();
    }
}
