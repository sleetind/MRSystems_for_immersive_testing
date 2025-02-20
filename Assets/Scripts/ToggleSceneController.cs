using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneToggleManager : MonoBehaviour
{
    public ToggleGroup toggleGroup; //UI本体にあるToggle Group
    public Toggle[] toggles; //管理に使用するToggleボタン
    private string currentSceneName;

    void Start() 
    {
        currentSceneName = SceneManager.GetActiveScene().name; //起動したら今開いているシーン名を取得
        SetToggleState(currentSceneName);
        foreach (var toggle in toggles) //toggleが押されるのを待つ
        {
            toggle.onValueChanged.AddListener(delegate { OnToggleChanged(toggle); });
        }
    }

    void OnToggleChanged(Toggle changedToggle) //LoadSceneを用いてシーンチェンジを行う
    {
        if (changedToggle.isOn)
        {
            string sceneName = changedToggle.name;
            if (sceneName != currentSceneName) //押したシーンが今いるシーンではないとき
            {
                SceneManager.LoadScene(sceneName);
            }
        }
    }

    void SetToggleState(string sceneName) //今開いているシーンのToggleをON状態にする
    {
        foreach (var toggle in toggles)
        {
           if(toggle.name == sceneName)
           {
               toggle.isOn = true;
           }
           else
           {
                toggle.isOn = false;
           }
        }
    }
}