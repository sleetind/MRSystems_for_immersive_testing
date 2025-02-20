using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PortalManager : MonoBehaviour
{
    //移動先の世界の3Dモデルをまとめたオブジェクト
    [SerializeField] GameObject worldObject;
    //カメラを入力する
    [SerializeField] private Camera mainCamera;
    //上記オブジェクトのマテリアルを保持するためのコード
    List<Material> worldMaterials = new List<Material>();
    //表示モードの管理(起動時はARモード)
    bool isARMode = true;
    float enteringSide;
    public GameObject SelectMenu;
    public GameObject Information;

    // 追加: ポータル内に入っているかどうかを判断するフラグ
    private bool isInPortal = false;

    void Start()
    {
        Debug.Log("start");
        //移動先の3DモデルのRendererを取得
        Renderer[] renderers = worldObject.GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            //マテリアルの取得
            Material material = renderer.sharedMaterial;
            //マテリアルがリストに登録されていない場合リストに追加
            if (!worldMaterials.Contains(material))
            {
                worldMaterials.Add(material);
            }
        }

        // アプリ起動時はクリッピングをOFFにする
        ClipMode("DISABLED");
    }

    void OnTriggerEnter(Collider other)
    {
        // mainCamera以外からのイベントは無視
        if (other.gameObject != mainCamera.gameObject) return;

        // 既にポータル内にいる場合は処理をスキップ
        if (isInPortal) return;
        isInPortal = true;

        Debug.Log("In Portal");
        SetStencilComparison(CompareFunction.Always);

        Vector3 localPos = transform.InverseTransformPoint(mainCamera.transform.position);
        enteringSide = Mathf.Sign(localPos.z);

        // enteringSideに基づいてClipping処理を変更
        if ((isARMode && enteringSide < 0) || (!isARMode && enteringSide > 0))
        {
            ClipMode("LESSTHAN");
        }
        else
        {
            ClipMode("GREATERTHAN");
        }
    }

    void OnTriggerExit(Collider other)
    {
        // mainCamera以外からのイベントは無視
        if (other.gameObject != mainCamera.gameObject) return;

        // ポータル内にいなければ処理しない
        if (!isInPortal) return;
        isInPortal = false;

        Vector3 localPos = transform.InverseTransformPoint(mainCamera.transform.position);
        float exitingSide = Mathf.Sign(localPos.z);
        Debug.Log("Out Portal");

        if (isARMode)
        {
            if (exitingSide != enteringSide)
            {//現在ARモード:VRモードにしてゲートの外側のみを表示
                SetStencilComparison(CompareFunction.NotEqual);
                isARMode = false;
                SelectMenu.SetActive(isARMode);
                Information.SetActive(isARMode);
            }
            else
            {
                SetStencilComparison(CompareFunction.Equal);
            }
        }
        else
        {
            if (exitingSide != enteringSide)
            {//現在VRモード:ARモードにしてゲートの内側のみを表示
                SetStencilComparison(CompareFunction.Equal);
                isARMode = true;
                SelectMenu.SetActive(isARMode);
                Information.SetActive(isARMode);
            }
            else
            {
                SetStencilComparison(CompareFunction.NotEqual);
            }
        }
        ClipMode("DISABLED");
    }

    void OnDestroy()
    {
        // アプリ終了時はクリッピングをOFFにする
        ClipMode("DISABLED");
        SetStencilComparison(CompareFunction.Equal);
    }

    void SetStencilComparison(CompareFunction Comp)
    {
        foreach (Material material in worldMaterials)
        {
            //読み込んだマテリアルに順番に比較条件を入れる
            material.SetInt("_StencilComp", (int)Comp);
        }
    }

    // CLIPPING Modeを設定するメソッド
    void ClipMode(string mode)
    {
        foreach (Material material in worldMaterials)
        {
            // まず全てのキーワードを無効化する
            material.DisableKeyword("_CLIPPING_MODE_DISABLED");
            material.DisableKeyword("_CLIPPING_MODE_GREATERTHAN");
            material.DisableKeyword("_CLIPPING_MODE_LESSTHAN");

            string keyword = "_CLIPPING_MODE_" + mode.ToUpper();
            // 指定されたキーワードのみ有効化する
            material.EnableKeyword(keyword);
        }
    }
}