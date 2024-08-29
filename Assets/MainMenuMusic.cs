using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MainMenuMusic : MonoBehaviour
{
    [SerializeField] AudioClip musicClip;

    private void Start()
    {
        AudioManager.Instance.PlayMusic(musicClip);
    }
}
