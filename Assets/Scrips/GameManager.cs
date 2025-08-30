using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public UiManager uiManager;
    public MonSpawner monSpawner;

    

    public bool IsGameOver { get; private set; }

    private void Start()
    {
        var findGo = GameObject.FindWithTag("Player");
        var playerHealth = findGo.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.OnDeath += EndGame;
        }
    }

    public void EndGame()
    {
        IsGameOver = true;
        uiManager.SetActiveGameOverUi(IsGameOver);
        monSpawner.enabled = false;
    }
}
