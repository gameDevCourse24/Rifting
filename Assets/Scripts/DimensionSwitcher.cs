using UnityEngine;

public class DimensionSwitcher : MonoBehaviour
{
    public Camera mainCamera; // Reference to the Main Camera
    public string[] dimensionLayers; // Array of dimension layer names
    private int currentDimension = 0; // Index of the current dimension
    private int defaultLayerMask; // Mask for the Default layer

    void Start()
    {
        if (mainCamera == null)
        {
            Debug.LogError("Main Camera is not assigned.");
            return;
        }

        if (dimensionLayers.Length == 0)
        {
            Debug.LogError("No dimension layers assigned.");
            return;
        }

        // Initialize the mask for the Default layer
        defaultLayerMask = 1 << LayerMask.NameToLayer("Default");

        // Initialize the culling mask to include the Default layer and the first dimension
        SetCameraCullingMask(dimensionLayers[currentDimension]);
    }

    void Update()
    {
        // Check for input to switch dimensions (e.g., pressing the "D" key)
        if (Input.GetKeyDown(KeyCode.R))
        {
            // Cycle to the next dimension
            currentDimension = (currentDimension + 1) % dimensionLayers.Length;
            SetCameraCullingMask(dimensionLayers[currentDimension]);
        }
    }

    void SetCameraCullingMask(string layerName)
    {
        int layer = LayerMask.NameToLayer(layerName);
        if (layer == -1)
        {
            Debug.LogError("Layer \"" + layerName + "\" not found.");
            return;
        }

        // Combine the Default layer mask with the specified layer mask
        mainCamera.cullingMask = defaultLayerMask | (1 << layer);
    }
}
