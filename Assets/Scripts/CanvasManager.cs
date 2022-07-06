using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    public SlingShotRubber SlingShotRubber;
    public DestroyAndCount DestroyAndCount;
    public Image FilltheBar;
    public TMP_Text ShotsCount;
    public TMP_Text percent;
    public TMP_Text percentfinishgame;
    public float OnePercent;
    public GameObject TaptoStartPanel;
    public GameObject FinishGamePanel;
    
   

    public void Start()
    {
        TaptoStartPanel.SetActive(true);
    }
    void Update()
    {
        OnePercent = DestroyAndCount.CountGameObjects * 1.04f;
        percent.text = OnePercent.ToString("0") + "%";
        percentfinishgame.text = OnePercent.ToString("0") + "%";
        ShotsCount.text = SlingShotRubber.shotscount.ToString() + "  Shot Left";
        FilltheBar.fillAmount = OnePercent/100f;
        if(SlingShotRubber.shotscount < 1)
        {   
            ShotsCount.text = "No More Shots";
                StartCoroutine("FinishGamePanelDelay");      
        }      
    }
    IEnumerator FinishGamePanelDelay()
    {
        yield return new WaitForSeconds(6f);
        FinishGamePanel.SetActive(true);
    }
    
    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void NextButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

