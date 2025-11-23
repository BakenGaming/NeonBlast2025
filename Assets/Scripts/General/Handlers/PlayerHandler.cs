using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerHandler : MonoBehaviour, IHandler
{
    #region Variables
    [SerializeField] private PlayerStatsSO playerStatsSO;

    private StatSystem stats;
    private HealthSystem _healthSystem;
    private IAttackHandler _attackHandler;
    private Rigidbody2D playerRB;
    private BoxCollider2D playerCollider;
    private Vector2 moveInput;
    private float moveRot;
    private bool isPlayerActive=false;
    private Camera mainCam;
    private Transform firePoint;

    #endregion
    #region Initialize
    public void Initialize()
    {
        stats = new StatSystem(playerStatsSO);
        _healthSystem = new HealthSystem(stats.GetPlayerHealth());
        playerRB = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<BoxCollider2D>();
        _attackHandler = GetComponent<IAttackHandler>();
        firePoint = transform.Find("FirePoint");
        mainCam = Camera.main;

        isPlayerActive = true;
    }

    #endregion

        #region Get Functions
    public HealthSystem GetHealthSystem()
    {
        return _healthSystem;
    }

    public StatSystem GetStatSystem()
    {
        return stats;
    }
    #endregion
    #region Handle Player Functions
    public void HandleDeath()
    {
        throw new System.NotImplementedException();
    }

    public void UpdateHealth()
    {
        throw new System.NotImplementedException();
    }
    #endregion
    #region Handle Actions From Input
    private void HandleAttack()
    {
        //_attackHandler.Attack(firePoint, stats.GetAttackDamage(), stats.GetBulletVelocity());
    }
    #endregion
    #region Loops
    void Update()
    {
        if(!isPlayerActive) return;
        if(!GameManager.i.GetIsPaused()) moveInput = InputManager.i.moveInput;
        else playerRB.linearVelocity = Vector2.zero;

        //if(InputManager.i.attackPressed) HandleAttack();

        UpdateTimers();
    }
    private void FixedUpdate() 
    {
        if (!isPlayerActive) return;

        if(GameManager.i.GetIsPaused()) 
        {
            playerRB.linearVelocity = Vector2.zero;
            return;
        }
        
        Vector2 moveSpeed = moveInput.normalized;
        playerRB.linearVelocity = new Vector2(moveSpeed.x * stats.GetMoveSpeed(), 
            moveSpeed.y *.5f * stats.GetMoveSpeed());

        moveRot = Mathf.Atan2(moveInput.y, moveInput.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0,0,moveRot);

    }
    private void UpdateTimers()
    {

    }
    #endregion
    #region Logic Checks
    #endregion
}
