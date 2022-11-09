using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HighestScore : MonoBehaviour
{
    public TextMeshProUGUI textmeshPro;
    public int highestScore;
    // Start is called before the first frame update
    void Start()
    {
        highestScore = ScoringSystem.scoreValue;
        textmeshPro.SetText("Your Highest Score: " + highestScore);
    }

    // Update is called once per frame
    void Update()
    {
        highestScore = ScoringSystem.scoreValue;
        textmeshPro.SetText("Your Highest Score: " + highestScore);
    }
}
