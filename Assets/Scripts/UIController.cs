using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Text score_text;
    public Text time_text;
    public float now_time = 0;
    
    void Start()
    {
        now_time = GameMaster.max_time;
    }

    void Update()
    {
        score_text.text = "ScoreÅF" + GameMaster.score.ToString();
        time_text.text = Math.Floor(now_time).ToString();
        CountTime();
    }

    void CountTime()
    {
        now_timeÅ@-= Time.deltaTime;
    }
}
