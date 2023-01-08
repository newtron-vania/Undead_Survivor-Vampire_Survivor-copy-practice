using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI_HPBar : UI_Base
{
    Stat _stat;
    enum GameObjects
    {
        HPBar,
    }


    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
    }

    private void Start()
    {
        _stat = transform.parent.GetComponent<Stat>();
    }

    private void Update()
    {
        Transform parent = transform.parent;
        transform.position = parent.position;

        float ratio = _stat.HP / (float)_stat.MaxHP;
        setHpRatio(ratio);
    }

    public void setHpRatio(float ratio)
    {
        if (ratio < 0)
            ratio = 0;
        if (ratio > 1)
            ratio = 1;
        GetObject((int)GameObjects.HPBar).GetComponent<Slider>().value = ratio;
    }
}
