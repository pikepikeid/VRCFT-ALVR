# This is an unofficial version.
Please note that functionality is not guaranteed. Proceed at your own discretion.

これは非公式版です。自己責任でお使いください。

# VRCFT-ALVR

VRCFaceTracking module for ALVR

## Build from source

### Prerequisites

* ALVR (nightly)
* VRCFaceTracking
* Visual Studio

### Build

1. Download this repository.
2. Build project `ALVRModule` in the Visual Studio solution. The module will be ready to use.

## Usage

1. Launch VRCFaceTracking. Uninstall all modules except `ALVRModule`.
2. Launch ALVR. Set `Eye and face tracking` to `VRCFaceTracking`. Then connect the client.
3. Launch VRChat and enable face tracking in the settings.

Note: VRCFaceTracking and ALVR can be launched in any order. If you launch VRCFaceTracking after VRChat, you need to reload your avatar.
