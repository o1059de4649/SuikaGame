using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Text score_text;
    public Text time_text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        score_text.text = "Score�F" + GameMaster.score.ToString();
        time_text.text =  GameMaster.time.ToString();
    }
}
