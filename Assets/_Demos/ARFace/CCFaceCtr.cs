using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARKit;

public class CCFaceCtr : MonoBehaviour
{
    public SkinnedMeshRenderer mFaceRenderer;
    public ARFaceManager _arFaceManager;
    // Start is called before the first frame update
    void Start()
    {
	    Debug.Log("unity: ccface start: begin");
// #if UNITY_IOS && !UNITY_EDITOR
        m_ARKitFaceSubsystem = _arFaceManager.subsystem as ARKitFaceSubsystem;
        Debug.Log("unity: m_ARKitFaceSubsystem: inited: "+!(m_ARKitFaceSubsystem==null));
// #endif
	    initBlendShape();
	    Debug.Log("unity: ccface start: initBlendShape: done");
        _arFaceManager.facesChanged += (faces) =>
        {
            if (faces.updated.Count>0)
            {
                m_Face = faces.updated[0];
                UpdateFaceFeatures();
            }
            // else
            // {
	           //  m_Face = null;
            // }
        };
        Debug.Log("unity: ccface start: done");

    }
    
    

// #if UNITY_IOS && !UNITY_EDITOR
        ARKitFaceSubsystem m_ARKitFaceSubsystem;

        // Dictionary<ARKitBlendShapeLocation, int> m_FaceArkitBlendShapeIndexMap = new Dictionary<ARKitBlendShapeLocation, int>();
// #endif

    ARFace m_Face;
    
    void UpdateFaceFeatures()
    {
        if (mFaceRenderer == null || !mFaceRenderer.enabled || mFaceRenderer.sharedMesh == null)
        {
            return;
        }


// #if UNITY_IOS && !UNITY_EDITOR
            using (var blendShapes = m_ARKitFaceSubsystem.GetBlendShapeCoefficients(m_Face.trackableId, Allocator.Temp))
            {
	            Debug.Log("unity: UpdateFaceFeatures: "+mBlendShapeIndexMap.Keys.Count);
                foreach (var featureCoefficient in blendShapes)
                {
                    int mappedBlendShapeIndex;
                    int tmp = (int) featureCoefficient.blendShapeLocation;
                    Debug.Log("unity: mappedBlendShapeIndex 1: "+tmp);
                    
                    if (mBlendShapeIndexMap.TryGetValue((CustomARKitBlendShapeLocation)tmp, out mappedBlendShapeIndex))
                    {
	                    if (mappedBlendShapeIndex >= 0)
                        {
	                        setBlendShapeWeight((CustomARKitBlendShapeLocation)tmp, featureCoefficient.coefficient * 100f);
	                        // mFaceRenderer.SetBlendShapeWeight(mappedBlendShapeIndex, featureCoefficient.coefficient * 100f);
                        }
                    }
                }
            }
// #endif
    }
    
    
    protected Dictionary<CustomARKitBlendShapeLocation, int> mBlendShapeIndexMap = new Dictionary<CustomARKitBlendShapeLocation, int>();
	protected void initBlendShape()
	{
		Debug.Log("unity: ccface start: initBlendShape: begin");

		registeBlendShape(CustomARKitBlendShapeLocation.BrowDownLeft, "A02_Brow_Down_Left");
		registeBlendShape(CustomARKitBlendShapeLocation.BrowDownRight, "A03_Brow_Down_Right");
		registeBlendShape(CustomARKitBlendShapeLocation.BrowInnerUp, "A01_Brow_Inner_Up");
		registeBlendShape(CustomARKitBlendShapeLocation.BrowOuterUpLeft, "A04_Brow_Outer_Up_Left");
		registeBlendShape(CustomARKitBlendShapeLocation.BrowOuterUpRight, "A05_Brow_Outer_Up_Right");
		registeBlendShape(CustomARKitBlendShapeLocation.CheekPuff, "A20_Cheek_Puff");
		registeBlendShape(CustomARKitBlendShapeLocation.CheekSquintLeft, "A21_Cheek_Squint_Left");
		registeBlendShape(CustomARKitBlendShapeLocation.CheekSquintRight, "A22_Cheek_Squint_Right");
		registeBlendShape(CustomARKitBlendShapeLocation.EyeBlinkLeft, "A14_Eye_Blink_Left");
		registeBlendShape(CustomARKitBlendShapeLocation.EyeBlinkRight, "A15_Eye_Blink_Right");
		registeBlendShape(CustomARKitBlendShapeLocation.EyeLookDownLeft, "A08_Eye_Look_Down_Left");
		registeBlendShape(CustomARKitBlendShapeLocation.EyeLookDownRight, "A09_Eye_Look_Down_Right");
		registeBlendShape(CustomARKitBlendShapeLocation.EyeLookInLeft, "A11_Eye_Look_In_Left");
		registeBlendShape(CustomARKitBlendShapeLocation.EyeLookInRight, "A12_Eye_Look_In_Right");
		registeBlendShape(CustomARKitBlendShapeLocation.EyeLookOutLeft, "A10_Eye_Look_Out_Left");
		registeBlendShape(CustomARKitBlendShapeLocation.EyeLookOutRight, "A13_Eye_Look_Out_Right");
		registeBlendShape(CustomARKitBlendShapeLocation.EyeLookUpLeft, "A06_Eye_Look_Up_Left");
		registeBlendShape(CustomARKitBlendShapeLocation.EyeLookUpRight, "A07_Eye_Look_Up_Right");
		registeBlendShape(CustomARKitBlendShapeLocation.EyeSquintLeft, "A16_Eye_Squint_Left");
		registeBlendShape(CustomARKitBlendShapeLocation.EyeSquintRight, "A17_Eye_Squint_Right");
		registeBlendShape(CustomARKitBlendShapeLocation.EyeWideLeft, "A18_Eye_Wide_Left");
		registeBlendShape(CustomARKitBlendShapeLocation.EyeWideRight, "A19_Eye_Wide_Right");
		registeBlendShape(CustomARKitBlendShapeLocation.JawForward, "A26_Jaw_Forward");
		registeBlendShape(CustomARKitBlendShapeLocation.JawLeft, "A27_Jaw_Left");
		registeBlendShape(CustomARKitBlendShapeLocation.JawOpen, "A25_Jaw_Open");
		registeBlendShape(CustomARKitBlendShapeLocation.JawRight, "A28_Jaw_Right");
		registeBlendShape(CustomARKitBlendShapeLocation.MouthClose, "A37_Mouth_Close");
		registeBlendShape(CustomARKitBlendShapeLocation.MouthDimpleLeft, "A42_Mouth_Dimple_Left");
		registeBlendShape(CustomARKitBlendShapeLocation.MouthDimpleRight, "A43_Mouth_Dimple_Right");
		registeBlendShape(CustomARKitBlendShapeLocation.MouthFrownLeft, "A40_Mouth_Frown_Left");
		registeBlendShape(CustomARKitBlendShapeLocation.MouthFrownRight, "A41_Mouth_Frown_Right");
		registeBlendShape(CustomARKitBlendShapeLocation.MouthFunnel, "A29_Mouth_Funnel");
		registeBlendShape(CustomARKitBlendShapeLocation.MouthLeft, "A31_Mouth_Left");
		registeBlendShape(CustomARKitBlendShapeLocation.MouthLowerDownLeft, "A46_Mouth_Lower_Down_Left");
		registeBlendShape(CustomARKitBlendShapeLocation.MouthLowerDownRight, "A47_Mouth_Lower_Down_Right");
		registeBlendShape(CustomARKitBlendShapeLocation.MouthPressLeft, "A48_Mouth_Press_Left");
		registeBlendShape(CustomARKitBlendShapeLocation.MouthPressRight, "A49_Mouth_Press_Right");
		registeBlendShape(CustomARKitBlendShapeLocation.MouthPucker, "A30_Mouth_Pucker");
		registeBlendShape(CustomARKitBlendShapeLocation.MouthRight, "A32_Mouth_Right");
		registeBlendShape(CustomARKitBlendShapeLocation.MouthRollLower, "A34_Mouth_Roll_Lower");
		registeBlendShape(CustomARKitBlendShapeLocation.MouthRollUpper, "A33_Mouth_Roll_Upper");
		registeBlendShape(CustomARKitBlendShapeLocation.MouthShrugLower, "A36_Mouth_Shrug_Lower");
		registeBlendShape(CustomARKitBlendShapeLocation.MouthShrugUpper, "A35_Mouth_Shrug_Upper");
		registeBlendShape(CustomARKitBlendShapeLocation.MouthSmileLeft, "A38_Mouth_Smile_Left");
		registeBlendShape(CustomARKitBlendShapeLocation.MouthSmileRight, "A39_Mouth_Smile_Right");
		registeBlendShape(CustomARKitBlendShapeLocation.MouthStretchLeft, "A50_Mouth_Stretch_Left");
		registeBlendShape(CustomARKitBlendShapeLocation.MouthStretchRight, "A51_Mouth_Stretch_Right");
		registeBlendShape(CustomARKitBlendShapeLocation.MouthUpperUpLeft, "A44_Mouth_Upper_Up_Left");
		registeBlendShape(CustomARKitBlendShapeLocation.MouthUpperUpRight, "A45_Mouth_Upper_Up_Right");
		registeBlendShape(CustomARKitBlendShapeLocation.NoseSneerLeft, "A23_Nose_Sneer_Left");
		registeBlendShape(CustomARKitBlendShapeLocation.NoseSneerRight, "A24_Nose_Sneer_Right");

		// mBlendShapeTeethJawOpenIndex =
		// 	mTeethRenderer.sharedMesh.GetBlendShapeIndex("YaChi.A25_Jaw_Open");
		// mBlendShapeTongueJawOpenIndex =
		// 	mTongueRenderer.sharedMesh.GetBlendShapeIndex("SheTou.A25_Jaw_Open");
		// mBlendShapeTongueTongueOutIndex =
		// 	mTongueRenderer.sharedMesh.GetBlendShapeIndex("Morpher_CC_Base_Tongue.A52_Tongue_Out");
	}
	protected void registeBlendShape(CustomARKitBlendShapeLocation shape, string name)
	{
		mBlendShapeIndexMap.Add(shape, mFaceRenderer.sharedMesh.GetBlendShapeIndex(name));
		// Debug.Log("unity: ccface start: registeBlendShape: done");
	}
	public void setBlendShapeWeight(CustomARKitBlendShapeLocation location, float weight)
	{
		int index = getBlendShapeIndex(location);
		if (location == CustomARKitBlendShapeLocation.TongueOut) {
			// mTongueRenderer?.SetBlendShapeWeight(mBlendShapeTongueTongueOutIndex, weight);
		} else if(location == CustomARKitBlendShapeLocation.JawOpen) {
			
			// mTeethRenderer?.SetBlendShapeWeight(mBlendShapeTeethJawOpenIndex, weight);
			// mTongueRenderer?.SetBlendShapeWeight(mBlendShapeTongueJawOpenIndex, weight);
			mFaceRenderer?.SetBlendShapeWeight(index, weight);
		}
		else 
		{
			Debug.Log("unity: mappedBlendShape real locate: "+(int)location+",,"+ index);

			mFaceRenderer?.SetBlendShapeWeight(index, weight);
		}
	}
	public int getBlendShapeIndex(CustomARKitBlendShapeLocation location) 
	{
		if (!mBlendShapeIndexMap.TryGetValue(location, out int mappedBlendShapeIndex))
		{
			return -1;
		}
		return mappedBlendShapeIndex;
	}
	public enum CustomARKitBlendShapeLocation
	{
		BrowDownLeft = 0,
		BrowDownRight = 1,
		BrowInnerUp = 2,
		BrowOuterUpLeft = 3,
		BrowOuterUpRight = 4,
		CheekPuff = 5,
		CheekSquintLeft = 6,
		CheekSquintRight = 7,
		EyeBlinkLeft = 8,
		EyeBlinkRight = 9,
		EyeLookDownLeft = 10,
		EyeLookDownRight = 11,
		EyeLookInLeft = 12,
		EyeLookInRight = 13,
		EyeLookOutLeft = 14,
		EyeLookOutRight = 15,
		EyeLookUpLeft = 16,
		EyeLookUpRight = 17,
		EyeSquintLeft = 18,
		EyeSquintRight = 19,
		EyeWideLeft = 20,
		EyeWideRight = 21,
		JawForward = 22,
		JawLeft = 23,
		JawOpen = 24,
		JawRight = 25,
		MouthClose = 26,
		MouthDimpleLeft = 27,
		MouthDimpleRight = 28,
		MouthFrownLeft = 29,
		MouthFrownRight = 30,
		MouthFunnel = 31,
		MouthLeft = 32,
		MouthLowerDownLeft = 33,
		MouthLowerDownRight = 34,
		MouthPressLeft = 35,
		MouthPressRight = 36,
		MouthPucker = 37,
		MouthRight = 38,
		MouthRollLower = 39,
		MouthRollUpper = 40,
		MouthShrugLower = 41,
		MouthShrugUpper = 42,
		MouthSmileLeft = 43,
		MouthSmileRight = 44,
		MouthStretchLeft = 45,
		MouthStretchRight = 46,
		MouthUpperUpLeft = 47,
		MouthUpperUpRight = 48,
		NoseSneerLeft = 49,
		NoseSneerRight = 50,
		TongueOut = 51
	}

}
