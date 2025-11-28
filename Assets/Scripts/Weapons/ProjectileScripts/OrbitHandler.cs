using UnityEngine;
using System;

public class OrbitHandler : MonoBehaviour
{
    private Transform pivot;
    private int damage;
    private float speed, critChance;
    public void InitializeOrbiter(Transform _p, int _d, float _s, float _c)
    {
        pivot = _p;
        damage = _d;
        speed = _s;
        critChance = _c;
    }
    private void Update()
    {
        transform.RotateAround(pivot.position, Vector3.forward, speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D trigger) 
    {
        IDamageable damageable = trigger.gameObject.GetComponent<IDamageable>();

        bool isCritical = UnityEngine.Random.Range(0f,100f) < critChance;

        if(damageable != null) 
            damageable.TakeDamage(damage, isCritical);
    }
}
