## Introduction  

Instruction for Multi-User painting demo.  

## Installation  

1. Android SDK for android devices  

## Setup for Android devices  

1. File -> Build Settings  
2. Choose "Android"  
3. Click "Switch Platform"  
4. Afterwards, Click "Player Settings"  
5. Change "Product Name" and "Company Name"  
6. In "Resolution and Presentation", find "Orientation" section, and change "Default Orientation" to "Landscape Left"  
7. In "Other Settings", find "Identification" section, and change "Bundle Identifier"  
8. Finally, Build and Run.  

## Setup for Different Users  

1.  Assume you are User "X"
2. In "Hierarchy", Find "Paint3DInterface -> Paint3DMain", and look at "Inspector" change "User_id" to "X" (which is the id for user)  
3. In "Hierarchy", Find "MinVRUnityClient(Ver 0.1) -> VRCameraPair", and look at "Inspector" change "User_id" to "X" (which is the id for user)  

