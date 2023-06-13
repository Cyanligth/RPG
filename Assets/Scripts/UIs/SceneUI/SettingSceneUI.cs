using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingSceneUI : SceneUI
{
    protected override void Awake()
    {
        base.Awake();

        buttons["ConfigButton"].onClick.AddListener(() => { OpenPausePopUpUI(); });
        buttons["SoundButton"].onClick.AddListener(() => { Debug.Log("Sound"); });
        buttons["InfoButton"].onClick.AddListener(() => { GameManager.UI.OpenWindowUI("UI/WindowUI"); });
    }

    public void OpenPausePopUpUI()
    {
        GameManager.UI.OpenPopUpUI<PopUpUI>("UI/SettingPopUpUI");
    }
}
