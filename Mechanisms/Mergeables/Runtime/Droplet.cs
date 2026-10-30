using EggCentric.Entities;
using EggCentric.Sensors;
using TMPro;
using UnityEngine;

public class Droplet : MonoBehaviour, IKinematicEntity
{
    public Vector3 Position => transform.position;
    public Vector3 Velocity { get; private set; }

    public float Radius { get; private set; }
    public float Weight => _weight;

    [SerializeField] private ProbeSettings3D _raycastLayers;
    [SerializeField] private Transform _model;
    [SerializeField] private TMP_Text _display;

    private CollisionProbe<RaycastHit> _collisionProbe;
    private float _weight;

    public void SetValue(float value)
    {
        _weight = value;
        UpdateText(value);

        UpdateSize(_weight);
    }

    public void Tick(float timeStep)
    {
        _collisionProbe.Tick(timeStep);
        transform.position += Velocity * timeStep;
        Velocity += 9.81f * Vector3.down * timeStep;
    }

    private void Awake()
    {
        _collisionProbe = new CollisionProbe<RaycastHit>(this, new RaycastProbe3D(_raycastLayers));
        _collisionProbe.OnContact += HandleHit;
    }

    private void UpdateSize(float weight)
    {
        Radius = GetSize(_weight) / 2f;

        _model.localScale = Vector3.one * Radius * 2f;
    }

    private void UpdateText(float value)
    {
        if (_display == null)
            return;

        _display.transform.parent.localPosition = Vector3.up * (Radius + 0.1f);
        _display.text = value.ToString("0.00");
    }

    private float GetSize(float weight) => Mathf.Sign(weight) * Mathf.Pow(Mathf.Abs(weight), 1f / 3f);

    private void HandleHit(RaycastHit hit)
    {
        transform.position = hit.point + hit.normal * 0.01f;
        Velocity = Vector3.zero;
    }
}
