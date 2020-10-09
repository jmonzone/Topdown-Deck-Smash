using UnityEngine;

public class ProjectileSFX : MonoBehaviour
{
    [SerializeField] private AudioClip onShot;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        var projectile = GetComponent<ProjectileView>();
        projectile.OnProjectileShot += OnProjectileShot;
    }

    private void OnProjectileShot()
    {
        audioSource.clip = onShot;
        audioSource.Play();
    }
}
