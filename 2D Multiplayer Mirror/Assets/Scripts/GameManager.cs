using UnityEngine;

public class GameManager : MonoBehaviour
{


    private static GameManager musicManagerInstance;



    private void Awake()
    {
        DontDestroyOnLoad(this);

        if (musicManagerInstance == null)
        {
            Debug.Log("musicInstance is null so");
            musicManagerInstance = this;

        }
        else
        {
            Debug.Log("destroying new instance");
            Destroy(gameObject);
        
        }
    }





}
