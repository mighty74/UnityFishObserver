using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralMove : MoveStrategy
{
    private bool x = true;
    private bool y = true;
    private bool z = true;
    // Start is called before the first frame update
    

    public Vector3 Move(float speed, Transform myTransform){
        //Debug.Log("test");
        //Transform myTransform = this.transform;
        Change();

        // 座標を取得
        Vector3 pos = myTransform.position;
        if(x){
          pos.x += 0.01f * speed;    // x座標へ0.01加算
        }
        if(y){
          pos.y += 0.01f * speed ;    // y座標へ0.01加算
        }
        if(z){
          pos.z += 0.01f * speed;    // z座標へ0.01加算
        }
        if(!x){
          pos.x -= 0.01f * speed;    // x座標へ0.01加算
        }
        if(!y){
          pos.y -= 0.01f * speed;    // y座標へ0.01加算
        }
        if(!z){
          pos.z -= 0.01f * speed;    // z座標へ0.01加算
        }
        return pos;  // 座標を設定
    }

    public void OnCollisionmove(Vector3 relativePoint){
        if (relativePoint.x > 0.5){
                //Debug.Log("Right");
                x = false;
            }

        if (relativePoint.x < -0.5){
            //Debug.Log("Left");
            x = true;
        }

        if (relativePoint.y > 0.5){
            //Debug.Log("Up");
            y = false;
        }
        if (relativePoint.y < -0.5){
            //Debug.Log("Bottom");
            y = true;
        }

        if (relativePoint.z > 0.5){
            //Debug.Log("Back");
            z = true;
        }
        if (relativePoint.z < -0.5){
            //Debug.Log("Front");
            z = false;
        }
    }

    void Change(){
      int i = Random.Range(0, 150);
      if(i == 1){
        if(x){
            x = false;
        }
        else {
            x = true;
        }
      }
      if(i == 2){
        if (y){
            y = false;
        }
        else {
            y = true;
        }
      }
      if(i == 3){
        if (z){
            z = false;
        }
        else {
            z = true;
        }
      }
    }

    


}