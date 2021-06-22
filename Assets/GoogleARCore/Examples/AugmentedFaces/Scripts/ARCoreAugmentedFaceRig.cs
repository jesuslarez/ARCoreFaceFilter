//-----------------------------------------------------------------------
// <copyright file="ARCoreAugmentedFaceRig.cs" company="Google LLC">
//
// Copyright 2018 Google LLC
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
// </copyright>
//-----------------------------------------------------------------------

namespace GoogleARCore.Examples.AugmentedFaces
{
    using System;
    using System.Collections.Generic;
    using GoogleARCore;
    using UnityEngine;

    /// <summary>
    /// Helper component to update face regions.
    /// </summary>
    public class ARCoreAugmentedFaceRig : MonoBehaviour
    {
        /// <summary>
        /// If true, this component will update itself using the first AugmentedFace detected by ARCore.
        /// </summary>
        public bool AutoBind = false;

        private static readonly Dictionary<AugmentedFaceRegion, string> _regionTransformNames =
            new Dictionary<AugmentedFaceRegion, string>()
            {
                { AugmentedFaceRegion.NoseTip, "NOSE_TIP" },
                { AugmentedFaceRegion.ForeheadLeft, "FOREHEAD_LEFT" },
                { AugmentedFaceRegion.ForeheadRight, "FOREHEAD_RIGHT" }
            };

        private AugmentedFace _augmentedFace;
        private List<AugmentedFace> augmentedFaceList = new List<AugmentedFace>();
        private Dictionary<AugmentedFaceRegion, Transform> _regionGameObjects =
            new Dictionary<AugmentedFaceRegion, Transform>();

        private List<Vector3> vertices;
        private List<Vector2> facecoordinates;
        private List<GameObject> labels;

        /// <summary>
        /// Gets or sets the ARCore AugmentedFace object that will be used to update the face region.
        /// </summary>
        public AugmentedFace AumgnetedFace
        {
            get
            {
                return _augmentedFace;
            }

            set
            {
                _augmentedFace = value;
                Update();
            }
        }

        /// <summary>
        /// The Unity Awake() method.
        /// </summary>
        public void Awake()
        {
            
            augmentedFaceList = new List<AugmentedFace>();
            InitializeFaceRegions();
        }
        public void Start()
        {
            labels = new List<GameObject>();
            labels.Add(GameObject.Find("LabelPrefab1"));
            labels.Add(GameObject.Find("LabelPrefab2"));
            labels.Add(GameObject.Find("LabelPrefab3"));
            labels.Add(GameObject.Find("LabelPrefab4"));
        }

        /// <summary>
        /// The Unity Update() method.
        /// </summary>
        public void Update()
        {
            if (!Application.isPlaying)
            {
                return;
            }

            if (AutoBind)
            {
                augmentedFaceList.Clear();
                Session.GetTrackables<AugmentedFace>(augmentedFaceList, TrackableQueryFilter.All);
                if (augmentedFaceList.Count != 0)
                {
                    _augmentedFace = augmentedFaceList[0];
                }
            }

            if (_augmentedFace == null)
            {
                return;
            }
            UpdateRegions();
            UpdateLabels();

        }


        private void UpdateLabels()
        {
            foreach (GameObject label in labels)
            {
                Vector3 labelForward = label.transform.forward;
                Vector3 cameraForward = Camera.main.transform.forward;
                Vector3 product = Vector3.Cross(cameraForward, labelForward);
                if (product.y == 0.0f)
                {
                    break;
                }
                float angle = Vector3.Angle(cameraForward, labelForward);
                print(angle);
                if (angle > 25f && product.y > 0f && labels.IndexOf(label) % 2 == 0)
                {
                    SetActiveAllChildren(label.transform, false);
                }
                else if (angle > 25f && product.y < 0f && labels.IndexOf(label) % 2 != 0)
                {
                    SetActiveAllChildren(label.transform, false);
                }
                else
                {
                    SetActiveAllChildren(label.transform, true);
                }
            }

        }
        private void SetActiveAllChildren(Transform transform, bool value)
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(value);

                SetActiveAllChildren(child, value);
            }
        }

        /// <summary>
        /// Method to initialize face region gameobject if not present.
        /// </summary>
        private void InitializeFaceRegions()
        {
            foreach (AugmentedFaceRegion region in _regionTransformNames.Keys)
            {
                string name = _regionTransformNames[region];
                Transform regionTransform = FindChildTransformRecursive(transform, name);
                if (regionTransform == null)
                {
                    GameObject newRegionObject = new GameObject(name);
                    newRegionObject.transform.SetParent(transform);
                    regionTransform = newRegionObject.transform;
                }

                _regionGameObjects[region] = regionTransform;
            }
        }

        private Transform FindChildTransformRecursive(Transform target, string name)
        {
            if (target.name == name)
            {
                return target;
            }

            foreach (Transform child in target)
            {
                if (child.name.Contains(name))
                {
                    return child;
                }

                Transform result = FindChildTransformRecursive(child, name);
                if (result != null)
                {
                    return result;
                }
            }

            return null;
        }

        /// <summary>
        /// Update all face regions associated with the mesh.
        /// </summary>
        private void UpdateRegions()
        {
            bool isTracking = _augmentedFace.TrackingState == TrackingState.Tracking;

            if (isTracking)
            {
                // Update the root transform;
                transform.position = _augmentedFace.CenterPose.position;
                transform.rotation = _augmentedFace.CenterPose.rotation;
            }

            foreach (AugmentedFaceRegion region in _regionGameObjects.Keys)
            {
                Transform regionTransform = _regionGameObjects[region];
                regionTransform.gameObject.SetActive(isTracking);
                if (isTracking)
                {
                    Pose regionPose = _augmentedFace.GetRegionPose(region);
                    regionTransform.position = regionPose.position;
                    regionTransform.rotation = regionPose.rotation;
                }
            }
        }
    }
}
