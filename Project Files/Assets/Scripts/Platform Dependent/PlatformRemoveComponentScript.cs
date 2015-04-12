using UnityEngine;
using System.Collections;

public class PlatformRemoveComponentScript : MonoBehaviour {

    public RuntimePlatform targetedPlatforms;
    public Component removeComponent;
    
    void Start()
    {
        if (((int)targetedPlatforms & (1 << (int)Application.platform)) > 0)
        {
            if(removeComponent==null)
                Destroy(this.gameObject);
            else if(removeComponent is Transform)
                Destroy(((Transform)removeComponent).gameObject);
            else
                Destroy(removeComponent);
        }
        else
        {
            enabled = false;
        }
    }
}
