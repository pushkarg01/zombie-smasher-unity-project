using UnityEngine;
using UnityEngine.UI;
public class PlayerHealth : MonoBehaviour
{
    public int healthVal = 100;

    private Slider healthSlider;
    private GameObject UIHolder;

    void Start()
    {
        Slider slider = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<Slider>();
        healthSlider = slider;

        healthSlider.value = healthVal;

        UIHolder = GameObject.Find("UIHolder");
    }

    public void ApplyDamage(int damage)
    {
        healthVal -= damage;
        if (healthVal < 0) { healthVal = 0; }

        healthSlider.value = healthVal;
        if(healthVal == 0)
        {
           UIHolder.SetActive(false);
           GameplayController.instance.GameoverGame();
        }
    }
}
