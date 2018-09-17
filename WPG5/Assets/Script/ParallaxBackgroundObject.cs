using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ParallaxBackgroundObject : MonoBehaviour {
    private bool stayInScreen;
    public bool StayInScreen { set { stayInScreen = StayInScreen; } get { return stayInScreen; } }

    private float duration;
    public float Duration { set { duration = Duration; } get { return duration; } }

    public Vector2 getBoundsSize() {
        return GetComponent<Renderer>().bounds.size;
    }
}
