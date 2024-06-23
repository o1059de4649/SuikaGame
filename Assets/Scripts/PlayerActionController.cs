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
            //�摜�����ւ�
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
        //���N���b�N�Ŕ���
        if (Input.GetMouseButtonDown(0)) {
            Shot(playerBall);
        }
    }

    /// <summary>
    /// �����_���ȃ{�[����Ԃ��B
    /// </summary>
    /// <returns></returns>
    public GameObject selectRandomBall()
    {
        var balls = ballMaster.Balls;
        var result = balls[Random.Range(0, balls.Count - 4)];
        return result;
    }

    /// <summary>
    /// �{�[������������
    /// </summary>
    public void Shot(GameObject ball) {

        // �}�E�X�̃|�C���^������X�N���[�����W���擾
        Vector3 screen_point = Input.mousePosition;
        // z �� 1 �����Ȃ��Ɛ������ϊ��ł��Ȃ�
        screen_point.z = 1.0f;
        // �X�N���[�����W�����[���h���W�ɕϊ�
        Vector3 world_position = Camera.main.ScreenToWorldPoint(screen_point);

        //�Ώۍ��W
        var mousePosition = world_position;

        //�{�[������������
        var obj = Instantiate(ball, mousePosition, Quaternion.identity);
        obj.transform.position = new Vector3(obj.transform.position.x, 4, -5);
        //���O�𓯂����O�ɂ���
        obj.name = obj.name.Replace("(Clone)", "");
        //�{�[���Đݒ�
        playerBall = selectRandomBall();
    }

    /// <summary>
    /// �v���C���[�̎����Ă���{�[���̉摜
    /// </summary>
    void UpdatePlayerBallImage(GameObject playerBall) {
        if (playerBall == null) return;
        //���ׂĂ��\��
        foreach (var ball in playerBallImages) { 
            ball.SetActive(false);
            //�{�[�����������O�̉摜������΁A����͕\���Ƃ���B
            if (ball.transform.gameObject.name.Equals(playerBall.transform.gameObject.name)) {
                ball.SetActive(true);
            }
        }
    }

}
