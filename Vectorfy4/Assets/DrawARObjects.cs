using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Vuforia;
using System.Collections.Specialized;


// SW Edited on 11-9-2023 to change vector operation to use the global variable
public class DrawARObjects : MonoBehaviour
{
    [SerializeField] private GameObject itAxesMarker;
    [SerializeField] private GameObject ctVectorA;               // A from CylinderTargetA
    [SerializeField] private GameObject ctVectorB;               // B from CylinderTargetB
    [SerializeField] private GameObject vectorResult;       // e.g. A+B (depending on operation)
    [SerializeField] private GameObject vectorShadowA;      // Shadow vector for CylinderTargetA
    [SerializeField] private GameObject vectorShadowB1;     // Shadow vector for CylinderTargetB
    [SerializeField] private GameObject vectorShadowB2;     // Shadow vector for CylinderTargetB (a second shadow)
    [SerializeField] private GameObject vectorScaled;       // e.g. 2A or -B (used with scalar and - operation)
    [SerializeField] private TMPro.TMP_Text textChallenge;         
    [SerializeField] private TMPro.TMP_Text textResult;         
    [SerializeField] private GlobalStringVariable targetsDetected;
    // "A"  => CylinderTargetA detected, but not CylinderTargetB
    // "AB" or "BA" => CylinderTargetA and CylinderTargetB both detected

    private float proximityDistance = 0.04f; // This is the width of target cylinders (30mm) + a bit for wiggle room!

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("DrawARObjects started");
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("DrawARObjects updated");
        //GameObject itChild = itAxesMarker.GetComponentInChildren < "" >;
        float scaleFactor;
        string strChallenge = " ";
        if ((targetsDetected.value == "AB" || targetsDetected.value == "BA") && 
            (GlobalVectorOperation.vectorOperation == "+" || GlobalVectorOperation.vectorOperation == "-" || 
             GlobalVectorOperation.vectorOperation == "." || GlobalVectorOperation.vectorOperation == "x"))
        {
            if (GlobalVectorOperation.vectorOperation == "+" ||
                GlobalVectorOperation.vectorOperation == "-")
                strChallenge = "What range of values can the result take?" + Environment.NewLine
                    + "Can you get the green vector " + Environment.NewLine
                    + "to 10.0cm or 14.1cm?"; 
            else if (GlobalVectorOperation.vectorOperation == ".")
                strChallenge = "What range of values can the result take?" + Environment.NewLine
                    + "Can you get the green vector " + Environment.NewLine
                    + "to 10.0cm or 5.0cm?";
            else if (GlobalVectorOperation.vectorOperation == "x")
                strChallenge = "What range of values can the result take?" + Environment.NewLine
                    + "Can you get the green vector " + Environment.NewLine
                    + "to 10.0cm or 5.0cm?";

            DrawResultVector(ctVectorA, ctVectorB, vectorResult, vectorScaled);
        }
        else if ((targetsDetected.value == "A" || targetsDetected.value == "B") &&
            (GlobalVectorOperation.vectorOperation != "+" && GlobalVectorOperation.vectorOperation != "-" && 
             GlobalVectorOperation.vectorOperation != "." && GlobalVectorOperation.vectorOperation != "x" && 
             GlobalVectorOperation.vectorOperation != "na"))
        {
            strChallenge = "Can you get the green vector " + Environment.NewLine
                + "to 10.0cm in TWO ways?"; 
            scaleFactor = float.Parse(GlobalVectorOperation.vectorOperation);
            vectorScaled.SetActive(false); // Hide this and use vectorResult for consistent colour
            GameObject ctVector = ctVectorA; // Default
            if (targetsDetected.value == "B")
            {
                ctVector = ctVectorB;
            }
            DrawScaledVector(ctVector, vectorResult, scaleFactor);
        }
        else
        {
            vectorResult.SetActive(false);
            vectorScaled.SetActive(false);
            vectorShadowA.SetActive(false);
            vectorShadowB1.SetActive(false);
            vectorShadowB2.SetActive(false);
            strChallenge = " ";
            textResult.text = " ";
        }
        textChallenge.color = Color.white;
        textChallenge.text = strChallenge;
    }


    // rm 2023-09-08
    // draws the Result Vector - depending on the vectorOperation: +, -, . (dot product), x (cross product) 
    // Mods
    // rm 2023-09-12 Added - (subtraction) operation
    // rm 2023-09-13 Added x (cross product) operation
    // st 2023-09-14 Fix to cross product
    // st 2023-09-18 Fix to cross product
    // rm 2023-09-19 Standardized code
    // rm 2023-09-25 Added strChallenge and updated textResult
    void DrawResultVector(GameObject ctVectorA, GameObject ctVectorB, GameObject vectorResult, GameObject vectorScaled)
    {
        Debug.Log("DrawResultVector");
        GameObject stA;
        GameObject stB;
        GameObject ahA;
        GameObject ahB;

        bool result = false;
        Vector3 positionStartRes;       // Res = Result Vector
        Vector3 positionEndRes;
        Vector3 directionRes;
        float magRes;
        Vector3 magVectorRes;
        Vector3 normVectorRes;
        Vector3 vecA;
        Vector3 vecB;
        Vector3 xPrd;                   // Cross Product
        string strResult = "";

        // Check to see if Target Vectors have the right points close enough
        string operationOrder = CheckProximityOfVectors(ctVectorA, ctVectorB);
        //Debug.LogFormat("DrawResultVector ctVectorA: {0}, ctVectorB: {1}, vectorResult: {2}", ctVectorA, ctVectorB, vectorResult);
        // + operation: this + that
        if (GlobalVectorOperation.vectorOperation == "+")
        {
            //if (operationOrder == "AB")
            //{
            //    gobjThisTail = ctVectorA.transform.Find("SphereTail").gameObject;
            //    gobjThatHead = ctVectorB.transform.Find("Arrowhead").gameObject;
            //}
            //else // operationOrder == "BA" reverse the order
            //{
            //    gobjThisTail = ctVectorB.transform.Find("SphereTail").gameObject;
            //    gobjThatHead = ctVectorA.transform.Find("Arrowhead").gameObject;
            //}
            //gobjThisTail = ctVectorA.transform.Find("SphereTail").gameObject;
            //gobjThatHead = ctVectorB.transform.Find("Arrowhead").gameObject;

            // work out position, rotation and scale for Result Vector
            positionStartRes = ctVectorA.transform.Find("SphereTail").gameObject.transform.position;

            // Draw supporting shadow vectors
            DrawShadowVector(ctVectorB, vectorShadowB1, positionStartRes);
            Vector3 posStartShadowA = vectorShadowB1.transform.Find("Arrowhead").gameObject.transform.position;
            DrawShadowVector(ctVectorA, vectorShadowA, posStartShadowA);
            Vector3 posStartShadowB2 = ctVectorA.transform.Find("Arrowhead").gameObject.transform.position;
            DrawShadowVector(ctVectorB, vectorShadowB2, posStartShadowB2);

            // Result Vector (continued)
            positionEndRes = vectorShadowB2.transform.Find("Arrowhead").gameObject.transform.position;
            directionRes = positionEndRes - positionStartRes;
            magRes = directionRes.magnitude;
            Debug.Log("CheckPoint. Got to here 1");
            // Note: 10.0f is because cylinder is 0.1 scale and positioned 0.05 (Don't need to divide by 2)
            magVectorRes = new Vector3(1.0f, 10.0f * magRes, 1.0f);
            Debug.LogFormat("directionRes: {0}; magRes: {1}; positionStartRes: {2}", directionRes, magRes, positionStartRes);

            vectorResult.transform.position = positionStartRes;
            Debug.Log("CheckPoint. Got to here 2");
            vectorResult.transform.localScale = magVectorRes;
            vectorResult.transform.rotation = Quaternion.identity;
            vectorResult.transform.up = directionRes;

            strResult = "Magnitude of green vector = " + Math.Round(100.0f * magRes, 1).ToString("0.0cm");

            // SW 2023-10-15
            if (Math.Round(100.0f * magRes, 1) > 9.5 && Math.Round(100.0f * magRes, 1) < 10.5)
            {
                result = true;
            }
            else if (Math.Round(100.0f * magRes, 1) > 13.6 && Math.Round(100.0f * magRes, 1) < 14.6)
            {
                result = true;
            }
            else
            {
                result = false;
            }

            vectorResult.SetActive(true);
            vectorScaled.SetActive(false);
            vectorShadowA.SetActive(true);
            vectorShadowB1.SetActive(true);
            vectorShadowB2.SetActive(true);
        }
        // - operation: A - B
        else if (GlobalVectorOperation.vectorOperation == "-")
        {
            // work out position, rotation and scale for Result Vector
            Vector3 posStartShadowB1 = ctVectorA.transform.Find("SphereTail").gameObject.transform.position;
            // draw supporting shadow vectors
            DrawShadowVector(ctVectorB, vectorShadowB1, posStartShadowB1);

            // work out position, rotation and scale for Result Vector
            positionStartRes = vectorShadowB1.transform.Find("Arrowhead").gameObject.transform.position;
            positionEndRes = ctVectorA.transform.Find("Arrowhead").gameObject.transform.position;
            directionRes = positionEndRes - positionStartRes;
            magRes = directionRes.magnitude;
            // Note: 10.0f is because cylinder is 0.1 scale and positioned 0.05 (Don't need to divide by 2)
            magVectorRes = new Vector3(1.0f, 10.0f * magRes, 1.0f);   // <<< CHECK this is right
            normVectorRes = directionRes.normalized;
            Debug.LogFormat("directionRes: {0}; magRes: {1}", directionRes, magRes);

            vectorResult.transform.position = positionStartRes;
            vectorResult.transform.localScale = magVectorRes;
            vectorResult.transform.rotation = Quaternion.identity;
            vectorResult.transform.up = directionRes;

            strResult = "Magnitude of green vector = " + Math.Round(100.0f * magRes, 1).ToString("0.0cm");

            // SW 2023-10-15
            if (Math.Round(100.0f * magRes, 1) > 9.5 && Math.Round(100.0f * magRes, 1) < 10.5)
            {
                result = true;
            }
            else if (Math.Round(100.0f * magRes, 1) > 13.6 && Math.Round(100.0f * magRes, 1) < 14.6)
            {
                result = true;
            }
            else
            {
                result = false;
            }

            vectorResult.SetActive(true);
            vectorScaled.SetActive(false);
            vectorShadowA.SetActive(false);
            vectorShadowB1.SetActive(true);
            vectorShadowB2.SetActive(false);
        }
        // . (dot product) A.B
        else if (GlobalVectorOperation.vectorOperation == ".")
        {
            // Note: use of DrawWireArc(Vector3 center, Vector3 normal, Vector3 from, float angle, float radius, float thickness = 0.0f);
            // find average of stA and stB           
            stA = ctVectorA.transform.Find("SphereTail").gameObject;
            stB = ctVectorB.transform.Find("SphereTail").gameObject;
            ahA = ctVectorA.transform.Find("Arrowhead").gameObject;
            ahB = ctVectorB.transform.Find("Arrowhead").gameObject;

            positionStartRes = stA.transform.position;
            DrawShadowVector(ctVectorB, vectorShadowB1, positionStartRes);

            // calculate the signed_angle
            vecA = ahA.transform.position - stA.transform.position;
            vecB = ahB.transform.position - stB.transform.position;
            Vector3 vecANormal = vecA.normalized;
            Vector3 vecBNormal = vecB.normalized;
            float magVecB = vecB.magnitude;

            float dotProd = Vector3.Dot(vecA, vecB);
            float dotProdNormal = Vector3.Dot(vecANormal, vecBNormal);
            float angle = Vector3.Angle(vecA, vecB);
            double angleRounded = Math.Round(angle * 5.0f, 0) / 5.0d; // Rounded to nearest 5 degrees  
            Vector3 normal = new Vector3(0.0f, 1.0f, 0.0f);
            // float sign = Mathf.Sign(Vector3.Dot(normal, xPrd));
            float sign = Mathf.Sign(Vector3.Dot(normal, Vector3.Cross(vecA, vecB)));
            float signedAngle = angle * sign;

            float projAOnB = dotProd / (magVecB * magVecB);

            Debug.LogFormat("Dot Prod signed_angle: {0}; dotProd: {1}; dotProdNormal: {2}", signedAngle, dotProd, dotProdNormal);

            // Result Vector is projection of A onto B
            // work out position, rotation and scale for Result Vector
            positionStartRes = stA.transform.position;
            positionEndRes = ahA.transform.position;
            directionRes = positionEndRes - positionStartRes;
            magRes = directionRes.magnitude * projAOnB;     // Note this variation;
            // Note: 10.0f is because cylinder is 0.1 scale and positioned 0.05 (Don't need to divide by 2)
            magVectorRes = new Vector3(1.0f, 10.0f * magRes, 1.0f);   // <<< CHECK this is right
            Debug.LogFormat("directionRes: {0}; magRes: {1}", directionRes, magRes);

            vectorResult.transform.position = positionStartRes;
            vectorResult.transform.localScale = magVectorRes;
            vectorResult.transform.rotation = Quaternion.identity;
            vectorResult.transform.up = directionRes;

            Vector3 positionStartNorm = vectorShadowB1.transform.Find("Arrowhead").gameObject.transform.position;
            Vector3 positionEndNorm = vectorResult.transform.Find("Arrowhead").gameObject.transform.position;
            // Draw supporting shadow vectors - Normal to Vector B
            Debug.Log("Dot Product Check Point 1 ");
            DrawFromToVector(ctVectorB, vectorScaled, positionStartNorm, positionEndNorm);
            Debug.Log("Dot Product Check Point 2 ");

            strResult = "Dot Product is NOT the vector!" + Environment.NewLine
                + "(Work out what the result vector is!)" + Environment.NewLine
                + "DP (as prop. of length) = " + Math.Round(dotProdNormal, 2).ToString("0.00") + Environment.NewLine
                + "Angle between vectors = " + Math.Round(signedAngle, 0).ToString("0") + "°";

            // SW 2023-10-15
            if (Math.Round(signedAngle, 0) > 85 && Math.Round(signedAngle, 0) < 95)
            {
                result = true;
            }
            else if (Math.Round(signedAngle, 0) > 40 && Math.Round(signedAngle, 0) < 50)
            {
                result = true;
            }
            else
            {
                result = false;
            }

            vectorResult.SetActive(true);
            vectorScaled.SetActive(true);
            vectorShadowA.SetActive(false);
            vectorShadowB1.SetActive(true);
            vectorShadowB2.SetActive(false);
        }
        // x (cross product) A x B 
        else if (GlobalVectorOperation.vectorOperation == "x")
        {
            // find average of stA and stB           
            stA = ctVectorA.transform.Find("SphereTail").gameObject;
            stB = ctVectorB.transform.Find("SphereTail").gameObject;
            ahA = ctVectorA.transform.Find("Arrowhead").gameObject;
            ahB = ctVectorB.transform.Find("Arrowhead").gameObject;

            // work out position for Result Vector
            // work out and cross product: vecA x vecBs
            // st 2023-09-18 bug fix
            // rm 2023-09-19 standardize
            vecA = ahA.transform.position - stA.transform.position;
            vecB = ahB.transform.position - stB.transform.position;
            xPrd = Vector3.Cross(vecA, vecB) * 10;
            // xPrd = Vector3.Cross(adjustedVecA, adjustedVecB) * 10; // Increase magnitude by 10 times (not sure why??? its just 10 times smaller than it should be)
            // The start position of vectorResult can be the average position of the tails
            positionStartRes = ctVectorA.transform.Find("SphereTail").gameObject.transform.position;
            positionEndRes = positionStartRes + xPrd;

            DrawShadowVector(ctVectorB, vectorShadowB1, positionStartRes);

            // Vector3 positionStartRes = (stA.transform.position + stB.transform.position) * 0.5f;     //OLD - Average of positions
            // Vector3 positionEndRes = positionStartRes + xPrd;
            // Work out rotation and scale for Result Vector
            directionRes = positionEndRes - positionStartRes;
            magRes = directionRes.magnitude;
            magVectorRes = new Vector3(1.0f, 10.0f * magRes, 1.0f);
            normVectorRes = directionRes.normalized;
            // rotationRes = Quaternion.LookRotation(normVectorRes);

            // check Dot Products = 0 - implies Result Vector is perpendicular to both vectors
            float dotProdAResult = Vector3.Dot(vecA, xPrd);
            float dotProdBResult = Vector3.Dot(vecB, xPrd);

            Debug.LogFormat("x (Cross Prod) directionres: {0}; mag: {1}", directionRes, magRes);
            Debug.LogFormat("Check Dot Product A-Result: {0}; B-Result: {1}", dotProdAResult, dotProdBResult);
            vectorResult.transform.position = positionStartRes;
            vectorResult.transform.localScale = magVectorRes;
            vectorResult.transform.rotation = Quaternion.identity;
            vectorResult.transform.up = directionRes;

            strResult = "Magnitude of green vector = " + Math.Round(100.0f * magRes, 1).ToString("0.0cm");

            // SW 2023-10-15
            if (Math.Round(100.0f * magRes, 1) > 9.5 && Math.Round(100.0f * magRes, 1) < 10.5)
            {
                result = true;
            }
            else if (Math.Round(100.0f * magRes, 1) > 4.5 && Math.Round(100.0f * magRes, 1) < 5.5)
            {
                result = true;
            }
            else
            {
                result = false;
            }

            vectorResult.SetActive(true);
            vectorScaled.SetActive(false);
            vectorShadowA.SetActive(false);
            vectorShadowB1.SetActive(true);
            vectorShadowB2.SetActive(false);
        }
        // if the answer is correct, make the text green as well, otherwise set to default colour
        if (result)
        {
            textResult.color = Color.green;
        }
        else
        {
            textResult.color = Color.white;
        }
        textResult.text = strResult;
    }


    // rm 2023-09-18
    // draws the Shadow Vector 
    void DrawShadowVector(GameObject ctVector, GameObject shadowVector, Vector3 posStart)
    {
        GameObject st = ctVector.transform.Find("SphereTail").gameObject;
        GameObject ah = ctVector.transform.Find("Arrowhead").gameObject;

        Vector3 positionStart = st.transform.position;
        Vector3 positionEnd = ah.transform.position;
        Vector3 direction = positionEnd - positionStart;
        float mag = direction.magnitude;
        // Note: 10.0f is because cylinder is 0.1 scale and positioned 0.05 (Don't need to divide by 2)
        Vector3 magVector = new Vector3(1.0f, 10.0f * mag, 1.0f);
        Vector3 normVector = direction.normalized;
        Quaternion rotation = Quaternion.LookRotation(normVector);
        Debug.LogFormat("DrawShadowVector Debug rotation: {0}; direction: {1}; mag: {2}", rotation, direction, mag);

        shadowVector.transform.position = posStart;
        shadowVector.transform.localScale = magVector;
        shadowVector.transform.rotation = Quaternion.identity;
        shadowVector.transform.up = direction;

        shadowVector.SetActive(true);
    }


    // rm 2023-09-22
    // draws a "Shadow" or "Scaled" Vector FROM positionStart TO posEnd
    void DrawFromToVector(GameObject ctVector, GameObject shadowVector, Vector3 posStart, Vector3 posEnd)
    {
        Vector3 direction = posEnd - posStart;
        float mag = direction.magnitude;
        // Note: 10.0f is because cylinder is 0.1 scale and positioned 0.05 (Don't need to divide by 2)
        Vector3 magVector = new Vector3(1.0f, 10.0f * mag, 1.0f);
        Vector3 normVector = direction.normalized;
        Quaternion rotation = Quaternion.LookRotation(normVector);
        Debug.LogFormat("DrawFromToVector Debug rotation: {0}; direction: {1}; mag: {2}", rotation, direction, mag);

        shadowVector.transform.position = posStart;
        shadowVector.transform.localScale = magVector;
        shadowVector.transform.rotation = Quaternion.identity;
        shadowVector.transform.up = direction;
        
        shadowVector.SetActive(true);
    }
    // rm 2023-09-11
    // draws the Scaled Vector (as vectorResult) - depending on the vectorOperation: -3, -2, -1, 0, 1, 2, 3 
    void DrawScaledVector(GameObject ctVector, GameObject vectorScaledOrResult, float scaleFactor)
    {
        Debug.Log("DrawScaledVector");
        
        GameObject gobjThisTail;
        GameObject gobjThisCyl;
        GameObject gobjThisHead;
        gobjThisTail = ctVector.transform.Find("SphereTail").gameObject;
        gobjThisCyl  = ctVector.transform.Find("Cylinder").gameObject;
        gobjThisHead = ctVector.transform.Find("Arrowhead").gameObject;
        
        if (gobjThisTail == null)
        {
            Debug.Log("ERROR: DrawScaledVector - neither vectorA or vectorB were found");
            vectorScaledOrResult.SetActive(false);
        }
        else
        {
            // work out position, rotation and scale for Result Vector
            Vector3 positionStart = gobjThisTail.transform.position;
            Vector3 positionEnd = gobjThisHead.transform.position;

            if (GlobalVectorOperation.vectorOperation == "0")
            {
                vectorScaledOrResult.transform.Find("SphereTail").gameObject.SetActive(true);
                vectorScaledOrResult.transform.Find("Cylinder").gameObject.SetActive(false);
                vectorScaledOrResult.transform.Find("Arrowhead").gameObject.SetActive(false);
            }
            else
            {
                Vector3 direction = (positionEnd - positionStart);
                float mag = scaleFactor * direction.magnitude;
                // Note: 10.0f is because cylinder is 0.1 scale and positioned 0.05 (Don't Divide by 2, just because that's default size of cylinder.)
                Vector3 magVector = new Vector3(1.0f, 10.0f * mag, 1.0f);   // <<< CHECK this is right
                Vector3 normVector = direction.normalized;
                Quaternion rotation = Quaternion.LookRotation(normVector);
                Debug.LogFormat("DrawScaledVector rotation: {0}; direction: {1}; scaleFactor: {2}; mag: {3}", rotation, direction, scaleFactor, mag);

                vectorScaledOrResult.transform.position = positionStart;
                vectorScaledOrResult.transform.localScale = magVector;
                vectorScaledOrResult.transform.rotation = Quaternion.identity;
                vectorScaledOrResult.transform.up = direction;

                Vector3 stLocalScale;
                stLocalScale = vectorScaledOrResult.transform.Find("SphereTail").gameObject.transform.localScale;
                Debug.Log("DrawScaledVector SphereTail.localScale: " + stLocalScale);
                //vectorResult.transform.Find("SphereTail").gameObject.transform.localScale = new Vector3(1.1f, 1.1f / Math.Abs(mag), 1.1f);
                stLocalScale = new Vector3(1.1f, 1.1f / Math.Abs(mag), 1.1f);
                Debug.LogFormat("DrawScaledVector SphereTail.localScale: {0}", stLocalScale);
                vectorScaledOrResult.transform.Find("SphereTail").gameObject.SetActive(true);
                vectorScaledOrResult.transform.Find("Cylinder").gameObject.SetActive(true);
                vectorScaledOrResult.transform.Find("Arrowhead").gameObject.SetActive(true);

                vectorScaledOrResult.SetActive(true);
            }
        }
    }


    // rm 2023-09-11
    // draws the Result Vector - depending on the vectorOperation: +, -, . (dot product), X (cross product) 
    string CheckProximityOfVectors(GameObject ctVectorA, GameObject ctVectorB)
    {
        bool result = false;
        string operationOrder = "none";       // "none" - operation invalid' / "AB" / "BA"
        Vector3 distance1 = new Vector3(1.0f, 1.0f, 1.0f);  // if used will default result to false
        Vector3 distance2 = new Vector3(1.0f, 1.0f, 1.0f);  // if used will default result to false
        if (GlobalVectorOperation.vectorOperation == "+" || GlobalVectorOperation.vectorOperation == "-")
        {
            // check if A Head and B Tail objects are close enough to do operation (must be within 30mm=diameter of physical object)
            GameObject gobjAHead = ctVectorA.transform.Find("Arrowhead").gameObject;
            GameObject gobjBTail = ctVectorB.transform.Find("SphereTail").gameObject;
            distance1 = gobjAHead.transform.position - gobjBTail.transform.position;

            // check if A Tail and B Head objects are close enough to do operation (must be within 30mm=diameter of physical object)
            GameObject gobjATail = ctVectorA.transform.Find("SphereTail").gameObject;
            GameObject gobjBHead = ctVectorB.transform.Find("Arrowhead").gameObject;
            distance2 = gobjBHead.transform.position - gobjATail.transform.position;
        }
        else if (GlobalVectorOperation.vectorOperation == "x" || GlobalVectorOperation.vectorOperation == ".")
        {
            // check if A Tail and B Tail objects are close enough to do operation (must be within 30mm=diameter of physical object)
            GameObject gobjATail = ctVectorA.transform.Find("SphereTail").gameObject;
            GameObject gobjBTail = ctVectorB.transform.Find("SphereTail").gameObject;
            distance1 = gobjATail.transform.position - gobjBTail.transform.position;
        }
        if (Math.Abs(distance1.x) <= proximityDistance && Math.Abs(distance1.y) <= proximityDistance && Math.Abs(distance1.z) <= proximityDistance)
        {
            result = true;
            operationOrder = "AB";
        }
        else if 
            (Math.Abs(distance2.x) <= proximityDistance && Math.Abs(distance2.y) <= proximityDistance && Math.Abs(distance2.z) <= proximityDistance)
        {
            result = true;
            operationOrder = "BA";
        }

        Debug.Log("CheckProximityOfVectors: " + result);
        return operationOrder;

    }

}
