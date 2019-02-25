using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using Vuforia;

public class EnableVideo : MonoBehaviour,
 ITrackableEventHandler
{

    private VideoPlayer vp;
    public GameObject quad;

    private TrackableBehaviour mTrackableBehaviour;
    // Start is called before the first frame update
    void Start()
    {
        vp = quad.GetComponent<VideoPlayer>();

        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }

    }

    public void OnTrackableStateChanged(
                                    TrackableBehaviour.Status previousStatus,
                                    TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            // Play audio when target is found
            Debug.Log("Video Playing");
            vp.Play();
        }
        else
        {
            Debug.Log("Video Playing");

            // Stop audio when target is lost
            vp.Stop();
        }
    }
}
