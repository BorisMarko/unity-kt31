using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSubtitle : MonoBehaviour
{
    public GameObject Object1;
    public GameObject Object2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player")
        {
            Object1.SetActive(true);
            Object2.SetActive(true);
            Destroy(gameObject);
        }
    }
}
