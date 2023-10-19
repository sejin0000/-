using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class button : MonoBehaviour
{
    public int type;

    public void stagestart()
    {

        if (type == 1)
        {
            gameManager.Type = type;
            SceneManager.LoadScene("MainScene");
        }
        else if (type == 2)
        {
            gameManager.Type = type;
            SceneManager.LoadScene("MainScene");
        }
        else if (type == 3)
        {
            gameManager.Type = type;
            SceneManager.LoadScene("MainScene");
        }

    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetFloat("level10") >= type)
        {       
            GetComponent<Button>().interactable = true;
 
        }

    }
}
