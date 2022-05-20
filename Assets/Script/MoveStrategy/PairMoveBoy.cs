using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PairMoveBoy : MoveStrategy
{
    private Fish target;
    private bool x = true;
    private bool y = false;
    private bool z = true;
    // Start is called before the first frame update

    public PairMoveBoy(Fish target){
        this.target = target;
    }
    

    public Vector3 Move(float speed, Transform myTransform){
        // 座標を取得
        Vector3 pos = myTransform.position;
        if(target != null){
            //Debug.Log(target.transform.position.y - myTransform.position.y);
            Vector3 targetPos = target.transform.position;
            if(targetPos.x-pos.x >0){
                x = true;
            }
            if(targetPos.y-pos.y >0){
                y = true;
            }
            if(targetPos.z-pos.z >0){
                z = true;
            }
            if(targetPos.x-pos.x <=0){
                x = false;
            }
            if(targetPos.y-pos.y <=0){
                y = false;
            }
            if(targetPos.z-pos.z <=0){
                z = false;
            }
        }
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

    
}
