using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeMangerd : MonoBehaviour
{
    public GameObject[] heart;

    public void UpdateHeart(int life)
    {
        for (int i = 0; i < heart.Length; i++)
        {
            if(i < life)
            {
                heart[i].SetActive(true);
            }
            else
            {
                heart[i].SetActive(false);
            }
        }
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
