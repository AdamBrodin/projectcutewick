using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{

    public Transform player;

    public void Checkpoint()
    {
        player.position = Checkpoints.spawnpoint;
    }
    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
    public void MainMenu()
    {
        //SceneManager.LoadScene();
    }
}
