﻿using Modding;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using USceneManager = UnityEngine.SceneManagement.SceneManager;

public class RadianceClimbSimulations : Mod {
    internal static RadianceClimbSimulations instance;

    public RadianceClimbSimulations() : base("Radiance Climb Simulations") {
        instance = this;
    }

    public override void Initialize(Dictionary<string, Dictionary<string, GameObject>> preloadedObjects) {
        USceneManager.activeSceneChanged += CheckForRadiance;
        Log("Initialized.");
    }

    public override string GetVersion() {
        return "1.0.0.0";
    }

    public void Unload() {
        USceneManager.activeSceneChanged -= CheckForRadiance;
    }

    private static void CheckForRadiance(Scene from, Scene to) {
        if (to.name != "GG_Radiance") {
            return;
        }

        new GameObject("AbsFinder", typeof(AbsFinder));
    }
}