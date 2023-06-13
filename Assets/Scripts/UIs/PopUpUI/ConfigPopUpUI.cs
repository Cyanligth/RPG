using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigPopUpUI : PopUpUI
{
    protected override void Awake()
    {
        base.Awake();

        buttons["ConfigExitButton"].onClick.AddListener(() => { GameManager.UI.ClosePopUpUI(); });
        buttons["ConfigSaveButton"].onClick.AddListener(() => { GameManager.UI.ClosePopUpUI(); });
    }
}
