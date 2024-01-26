using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuUIHandeler : MonoBehaviour
{

    [Header("Player Name")]
    [SerializeField] private string playerName = "";
    public TMP_InputField inputField;

    [Header("High Score")]
    public TextMeshProUGUI highScoreText;

    void Start()
    {
        inputField = GameObject.Find("PlayerNameInputField").GetComponent<TMP_InputField>();
        GameManager.Instance.LoadHighScore();
        highScoreText.text = "High Score : " + GameManager.Instance.playerName + " : " + GameManager.Instance.highScore;
    }

    public void StartNew()
    {
        SceneManager.LoadScene(1);
    }

    public void InputPlayerName()
    {
        playerName = inputField.text;
        GameManager.Instance.currentName = playerName;
    }


    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif
    }




}
