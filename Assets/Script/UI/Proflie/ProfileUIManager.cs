using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfileUIManager : MonoBehaviour
{
    [SerializeField]private GameObject profileUI,profileStage1UI,
    profileStage2UI,SettingUI,AchievementsUI,TroopsUI,StagesUI,ButtonBack;
    [SerializeField]private SettingsManager settingsManager;

    private ProflieTroopUI proflieTroopUI;

    void Start()
    {
        proflieTroopUI=GetComponent<ProflieTroopUI>();   
    }
    public void OpenProfileUI()
    {
        profileUI.SetActive(true);
        profileStage1UI.SetActive(true);
    }
    public void CloseProfileUI()
    {
        profileUI.SetActive(false);
        foreach (Transform child in StagesUI.transform)
        {
            child.gameObject.SetActive(false);
        }
        foreach (Transform child in profileStage2UI.transform)
        {
            child.gameObject.SetActive(false);
        }
    }
    public void OpenSettingUI(){
        ActivateStage2UI();

        SettingUI.SetActive(true);

        settingsManager.setCurrentData();
    }
    public void OpenAchievementsUI(){
        ActivateStage2UI();

        AchievementsUI.SetActive(true);
    }
    public void OpenTroopsUI(){
        ActivateStage2UI();
        TroopsUI.SetActive(true);
        proflieTroopUI.DisplayTroopsInfo();
    }
    void ActivateStage2UI(){
        ButtonBack.SetActive(true);
        profileStage1UI.SetActive(false);
        profileStage2UI.SetActive(true);
    }
    public void BackToProfileStage1(){
        foreach (Transform child in profileStage2UI.transform)
        {
            child.gameObject.SetActive(false);
        }
        profileStage2UI.SetActive(false);
        profileStage1UI.SetActive(true);
    }
}
