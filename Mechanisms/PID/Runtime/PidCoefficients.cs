using UnityEngine;

[System.Serializable]
public struct PidCoefficients
{
    public float Kp => _Kp;
    public float Ki => _Ki;
    public float Kd => _Kd;

    [SerializeField] private float _Kp;
    [SerializeField] private float _Ki;
    [SerializeField] private float _Kd;

    public PidCoefficients(float p = 1f, float i = 1f, float d = 1f)
    {
        _Kp = p;
        _Ki = i;
        _Kd = d;
    }
}
