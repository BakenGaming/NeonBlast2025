using Unity.VisualScripting;
using UnityEngine;

public class EnemyHandler : MonoBehaviour, IDamageable
{
    private Vector3 offset = new Vector3(0f,1.25f,0f);
    private GameObject healthBarGraphic;
    private Camera mainCam;
    void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        mainCam = Camera.main;
        healthBarGraphic = transform.Find("HealthBar").gameObject;
    }

    void Update()
    {
        UpdateHealthBar();
    }
        private void UpdateHealthBar()
    {
        healthBarGraphic.transform.rotation = mainCam.transform.rotation;
        healthBarGraphic.transform.position = transform.position + offset; 
    }

    public void TakeDamage(int _d, bool _isCrit)
    {
        DamagePopup.Create(transform.position, _d, _isCrit);
        //Debug.Log($"Enemy Takes Damage {_d}, and it is CRIT {_isCrit}");
    }
}
