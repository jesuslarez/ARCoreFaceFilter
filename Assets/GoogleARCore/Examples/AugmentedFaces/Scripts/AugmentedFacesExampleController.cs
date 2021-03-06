//-----------------------------------------------------------------------
// <copyright file="AugmentedFacesExampleController.cs" company="Google LLC">
//
// Copyright 2019 Google LLC
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
    using System.Collections.Generic;
    using GoogleARCore;
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// Controller for the AugmentedFaces sample scene.
    /// </summary>
    public class AugmentedFacesExampleController : MonoBehaviour
    {
        /// <summary>
        /// The game object that renders the face attachment on an Augmented Face.
        /// </summary>
        public GameObject FaceAttachment;
        public Sprite noFacesDetectedSprite;
        private bool isMenuActive = true;
        public Sprite headerSprite;
        /// <summary>
        /// True if the app is in the process of quitting due to an ARCore connection error,
        /// otherwise false.
        /// </summary>
        private bool _isQuitting = false;

        private List<AugmentedFace> _tempAugmentedFaces = new List<AugmentedFace>();

        /// <summary>
        /// The Unity Awake() method.
        /// </summary>
        public void Awake()
        {
            // Enable ARCore to target 60fps camera capture frame rate on supported devices.
            // Note, Application.targetFrameRate is ignored when QualitySettings.vSyncCount != 0.
            Application.targetFrameRate = 60;
        }

        /// <summary>
        /// The Unity Update method.
        /// </summary>
        public void Update()
        {
            UpdateApplicationLifecycle();

            // Gets all Augmented Faces.
            Session.GetTrackables<AugmentedFace>(_tempAugmentedFaces, TrackableQueryFilter.All);

            // Only allows the screen to sleep when ARCore can't detect a face.
            if (_tempAugmentedFaces.Count == 0)
            {
                Screen.sleepTimeout = SleepTimeout.SystemSetting;
                SetActiveAllChildren(FaceAttachment.transform, false);
                FaceAttachment.SetActive(false);
                GameObject.Find("HeaderButton").GetComponent<Image>().sprite = noFacesDetectedSprite;
            }
            else
            {
                Screen.sleepTimeout = SleepTimeout.NeverSleep;
                if (!isMenuActive)
                {
                    FaceAttachment.SetActive(true);
                    SetActiveAllChildren(FaceAttachment.transform, true);
                    if (GameObject.Find("HeaderButton").GetComponent<Image>().sprite == noFacesDetectedSprite)
                    {
                        GameObject.Find("HeaderButton").GetComponent<Image>().sprite = headerSprite;
                    }
                }
                else
                {
                    FaceAttachment.SetActive(false);
                    SetActiveAllChildren(FaceAttachment.transform, false);
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
        public void ActiveMenu(bool value)
        {
            isMenuActive = value;
        }
        /// <summary>
        /// Check and update the application lifecycle.
        /// </summary>
        private void UpdateApplicationLifecycle()
        {
            // Exit the app when the 'back' button is pressed.
            if (Input.GetKey(KeyCode.Escape))
            {
                Application.Quit();
            }

            if (_isQuitting)
            {
                return;
            }

            // Quit if ARCore was unable to connect and give Unity some time for the toast to
            // appear.
            if (Session.Status == SessionStatus.ErrorPermissionNotGranted)
            {
                ShowAndroidToastMessage("Camera permission is needed to run this application.");
                _isQuitting = true;
                Invoke("DoQuit", 0.5f);
            }
            else if (Session.Status.IsError())
            {
                ShowAndroidToastMessage(
                    "ARCore encountered a problem connecting.  Please start the app again.");
                _isQuitting = true;
                Invoke("DoQuit", 0.5f);
            }
        }

        /// <summary>
        /// Actually quit the application.
        /// </summary>
        private void DoQuit()
        {
            Application.Quit();
        }

        /// <summary>
        /// Show an Android toast message.
        /// </summary>
        /// <param name="message">Message string to show in the toast.</param>
        private void ShowAndroidToastMessage(string message)
        {
            AndroidJavaClass unityPlayer =
                new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject unityActivity =
                unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

            if (unityActivity != null)
            {
                AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");
                unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
                {
                    AndroidJavaObject toastObject = toastClass.CallStatic<AndroidJavaObject>(
                        "makeText", unityActivity, message, 0);
                    toastObject.Call("show");
                }));
            }
        }
    }
}
