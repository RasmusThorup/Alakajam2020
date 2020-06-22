using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISound : MonoBehaviour
{

    public void PlaySound()
    {
        AkSoundEngine.PostEvent("ui_button_activate", this.gameObject);
    }
}
