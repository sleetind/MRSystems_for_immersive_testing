using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Oculus.Haptics;

public class TennisHapticsPlayer : MonoBehaviour
{
    public Toggle HandednessToggle;
    public HapticClip HapticClip;
    public AudioClip seClip;
    AudioSource audioSource;

    private HapticClipPlayer player;
    private bool canPlaySH = true; //S=Sound,H=Haptics

    //Haptics再生切り替え用bool
    public bool HapticsEnabled = true;

    // Start is called before the first frame update
    void Awake()
    {
        player = new HapticClipPlayer(HapticClip);
    
    // AudioSourceを取得または追加
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.playOnAwake = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!HapticsEnabled) return;
        if (canPlaySH)
        {
            PlayHapticClip();
            audioSource.PlayOneShot(seClip);
            canPlaySH = false;
            StartCoroutine(SECoolDown());
        }
        
        
    }

    private IEnumerator SECoolDown()
    {
        yield return new WaitForSeconds(0.1f);
        canPlaySH = true; 
    }
    public void PlayHapticClip()
    {
        if (HandednessToggle.isOn)
        {
            player.Play(Controller.Left);
        }
        else
        {
            player.Play(Controller.Right);
        }
       
    }
    
    
        void OnDestroy()
    {
        player.Dispose();
    }
}
