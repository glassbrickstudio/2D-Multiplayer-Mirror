using UnityEngine;

public class GameManager : MonoBehaviour
{


    private static GameManager gameManager;



    private void Awake()
    {
        DontDestroyOnLoad(this);

        if (gameManager == null)
        {
            Debug.Log("musicInstance is null so");
            gameManager = this;

        }
        else
        {
            Debug.Log("destroying new instance");
            Destroy(gameObject);
        
        }
    }





}
