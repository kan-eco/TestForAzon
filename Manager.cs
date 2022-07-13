using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Manager : MonoBehaviour
{
    [SerializeField]
    GameObject dark;

    [SerializeField]
    GameObject startButton;

    [SerializeField]
    GameObject restartButton;

    [SerializeField]
    TextMeshProUGUI text;

    HealthBar bar;
    int _score = 0;

    void Awake()
    {
        Time.timeScale = 0;
        restartButton.SetActive(false);
        bar = GetComponent<HealthBar>();
        //text.text = "Bandits killed:  " + 0;
    }

    public void StartButton()
    {
        Time.timeScale = 1;
        dark.SetActive(false);
    }

    public void EndGame()
    {
        StartCoroutine(SeeHowIDie());
    }

    public void RestartButton()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void Score(int score)
    {
        _score += score;
        text.text = "Bandits killed:  " + _score;
    }

    IEnumerator SeeHowIDie()
    {
        yield return new WaitForSeconds(2);
        Time.timeScale = 0;
        dark.SetActive(true);
        startButton.SetActive(false);
        restartButton.SetActive(true);
    }
}
