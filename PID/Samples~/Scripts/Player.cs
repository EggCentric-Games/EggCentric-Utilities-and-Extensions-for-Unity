using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Train _controlledTrain;
    [SerializeField] private float _destination;
    [SerializeField] private PidCoefficients _coefficients;

    private TrainController _controller;

    private void Awake()
    {
        _controller = new TrainController(() => _controlledTrain.transform.position.x);
        UpdateValues();
    }

    private void Update()
    {
        _controller.Tick(Time.deltaTime);
        _controlledTrain.SetThrottle(_controller.Output);
    }

    private void OnValidate() => UpdateValues();

    private void UpdateValues()
    {
        if (_controller == null)
            return;

        _controller.SetTarget(_destination);
        _controller.SetCoefficients(_coefficients);
    }
}