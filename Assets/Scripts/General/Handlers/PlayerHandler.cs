using UnityEngine;
using UnityEngine.UI;

public class PlayerHandler : MonoBehaviour, IHandler
{
    #region Variables
    [SerializeField] private PlayerStatsSO playerStatsSO;
    [SerializeField] private InputReader _input;
    private Vector3 offset = new Vector3(0f,-1.5f,0f);
    private StatSystem stats;
    private HealthSystem _healthSystem;
    private Rigidbody2D playerRB;
    private BoxCollider2D playerCollider;
    private Vector2 moveInput, lastRecordedInput, smoothMovementInput, smoothInputVelocity;
    private float moveRot;
    private bool isPlayerActive=false, playerIsMoving=false, facingRight=true;
    private Camera mainCam;
    private Transform firePoint;
    private GameObject healthBarGraphic;
    private Slider healthValueSlider;

    #endregion
    #region Initialize
    private void Awake()
    {
        Debug.Log($"Initialize {this}");
        Initialize();
    }
    public void Initialize()
    {
        ///TESTING ONLY
        GameManager.i.SetPlayerGO(gameObject);
        //*****************************************
        _input.moveEvent += SetMoveDirection;
        stats = new StatSystem(playerStatsSO);
        _healthSystem = new HealthSystem(stats.GetHealth());
        playerRB = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<BoxCollider2D>();
        firePoint = transform.Find("FirePoint");
        mainCam = Camera.main;
        healthBarGraphic = transform.Find("HealthBar").gameObject;
        healthValueSlider = healthBarGraphic.transform.Find("Slider").GetComponent<Slider>();
        healthValueSlider.value = _healthSystem.GetCurrentHealth();
        GetComponent<WeaponSystem>().InitializeFreshGame(GameManager.i.GetStartingWeapon());

        isPlayerActive = true;
    }
    void OnDisable()
    {
        _input.moveEvent -= SetMoveDirection;
    }

    #endregion

    #region Get Functions
    public HealthSystem GetHealthSystem()
    {
        return _healthSystem;
    }

    public StatSystem GetStats()
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
    private void SetMoveDirection(Vector2 _mInput)
    {
        moveInput = _mInput;
    }
    #endregion
    #region Loops
    void Update()
    {
        if(!isPlayerActive) return;
        if (GameManager.i.GetIsPaused()) return;

        if(moveInput == Vector2.zero) playerIsMoving = false;
        else playerIsMoving = true;

        UpdateTimers();
        UpdateHealthBar();
    }
    private void FixedUpdate() 
    {
        if (!isPlayerActive) return;

        if(GameManager.i.GetIsPaused()) 
        {
            playerRB.linearVelocity = Vector2.zero;
            return;
        }
        SmoothedMovement();
        RotateShip();
        //Vector2 moveSpeed = moveInput.normalized;
        //playerRB.linearVelocity = new Vector2(moveInput.x * stats.GetMoveSpeed(), 
        //    moveInput.y *.5f * stats.GetMoveSpeed());
        //FlipPlayer();
        //moveRot = Mathf.Atan2(moveInput.y, moveInput.x) * Mathf.Rad2Deg -90f;
        //transform.rotation = Quaternion.Euler(0,0,moveRot);

    }
    private void UpdateHealthBar()
    {
        healthBarGraphic.transform.rotation = mainCam.transform.rotation;
        healthBarGraphic.transform.position = transform.position + offset; 
    }

    private void FlipPlayer()
    {
        if (moveInput.x < 0)
        {
            playerRB.transform.localScale = new Vector3(-1f, 1f, 1f);
            facingRight = false;
        }
        else if (moveInput.x > 0)
        {
            playerRB.transform.localScale = Vector3.one;
            facingRight = true;
        }
    } 
    private void SmoothedMovement()
    {
        smoothMovementInput = Vector2.SmoothDamp(
            smoothMovementInput, moveInput,
            ref smoothInputVelocity, 0.25f);

        //if(smoothMovementInput == Vector2.zero) smoothMovementInput = lastRecordedInput;
        playerRB.linearVelocity = smoothMovementInput * stats.GetMoveSpeed() * Time.deltaTime;
    }
    private void RotateShip()
    {
        if(moveInput == Vector2.zero) return;

        Quaternion targetRotation = Quaternion.LookRotation(transform.forward, smoothMovementInput);
        Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 720f * Time.deltaTime);
        playerRB.SetRotation(rotation);
    }
    private void UpdateTimers()
    {

    }
    #endregion
    #region Logic Checks
    #endregion
    #region Get Functions
    public bool GetPlayerIsMoving(){return playerIsMoving;}
    #endregion
}
