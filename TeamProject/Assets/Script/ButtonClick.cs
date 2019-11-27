using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonClick : MonoBehaviour
{
    private Datasets dataset;

    void Start()
    {
        dataset = GameObject.Find("Dataset").GetComponent<Datasets>();
    }
    public void ChangeScene()
    {
        SceneManager.LoadScene(sceneName: "Game_stage");
    }

    public void StartScene()
    {
        dataset.init();
        SceneManager.LoadScene(sceneName: "Game_stage");
    }
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        
    }
}
