using EggCentric.LifeCycleHandling;
using EggCentric.Sensors;
using EggCentric.ValueProviders;
using UnityEngine;

public class DropletTreeItem : ITreeItem<DropletTreeItem>, ITickable
{
    public ITreeNode<DropletTreeItem> Parent { get; private set; }
    public Vector3 Position => _position;
    public float Radius => Droplet.Radius;

    public readonly Droplet Droplet;
    private readonly Transform _transform;

    private CollisionProbe<RaycastHit> _collisionProbe;
    private Vector3 _position;
    private Vector3 _velocity;

    public DropletTreeItem(Droplet entity, ProbeSettings3D probeSettings)
    {
        Parent = null;

        Droplet = entity;
        _transform = Droplet.transform;
        _position = _transform.position;

        _collisionProbe = new CollisionProbe<RaycastHit>(new RaycastProbe3D(probeSettings), new Getter<Vector3>(() => _position), new Getter<Vector3>(() => _velocity));
        _collisionProbe.OnContact += HandleHit;
    }

    public void AssignTo(ITreeNode<DropletTreeItem> parent) => Parent = parent;
    public void MoveTo(Vector3 position) => _position = position;

    private void HandleHit(RaycastHit hit)
    {
        MoveTo(hit.point + hit.normal * 0.01f);
        _velocity = Vector3.zero;
    }

    public void Tick(float timeStep)
    {
        _velocity += 9.81f * Vector3.down * timeStep;
        _collisionProbe.Tick(timeStep);
        _position += _velocity * timeStep;
        _transform.position = _position;
    }
}