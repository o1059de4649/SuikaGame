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
    public AudioClip hitSE;
    public AudioClip explosionSE;
    public AudioSource audioSource; 
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
                    // ���ꂽ�{�[�����Ō��ʉ����Đ�
                    obj.GetComponent<AudioSource>().PlayOneShot(hitSE);
                }
            }
            else
            {
                //���������o��
                this.GetComponent<AudioSource>().PlayOneShot(explosionSE);
            }
            //�������悤�Ɍ�����
            var spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            spriteRenderer.enabled = false;
            var collider2D = gameObject.GetComponent<Collider2D>();
            collider2D.enabled = false;
            //������B
            Destroy(this.gameObject,1);
        }
    }
}
