using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARKit;

public class FaceCtr : MonoBehaviour
{
    public SkinnedMeshRenderer skinnedMeshRenderer;
    public ARFaceManager _arFaceManager;
    // Start is called before the first frame update
    void Start()
    {
// #if UNITY_IOS && !UNITY_EDITOR
        m_ARKitFaceSubsystem = (ARKitFaceSubsystem)_arFaceManager.subsystem;
// #endif
        CreateFeatureBlendMapping();
        _arFaceManager.facesChanged += (faces) =>
        {
            if (faces.updated.Count>0)
            {
                m_Face = faces.updated[0];
                UpdateFaceFeatures();
            }
        };
        
    }
    
    

// #if UNITY_IOS && !UNITY_EDITOR
        ARKitFaceSubsystem m_ARKitFaceSubsystem;

        Dictionary<ARKitBlendShapeLocation, int> m_FaceArkitBlendShapeIndexMap;
// #endif

    ARFace m_Face;
    
    void UpdateFaceFeatures()
    {
        if (skinnedMeshRenderer == null || !skinnedMeshRenderer.enabled || skinnedMeshRenderer.sharedMesh == null)
        {
            return;
        }

// #if UNITY_IOS && !UNITY_EDITOR
            using (var blendShapes = m_ARKitFaceSubsystem.GetBlendShapeCoefficients(m_Face.trackableId, Allocator.Temp))
            {
                foreach (var featureCoefficient in blendShapes)
                {
                    int mappedBlendShapeIndex;
                    if (m_FaceArkitBlendShapeIndexMap.TryGetValue(featureCoefficient.blendShapeLocation, out mappedBlendShapeIndex))
                    {
                        if (mappedBlendShapeIndex >= 0)
                        {
                            skinnedMeshRenderer.SetBlendShapeWeight(mappedBlendShapeIndex, featureCoefficient.coefficient * 100f);
                        }
                    }
                }
            }
// #endif
    }
    
    
    
    void CreateFeatureBlendMapping()
        {
            if (skinnedMeshRenderer == null || skinnedMeshRenderer.sharedMesh == null)
            {
                return;
            }

    #if UNITY_IOS && !UNITY_EDITOR
            const string strPrefix = "blendShape2.";
            m_FaceArkitBlendShapeIndexMap = new Dictionary<ARKitBlendShapeLocation, int>();

            m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.BrowDownLeft        ]   = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(strPrefix + "browDown_L");
            m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.BrowDownRight       ]   = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(strPrefix + "browDown_R");
            m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.BrowInnerUp         ]   = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(strPrefix + "browInnerUp");
            m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.BrowOuterUpLeft     ]   = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(strPrefix + "browOuterUp_L");
            m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.BrowOuterUpRight    ]   = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(strPrefix + "browOuterUp_R");
            m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.CheekPuff           ]   = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(strPrefix + "cheekPuff");
            m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.CheekSquintLeft     ]   = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(strPrefix + "cheekSquint_L");
            m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.CheekSquintRight    ]   = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(strPrefix + "cheekSquint_R");
            m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.EyeBlinkLeft        ]   = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(strPrefix + "eyeBlink_L");
            m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.EyeBlinkRight       ]   = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(strPrefix + "eyeBlink_R");
            m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.EyeLookDownLeft     ]   = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(strPrefix + "eyeLookDown_L");
            m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.EyeLookDownRight    ]   = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(strPrefix + "eyeLookDown_R");
            m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.EyeLookInLeft       ]   = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(strPrefix + "eyeLookIn_L");
            m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.EyeLookInRight      ]   = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(strPrefix + "eyeLookIn_R");
            m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.EyeLookOutLeft      ]   = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(strPrefix + "eyeLookOut_L");
            m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.EyeLookOutRight     ]   = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(strPrefix + "eyeLookOut_R");
            m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.EyeLookUpLeft       ]   = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(strPrefix + "eyeLookUp_L");
            m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.EyeLookUpRight      ]   = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(strPrefix + "eyeLookUp_R");
            m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.EyeSquintLeft       ]   = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(strPrefix + "eyeSquint_L");
            m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.EyeSquintRight      ]   = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(strPrefix + "eyeSquint_R");
            m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.EyeWideLeft         ]   = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(strPrefix + "eyeWide_L");
            m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.EyeWideRight        ]   = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(strPrefix + "eyeWide_R");
            m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.JawForward          ]   = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(strPrefix + "jawForward");
            m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.JawLeft             ]   = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(strPrefix + "jawLeft");
            m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.JawOpen             ]   = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(strPrefix + "jawOpen");
            m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.JawRight            ]   = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(strPrefix + "jawRight");
            m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.MouthClose          ]   = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(strPrefix + "mouthClose");
            m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.MouthDimpleLeft     ]   = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(strPrefix + "mouthDimple_L");
            m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.MouthDimpleRight    ]   = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(strPrefix + "mouthDimple_R");
            m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.MouthFrownLeft      ]   = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(strPrefix + "mouthFrown_L");
            m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.MouthFrownRight     ]   = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(strPrefix + "mouthFrown_R");
            m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.MouthFunnel         ]   = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(strPrefix + "mouthFunnel");
            m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.MouthLeft           ]   = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(strPrefix + "mouthLeft");
            m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.MouthLowerDownLeft  ]   = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(strPrefix + "mouthLowerDown_L");
            m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.MouthLowerDownRight ]   = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(strPrefix + "mouthLowerDown_R");
            m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.MouthPressLeft      ]   = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(strPrefix + "mouthPress_L");
            m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.MouthPressRight     ]   = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(strPrefix + "mouthPress_R");
            m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.MouthPucker         ]   = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(strPrefix + "mouthPucker");
            m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.MouthRight          ]   = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(strPrefix + "mouthRight");
            m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.MouthRollLower      ]   = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(strPrefix + "mouthRollLower");
            m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.MouthRollUpper      ]   = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(strPrefix + "mouthRollUpper");
            m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.MouthShrugLower     ]   = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(strPrefix + "mouthShrugLower");
            m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.MouthShrugUpper     ]   = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(strPrefix + "mouthShrugUpper");
            m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.MouthSmileLeft      ]   = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(strPrefix + "mouthSmile_L");
            m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.MouthSmileRight     ]   = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(strPrefix + "mouthSmile_R");
            m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.MouthStretchLeft    ]   = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(strPrefix + "mouthStretch_L");
            m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.MouthStretchRight   ]   = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(strPrefix + "mouthStretch_R");
            m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.MouthUpperUpLeft    ]   = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(strPrefix + "mouthUpperUp_L");
            m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.MouthUpperUpRight   ]   = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(strPrefix + "mouthUpperUp_R");
            m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.NoseSneerLeft       ]   = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(strPrefix + "noseSneer_L");
            m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.NoseSneerRight      ]   = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(strPrefix + "noseSneer_R");
            m_FaceArkitBlendShapeIndexMap[ARKitBlendShapeLocation.TongueOut           ]   = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(strPrefix + "tongueOut");
    #endif
        }
}
