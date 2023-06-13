using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InfoSceneUI : SceneUI
{
    public TMP_Text lifeTxt;
    protected override void Awake()
    {
        base.Awake();

        texts["LifeTxt"].text = "1234";
        // texts["GoldTxt"].text = "4321";
    }
}
