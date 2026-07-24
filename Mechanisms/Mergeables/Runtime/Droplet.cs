using TMPro;
using UnityEngine;

public class Droplet : MonoBehaviour
{
    public float Radius { get; private set; }
    public float Weight => _weight;

    [SerializeField] private Transform _model;
    [SerializeField] private TMP_Text _display;

    private float _weight;

    public void SetValue(float value)
    {
        _weight = value;
        UpdateText(value);

        UpdateSize(_weight);
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
}
