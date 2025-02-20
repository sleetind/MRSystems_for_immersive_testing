using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class handednesschange : MonoBehaviour
{
    public Toggle HandnessToggle;
    public Text StatusText;

    // Start is called before the first frame update
    void Start()
    {

        HandnessToggle.onValueChanged.AddListener(UpdateText);

        UpdateText(HandnessToggle.isOn);
    }
    
    void UpdateText(bool status)
    {
        if (status)
        {
            StatusText.text = "　右コントローラー";
            StatusText.alignment = TextAnchor.MiddleRight;
        }
        else
        {
            StatusText.text = "左コントローラー　";
            StatusText.alignment = TextAnchor.MiddleLeft;
        }
        
    }

    // Update is called once per frame

}
