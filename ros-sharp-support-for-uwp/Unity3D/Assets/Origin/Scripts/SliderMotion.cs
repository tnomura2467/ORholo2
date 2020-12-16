using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;

public class SliderMotion : MonoBehaviour
{
    [SerializeField]
    private Transform transformShelf = null;
    [SerializeField]
    private GameObject targetShelf = null;

    public BoundingBox targetBoundingBox;

    public void OnSliderUpdated(SliderEventData eventData)
    {
        if (transformShelf != null)
        {
            transformShelf.localPosition = new Vector3(transformShelf.localPosition.x, transformShelf.localPosition.y, -eventData.NewValue);
        }
    }

    public void OnSliderStarted(SliderEventData eventData)
    {
        if (targetShelf != null)
        {
            targetBoundingBox = targetShelf.AddComponent<BoundingBox>();
        }
    }

    public void OnSliderEnded(SliderEventData eventData)
    {
        if (targetShelf != null)
        {
            Destroy(targetBoundingBox);
        }
    }

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }
}
