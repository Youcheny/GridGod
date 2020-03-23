using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

namespace Assets
{
    public class LevelCompleteEvent : MonoBehaviour
    {
        private const string Name = "level_complete";

        private Dictionary<string, object> parameters = new Dictionary<string, object>();

        void Start()
        {
            // Define parameters with default values
            parameters.Add("character_class", "Unknown");
            parameters.Add("health", 0);
            parameters.Add("xp", 0);
            parameters.Add("world_x", 0);
            parameters.Add("world_y", 0);
            parameters.Add("world_z", 0);
        }

        public bool Dispatch(string characterClass,
                             int health,
                             int experience,
                             Vector3 location)
        {

            // Set parameter values for a specific event
            parameters["character_class"] = characterClass;
            parameters["health"] = health;
            parameters["xp"] = experience;
            parameters["world_x"] = location.x;
            parameters["world_y"] = location.y;
            parameters["world_z"] = location.z;

            // Send event
            AnalyticsResult result
                = AnalyticsEvent.Custom(Name, parameters);
            if (result == AnalyticsResult.Ok)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
