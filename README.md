# Effect of Hand-movement on the Presence of another Person in a Virtual Environment

Project for the lecture VR+AR in SS19.

We used the following Librarys:

- SteamVR
- LeapMotion (Core, Hands)
- Photon Pun 2

## Setup

- Hardware:
  - 2 computers
  - 2 VR-Headsets
  - 2 Leap Motions
  - 2 3D-printed Leap Motion mount
  - 1 network  connecting these 2 computers
- Software:
  - start the photon server software (this computer need internet access)
  - make sure you selected the right IP in the photon-server, so the server binds to the correct adress. (right click on the tray-icon)
  - disable the firewall
  - start Unity
  - edit `Assets/Photon/PhotonUnityNetworking/Resources/PhotonServerSettings` in Unity and adjust the values to your network setup
  - start the scene on both computer
  - the first one joining will be the master of the scene (have the controll over the different conditions)
  - button `1`, `2`, `3` switches between the three different conditions
  - button `g` and `h` starts and stops the recording, and for condition 3 starts the animation
  - button `arrow right` and  `arrow left` switches between the different pictures and questions.