using UnityEngine;
using UnityEngine.Audio;

public class StaticVariables : MonoBehaviour
{
    public static StaticVariables i;
    [SerializeField] private LayerMask whatIsGround, whatIsPlayer, whatIsEnemy, 
        whatIsCollectable, whatIsUI, whatIsObstacleLayer;
    [SerializeField] private AudioMixerGroup masterMixer, sfxMixer, musicMixer;
    [SerializeField] private float enemyDetectionRadius;
    [SerializeField] private float playerDetectionRadius;

    private void Awake() 
    {
        i = this;
    }

    public LayerMask GetGroundLayer() { return whatIsGround; }
    public LayerMask GetPlayerLayer() { return whatIsPlayer; }
    public LayerMask GetEnemyLayer() { return whatIsEnemy; }
    public LayerMask GetObstacleLayer(){return whatIsObstacleLayer;}
    public LayerMask GetCollectableLayer() { return whatIsCollectable; }
    public LayerMask GetUILayer(){ return whatIsUI; }
    public AudioMixerGroup GetMasterMixer(){ return masterMixer; }
    public AudioMixerGroup GetSFXMixer(){ return sfxMixer; }
    public AudioMixerGroup GetMusicMixer(){ return musicMixer; }
    public float GetEnemyDetectionRadius(){return enemyDetectionRadius;}
    public float GetPlayerDetectionRadius(){return playerDetectionRadius;}

}
