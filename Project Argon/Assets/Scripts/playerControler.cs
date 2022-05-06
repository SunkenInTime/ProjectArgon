using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControler : MonoBehaviour
{
    float yForce;
    float xForce;
    [SerializeField]float controlSens = 20f;
    [SerializeField]float xRange = 5f;
    [SerializeField]float yRange = 4f;
    [SerializeField] float posPitchFactor = -2f;
    [SerializeField] float crtlPitchFactor = -10f;

    [SerializeField] float posYawFactor = -2f;
    [SerializeField] float ctrlRollFactor = -10f;
    void Update()
    {
        processTranslation();
        processRotation();
    }

    void processRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * posPitchFactor;
        float pitchDueToCtrlForce = yForce * crtlPitchFactor;

        float pitch =  pitchDueToPosition + pitchDueToCtrlForce;
        float yaw =    transform.localPosition.x * posYawFactor;
        float roll = xForce * ctrlRollFactor;//+ xForce + ctrlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }
    void processTranslation()
    {
        xForce = Input.GetAxis("Horizontal");
        yForce = Input.GetAxis("Vertical");


        float xOffset = xForce * Time.deltaTime;
        float rawXPos = transform.localPosition.x + xOffset * controlSens;


        float yOffset = yForce * Time.deltaTime;
        float rawYPos = transform.localPosition.y + yOffset * controlSens;

        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange-6f, yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }
}
