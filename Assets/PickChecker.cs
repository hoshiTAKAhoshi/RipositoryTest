using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using System.Linq;

public class PickChecker : MonoBehaviour
{
    public GameObject targetObject;

    private Controller controller;
    private Finger[] fingers;
    private bool[] isGripFingers;

    private Vector3 ThumbFingerPos;
    private Vector3 IndexFingerPos;

    private Vector3 PosBitweenThumbAndIndex;
    // Use this for initialization
    void Start()
    {
        controller = new Controller();
        fingers = new Finger[5];
        isGripFingers = new bool[5];
    }

    // Update is called once per frame
    void Update()
    {
        Frame frame = controller.Frame();
        if (frame.Hands.Count != 0)
        {
            List<Hand> hand = frame.Hands;
            fingers = hand[0].Fingers.ToArray();
            isGripFingers = Array.ConvertAll(fingers, new Converter<Finger, bool>(i => i.IsExtended));
            Debug.Log(isGripFingers[0] + "," + isGripFingers[1] + "," + isGripFingers[2] + "," + isGripFingers[3] + "," + isGripFingers[4]);
            int extendedFingerCount = isGripFingers.Count(n => n == true);

            ThumbFingerPos = new Vector3(-fingers[0].TipPosition.x, -fingers[0].TipPosition.z, fingers[0].TipPosition.y);
            IndexFingerPos = new Vector3(-fingers[1].TipPosition.x, -fingers[1].TipPosition.z, fingers[1].TipPosition.y);

            PosBitweenThumbAndIndex = (ThumbFingerPos + IndexFingerPos) / 2;

            targetObject.transform.position = PosBitweenThumbAndIndex / 1000;
        }
    }
}