using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombineBallController : MonoBehaviour
{
    /// <summary>
    /// ボールのランク
    /// </summary>
    public int rank;
    public BallMaster ballMaster;
    public GameObject ballMasterObj;
    public AudioClip hitSE;
    public AudioClip explosionSE;
    public AudioSource audioSource; 
    // Start is called before the first frame update
    void Start()
    {
        //もし図鑑がないなら、とりにいく。
        if (!ballMaster) {
            ballMaster = GameObject.FindGameObjectWithTag("GameController").GetComponent<BallMaster>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// ぶつかると1回動く関数
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //ボールの情報が準備されていない場合は処理を終了する。
        if (!ballMaster) return;
        if (ballMaster.Balls.Count == 0) return;

        // もし同じ種類のボールがぶつかったら、消える。
        // 新しく、次のランクのボールが1つ増える。
        if (collision.transform.gameObject.name.Contains(this.gameObject.name)) {

            Debug.Log("ぶつかってきた方" + collision.transform.position.y);
            Debug.Log("ぶつかられた方" + this.gameObject.transform.position.y);

            //Y軸で片方を判定する(自分のほうが低い場合)
            if (collision.transform.position.y >= this.gameObject.transform.position.y) {
                
                //次のランクレベル
                int nextRank = rank + 1;

                //★次のランクのボールがない場合は実行しない。
                if (nextRank < ballMaster.Balls.Count)
                {
                    //次のランクのボールが1つ増える。
                    GameObject nextBall = ballMaster.Balls[nextRank];
                    var obj = Instantiate(nextBall, this.gameObject.transform.position, Quaternion.identity);
                    //名前を同じ名前にする
                    obj.name = obj.name.Replace("(Clone)", "");
                    // 作られたボール側で効果音を再生
                    obj.GetComponent<AudioSource>().PlayOneShot(hitSE);
                }
            }
            else
            {
                //爆発音を出す
                this.GetComponent<AudioSource>().PlayOneShot(explosionSE);
            }
            //消えたように見せる
            var spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            spriteRenderer.enabled = false;
            var collider2D = gameObject.GetComponent<Collider2D>();
            collider2D.enabled = false;
            //消える。
            Destroy(this.gameObject,1);
        }
    }
}
