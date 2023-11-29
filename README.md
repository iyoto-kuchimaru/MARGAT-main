# MARGAT Group 10 README
## Overview

MARGAT began as a CS715 course project, in July 2023.<br/>
There are two parts to this app plus a main menu screen.

All scripts are in the assets folder, unless specified otherwise.

An Android phone with at least Android 8.0 is required to run the application. To use the AR Sandbox section, you will also have to make the tangible components, described further in the Vectorfy section of the readme.

## Build Process
The build process will be highlighted below, however there is also an APK file of the built project in the root directory named "VectorfyFinal.apk" as the build process is non-trivial. Simply move this APK file to your phone and open the file, then when prompted, press the install app button.


1. Open the Vectorfy4 folder as a Unity project
2. If downloaded from GitHub, you will need to create a Vuforia account, and import the Vuforia engine into the project
2. Ensure that your Android device has USB debugging enabled
3. In file --> build settings, switch platform to android and set your device once plugged in
4. Add the scenes in the order of IntroScreen, Level3, Iyoto_Ruikai2

These steps should look like the following:

![BuildSettings](/ReadmePictures/BuildSettings.png)

Now, pressing player settings at the bottom left of the corner should open android player settings.
5. Ensure that Minimum API Level is Android 8.0 'Oreo', Scripting Backend is IL2CPP, and the ARM64 Architecture is the only target architecture ticked, although this could be different depending on device.

This should look like the following:

![PlayerSettings](/ReadmePictures/PlayerSettings.png)

6. You should now be able to press Build and Run on the build settings screen, which should open the app on your Android device once the compilation is complete.

## Main Menu (IntroScreen)
This is the main menu of the application, and includes simple ways to navigate to the first and second sections of the application. Two scripts are attached to two buttons on the GUI.

**Key Scripts:** <br/>
[EnterCoordinateScene.cs](/Vectorfy4/Assets/EnterCoordinateScene.cs) - This navigates the user into the 3D space testing coordinates scene.
[EnterSandboxScene.cs](/Vectorfy4/Assets/EnterSandboxScene.cs) - This navigates the user into the AR Sandbox scene.

## Finding Point (Iyoto_Ruikai2)
This is the first section of the app.

![ARMarker](/ReadmePictures/ARMarker.png)

After launching the app, press 3D space testing from the main menu and hold the camera over the AR marker image to jump to the Finding Point Game menu. From there, there are three problems that allow you to participate in a game of finding the vertex of a pyramid, a prism, and a cube, respectively. <br/>
Text for the games needs clarifying… but that imposes difficulties, in itself.

To play, all you have to do is follow the text written on the screen. It is created to fit within a 7x7x7 grid. <br/>
For Question 2, please create a rectangle that is longer in the x-axis direction than the z-axis direction.

Bug: You must be able to see the marker to go back to the IntroScreen.

## Vectorfy (Level3)
This is the second section of the app, the tangible AR Vector sandbox.<br/>
Images were cut-and-pasted onto 100mm long sections of 30mm diameter dowel (available from Mitre10, or Bunnings).

The Vuforia engine package is heavily utilised here, and we use Vuforia databases to scan the images.

**Key Scripts:** <br/>
[DetectTarget.cs](/Vectorfy4/Assets/DetectTarget.cs) - This updates the global parameter targetsDetected when a target is found or lost.
[DrawARObjects.cs](/Vectorfy4/Assets/DrawARObjects.cs) - This file is in depth, and its methods are described below.<br/>

**public class DrawARObjects (Included Methods)**: <br/>
void DrawResultVector(GameObject ctVectorA, GameObject ctVectorB, GameObject vectorResult, GameObject vectorScaled) - This function draws a GREEN result vector for the result of a vector operation.

void DrawShadowVector(GameObject ctVector, GameObject shadowVector, Vector3 posStart) - This function draws secondary supporting vectors for understanding, with the vectors being more transparent rather than solid.

void DrawScaledVector(GameObject ctVector, GameObject vectorScaledOrResult, float scaleFactor) - This function draws a scaled vector based on a single vector, extending in the positive or negative direction. The result vector is GREEN.

Note the use of a Scriptable Object (rather than a Mono Behavior) in [GlobalStringVariable.cs](/Vectorfy4/Assets/GlobalStringVariable.cs)
```
public class GlobalStringVariable : ScriptableObject
{
    public string value;
}
```
This is used to create an asset [TargetsDetected](/Vectorfy4/Assets/TargetsDetected.asset) whose value is updated by [DetectTarget.cs](/Vectorfy4/Assets/DetectTarget.cs).

**Images Required:**<br/>
The following section describes the images we use in our Vuforia database to place onto the cylinders, for the tangible vector object tracking in the AR Sandbox scene.

Rocket Image: tail to head should be 100mm (10cm)

![Rocket](/ReadmePictures/RocketCylinder.png)

The following images should fit on the end of the 30mm dowel cylinders.

![ArrowTailEnd](/ReadmePictures/ArrowTailEnd.png)
![ArrowTailHead](/ReadmePictures/ArrowTailHead.png)
![BHind](/ReadmePictures/BHind.png)
![Bhead](/ReadmePictures/BHead.png)

Destiny Image: (tail to head - on the right hand side as the “arrow” directs) should be 10cm. <br/>
[Note: Next time, do not use text that appears “upside down” when the arrow points up.] 

![Destiny](/ReadmePictures/DestinyImage.png)

These two cylinders can now be used in the AR Sandbox section of the application, and can be held in front of the camera as tangible AR vectors to use for the vector operations.