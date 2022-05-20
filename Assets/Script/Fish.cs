using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
  //動きのstrategy
  private MoveStrategy moveStrategy;
  //基本スピード
  public float speed = 1.0f;
  //基本体力
  public float health = 15;
  //成長し切ったかどうかのキー
  private bool adalt = false;
  //オスかメスかのキー
  private bool se;
  //ペアがいるかのキー
  private bool havePair = false;
  //ペアである時の相手を記録
  private Fish targetPair = null;
  //状態が何回変化したかのキー
  private int change = 0;
  //時間のカウント
  public float count = 0;
  //生成するオブジェクト
  [SerializeField] public GameObject obj;
    void Start()
    {
      //最初の動きをセット
      moveStrategy = new GeneralMove();
      //性別をセット
      setSe();
      //メス(se=alse)の時に体力を10増加
      if(!se){
        this.health += 10;
      }
      //サイズを変化させるのをスタート
      StartCoroutine("ScaleUp");
      //メス(se=false)であれば色を赤色に
      if(!se){
        this.GetComponent<Renderer>().material.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
      }
    }

    void Update()
    {
      //秒数をカウントする
      this.count += Time.deltaTime;
      //動く
      MoveDefine();
      //体力が0より小さくなったらdestroy
      if(this.health <= 0){
        Destroy(this.gameObject);
      }
      //動きを変える
      ActionChage();
      
    }

    //性別のゲッター
    public bool GetSe(){
      return this.se;
    }

    //ペアがいるかのゲッター
    public bool GetHavePair(){
      return this.havePair;
    }

    //大人かのゲッター
    public bool GetAdalt(){
      return this.adalt;
    }

    //ペア対象のコンポーネントのゲッター
    public Fish GetTargetPair(){
      return this.targetPair;
    }

    //ペア対象のコンポーネントのセッター
    public void setTargetPair(Fish target){
      this.targetPair = target;
    }

    //接触判定
    void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Hit"); // ログを表示する
        foreach (ContactPoint point in collision.contacts)//接触位置
        {
          Vector3 relativePoint = transform.InverseTransformPoint(point.point);
          moveStrategy.OnCollisionmove(relativePoint);//接触位置で動作を変える

        }
        //体力を減らす
        this.health -= 0.5f;
        //大人の時
        if(adalt){
          //ペアがいない時
          if(!havePair){
            //接触相手にFishがついていたら
            if(collision.gameObject.GetComponent<Fish>() != null){
              //接触相手のコンポーネントを取得
              Fish target = collision.gameObject.GetComponent<Fish>();
              //Debug.Log(target);　//確認用
              //対象が大人かどうかの判定
              if(target.GetAdalt()){
                //対象の性別が自分と違う、かつ、対象がペアを持っていない場合
                if(this.se != target.GetSe() && !target.GetHavePair()){
                  //自分のペアに対象をセット
                  setTargetPair(target);
                  //対象のペアに自分をセット
                  target.setTargetPair(this);
                  //対象のペアがいるかのキーをtrueに
                  target.havePair = true;
                  //ペアがいるかのキーをtrueに
                  havePair = true;
                }
              }
            }
          }
        }

    }


    void MoveDefine(){
      Transform myTransform = this.transform;
      Vector3 afterPosition = moveStrategy.Move(speed, myTransform);
      Vector3 diff = afterPosition - myTransform.position;
      //ベクトルの長さが0.01fより大きい場合にプレイヤーの向きを変える処理を入れる(0では入れないので）
      if (diff.magnitude > 0.01f) {
        //ベクトルの情報をQuaternion.LookRotationに引き渡し回転量を取得しプレイヤーを回転させる
        Quaternion targetRotation = Quaternion.LookRotation(diff);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime);
      }
      myTransform.position = afterPosition;
    }

    void setSe(){
      int i = Random.Range(0, 2);
      if(i == 1){
        se = false;
      }
      else {
        se = true;
      }
    }

    IEnumerator ScaleUp()
    {
        for ( float i = 0 ; i < 10 ; i+=1 ){
            this.transform.localScale = new Vector3(1+i/10,1+i/10,1+i/10);
            this.health += 2;
            if(i == 4){
              this.speed = 1.3f;
            }
            if(i == 9){
              adalt = true;
            }
            yield return new WaitForSeconds(1.0f);
        }
    }

    void IsPair(){
      if(targetPair != null && targetPair.GetTargetPair() != null){
        if(havePair){
          if(se){
            moveStrategy = new PairMoveBoy(targetPair);
            //this.GetComponent<Renderer>().material.color = new Color(1.0f, 0.0f, 1.0f, 1.0f);
            //Debug.Log("change move boy");
          }
          else{
            //this.GetComponent<Renderer>().material.color = new Color(1.0f, 1.0f, 0.0f, 1.0f);
            moveStrategy = new PairMoveGurl();
            //Debug.Log("change move gurl");
          }
        }
      }
      else{
        targetPair.setTargetPair(null);
        targetPair = null;
      }
    }

    void Born(){
      if(!se){
        //this.GetComponent<Renderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        moveStrategy = new BornMove();
        //Debug.Log("change born move");
      }
    }

    void ActionChage(){
      if(change != 2){
        this.health -= Time.deltaTime;
      }
      else if(change == 3){
        this.health -= Time.deltaTime*3/2;
      }
      if(change == 0 && havePair){
        change += 1;
        count = 0;
        IsPair();
      }
      if(change == 1 && count > 5){
        change += 1;
        Born();
      }
      if(change == 2 && count > 10){
        change += 1;
        if(havePair && !se){
          Instantiate (obj, new Vector3 (this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.identity);
          Instantiate (obj, new Vector3 (this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.identity);
        }
        moveStrategy = new GeneralMove();
      }
    }

}
