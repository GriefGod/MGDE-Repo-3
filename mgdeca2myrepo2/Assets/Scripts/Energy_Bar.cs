using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Energy_Bar : MonoBehaviour
{
    public Image currentEnergy;
    public  float currentTime;
    public float maxduration;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void UpdateEnergyBar(float currentTime , float maxduration)
    {
        float ratio = currentTime / maxduration;
        currentEnergy.rectTransform.localScale = new Vector3(ratio, 1, 1);
    }

    private void EnergyDepletion(float deplete)
    {

    }
}
