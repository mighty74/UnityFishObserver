using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{
    public int health = 10;
    [SerializeField] public GameObject obj;
    public float time = 3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        destroyEgg();
    }

    void OnCollisionEnter(Collision collision)
    {
        this.health -= 1;
    }

    void destroyEgg(){
        if(health <= 0){
            Destroy(this.gameObject);
        }
        if(time <= 0){
            int i = Random.Range(0, 5);
            if(i != 1){
                //Instantiate (obj, new Vector3 (this.transform.position.x, 1.0f, this.transform.position.z), Quaternion.identity);
                Instantiate (obj, new Vector3 (this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.identity);
                Instantiate (obj, new Vector3 (this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.identity);

            }
            Destroy(this.gameObject);
        }
    }
}
