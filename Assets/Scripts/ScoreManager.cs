using UnityEngine;
using TMPro; // TextMeshPro for UI

// TODO: pull out all hard coded values

public class ScoreManager: MonoBehaviour
{
    public int score = 0;
    public TMP_Text scoreText; // Assign score UI Text component in Inspector

    void Start()
    {
        UpdateScoreUI();
        PlayerController.CatchSnackEvent += HandleCatchSnack;
        PlayerController.BonkEvent += HandleBonk;
        PlayerController.EatSplatEvent += HandleEatSplat;
    }

    private void OnDestroy()
    {
        PlayerController.CatchSnackEvent -= HandleCatchSnack;
        PlayerController.BonkEvent -= HandleBonk;
        PlayerController.EatSplatEvent -= HandleEatSplat;
    }

    void HandleCatchSnack(string snackType)
    {
        if(snackType == "Safe")
        {
            score += 10; // Adjust points as needed
        }
        else if(snackType == "Special")
        {
            score += 25; // Adjust points as needed
            // Implement special effect logic here
        }
        UpdateScoreUI();
    }

    void HandleBonk(string bonkType)
    {
        score -= 5; // Adjust penalty as needed
        UpdateScoreUI();
        // Implement temporary speed debuff logic in PlayerController
    }

    void HandleEatSplat()
    {
        score -= 2; // Adjust penalty as needed
        UpdateScoreUI();
        // Implement weight gain and speed reduction logic in PlayerController
    }

    void UpdateScoreUI()
    {
        if(scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
        else
        {
            Debug.LogWarning("Score Text UI not assigned to ScoreManager!");
        }

        if(score < 0)
        {
            Debug.Log("Score is negative! Game Over!");
            // Implement Game Over logic here
        }
    }
}