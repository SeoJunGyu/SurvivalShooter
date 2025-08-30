using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GunData gunData;

    public ParticleSystem shootEffect;
    public Transform firePosition;

    private LineRenderer lineRenderer;
    private AudioSource audioSource;
    private float lastFireTime;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        audioSource = GetComponent<AudioSource>();

        lineRenderer.enabled = false;
        lineRenderer.positionCount = 2;
    }

    private void OnEnable()
    {
        lastFireTime = 0f;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            //Fire();
        }
    }

    private IEnumerator CoShotEffect(Vector3 hitPosition)
    {
        audioSource.PlayOneShot(gunData.shootClip);

        shootEffect.Play();
        shootEffect.lights.light.enabled = true;

        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, firePosition.position);
        lineRenderer.SetPosition(1, hitPosition);

        yield return new WaitForSeconds(0.2f);

        lineRenderer.enabled = false;
        shootEffect.lights.light.enabled = false;
    }

    private void Shoot()
    {
        Vector3 hitPosition = Vector3.zero;

        if(Physics.Raycast(firePosition.position, firePosition.forward, out RaycastHit hit, gunData.fireDistance))
        {
            hitPosition = hit.point;

            var damagable = hit.collider.GetComponent<IDamagable>();
            if (damagable != null)
            {
                damagable.OnDamage(gunData.damage, hit.point, hit.normal);
            }
        }
        else
        {
            hitPosition = firePosition.position + firePosition.forward * gunData.fireDistance;
        }

        StartCoroutine(CoShotEffect(hitPosition));
    }

    public void Fire()
    {
        if(Time.time > (lastFireTime + gunData.timeBetFire))
        {
            lastFireTime = Time.time;
            Shoot();
        }
    }
}
