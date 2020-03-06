dice_unity 1.0 <01.10.2019>

dice_unity - README 
-------------------------------------------------------------------     

dice_unity is a package for enhancing contrast in unity scenes. The
DiCE paper can be found here along with other links: 
https://www.cl.cam.ac.uk/research/rainbow/projects/dice/
        
1. Post Effect Setup
-------------------------------------------------------------------
The simplest setup only requires you to attach the 'DiCE_PostEffect'
script to the active VR camera and it will automatically turn on at
runtime.

2. DiCE Manager & DiCE Settings
-------------------------------------------------------------------
To control the DiCE effect place the 'DiCE_Manager' script anywhere
in the scene. The default controls are:
  • 'T' key to toggle effect on/off
  • 'E' key to switch tone curves between eyes
  • 'Up' & 'Down' to switch levels (default is lowest)
  • 'R' & 'F' to fade effect in/out

The static class 'DiCE_Settings' exposes methods to change the DiCE effect,
the DiCE_Manager uses these methods, but if you want fine control over
the effect then you can implement a custom Manager and use the DiCE_Settings
methods to tweak DiCE.
Both the 'DiCE_Manager' and the 'DiCE_Settings' work with the '1. Post 
Effect Setup' and also the '3. Fragment' shader setup.

3. Fragment & Mobile
-------------------------------------------------------------------
For desktop VR please use the "1. Post Effect Setup" as then DiCE is
applied to the final pixel colour AFTER tonemapping. This is the setup
described in the DiCE paper.

However if you are targeting mobile VR this can have a significant 
performance hit (3b) and so an alternative method has been included.
To use this you must replace the shaders in ALL materials with DiCE
versions of the shaders. The procedure for this is described in 3a.
This way the DiCE code runs at the end of the fragment shader and 
modifies the returned colour. 
IMPORTANTLY this means DiCE is applied before tonemapping and isn't 
applied when calculating some lighting effects! And so is a rough 
approximation of the actual effect described in the DiCE paper.

3a. Built in shaders
-------------------------------------------------------------------
DiCE versions of a few Unity built in shaders are included, they are
commented to show the process of converting them. These are from Unity
version 2018.4.8f1 and so may not work with other versions, please
follow the instructions below to convert shaders on your own:

  a:
Unity's built in shaders can be found here: 
  https://unity3d.com/get-unity/download/archive
find your Unity version and select the 'Built in shaders' option
in the dropdown. Now find your shader in the file you downloaded.
  b:
In the shader you have to find the 'fragment' function if so goto d:
otherwise you will find something this:
  #pragma fragment fragBase
which means you have to look for 'fragBase' rather than 'fragment'.
  c:
This means that the functions will probably be in an include. Follow 
the #includes until you find the fragment function.
  d:
Before the fragment function add:
  #include "Assets/Rainbow/DiCE/General/DiCE.cginc"
then take the return value of the function and call:
  returnCol.rgb = GetDiceCol(returnCol.rgb, i.screenPos.x / i.screenPos.w);
where 'returnCol' is the final fragment function colour.
You must ensure that the input struct 'i' contains a definition for a
screenPos so that the postion on the screen can be calculated.
If not add the definition to the struct. Then add the following:
  o.screenPos = ComputeScreenPos(UnityObjectToClipPos(v.vertex));
to the vertex function.

Look through the example shaders in 'Assets/Rainbow/DiCE/b_Fragment'
for more documentation on this.

3b. Oculus Quest
-------------------------------------------------------------------
I suspect the reason the post effect is slow on mobile (tested on Quest)
is due to GPU memory size/bandwith limitations with mobile tiled gpus:
 https://youtu.be/JvMQUz0g_Tk?t=565
The post effect would essentially have to copy all data from the 
framebuffer in main memory to the GPU and back to apply the effect.
