using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
   public GameObject currentItem;
    private GameObject correctIcon;
    private GameObject incorrectIcon;

    void Awake()
    {
        // Find the icons by name (or tag, or custom component)
        correctIcon = transform.Find("CorrectIcon")?.gameObject;
        incorrectIcon = transform.Find("IncorrectIcon")?.gameObject;
    }

    public void ShowFeedback(bool isCorrect)
    {
        if (correctIcon != null) correctIcon.SetActive(isCorrect);
        if (incorrectIcon != null) incorrectIcon.SetActive(!isCorrect);

        CancelInvoke(nameof(HideFeedback));
        Invoke(nameof(HideFeedback), 1f);
    }

    void HideFeedback()
    {
        if (correctIcon != null) correctIcon.SetActive(false);
        if (incorrectIcon != null) incorrectIcon.SetActive(false);
    }
}
