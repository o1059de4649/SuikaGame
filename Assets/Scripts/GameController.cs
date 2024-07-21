using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    /// <summary>
    /// ���g���C�{�^��
    /// </summary>
    public GameObject retryButton;
    /// <summary>
    /// �Q�[���I�[�o�[���ǂ���
    /// </summary>
    public bool gameOver = false;
    public UIController uiController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (uiController.now_time < 0)
        {
            gameOver = true;
        }
        if (gameOver)
        {
            retryButton.SetActive(true);
        }      
    }

    /// <summary>
    /// ���g���C���s
    /// </summary>
    public void RetryExecute()
    {
        SceneManager.LoadScene("Scenes/PlayGameScene");
    }
}
