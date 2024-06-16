using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombineBallController : MonoBehaviour
{
    /// <summary>
    /// �{�[���̃����N
    /// </summary>
    public int rank;
    public BallMaster ballMaster;
    public GameObject ballMasterObj;
    // Start is called before the first frame update
    void Start()
    {
        //�����}�ӂ��Ȃ��Ȃ�A�Ƃ�ɂ����B
        if (!ballMaster) {
            ballMaster = GameObject.FindGameObjectWithTag("GameController").GetComponent<BallMaster>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// �Ԃ����1�񓮂��֐�
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //�{�[���̏�񂪏�������Ă��Ȃ��ꍇ�͏������I������B
        if (!ballMaster) return;
        if (ballMaster.Balls.Count == 0) return;

        // ����������ނ̃{�[�����Ԃ�������A������B
        // �V�����A���̃����N�̃{�[����1������B
        if (collision.transform.gameObject.name.Contains(this.gameObject.name)) {

            Debug.Log("�Ԃ����Ă�����" + collision.transform.position.y);
            Debug.Log("�Ԃ���ꂽ��" + this.gameObject.transform.position.y);

            //Y���ŕЕ��𔻒肷��(�����̂ق����Ⴂ�ꍇ)
            if (collision.transform.position.y >= this.gameObject.transform.position.y) {
                
                //���̃����N���x��
                int nextRank = rank + 1;

                //�����̃����N�̃{�[�����Ȃ��ꍇ�͎��s���Ȃ��B
                if (nextRank < ballMaster.Balls.Count)
                {
                    //���̃����N�̃{�[����1������B
                    GameObject nextBall = ballMaster.Balls[nextRank];
                    var obj = Instantiate(nextBall, this.gameObject.transform.position, Quaternion.identity);
                    //���O�𓯂����O�ɂ���
                    obj.name = obj.name.Replace("(Clone)", "");
                }
            }

            //������B
            Destroy(this.gameObject);
        }
    }
}
