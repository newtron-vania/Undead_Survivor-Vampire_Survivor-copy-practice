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
        _stat = transform.parent.GetComponent<Stat>();
    }

    private void Update()
    {
        Transform parent = transform.parent;
        //캐릭터마다 키가 다르기 때문에 각자 y값을 조정해줘야 한다.
        //각 캐릭터의 콜라이더를 찾아내어 y값만큼 높이를 올려준다.
        transform.position = parent.position + Vector3.up * (parent.GetComponent<Collider>().bounds.size.y + 1);
        transform.rotation = Camera.main.transform.rotation;

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
