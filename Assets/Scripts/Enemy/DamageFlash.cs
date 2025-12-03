using System.Collections;
using UnityEngine;

public class DamageFlash : MonoBehaviour
{
    [SerializeField] private Color flashColor = Color.white;
    [SerializeField] private float flashTime = .25f;
    private SpriteRenderer sr;
    private Material mat;
    private Coroutine _damageFlashCoroutine;

    void Awake()
    {
        sr = transform.Find("Sprite").GetComponent<SpriteRenderer>();
        mat = sr.material;
    }
    public void CallDamageFlash()
    {
        _damageFlashCoroutine = StartCoroutine(DamageFlasher());
    }

    private IEnumerator DamageFlasher()
    {
        SetFlashColor();
        float currentFlashAmount = 0f;
        float elapsedTime = 0f;
        while (elapsedTime < flashTime)
        {
            elapsedTime += Time.deltaTime;
            currentFlashAmount = Mathf.Lerp(1f, 0f, (elapsedTime / flashTime));
            SetFlashAmount(currentFlashAmount);
            yield return null;
        }
    }
    private void SetFlashColor()
    {
        mat.SetColor("_FlashColor", flashColor);
    }
    private void SetFlashAmount(float v)
    {
        mat.SetFloat("_FlashAmount", v);
    }
}
