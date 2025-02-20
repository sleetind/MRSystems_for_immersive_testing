using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour
{
    public GameObject ballPrefab;
    public Transform leftController;
    public Transform rightController;
    public Toggle HandednessToggle;

    private GameObject BallInstance;
    private bool GrabtheBall = false;

    [SerializeField]//速度を補正する
    private float velocityMultiplier = 1f;
    [SerializeField]//角速度を補正する
    private float angularVelocityMultiplier = 1f;

     void Start()
    {
        //起動時は左で投げるように設定(右利きの人は右手でラケットを振るので)
        if(HandednessToggle != null)
        {
            HandednessToggle.isOn = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        Transform activeController; //現在Toggleで設定されているコントローラー(右 or 左)
        OVRInput.Controller activeHand; //Toggleで設定されているコントローラーのTrigger

        //ToggleがOFFのときは左手、ONの時は右手
        
        if (HandednessToggle.isOn)
        {//ONの時は右手で投げる
            activeController = rightController;
            activeHand = OVRInput.Controller.RTouch;
        }
        else
        {//OFFの時は左手で投げる
            activeController = leftController;
            activeHand = OVRInput.Controller.LTouch;
        }

        bool HandTriggerPressed = OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, activeHand);
        if (HandTriggerPressed)//activeControllerのトリガーを押している時
        {
            if(BallInstance == null)//
            {
                //Ballインスタンスが生成されていない場合、生成してactiveControllerに追従
                BallInstance = Instantiate(ballPrefab, activeController.position, Quaternion.identity);
                BallInstance.GetComponent<Rigidbody>().isKinematic = true;
                GrabtheBall = true;
            }
            else if (!GrabtheBall)
            {
                //Ballがコントローラーにない場合、生成したBallをactiveControllerに追従
                BallInstance.transform.position = activeController.position;
                BallInstance.GetComponent<Rigidbody>().isKinematic = true;
                GrabtheBall = true;
            }
            
        }
        else
        {
            if(GrabtheBall && BallInstance != null)//左ハンドトリガーを離したときにGrabtheBallがtrueかつBallのインスタンスがnullではないとき
            {
                Rigidbody rb = BallInstance.GetComponent<Rigidbody>();
                rb.isKinematic = false; //重力を使用

                //コントローラーの速度と角速度を取得し補正値と掛け算した値をBallインスタンスのRigidBodyに適用
                rb.velocity = OVRInput.GetLocalControllerVelocity(activeHand)* velocityMultiplier;
                rb.angularVelocity = OVRInput.GetLocalControllerAngularVelocity(activeHand) * angularVelocityMultiplier ;

                GrabtheBall = false;
            }
        }
        if (GrabtheBall && BallInstance != null) //ボールを持っていてBallインスタンスがnullではないとき
        {
            //activeControllerを追従
            BallInstance.transform.position = activeController.position;
        }
    }
}
