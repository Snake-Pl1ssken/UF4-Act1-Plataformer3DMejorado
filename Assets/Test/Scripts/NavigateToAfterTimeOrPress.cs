using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class NavigateToAfterTimeOrPress : MonoBehaviour
{
    [SerializeField] string NextScene;
    [SerializeField] InputActionReference Skip;
    private float timeNextScene = 5;
    private bool CheckCallScene;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        Skip.action.Enable();
    }

    void Awake()
    {
        Invoke("NavigateToNextScreen", timeNextScene);
    }

    // Update is called once per frame
    void Update()
    {
        if (Skip.action.triggered)
        { 
            NavigateToNextScreen();
        }
    }

    void OnDisable()
    {
        Skip.action.Disable();   
    }

    void NavigateToNextScreen()
    {
        if(!CheckCallScene) 
        {
            CheckCallScene = true;
            SceneManager.LoadScene(NextScene);
        }

    }
}


//boleano dentro de llamar a la escena que dira si se a accedido o no a esa escena