using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(HPSettings))]
public class HPSettingsInspector : Editor 
{
    private HPSettings settings;

    public void OnEnable()
    {
        settings = (HPSettings)target;
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.LabelField("General", EditorStyles.boldLabel);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Respawn HP Cost: ");
        settings.respawnHPCost = EditorGUILayout.FloatField(settings.respawnHPCost);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Max Speed Reduction: ");
        settings.maxSpeedReduction = EditorGUILayout.FloatField(settings.maxSpeedReduction);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.LabelField("Collision", EditorStyles.boldLabel);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Minimum Impact Force: ");
        settings.minImpactForce = EditorGUILayout.FloatField(settings.minImpactForce);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Impact Damage Multiplier: ");
        settings.impactDamageMultiplier = EditorGUILayout.FloatField(settings.impactDamageMultiplier);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.LabelField("Items", EditorStyles.boldLabel);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Missile Damage: ");
        settings.missileDamage = EditorGUILayout.FloatField(settings.missileDamage);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Poison Damage (Per Second): ");
        settings.poisonDamage = EditorGUILayout.FloatField(settings.poisonDamage);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Health Regeneration: ");
        settings.healthRegeneration = EditorGUILayout.FloatField(settings.healthRegeneration);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Shield Regeneration (Per Second): ");
        settings.shieldRegeneration = EditorGUILayout.FloatField(settings.shieldRegeneration);
        EditorGUILayout.EndHorizontal();
    }
}
