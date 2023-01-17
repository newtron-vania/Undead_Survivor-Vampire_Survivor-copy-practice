using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimeOver : MonoBehaviour
{
    public bool _animeOver = false;

    public void SetAnimeOver()
    {
        _animeOver = true;
        Managers.Sound.Play("A_Bit_Of_Hope", Define.Sound.Bgm);
    }
}
