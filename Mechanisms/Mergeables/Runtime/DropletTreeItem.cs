using EggCentric.DataStructures.Trees.Octree;
using EggCentric.Entities;
using EggCentric.Sensors;
using UnityEngine;

public class DropletTreeItem : ITreeItem<DropletTreeItem>, IKinematicEntity
{
    public ITreeNode<DropletTreeItem> Parent { get; private set; }
    public Vector3 Position => Droplet.Position;
    public Vector3 Velocity => Droplet.Velocity;
    public float Radius => Droplet.Radius;

    public readonly Droplet Droplet;

    public DropletTreeItem(Droplet entity, ProbeSettings3D probeSettings)
    {
        Parent = null;
        Droplet = entity;
    }

    public void AssignTo(ITreeNode<DropletTreeItem> parent) => Parent = parent;
}