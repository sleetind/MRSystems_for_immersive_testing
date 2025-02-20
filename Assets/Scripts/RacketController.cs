using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacketController : MonoBehaviour
{
    [SerializeField]
    public float hitForceMultiplier = 1f;
    private Vector3 previousPosition;
    private Vector3 currentVelocity;

    void Start()
    {
        previousPosition = transform.position;
    }
    void Update()
    {
        //ラケットが動くたびに位置を取得して速度を計算
        currentVelocity = (transform.position - previousPosition) / Time.deltaTime;
        previousPosition = transform.position;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))//Collisionを検知したときに検知したオブジェクトのタグがBallのとき
        {
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                //Rigidbodyが存在していたら、ボールに力を加える
                Vector3 force = currentVelocity * hitForceMultiplier;
                rb.isKinematic = false;
                rb.AddForce(force, ForceMode.Impulse);
            }
        }
    }
}
