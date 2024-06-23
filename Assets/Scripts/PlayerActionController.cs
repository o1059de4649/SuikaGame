using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class PlayerActionController : MonoBehaviour
{
    private GameObject _playerBall;
    public GameObject playerBall { 
       get { return _playerBall; }
       set {
            _playerBall = value;
            //画像差し替え
            UpdatePlayerBallImage(_playerBall);
        }
    }
    public BallMaster ballMaster;
    public List<GameObject> playerBallImages = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        playerBall = selectRandomBall();
    }

    // Update is called once per frame
    void Update()
    {
        //左クリックで発射
        if (Input.GetMouseButtonDown(0)) {
            Shot(playerBall);
        }
    }

    /// <summary>
    /// ランダムなボールを返す。
    /// </summary>
    /// <returns></returns>
    public GameObject selectRandomBall()
    {
        var balls = ballMaster.Balls;
        var result = balls[Random.Range(0, balls.Count - 4)];
        return result;
    }

    /// <summary>
    /// ボールを解き放つ
    /// </summary>
    public void Shot(GameObject ball) {

        // マウスのポインタがあるスクリーン座標を取得
        Vector3 screen_point = Input.mousePosition;
        // z に 1 を入れないと正しく変換できない
        screen_point.z = 1.0f;
        // スクリーン座標をワールド座標に変換
        Vector3 world_position = Camera.main.ScreenToWorldPoint(screen_point);

        //対象座標
        var mousePosition = world_position;

        //ボールを解き放つ
        var obj = Instantiate(ball, mousePosition, Quaternion.identity);
        obj.transform.position = new Vector3(obj.transform.position.x, 4, -5);
        //名前を同じ名前にする
        obj.name = obj.name.Replace("(Clone)", "");
        //ボール再設定
        playerBall = selectRandomBall();
    }

    /// <summary>
    /// プレイヤーの持っているボールの画像
    /// </summary>
    void UpdatePlayerBallImage(GameObject playerBall) {
        if (playerBall == null) return;
        //すべてを非表示
        foreach (var ball in playerBallImages) { 
            ball.SetActive(false);
            //ボールが同じ名前の画像があれば、それは表示とする。
            if (ball.transform.gameObject.name.Equals(playerBall.transform.gameObject.name)) {
                ball.SetActive(true);
            }
        }
    }

}
