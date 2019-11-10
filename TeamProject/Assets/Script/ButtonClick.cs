using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonClick : MonoBehaviour
{
    public void ChangeScene()
    {
        SceneManager.LoadScene(sceneName: "Load_Menu");
    }

    public void StartScene()
    {
        SceneManager.LoadScene(sceneName: "Game_stage");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
