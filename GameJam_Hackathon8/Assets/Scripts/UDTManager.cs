using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using UnityEngine.UI;

public class UDTManager : MonoBehaviour, IUserDefinedTargetEventHandler
{
    UserDefinedTargetBuildingBehaviour udt_targetBuildingBehaviour;

    ObjectTracker objectTracker;
    DataSet dataset;

    public ImageTargetBehaviour targetBehaviour;
    public Text text;

    int targetCounter;

    private bool scanning;

    // Start is called before the first frame update
    void Start()
    {
        udt_targetBuildingBehaviour = GetComponent<UserDefinedTargetBuildingBehaviour>();
        if (udt_targetBuildingBehaviour)
        {
            udt_targetBuildingBehaviour.RegisterEventHandler(this);
        }
    }

    ImageTargetBuilder.FrameQuality udt_FrameQuality;

    // Update is called once per frame
    void Update()
    {
        
    }
    // This method updates the framequaliyt
    public void OnFrameQualityChanged(ImageTargetBuilder.FrameQuality frameQuality)
    {
        udt_FrameQuality = frameQuality;
    }

    public void OnInitialized()
    {
        objectTracker = TrackerManager.Instance.GetTracker<ObjectTracker>();
        if (objectTracker != null)
        {
            dataset = objectTracker.CreateDataSet(); // creating a new dataset
            objectTracker.ActivateDataSet(dataset);

        }
    }

    public void OnNewTrackableSource(TrackableSource trackableSource)
    {
        targetCounter++;

        objectTracker.DeactivateDataSet(dataset);
        dataset.CreateTrackable(trackableSource, targetBehaviour.gameObject);

        objectTracker.ActivateDataSet(dataset);
        udt_targetBuildingBehaviour.StartScanning();
    }

    public void BuildTarget()
    {
        if(udt_FrameQuality == ImageTargetBuilder.FrameQuality.FRAME_QUALITY_HIGH)
        {
            udt_targetBuildingBehaviour.BuildNewTarget("User", targetBehaviour.GetSize().x);
            text.text = "Builded";
        }
    }
}
