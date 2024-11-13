using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunControi : MonoBehaviour
{

    public GameObject bulletPrefab;

    public Transform barrelPos;

    public AudioClip gunSfx;
    public ParticleSystem gunFx;

    private AudioSource _audioSource;
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void Shoot()
    {
        Instantiate(bulletPrefab, barrelPos.position, barrelPos.rotation);
    
        if (gunSfx != null && _audioSource != null)
        {
            _audioSource.PlayOneShot(gunSfx, 1);
        }
        else
        {
            Debug.LogWarning("AudioClip หรือ AudioSource ยังไม่ได้กำหนดใน GunControi!");
        }
    
        gunFx.Play();
        print("Shoot!!");
    }

    
    
    
    
}
