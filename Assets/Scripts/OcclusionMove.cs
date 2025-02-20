using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OcclusionMove : MonoBehaviour
{
    public float Movespeed = 5.0f;
    public float Rotatespeed = 5.0f;
    void Update()
        {
            Vector2 Rstick = OVRInput.Get(OVRInput.RawAxis2D.RThumbstick);
            Vector2 Lstick = OVRInput.Get(OVRInput.RawAxis2D.LThumbstick);

            float Rrote = OVRInput.Get(OVRInput.RawAxis1D.RIndexTrigger);
            float Lrote = OVRInput.Get(OVRInput.RawAxis1D.LIndexTrigger);

            // HMDの向きを考慮
            Transform camTransform = Camera.main.transform;
            Vector3 forward = camTransform.forward;
            Vector3 right = camTransform.right;

            // Y軸を無視して平面上のベクトルを作成
            forward.y = 0;
            right.y = 0;
            forward.Normalize();
            right.Normalize();

            Vector3 Move = (right * Rstick.x + forward * Rstick.y + Vector3.up * Lstick.y) * Movespeed * Time.deltaTime;
            transform.Translate(Move, Space.World);

            Vector3 Rotate = new Vector3(0, Rrote - Lrote, 0) * Rotatespeed * Time.deltaTime;
            transform.Rotate(Rotate, Space.World);
        }
}
