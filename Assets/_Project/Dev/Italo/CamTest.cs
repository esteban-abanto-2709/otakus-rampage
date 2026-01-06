using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class CamTest : MonoBehaviour
{
    [Range (0,1)]
    public float testValue;
    PostProcessingBehaviour postProfile;



    // Start is called before the first frame update
    void Start()
    {

        postProfile = GetComponent<PostProcessingBehaviour>();
        
    }

    // Update is called once per frame
    void Update()
    {
        ChangeSaturationSetting();
    }

    void ChangeSaturationSetting()
    {
        //Para encontrar la ubicación debes de ver el PostProcessingProfile en vista Debug

        ColorGradingModel.Settings _settings = postProfile.profile.colorGrading.settings;
        _settings.basic.saturation = testValue;
        postProfile.profile.colorGrading.settings = _settings;

    }
}
