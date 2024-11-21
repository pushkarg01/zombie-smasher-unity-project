using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class GameplayController : MonoBehaviour
{
    public static GameplayController instance;

    public GameObject[] obstaclePrefabs;
    public GameObject[] zombiePrefabs;

    public Transform[] lanes;

    public float minDelay = 10f, maxDelay = 40f;

    private float halfGroundSize;
    private BaseController playerController;

    private TMP_Text scoreText;
    private int zombieKillCount;

    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject gameoverPanel;
    [SerializeField] private TMP_Text finalText;

     void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        halfGroundSize = GameObject.Find("GroundBlock Main").GetComponent<GroundScript>().halfLength;
        playerController= GameObject.FindGameObjectWithTag("Player").GetComponent<BaseController>();

        StartCoroutine("GenerateObs");

        scoreText=GameObject.Find("Score").GetComponent<TextMeshProUGUI>();
    }

    IEnumerator GenerateObs()
    {
        float timer =Random.Range(minDelay, maxDelay) /playerController.speed.z;
        yield return new WaitForSeconds(timer);

        CreateObs(playerController.gameObject.transform.position.z + halfGroundSize);
        StartCoroutine("GenerateObs");
    }

    void CreateObs(float zPos)
    {
        int r =Random.Range(0,10);

        if(r>=0 && r < 7)
        {
            int obsLane = Random.Range(0,lanes.Length);

            AddObs(new Vector3(lanes[obsLane].transform.position.x,0f,zPos),Random.Range(0,obstaclePrefabs.Length));
          
            int zombieLane = 0;

            if (obsLane == 0)
            {
                zombieLane = Random.Range(0, 2) == 1 ? 1 : 2; 
            }
            else if (obsLane == 1)
            {
                zombieLane = Random.Range(0, 2) == 1 ? 0 : 2;
            }
            else if(obsLane == 2)
            {
                zombieLane = Random.Range(0, 2) == 1 ? 1 : 0;
            }

            AddZombies(new Vector3(lanes[zombieLane].transform.position.x,0.15f,zPos));
        }
    }

    void AddObs(Vector3 pos ,int type)
    {
        GameObject obs = Instantiate(obstaclePrefabs[type],pos,Quaternion.identity,transform);
        bool mirror =Random.Range(0,2) == 1;

        switch (type)
        {
            case 0:
                obs.transform.rotation = Quaternion.Euler(0f, mirror ? -20 : 20, 0f);
                break;
            case 1:
                obs.transform.rotation = Quaternion.Euler(0f, mirror ? -20 : 20, 0f);
                break;
            case 2:
                obs.transform.rotation = Quaternion.Euler(0f, mirror ? -1 : 1, 0f);
                break;
            case 3:
                obs.transform.rotation = Quaternion.Euler(0f, mirror ? -170 : 170, 0f);
                break;
        }
        obs.transform.position = pos;
    }

    void AddZombies(Vector3 pos)
    {
        int count =Random.Range(0,3) +1;

        for (int i = 0; i < count; i++) {
            Vector3 shift = new Vector3(Random.Range(-0.5f, 0.5f), 0f,Random.Range(1f,10f) * i);
            Instantiate(zombiePrefabs[Random.Range(0,zombiePrefabs.Length)],pos+shift *i,Quaternion.identity,transform);
        }
    }

    public void IncreaseScore()
    {
        zombieKillCount++;
        scoreText.text =zombieKillCount.ToString();
    }

    public void PauseGame()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ExitGame()
    {
       Time.timeScale = 1f;
       SceneManager.LoadScene(0);
    }

    public void GameoverGame()
    {
        Time.timeScale = 0f;
        gameoverPanel.SetActive(true);
        finalText.text = "Killed: " + zombieKillCount;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Game");
    }
}

