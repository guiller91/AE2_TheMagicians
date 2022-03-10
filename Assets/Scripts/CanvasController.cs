using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Reiniciar()
    {
        SceneManager.LoadScene("Game");
    }
    public void Salir()
    {
        Application.Quit();
    }
}
