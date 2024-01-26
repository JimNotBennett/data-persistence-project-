using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text HighScoreText;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;

    [SerializeField] private string m_playerName = "";
    [SerializeField] private int m_HighScorePoints = 0;

    private bool m_GameOver = false;
    private bool reset;

    
    
    void Start()
    {
        m_Points = 0;
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }

        
            m_HighScorePoints = GameManager.Instance.highScore;
        HighScoreText.text = "High Score : " + GameManager.Instance.playerName + " : " + GameManager.Instance.highScore;

    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    void HighScore()
    {
        if (!reset)
        {
            if (m_Points > m_HighScorePoints)
            {
                m_HighScorePoints = m_Points;
                m_playerName = GameManager.Instance.currentName;
                HighScoreText.text = "Best Score : " + m_playerName + " : " + m_HighScorePoints;
                GameManager.Instance.playerName = m_playerName;
                GameManager.Instance.highScore = m_HighScorePoints;

                GameManager.Instance.SaveHighScore();
            }
        }
        else
        {
            m_HighScorePoints = 0;
            m_playerName = GameManager.Instance.currentName;
            HighScoreText.text = "Best Score : " + m_playerName + " : " + m_HighScorePoints;
            GameManager.Instance.playerName = m_playerName;
            GameManager.Instance.highScore = m_HighScorePoints;

            GameManager.Instance.SaveHighScore();
        }
    }
        public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
    }
}
