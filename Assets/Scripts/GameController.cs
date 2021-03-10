using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameOverScreen gameOverScreen;
    public GameOverScreen victoryScreen;
    // Start is called before the first frame update
    public void GameOver()
    {
        gameOverScreen.Setup();
    }

    public void Victory()
    {
        victoryScreen.Setup();
    }
}
