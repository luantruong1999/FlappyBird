using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private GameObject pipe;
    [SerializeField] private Transform spawnPos;
    [SerializeField] private float spawnTime;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Image OverPannel;
    [SerializeField] private TextMeshProUGUI ScoreOverText;
    [SerializeField] private TextMeshProUGUI BestText;
    [SerializeField] private Button ResetBtn;
    [SerializeField] private Image Logo;
    
    private int score=0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void StartGame()
    {
        InvokeRepeating("SpawnPipe",3f,spawnTime);
        scoreText.gameObject.SetActive(true);
        Logo.gameObject.SetActive(false);
    }

    private void SpawnPipe()
    {
        Vector3 spawnPos = new Vector2(this.spawnPos.position.x, Random.Range(-1.5f, 3f));
        GameObject obj = Instantiate(pipe, spawnPos, quaternion.identity);
    }

    public void AddScore()
    {
        score++;
        UpdateScore(score);
    }

    private void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        ScoreOverText.text = score.ToString();
        int best = PlayerPrefs.GetInt("Best", 0);
        if (score > best)
        {
            PlayerPrefs.SetInt("Best",score);
            best = score;
        }
        BestText.text = best.ToString();
        OverPannel.gameObject.SetActive(true);
        ResetBtn.gameObject.SetActive(true);
        
        
    }

    public void OnResetBtn()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ActiveScoreText()
    {
        
    }
}
