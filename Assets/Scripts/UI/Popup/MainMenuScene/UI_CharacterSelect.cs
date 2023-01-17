using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class UI_CharacterSelect : UI_Popup
{
    public override Define.PopupUIGroup _popupID { get { return Define.PopupUIGroup.UI_CharacterSelect; } }
    
    Dictionary<int, Data.Player> playerData;
    List<GameObject> playerGOList = new List<GameObject>();
    Data.Player currentPlayer = null;
    enum Images
    {
        BackgroundImg,
        ProtectImage,
        PlayerImage

    }
    enum Panels
    {
        SelectPanel,
        GridPanel,
        InfoPanel
    }


    enum Texts
    {
        NameText,
        DescText
    }

    enum Buttons
    {
        StartButton
    }

    public override void Init()
    {
        playerData = Managers.Data.PlayerData;

        Bind<Image>(typeof(Images));
        Bind<GameObject>(typeof(Panels));
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Button>(typeof(Buttons));

        GetImage((int)Images.ProtectImage).gameObject.AddUIEvent(OnClickDelete);


        GameObject gridPanel = GetObject((int)Panels.GridPanel).gameObject;
        foreach(Transform child in gridPanel.transform)
        {
            Managers.Resource.Destroy(child.gameObject);
        }

        //PlayerInven Create
        foreach (KeyValuePair<int, Data.Player> kv in playerData)
        {
            if (kv.Key > 2)
                break;
            GameObject go = Managers.Resource.Instantiate("UI/SubItem/PlayerInven", parent: gridPanel.transform);
            go.FindChild<Image>("PlayerImg", true).sprite = Managers.Resource.LoadSprite(kv.Value.name);
            go.GetOrAddComponent<PlayerInven>().player = kv.Value;
            Data.Player playerStat = go.GetOrAddComponent<PlayerInven>().player;
            BindUIEvent(go, (PointerEventData data) => { OnClickUpdate(go, playerStat); });
            playerGOList.Add(go);
        }
        OnClickUpdate(playerGOList[0], playerGOList[0].GetComponent<PlayerInven>().player);

        GetButton((int)Buttons.StartButton).gameObject.AddUIEvent(OnClickStartGame);
        
    }

    void OnClickUpdate(GameObject go, Data.Player playerStat)
    {
        Managers.Sound.Play("Select", Define.Sound.Effect);
        Debug.Log($"gameobject : {go.name}, player = {playerStat.id}");
        currentPlayer = playerStat;
        foreach(GameObject goes in playerGOList)
        {
            goes.GetComponent<Image>().color = Color.white;
        }
        go.GetComponent<Image>().color = Color.yellow;
        string name = playerStat.name;
        string desc = $"HP : {playerStat.maxHp}\tDamage : {playerStat.damage}\n" +
            $"Speed : {playerStat.moveSpeed}\tDefense : {playerStat.defense}\n" +
            $"Cooldown : {playerStat.coolDown}\tAmount : {playerStat.amount}";

        GetImage((int)Images.PlayerImage).sprite = Managers.Resource.LoadSprite(name);
        GetText((int)Texts.NameText).text = name;
        GetText((int)Texts.DescText).text = desc;
    }
    void OnClickDelete(PointerEventData data)
    {
        Managers.Sound.Play("Select", Define.Sound.Effect);
        Managers.UI.CloseAllGroupPopupUI(_popupID);
    }


    void OnClickStartGame(PointerEventData data)
    {
        Managers.Sound.Play("Select", Define.Sound.Effect);
        //Todo
        //Setting PlayerStat
        Managers.Game.StartPlayer = currentPlayer;
        //Move to GameScene
        Debug.Log($"StartPlayer : {Managers.Game.StartPlayer.id}");
        Managers.Scene.LoadScene(Define.SceneType.GameScene);
    }
}
