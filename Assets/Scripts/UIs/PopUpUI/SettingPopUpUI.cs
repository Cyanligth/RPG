using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingPopUpUI : PopUpUI
{
    protected override void Awake()
    {
        base.Awake();

        buttons["ContinueButton"].onClick.AddListener(() => { ClosePausePopUpUI(); });
        buttons["SettingButton"].onClick.AddListener(() => { OpenSettingPopUpUI(); });
        buttons["ExitButton"].onClick.AddListener(() => { OpenExitPopUpUI(); });
    }

    public void ClosePausePopUpUI()
    {
        GameManager.UI.ClosePopUpUI();
    }

    public void OpenSettingPopUpUI()
    {
        GameManager.UI.OpenPopUpUI<PopUpUI>("UI/ConfigPopUpUI");
    }
    public void OpenExitPopUpUI()
    {
        Debug.Log("Exit");
    }
}