using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Meta.XR.EnvironmentDepth;

public class HapticsOnOff : MonoBehaviour
{
    public Toggle HapticsToggle;
    public Text StatusText;
    [SerializeField] GameObject TennisRacket;

    void Start()
    {
        HapticsToggle.onValueChanged.AddListener(UpdateText);
        UpdateText(HapticsToggle.isOn);
    }

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
        HapticsSEOnOff(status);
    }

    void HapticsSEOnOff(bool status)
    {
        TennisHapticsPlayer player = TennisRacket.GetComponent<TennisHapticsPlayer>();
        if (player == null)
        {
            Debug.LogError("TennisHapticsPlayer が見つかりません。");
            return;
        }

        // トグルの状態と同期
        player.HapticsEnabled = status;
    }
}