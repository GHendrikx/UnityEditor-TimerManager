using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace UnityEngine.Timers
{
    public class EditorTimerWindow : EditorWindow
    {
        //Scroll position
        private Vector2 scrollPosition;
        //Allignment text center
        private GUIStyle centered;
        //StopButton
        private Texture2D stopButton;
        //Unity's playbutton
        private Texture2D playButton
        {
            get
            {
                return EditorGUIUtility.FindTexture("PlayButton");
            }
        }
        //Unity's pausebutton
        private Texture2D pauseButton
        {
            get
            {
                return EditorGUIUtility.FindTexture("PauseButton");
            }
        }

        /// <summary>
        /// Initialization of the window
        /// </summary>
        [MenuItem("Window/General/Timer Manager %&t")]
        private static void Init()
        {
            EditorTimerWindow window = (EditorTimerWindow)EditorWindow.GetWindow(typeof(EditorTimerWindow));

            //Setting the Title
            window.titleContent.text = "Time Manager";

            //Setting the icon.
            Texture2D myIcon = EditorGUIUtility.FindTexture("UnityEditor.AnimationWindow") as Texture2D;
            window.titleContent.image = myIcon;
        }

        /// <summary>
        /// Editor window GUI
        /// </summary>
        private void OnGUI()
        {
            if (!stopButton)
                stopButton = GenerateTexture();

            IntroText();

            if (TimerManager.Instance)
            {
                //beginning scroll view.
                scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

                WindowTimerText();

                //Repainting GUI
                Repaint();

                //ending scroll view.
                EditorGUILayout.EndScrollView();
            }
        }

        /// <summary>
        /// Window IntroText
        /// </summary>
        private void IntroText()
        {
            if (centered == null)
            {
                centered = new GUIStyle();
                centered.alignment = TextAnchor.MiddleCenter;
            }
           
            EditorGUILayout.LabelField("Timer Manager", centered);
            EditorGUILayout.LabelField("Written by Geoffrey Hendrikx", centered);
            EditorGUILayout.LabelField("This window is made for managing your timers.", centered);
            EditorGUILayout.LabelField("--------------------------------");
        }

        ///<summary>
        ///Drawing all the timers in the window
        ///</summary>
        private void WindowTimerText()
        {
            //Looping through the timers in the timermanager.
            //doing it in a for loop, because foreach gives an error that im editing the list.
            for(int i = 0; i<TimerManager.Instance.Timers.Count; i++)
            {
                Timer timer = TimerManager.Instance.Timers[i];

                float time = timer.SettedTime;
                string temp = "";

                //Good English is the base of everything.
                temp = (time <= 1) ? "second" : "seconds";

                EditorGUILayout.LabelField("New Unity Timer");
                
                EditorGUILayout.LabelField(string.Format("Timer running for the next {0} {1}", time.ToString("#00.0"), temp));
                EditorGUILayout.LabelField(string.Format("Calling methode: {0:0}", timer.methodeInfo));
                //add stop pauze and play button
                GUILayout.BeginHorizontal();
                if (GUILayout.Button(playButton, GUILayout.Width(Screen.width * 0.02f)))
                    timer.PauseTimer = false;
                if (GUILayout.Button(pauseButton, GUILayout.Width(Screen.width * 0.02f)))
                    timer.PauseTimer = true;
                //Screen.width multiply by a value whats the offset
                float heightOffset = Screen.height * 0.02801f;

                if (GUILayout.Button(stopButton, GUILayout.Width(Screen.width * 0.02f), GUILayout.Height(heightOffset)))
                    timer.RemoveTimer();
                GUILayout.EndHorizontal();
                EditorGUILayout.LabelField("--------------------------------");

            }
            #region Deprecated code 
            //foreach (Timer timer in TimerManager.Instance.Timers)
            //{
            //    float time = timer.SettedTime;
            //    string temp = "";

            //    //good english is the base of everything.
            //    if (time <= 1)
            //        temp = "second";
            //    else
            //        temp = "seconds";

            //    EditorGUILayout.LabelField("New Unity Timer");
            //    EditorGUILayout.LabelField(string.Format("Timer running for the next {0} {1}", time.ToString("#00.0"), temp));
            //    EditorGUILayout.LabelField(string.Format("Calling methode: {0:0}", timer.methodeInfo));
            //    //add stop pauze and play button
            //    GUILayout.BeginHorizontal();
            //    if (GUILayout.Button(playButton, GUILayout.Width(Screen.width * 0.02f)))
            //        timer.PauseTimer = false;
            //    if (GUILayout.Button(pauseButton, GUILayout.Width(Screen.width * 0.02f)))
            //        timer.PauseTimer = true;
            //    //Screen.width multiply by a value whats the offset
            //    float heightOffset = Screen.height * 0.02801f;

            //    if (GUILayout.Button(stopButton, GUILayout.Width(Screen.width * 0.02f), GUILayout.Height(heightOffset)))
            //        timer.RemoveTimer();
            //    GUILayout.EndHorizontal();
            //    EditorGUILayout.LabelField("--------------------------------");
            //}
            #endregion
        }

        /// <summary>
        /// Generating texture (stopbutton
        /// </summary>
        private Texture2D GenerateTexture()
        {
            Texture2D temp = new Texture2D(8, 8);

            for (int x = 0; x <= temp.width; x++)
                for (int y = 0; y <= temp.height; y++)
                    temp.SetPixel(x, y, Color.black);
            
            //Applying the pixels to the texture.
            temp.Apply();
            return temp;
        }

    }
}