using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    float speed = 3f;
    bool fighting;
    bool walk;
    public PlayerControl(float _speed, bool _figthing, bool _walk)
    {
        _speed = speed;
        _figthing = fighting;
        _walk = walk;
    }

    private void Update()
    {
        /*
        if()
        {
            StopAndFight();
        }
        else if()
        {
            End();
        }
        else
        {
            Walk();
        }
        */
    }

    private void Walk()
    {
        walk = true;
        transform.position = new Vector3(+speed * Time.deltaTime, 0, 0);
    }

    private void StopAndFight()
    {

    }

    private void End()
    {

    }
}
