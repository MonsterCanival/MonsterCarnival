using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCoolTime : MonoBehaviour {

    public Image Img;
    public Image Img1;
    public Image Img2;

    public float leftTime { get; set; }
    public float leftTime1 { get; set; }
    public float leftTime2 { get; set; }

    public float coolTime { get; set; }
    public float coolTime1 { get; set; }
    public float coolTime2 { get; set; }
    Player PlayerScript;

    bool isTriggered;
    bool isTriggered1;
    bool isTriggered2;
    private void Start()
    {
        PlayerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if(PlayerScript == null)
        {
            gameObject.SetActive(false);
        }
        else
        {
            
            leftTime = (float)PlayerScript.DelaySingleSkill;
            leftTime1 = (float)PlayerScript.DelayMultipleSkill;
            leftTime2 = (float)PlayerScript.DelaySkillSpeed;

            coolTime = leftTime;
            coolTime1 = leftTime1;
            coolTime2 = leftTime2;

            isTriggered = false;
            isTriggered1 = false;
            isTriggered2 = false;
        }
    }
    // Update is called once per frame
    void Update () {
		if(leftTime > 0)
        {
            leftTime -= Time.deltaTime;
            if(leftTime < 0)
            {
                leftTime = 0;
            }
            float ratio = 1.0f - (leftTime / coolTime);
            if (Img)
            {
                Img.fillAmount = ratio;
            }
        }
        if (leftTime1 > 0)
        {
            leftTime1 -= Time.deltaTime;
            if (leftTime1 < 0)
            {
                leftTime1 = 0;
            }
            float ratio = 1.0f - (leftTime1 / coolTime2);
            if (Img1)
            {
                Img1.fillAmount = ratio;
            }
        }
        if (leftTime2 > 0)
        {
            leftTime2 -= Time.deltaTime;
            if (leftTime2 < 0)
            {
                leftTime2 = 0;
            }
            float ratio = 1.0f - (leftTime2 / coolTime2);
            if (Img2)
            {
                Img2.fillAmount = ratio;
            }
        }

        if(PlayerScript.Behaviable.bCanSingleSkill == false)
        {
            isTriggered = true;
        }
        if(PlayerScript.Behaviable.bCanMultipleSkill == false)
        {
            isTriggered1 = true;
        }
        if(PlayerScript.Behaviable.bCanSpeedSkill == false)
        {
            isTriggered2 = true;
        }

        if (isTriggered)
        {
            isTriggered = false;
            ResetCoolTime();
        }
        if (isTriggered1)
        {
            isTriggered1 = false;
            ResetCoolTime1();
        }
        if (isTriggered2)
        {
            isTriggered2 = false;
            ResetCoolTime2();
        }
    }
    
    public void ResetCoolTime()
    {
        leftTime = coolTime;
    }

    public void ResetCoolTime1()
    {
        leftTime1 = coolTime1;
    }

    public void ResetCoolTime2()
    {
        leftTime2 = coolTime2;
    }
}
