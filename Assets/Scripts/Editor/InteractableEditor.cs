using UnityEditor;

[CustomEditor(typeof(Interacrable), true)]
public class InteractableEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Interacrable interacrable = (Interacrable)target;
        if (target.GetType() == typeof(EventOnlyInteractable))
        {
            interacrable.promptMessage = EditorGUILayout.TextField("Prompt Message", interacrable.promptMessage);
            EditorGUILayout.HelpBox("EventOnlyInteratable can ONLY use UnityEvents.", MessageType.Info);
            if (interacrable.GetComponent<InteractionEvent>() == null)
            {
                interacrable.useEvents = true;
                interacrable.gameObject.AddComponent<InteractionEvent>();
            }
        }
        else { 
            base.OnInspectorGUI();
            if (interacrable.useEvents)
            {
                if (interacrable.GetComponent<InteractionEvent>() == null)
                    interacrable.gameObject.AddComponent<InteractionEvent>();
            }
            else
            {
                if (interacrable.GetComponent<InteractionEvent>() != null)
                    DestroyImmediate(interacrable.GetComponent<InteractionEvent>());
            }
        }
    }
}
