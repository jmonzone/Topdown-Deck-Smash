using System.Collections.Generic;
using UnityEngine;

public class WeaponSFX : MonoBehaviour
{
    [SerializeField] private List<AudioClip> sfx;

    private AudioSource audioSource;

    private void Awake()
    {
        var weapon = GetComponent<WeaponView>();
        weapon.OnAttack += PlaySFX;

        audioSource = GetComponent<AudioSource>();
    }

    private void PlaySFX(int index)
    {
        audioSource.clip = sfx[index];
        audioSource.Play();
    
    }
}
