using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Meta.XR.EnvironmentDepth;

public class ONOFFDisplay : MonoBehaviour
{
    public Toggle OcclusionToggle;
    public Text StatusText;
    public EnvironmentDepthManager enviromentDepthManager;

    //起動時、トグルのListenerを登録し、テキストを起動時のトグルの状態にする
    void Start()
    {
        OcclusionToggle.onValueChanged.AddListener(UpdateText);
        UpdateText(OcclusionToggle.isOn);
    }

    // トグルボタンのstatusに対応するON-OFFのテキストを表示
    void UpdateText(bool status)
    {
        if (status)
        {
            StatusText.text = "ON";
            StatusText.alignment = TextAnchor.MiddleRight;
        }
        else
        {
            StatusText.text = "OFF";
            StatusText.alignment = TextAnchor.MiddleLeft;
        }
        OcclusionOnOff(status);
    }

    //Occlusionの設定をトグルボタンに対応した設定に変更
    void OcclusionOnOff(bool status)
    {
        if (status)
        {
            enviromentDepthManager.OcclusionShadersMode = OcclusionShadersMode.SoftOcclusion;
            Debug.Log("OcclusionShaderModeをSoftOcclusionに設定");
        }
        else
        {
            enviromentDepthManager.OcclusionShadersMode = OcclusionShadersMode.None;
            Debug.Log("OcclusionShaderModeをNoneに設定");

        }
    }
}
