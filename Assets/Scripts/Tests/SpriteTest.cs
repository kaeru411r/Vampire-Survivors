using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteTest : MonoBehaviour
{

    [SerializeField] Sprite s1;
    [SerializeField] Sprite s2;
    Sprite s3;
    static Sprite s4;
    static Sprite S4
    {
        get
        {
            if (s4 == null)
            {
                s4 = FindObjectOfType<SpriteRenderer>().sprite;
            }
            return s4;
        }
    }
// Start is called before the first frame update
void Start()
    {
        s3 = s1;
        Debug.Log(s1 == s2 ? "s1 == s2" : "s1 != s2");
        Debug.Log(s1 == s3 ? "s1 == s3" : "s1 != s3");
        Debug.Log(s1 == S4 ? "s1 == s4" : "s1 != s4");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
