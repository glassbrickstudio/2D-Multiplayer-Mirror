using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMace : MonoBehaviour
{



    private Vector3 startPos;
    [SerializeField] private float amplitube = 2f;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = startPos + amplitube * new Vector3(0f, Mathf.Sin(Time.time), 0f);
        
    }
}
