/*===============================================================================
Copyright (c) 2019 PTC Inc. All Rights Reserved.

Confidential and Proprietary - Protected under copyright and other laws.
Vuforia is a trademark of PTC Inc., registered in the United States and other 
countries.
===============================================================================*/

using System;
using UnityEngine;
using UnityEngine.EventSystems;
#if PLATFORM_ANDROID
using UnityEngine.Android;
#endif

namespace Vuforia.UnityCompiled
{
    public class RuntimeOpenSourceInitializer
    {
        static IUnityCompiledFacade sFacade;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void OnRuntimeMethodLoad()
        {
            InitializeFacade();
        }

        static void InitializeFacade()
        {
            if (sFacade != null) return;

            sFacade = new OpenSourceUnityCompiledFacade();
            UnityCompiledFacade.Instance = sFacade;
        }

        class OpenSourceUnityCompiledFacade : IUnityCompiledFacade
        {
            readonly IUnityRenderPipeline mUnityRenderPipeline = new UnityRenderPipeline();
            readonly IUnityAndroidPermissions mUnityAndroidPermissions = new UnityAndroidPermissions();

            public IUnityRenderPipeline UnityRenderPipeline
            {
                get { return mUnityRenderPipeline; }
            }
            
            public IUnityAndroidPermissions UnityAndroidPermissions
            {
                get { return mUnityAndroidPermissions; }
            }


            public bool IsUnityUICurrentlySelected()
            {
                return !(EventSystem.current == null || EventSystem.current.currentSelectedGameObject == null);
            }
        }

        class UnityRenderPipeline : IUnityRenderPipeline
        {
            public event Action<Camera[]> BeginFrameRendering;
            public event Action<Camera> BeginCameraRendering;

            public UnityRenderPipeline()
            {
#if UNITY_2018
                UnityEngine.Experimental.Rendering.RenderPipeline.beginFrameRendering += OnBeginFrameRendering;
                UnityEngine.Experimental.Rendering.RenderPipeline.beginCameraRendering += OnBeginCameraRendering;
#else
                UnityEngine.Rendering.RenderPipelineManager.beginFrameRendering += OnBeginFrameRendering;
                UnityEngine.Rendering.RenderPipelineManager.beginCameraRendering += OnBeginCameraRendering;
#endif
            }

#if UNITY_2018
            void OnBeginCameraRendering(Camera camera)
#else
            void OnBeginCameraRendering(UnityEngine.Rendering.ScriptableRenderContext context, Camera camera)
#endif
            {
                if (BeginCameraRendering != null)
                    BeginCameraRendering(camera);
            }

#if UNITY_2018
            void OnBeginFrameRendering(Camera[] cameras)
#else
            void OnBeginFrameRendering(UnityEngine.Rendering.ScriptableRenderContext context, Camera[] cameras)
#endif
            {
                if (BeginFrameRendering != null)
                    BeginFrameRendering(cameras);
            }
        }

        class UnityAndroidPermissions : IUnityAndroidPermissions
        {
            public bool HasRequiredPermissions()
            {
#if PLATFORM_ANDROID
                return Permission.HasUserAuthorizedPermission(Permission.Camera);
#else
                return true;
#endif
            }

            public void AskForPermissions()
            {
#if PLATFORM_ANDROID
                Permission.RequestUserPermission(Permission.Camera);
#endif
            }
        }
    }
}
