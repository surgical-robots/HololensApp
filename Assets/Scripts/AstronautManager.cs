﻿using HoloToolkit;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class AstronautManager : Singleton<AstronautManager>
{
    float expandAnimationCompletionTime;
    // Store a bool for whether our astronaut model is expanded or not.
    bool isModelExpanding = false;

    // KeywordRecognizer object.
    KeywordRecognizer keywordRecognizer;

    // Defines which function to call when a keyword is recognized.
    delegate void KeywordAction(PhraseRecognizedEventArgs args);
    Dictionary<string, KeywordAction> keywordCollection;

    void Start()
    {
        keywordCollection = new Dictionary<string, KeywordAction>();

        // Add keyword to start manipulation. 
        keywordCollection.Add("Move", MoveAstronautCommand);
        //select current object
        keywordCollection.Add("Video", VideoCommand);
        keywordCollection.Add("Body", BodyCommand);
        keywordCollection.Add("Robot", RobotCommand);
        // size control
        keywordCollection.Add("Bigger", MagnifyModelCommand);
        keywordCollection.Add("Smaller", ShrinkModelCommand);

        //visability
        keywordCollection.Add("Hide", HideModelCommand);
        keywordCollection.Add("Show", ShowModelCommand);

        /* TODO: DEVELOPER CODING EXERCISE 5.a */

        // 5.a: Add keyword Expand Model to call the ExpandModelCommand function.
        keywordCollection.Add("Expand", ExpandModelCommand);

        // 5.a: Add keyword Reset Model to call the ResetModelCommand function.
        keywordCollection.Add("Reset", ResetModelCommand);


        // Initialize KeywordRecognizer with the previously added keywords.
        keywordRecognizer = new KeywordRecognizer(keywordCollection.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
        keywordRecognizer.Start();
    }

    void OnDestroy()
    {
        keywordRecognizer.Dispose();
    }

    private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        KeywordAction keywordAction;

        if (keywordCollection.TryGetValue(args.text, out keywordAction))
        {
            keywordAction.Invoke(args);
        }
    }

    private void MoveAstronautCommand(PhraseRecognizedEventArgs args)
    {
        GestureManager.Instance.Transition(GestureManager.Instance.ManipulationRecognizer);
    }

    private void ResetModelCommand(PhraseRecognizedEventArgs args)
    {
        // Reset local variables.
        isModelExpanding = false;

        // Disable the expanded model.
        ExpandModel.Instance.ExpandedModel.SetActive(false);

        // Enable the idle model.
        ExpandModel.Instance.gameObject.SetActive(true);

        // Enable the animators for the next time the model is expanded.
        Animator[] expandedAnimators = ExpandModel.Instance.ExpandedModel.GetComponentsInChildren<Animator>();
        foreach (Animator animator in expandedAnimators)
        {
            animator.enabled = true;
        }

        ExpandModel.Instance.Reset();
    }

    private void ExpandModelCommand(PhraseRecognizedEventArgs args)
    {
        // Swap out the current model for the expanded model.
        GameObject currentModel = ExpandModel.Instance.gameObject;

        ExpandModel.Instance.ExpandedModel.transform.position = currentModel.transform.position;
        ExpandModel.Instance.ExpandedModel.transform.rotation = currentModel.transform.rotation;
        ExpandModel.Instance.ExpandedModel.transform.localScale = currentModel.transform.localScale;

        currentModel.SetActive(false);
        ExpandModel.Instance.ExpandedModel.SetActive(true);

        // Play animation.  Ensure the Loop Time check box is disabled in the inspector for this animation to play it once.
        Animator[] expandedAnimators = ExpandModel.Instance.ExpandedModel.GetComponentsInChildren<Animator>();
        // Set local variables for disabling the animation.
        if (expandedAnimators.Length > 0)
        {
            expandAnimationCompletionTime = Time.realtimeSinceStartup + expandedAnimators[0].runtimeAnimatorController.animationClips[0].length * 0.9f;
        }

        // Set the expand model flag.
        isModelExpanding = true;

        ExpandModel.Instance.Expand();
    }
    //bigger
    private void MagnifyModelCommand(PhraseRecognizedEventArgs args)
    {
        GameObject newG = GameObject.Find("Managers");
        ManifyModel newMa = newG.GetComponent<ManifyModel>();
        int cm = newMa.currentModel;
        if (cm == 0)
            newMa.biggerV = 0.01F; //for video
        if (cm == 1)
            newMa.biggerB = 0.005F; //for body
        if (cm == 2)
            newMa.biggerR = 0.0002F;//for robot
    }
    //smaller
    private void ShrinkModelCommand(PhraseRecognizedEventArgs args)
    {
        GameObject newG = GameObject.Find("Managers");
        ManifyModel newMa = newG.GetComponent<ManifyModel>();
        int cm = newMa.currentModel;
        if (cm == 0)
            newMa.biggerV =-0.01F; //for video
        if (cm == 1)
            newMa.biggerB =-0.005F; //for body
        if (cm == 2)
            newMa.biggerR = -0.0002F;//for robot
    }
    
    //hide

    private void HideModelCommand(PhraseRecognizedEventArgs args)
    {
        GameObject newG = GameObject.Find("Managers");
        ManifyModel newMa = newG.GetComponent<ManifyModel>();
        int temp = newMa.currentModel;
        if (temp == 0)
            newMa.hide = true;
       
    }
    //show
    private void ShowModelCommand(PhraseRecognizedEventArgs args)
    {
        GameObject newG = GameObject.Find("Managers");
        ManifyModel newMa = newG.GetComponent<ManifyModel>();
        int temp = newMa.currentModel;
        if (temp==0)
        newMa.hide = false; 

    }

    //update current gameobject
    private void VideoCommand(PhraseRecognizedEventArgs args)
    {
        GameObject newG = GameObject.Find("Managers");
        ManifyModel newMa = newG.GetComponent<ManifyModel>();
        newMa.currentModel = 0;
    }
    private void BodyCommand(PhraseRecognizedEventArgs args)
    {
        GameObject newG = GameObject.Find("Managers");
        ManifyModel newMa = newG.GetComponent<ManifyModel>();
        newMa.currentModel = 1;
    }
    private void RobotCommand(PhraseRecognizedEventArgs args)
    {
        GameObject newG = GameObject.Find("Managers");
        ManifyModel newMa = newG.GetComponent<ManifyModel>();
        newMa.currentModel = 2;
    }

    public void Update()
    {
        if (isModelExpanding && Time.realtimeSinceStartup >= expandAnimationCompletionTime)
        {
            isModelExpanding = false;

            Animator[] expandedAnimators = ExpandModel.Instance.ExpandedModel.GetComponentsInChildren<Animator>();

            foreach (Animator animator in expandedAnimators)
            {
                animator.enabled = false;
            }
        }
    }
}