using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CharacterCustomization : MonoBehaviour
{
    public List<Customization> Customizations;
    public int currentCustomizationIndex;
    public Customization CurrentCustomization {get; private set;}

    public IndexData saveData;

    [SerializeField] private TextMeshProUGUI bodyColorText;
    [SerializeField] private TextMeshProUGUI trailText;

    public Animator anim;

    public void Save()
    {
        saveData.materialIndex = Customizations[0].materialIndex;
        saveData.subObjectIndex = Customizations[2].subObjectIndex;

        SaveSystem.SavePlayer(saveData);

        SceneManager.LoadScene("Gameplay");

        FindObjectOfType<AudioManager>().StopPlaying("NylonFlapping");
        FindObjectOfType<AudioManager>().StopPlaying("Wind");
    }

    public void Load()
    {
        saveData = SaveSystem.LoadPlayer();

        Customizations[0].materialIndex = saveData.materialIndex;
        foreach (var customization in Customizations)
        {
            customization.subObjectIndex = saveData.subObjectIndex;
            customization.UpdateRenderers();
            if(anim == null)
            {
                customization.UpdateSubObjects();                
            }                 
        }

        if(bodyColorText && trailText != null)
        {
            bodyColorText.text = Customizations[0].materialIndex.ToString();
            trailText.text = Customizations[2].subObjectIndex.ToString();
        }        
    }

    void Awake()
    {
        Load();        
    }

    public void SelectBodyColor(bool isForward)
    {
        currentCustomizationIndex = 0;
        CurrentCustomization = Customizations[currentCustomizationIndex];
        if(isForward)
        {
            CurrentCustomization.NextMaterial();
        }
        else
        {
            CurrentCustomization.PreviousMaterial();
        }

        if(bodyColorText != null)
        {
            bodyColorText.text = CurrentCustomization.materialIndex.ToString();
        }        
    }

    public void SelectLTrail(bool isForward)
    {
        currentCustomizationIndex = 1;
        CurrentCustomization = Customizations[currentCustomizationIndex];
        if(isForward)
        {
            CurrentCustomization.NextSubObject();
        }
        else
        {
            CurrentCustomization.PreviousSubObject();
        }        

        if(trailText != null)
        {
            trailText.text = CurrentCustomization.subObjectIndex.ToString();
        }        
    }

    public void SelectRTrail(bool isForward)
    {
        currentCustomizationIndex = 2;
        CurrentCustomization = Customizations[currentCustomizationIndex];
        if(isForward)
        {
            CurrentCustomization.NextSubObject();
        }
        else
        {
            CurrentCustomization.PreviousSubObject();
        }        

        if(trailText != null)
        {
            trailText.text = CurrentCustomization.subObjectIndex.ToString();
        } 
    }
}