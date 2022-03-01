using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public List<CinemachineVirtualCamera> rooms;
    int _activeRoom = 2;
    public int activeRoom
    {
        get { return _activeRoom; }
        set
        {
            if(value != _activeRoom)
            {
                _activeRoom = value;
                roomChanged = true;
            }
        }
    }
    bool roomChanged, pressed;
    #region Singleton Shit
    public static CameraController instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(instance);
    }
    #endregion
    private void Update()
    {
        if(roomChanged)
        {
            foreach(CinemachineVirtualCamera room in rooms)
            {
                room.Priority = 0;
            }
            rooms[activeRoom - 1].Priority = 1;
            roomChanged = false;
        }
        Vector2 input = GetInput();
        if (input.magnitude > 0)
        {
            if (!pressed)
            {
                switch (activeRoom)
                {
                    case 1:
                        if (input.x >= .5) activeRoom++;
                        break;
                    case 2:
                        if (input.y <= -.5) activeRoom = 4;
                        else if (input.x >= .5) activeRoom++;
                        else if (input.x <= -.5) activeRoom--;
                        break;
                    case 3:
                        if (input.x <= -.5) activeRoom--;
                        break;
                    case 4:
                        if (input.y >= .5) activeRoom = 2;
                        break;
                }
                pressed = true;
            }
        }
        else pressed = false;
    }
    Vector2 GetInput()
    {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }
}
