using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IHealth
{ 
    void TakeDamage(float damageAmount);
    void HealHealthPoints(float healAmount);
}

public class s : MonoBehaviour, IHealth
{
    public int ScoreAmount = 1;
    public UnityEngine.UI.Image HealthBar;
    public GameObject HitEffect;
    public UnityEvent OnHealthChanged;
    public UnityEvent OnHealthCleared;

    public float StartingHealth = 100f;
    public float DieTime = 1.5f;
    public float HealthPoints
    {
        get { return healthPoints; }
        set
        {
            healthPoints = Mathf.Clamp(value, 0f, StartingHealth);

            OnHealthChanged.Invoke();

            if(healthPoints <= 0f)
            {
                OnHealthCleared.Invoke();
                Die();
            }
        }
    }

    [SerializeField]
    private float healthPoints;

    private bool damagable = true;

    public void TakeDamage(float damageAmount)
    {
        if(damagable)
        {
            if (!HitEffect.activeSelf)
                HitEffect.SetActive(true);
            HealthPoints -= damageAmount;
            HealthBar.fillAmount = healthPoints / StartingHealth;
            StartCoroutine(TakeDamageCooldown());
        }
    }
    public IEnumerator TakeDamageCooldown()
    {
        damagable = false;
        yield return new WaitForSeconds(1f);
        HitEffect.SetActive(false);
        damagable = true;
    }

    public void HealHealthPoints(float healAmount)
    {
        HealthPoints += healAmount;
        HealthBar.fillAmount = healthPoints / StartingHealth;
    }

    private void Start()
    {
        HealthPoints = StartingHealth;
    }

    private void Die()
    {
        Debug.Log("Entity with name: " + this.gameObject.name + " Died honarably in the name of babylon");

        //EntityDied


        //Add score 
        HighScoreManager.Instance?.SetScore(ScoreAmount);

        StartCoroutine(DieAfterTime());
    }

    private IEnumerator DieAfterTime()
    {
        yield return new WaitForSeconds(DieTime);
        gameObject.SetActive(false);
    }
}
