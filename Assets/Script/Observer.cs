using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
    public int maxCount = 3;
    public int count;
    public int countEgg;
    [SerializeField] public GameObject obj;

    // Start is called before the first frame update
    void Start()
    {
        generateFish();
    }

    // Update is called once per frame
    void Update()
    {
        this.count = GameObject.FindGameObjectsWithTag("fish").Length;
        this.countEgg = GameObject.FindGameObjectsWithTag("egg").Length;
      if (Input.GetKeyDown (KeyCode.Space)) {
        Debug.Log("generate");
        Instantiate (obj, new Vector3 (0.0f, 5.0f, 0.0f), Quaternion.identity);
      }
      if(this.count < maxCount){
        //Debug.Log("a");
        Instantiate(obj, new Vector3(0.0f, 5.0f, 0.0f), Quaternion.identity);
      }
    }

    void generateFish(){
        Instantiate(obj, new Vector3(0.0f, 5.0f, 0.0f), Quaternion.identity);
    }
}
