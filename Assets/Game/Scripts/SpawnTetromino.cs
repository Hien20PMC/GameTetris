using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SpawnTetromino : MonoBehaviour
{
    [SerializeField] private GameObject[] Tetrominoes;
    [SerializeField] private TMP_Text highscore;
    [SerializeField] private Text scoreText;
    private int Score = 0;
    private int HightScore = 0;
   


    // Start is called before the first frame update
    void Start()
    {
        NewTetromino();
        HightScore = PlayerPrefs.GetInt("HighScore", 0);
        highscore.text = "HighScore: "+HightScore.ToString();

    }
    void Update()
    {
        scoreText.text = Mathf.Round(Score).ToString();
        UpdateHighScore();

    }


    // Update is called once per frame
    public void NewTetromino()
    {
        Instantiate(Tetrominoes[Random.Range(0, Tetrominoes.Length)], transform.position, Quaternion.identity);
        Score++;
    }

    public void UpdateHighScore()
    {
        highscore.text = "HighScore: " + HightScore.ToString();
        if(Score > HightScore)
        {
            PlayerPrefs.SetInt("HighScore", Score);
        }
    }
}
