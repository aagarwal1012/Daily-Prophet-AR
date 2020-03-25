/*==============================================================================
Copyright (c) 2019 PTC Inc. All Rights Reserved.

Confidential and Proprietary - Protected under copyright and other laws.

Vuforia is a trademark of PTC Inc., registered in the United States and other 
countries.
==============================================================================*/

using System;
using UnityEngine;
using Vuforia;

/// <summary>
/// NOTE: This class is obsolete and no longer functional. It is used only for migration purposes.
/// </summary>
[Obsolete("This class is obsolete and will be removed in a future version of the Vuforia Engine.")]
public class DefaultModelRecoEventHandler : MonoBehaviour, IObjectRecoEventHandler
{
    /// <summary>
    /// Can be set in the Unity inspector to display error messages in UI.
    /// </summary>
    [Tooltip("UI Text label to display model reco errors.")]
    public UnityEngine.UI.Text ModelRecoErrorText;

    /// <summary>
    /// Can be set in the Unity inspector to tell Vuforia whether it should:
    /// - stop searching for new models, once a first model was found,
    ///   or:
    /// - continue searching for new models, even after a first model was found.
    /// </summary>
    [Tooltip("Whether Vuforia should stop searching for other models, after the first model was found.")]
    public bool StopSearchWhenModelFound = false;

    /// <summary>
    /// Can be set in the Unity inspector to tell Vuforia whether it should:
    /// - stop searching for new models, while a target is being tracked and is in view,
    ///   or:
    /// - continue searching for new models, even if a target is currently being tracked.
    /// </summary>
    [Tooltip("Whether Vuforia should stop searching for other models, while current model is tracked and visible.")]
    public bool StopSearchWhileTracking = true;//true by default, as this is the recommended behaviour

    public void OnInitialized(TargetFinder targetFinder) { }

    public void OnInitError(TargetFinder.InitState initError) { }

    public void OnUpdateError(TargetFinder.UpdateState updateError) { }

    public virtual void OnStateChanged(bool scanning) { }

    public void OnNewSearchResult(TargetFinder.TargetSearchResult targetSearchResult) { }
}
