using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundEffects : MonoBehaviour
{
    [SerializeField] private AudioSource stepSfx;

    public void PlayStepSfx() {
        stepSfx.Play();
    }
}
