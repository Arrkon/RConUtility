using System.Collections;
using UnityEngine;

public static class MonoBehaviourExtensions
{
    // Gets an existing component, or adds and returns a new component if it does not exist
    static public T GetOrAddComponent<T>(this Component child) where T : Component
    {
        T result = child.GetComponent<T>();
        if(result == null)
        {
            result = child.gameObject.AddComponent<T>();
        }
        return result;
    }

    // if position is outside minPosition/maxPosition on an axis, sets position to be within them
    static public bool ClampPosition(this Transform t, Vector3 minPosition, Vector3 maxPosition, Space referenceSpace)
    {
        float x = t.position.x; float y = t.position.y; float z = t.position.z;
        if(referenceSpace == Space.Self)
        {
            x = t.localPosition.x; y = t.localPosition.y; z = t.localPosition.z;
        }

        bool changed = false;
        if(x < minPosition.x)
        {
            x = minPosition.x;
            changed = true;
        }
        else if(x > maxPosition.x)
        {
            x = maxPosition.x;
            changed = true;
        }
        if(y < minPosition.y)
        {
            y = minPosition.y;
            changed = true;
        }
        else if(y > maxPosition.y)
        {
            y = maxPosition.y;
            changed = true;
        }
        if(z < minPosition.z)
        {
            z = minPosition.z;
            changed = true;
        }
        else if(z > maxPosition.z)
        {
            z = maxPosition.z;
            changed = true;
        }

        if(changed)
        {
            Vector3 newPosition = new Vector3(x, y, z); ;
            if(referenceSpace == Space.Self)
            {
                t.localPosition = newPosition;
            }
            else
            {
                t.position = newPosition;
            }
        }

        return changed;
    }

    // if rotation is outside minRotation/maxRotation on an axis, sets rotationto be within them
    static public bool ClampRotation(this Transform t, Quaternion minRotation, Quaternion maxRotation, Space referenceSpace)
    {
        float x = t.rotation.x; float y = t.rotation.y; float z = t.rotation.z; float w = t.rotation.w;
        if(referenceSpace == Space.Self)
        {
            x = t.localRotation.x; y = t.localRotation.y; z = t.localRotation.z; w = t.localRotation.w;
        }

        bool changed = false;
        if(x < minRotation.x)
        {
            x = minRotation.x;
            changed = true;
        }
        else if(x > maxRotation.x)
        {
            x = maxRotation.x;
            changed = true;
        }
        if(y < minRotation.y)
        {
            y = minRotation.y;
            changed = true;
        }
        else if(y > maxRotation.y)
        {
            y = maxRotation.y;
            changed = true;
        }
        if(z < minRotation.z)
        {
            z = minRotation.z;
            changed = true;
        }
        else if(z > maxRotation.z)
        {
            z = maxRotation.z;
            changed = true;
        }
        if(w < minRotation.w)
        {
            w = minRotation.w;
            changed = true;
        }
        else if(w > maxRotation.w)
        {
            w = maxRotation.w;
            changed = true;
        }

        if(changed)
        {
            Quaternion newRotation = new Quaternion(x, y, z, w);
            if(referenceSpace == Space.Self)
            {
                t.localRotation = newRotation;
            }
            else
            {
                t.rotation = newRotation;
            }
        }

        return changed;
    }

    // Author: aarthificial Date: 2022-07-21
    public static CoroutineHandle RunCoroutine(this MonoBehaviour owner, IEnumerator coroutine)
    {
        return new CoroutineHandle(owner, coroutine);
    }
}
