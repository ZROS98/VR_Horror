using System;
using System.IO;
using UnityEngine;

namespace VR_Horror
{
    /*public class PupilDataController : MonoBehaviour
    {
        public GameObject VIDEOPLAYERGO;
        
        void SetStimulus(int _stimulus_trigger,
            string _stimulus_file_name,
            float _stimulus_duration,
            Vector4 _background_color,
            float _stimulus_offset_x,
            float _stimulus_offset_y,
            float _stimulus_scale_XY,
            float _stimulus_distance,
            int _scenarioCSVRowsIterator)
        {
            try
            {
                string _stimulusPath = "";
                
                if (_stimulus_file_name.StartsWith("http")) //internet source of stimulus detected
                {
                    if (_stimulus_file_name.EndsWith("png") || _stimulus_file_name.EndsWith("jpg")) //the stimulus is an image
                    {
                        _stimulusPath = _stimulus_file_name;
                        Debug.Log("the stimulus file is an image from internet resource, trying to load.");
                        StartCoroutine
                        (imageLoader.ChangeImageByPathAndSetGameObjectOffsetFromCameraCenter(_stimulusPath,
                            imageLoader.ScreenForStimulusDisplay, _background_color, _stimulus_offset_x,
                            _stimulus_offset_y,
                            _stimulus_scale_XY, _stimulus_distance));
                    }
                    else if (_stimulus_file_name.EndsWith("avi") ||
                             _stimulus_file_name.EndsWith("mp4")) //the stimulus is a video
                    {
                        _stimulusPath = _stimulus_file_name;
                        Debug.Log("the stimulus file is a video from internet resource, trying to load.");
                    }
                }
                else //if source of stimulus is stored locally
                {
                    if (_stimulus_file_name.EndsWith("png") || _stimulus_file_name.EndsWith("jpg"))
                        //  the stimulus is an image, so look for it in 'images' subfolder of the picked scenario folder ('_selectedScenarioName')
                    {
                        _stimulusPath = Application.dataPath + "/StreamingAssets/" + _selectedScenarioName + "/" +
                                        "images/" + _stimulus_file_name;
                        //Debug.Log("<color=orange>trying to load stimulus from path: </color>" + _stimulusPath);
                        try
                        {
                            if (File.Exists(_stimulusPath))
                            {
                                //  Debug.Log($"File exists at path <color=green>{_stimulusPath}</color>\n Trying to load stimulus");
                                try
                                {
                                    //turn off background image
                                    if (current_stimulus_trigger == calibration_trigger)
                                    {
                                        if (VIDEOPLAYERGO.activeInHierarchy)
                                            VIDEOPLAYERGO.SetActive(false); //deactivate videoplayer gameobject
                                        //   if (planeForImages.activeInHierarchy) planeForImages.SetActive(false);  //deactivate images display
                                        // planeForImages.
                                        if (ignoreImagesDuringCalibration)
                                        {
                                            planeForImages.SetActive(false);
                                        }

                                        imageLoader.SetBackgroundColor(_background_color);
                                        pupilEyetrackerTriggerSender.SendTriggerAnnotation(_stimulus_trigger
                                            .ToString());
                                        tcpTriggerSender.triggerMessageToBeSent = _stimulus_trigger.ToString();
                                        tcpTriggerSender.SendMessage();
                                    }
                                    else if (current_stimulus_trigger == time_limitless_trigger)
                                    {
                                        if (VIDEOPLAYERGO.activeInHierarchy)
                                            VIDEOPLAYERGO.SetActive(false); //deactivate videoplayer gameobject
                                        if (!planeForImages.activeInHierarchy)
                                            planeForImages.SetActive(true); //activate images display
                                        StartCoroutine(
                                            imageLoader.ChangeImageByPathAndSetGameObjectOffsetFromCameraCenter(
                                                _stimulusPath, imageLoader.ScreenForStimulusDisplay, _background_color,
                                                _stimulus_offset_x, _stimulus_offset_y, _stimulus_scale_XY,
                                                _stimulus_distance));
                                        pupilEyetrackerTriggerSender.SendTriggerAnnotation(_stimulus_trigger
                                            .ToString());
                                        tcpTriggerSender.triggerMessageToBeSent = _stimulus_trigger.ToString();
                                        tcpTriggerSender.SendMessage();
                                    }
                                    else if (current_stimulus_trigger == recording_start_trigger)
                                    {
                                        if (VIDEOPLAYERGO.activeInHierarchy)
                                            VIDEOPLAYERGO.SetActive(false); //deactivate videoplayer gameobject
                                        if (!planeForImages.activeInHierarchy)
                                            planeForImages.SetActive(false); //activate images display
                                        pupilEyetrackerTriggerSender.SendTriggerAnnotation(_stimulus_trigger
                                            .ToString());
                                        tcpTriggerSender.triggerMessageToBeSent = _stimulus_trigger.ToString();
                                        tcpTriggerSender.SendMessage();
                                    }
                                    else if (current_stimulus_trigger == recording_stop_trigger)
                                    {
                                        if (VIDEOPLAYERGO.activeInHierarchy)
                                            VIDEOPLAYERGO.SetActive(false); //deactivate videoplayer gameobject
                                        if (!planeForImages.activeInHierarchy)
                                            planeForImages.SetActive(false); //activate images display
                                        pupilEyetrackerTriggerSender.SendTriggerAnnotation(_stimulus_trigger
                                            .ToString());
                                        tcpTriggerSender.triggerMessageToBeSent = _stimulus_trigger.ToString();
                                        tcpTriggerSender.SendMessage();
                                    }
                                    else
                                    {
                                        if (VIDEOPLAYERGO.activeInHierarchy)
                                            VIDEOPLAYERGO.SetActive(false); //deactivate videoplayer gameobject
                                        if (!planeForImages.activeInHierarchy)
                                            planeForImages.SetActive(true); //activate images display
                                        StartCoroutine(
                                            imageLoader.ChangeImageByPathAndSetGameObjectOffsetFromCameraCenter(
                                                _stimulusPath, imageLoader.ScreenForStimulusDisplay, _background_color,
                                                _stimulus_offset_x, _stimulus_offset_y, _stimulus_scale_XY,
                                                _stimulus_distance));
                                        scenarioWaitingTimeForStimulusToRemainShown = _stimulus_duration;
                                        pupilEyetrackerTriggerSender.SendTriggerAnnotation(_stimulus_trigger
                                            .ToString());
                                        //  tcpClientTriggerSender.SendTriggerToIP(_stimulus_trigger.ToString());
                                        tcpTriggerSender.triggerMessageToBeSent = _stimulus_trigger.ToString();
                                        tcpTriggerSender.SendMessage();
                                    }
                                }
                                catch (Exception e)
                                {
                                    Debug.LogError($"Could not run file from {_stimulusPath}. Reason:\n {e}");
                                }
                            }
                            else
                            {
                                Debug.LogError(
                                    $"The file <color=red>{Path.GetFileName(_stimulusPath)}</color> at path<color=red>{_stimulusPath}</color> does not seem to exist. Please make sure all files necessary for the selected scenario are under the scenario folder in 'images' and 'videos' folders.");
                            }
                        }
                        catch (Exception e)
                        {
                            Debug.LogError(
                                $"Something went wrong with loading stimulus from path {_stimulusPath}. Reason: \n {e}");
                        }
                    }
                    else if (_stimulus_file_name.EndsWith("avi") || _stimulus_file_name.EndsWith("mp4") ||
                             _stimulus_file_name.EndsWith("mov") ||
                             _stimulus_file_name.EndsWith("asf")) //the stimulus is a video,
                    {
                        _stimulusPath = Application.dataPath + "/StreamingAssets/" + _selectedScenarioName + "/" +
                                        "videos/" + _stimulus_file_name;
                        // Debug.Log("<color=orange>trying video stimulus via path:</color>" + _stimulusPath);
                        try
                        {
                            if (File.Exists(_stimulusPath)) //the file exists at the path specified
                            {
                                //  Debug.Log($"File {_stimulus_file_name} exists at path <color=green>{_stimulusPath}</color>\n Trying to load stimulus");
                                try
                                {
                                    if (current_stimulus_trigger == calibration_trigger)
                                    {
                                        if (planeForImages.activeInHierarchy)
                                            planeForImages.SetActive(false); //deactivate videoplayer gameobject
                                        //   if (planeForImages.activeInHierarchy) planeForImages.SetActive(false);  //deactivate images display
                                        // planeForImages.
                                        imageLoader.SetBackgroundColor(_background_color);
                                    }
                                    else if (current_stimulus_trigger == time_limitless_trigger)
                                    {
                                        if (VIDEOPLAYERGO.activeInHierarchy)
                                            VIDEOPLAYERGO.SetActive(true); //deactivate videoplayer gameobject
                                        if (!planeForImages.activeInHierarchy)
                                            planeForImages.SetActive(false); //activate images display
                                        StartCoroutine(videoLoader.playVideoFromCSV(_stimulusPath,
                                            videoLoader.ScreenForStimulusDisplay, _background_color, _stimulus_offset_x,
                                            _stimulus_offset_y, _stimulus_scale_XY, _stimulus_distance));

                                        pupilEyetrackerTriggerSender.SendTriggerAnnotation(_stimulus_trigger
                                            .ToString());
                                        tcpTriggerSender.triggerMessageToBeSent = _stimulus_trigger.ToString();
                                        tcpTriggerSender.SendMessage();
                                    }
                                    else if (current_stimulus_trigger == recording_start_trigger)
                                    {
                                        if (VIDEOPLAYERGO.activeInHierarchy)
                                            VIDEOPLAYERGO.SetActive(false); //deactivate videoplayer gameobject
                                        if (!planeForImages.activeInHierarchy)
                                            planeForImages.SetActive(false); //activate images display

                                        pupilEyetrackerTriggerSender.SendTriggerAnnotation(_stimulus_trigger
                                            .ToString());
                                        tcpTriggerSender.triggerMessageToBeSent = _stimulus_trigger.ToString();
                                        tcpTriggerSender.SendMessage();
                                    }
                                    else if (current_stimulus_trigger == recording_stop_trigger)
                                    {
                                        if (VIDEOPLAYERGO.activeInHierarchy)
                                            VIDEOPLAYERGO.SetActive(false); //deactivate videoplayer gameobject
                                        if (!planeForImages.activeInHierarchy)
                                            planeForImages.SetActive(false); //activate images display

                                        pupilEyetrackerTriggerSender.SendTriggerAnnotation(_stimulus_trigger
                                            .ToString());
                                        tcpTriggerSender.triggerMessageToBeSent = _stimulus_trigger.ToString();
                                        tcpTriggerSender.SendMessage();
                                    }
                                    else
                                    {
                                        if (planeForImages.activeInHierarchy)
                                            planeForImages.SetActive(false); //turn off images display
                                        if (!VIDEOPLAYERGO.activeInHierarchy)
                                            VIDEOPLAYERGO.SetActive(true); //activate videoplayer gameobject
                                        // StartCoroutine(imageLoader.ChangeImageByPathAndSetGameObjectOffsetFromCameraCenter(_stimulusPath, imageLoader.ScreenForStimulusDisplay, _stimulus_offset_x, _stimulus_offset_y));
                                        // scenarioWaitingTimeForStimulusToRemainShown = _stimulus_duration;

                                        //scenarioWaitingTimeForStimulusToRemainShown = TryConvertStringToFloatSimple(videoLoader.currentVideoMetaData.duration);
                                        StartCoroutine(videoLoader.playVideoFromCSV(_stimulusPath,
                                            videoLoader.ScreenForStimulusDisplay, _background_color, _stimulus_offset_x,
                                            _stimulus_offset_y, _stimulus_scale_XY, _stimulus_distance));
                                        pupilEyetrackerTriggerSender.SendTriggerAnnotation(_stimulus_trigger
                                            .ToString());
                                        // pupilEyetrackerTriggerSender.SendTriggerAnnotationWithDuration(_stimulus_trigger.ToString(), videoLoader.currentVideoMetaData.duration.);
                                        //   tcpClientTriggerSender.SendTriggerToIP(_stimulus_trigger.ToString());
                                        tcpTriggerSender.triggerMessageToBeSent = _stimulus_trigger.ToString();
                                        tcpTriggerSender.SendMessage();
                                        //string time = "00:20:12.0"
                                        string[] times = videoLoader.currentVideoMetaData.duration.Split(':', '.');
                                    }
                                }
                                catch (Exception e) // no such file at specified path
                                {
                                    Debug.LogError($"Could not run file from {_stimulusPath}. Reason:\n {e}");
                                }
                            }
                            else
                            {
                                Debug.LogError(
                                    $"The file <color=red>{Path.GetFileName(_stimulusPath)}</color> at path<color=red>{_stimulusPath}</color> does not seem to exist. Please make sure all files necessary for the selected scenario are under the scenario folder in 'images' and 'videos' folders.");
                            }
                        }
                        catch (Exception e)
                        {
                            Debug.LogError(
                                $"Something went wrong with loading stimulus from path {_stimulusPath}. Reason: \n {e}");
                        }
                    }
                    // urlForImages = Application.dataPath + "/StreamingAssets/images/";


                    //  if (File.Exists(_stimulusPath)) imageLoader.ChangeImageByPath(_stimulusPath);
                    //  else Debug.LogError($"no such file as {_stimulus_file_name} at path \n {_stimulusPath}");
                }
            }
            catch (Exception e)
            {
                Debug.LogError("Could not set the image by path because of: " + e.ToString());
            }
            //set camera solid color color by _background_color

            //move 3D plane with stimulus by 2D offset from the center of the screen by _stimulus_offset

            //set stimulus scale by stimulus_scale_XY

            //set distance of the 3D plane from Main Camera by _stimulus_distance

            //send stimulus_trigger over IP
            imageLoader.PrintLoadedStimulusRowContentIntoConsole(_stimulus_trigger,
                _stimulus_file_name,
                _stimulus_duration,
                _background_color,
                _stimulus_offset_x,
                _stimulus_offset_y,
                _stimulus_scale_XY,
                _stimulus_distance,
                _scenarioCSVRowsIterator);
        }
    }*/
}