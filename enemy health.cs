//enemy health
using system.collections;
using system.collections.generic;
using unityengine;
using unityengine.UI;

public class healthbar : monobehaviour
{
    
    public slider slider;

    public void setmaxhealth(int health)
    {
        slider.maxvalue = health;
        slider.value = health;
    }

    public void sethealth(int health)
    {
        slider.value = health;
    }
}