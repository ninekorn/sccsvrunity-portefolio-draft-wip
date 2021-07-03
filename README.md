<a href="https://ibb.co/1LDDppv"><img src="https://i.ibb.co/Ns88kkV/sccsvrunityv0.png" alt="sccsvrunityv0" border="0"></a>

# sccsUnity3DIKNVoxels
I finished building the first version of this in 2017 but there wasn't any VR IK first person controller inside of my project, only the basic unity3d vr controller, and a voxel planet with settings for the planet able to be changed only from the inspector so it was very basic and the settings in my unity3d project still are very basic as i didn't take the time to go check some tutorials on the UI but i did save them in a youtube list somewhere. 
Last year i developped more of it and added destroyable voxels, and the voxels creation and destruction is still based on the craig perko 1st youtube minecraft tutorial.
This year to this project, i added an IK VR Rig with the unity physics engine and the jitter physics that alexzzzz made available to the community of unity3d by integrating 
the physics engine jitter and you can find his post here https://forum.unity.com/threads/jitter-physics-engine-vs-built-in-physx.186325/ . Before i coded SCCoreSystems Inverse
Kinematics, i had purchased the asset FinalIK inside of Unity3d and you can find a trailer of what i was doing with finalIK here
(https://www.youtube.com/watch?v=SvtnJIKC2VY&amp;ab_channel=SteveChass%C3%A9) and it was great but i wanted to learn the basics of Inverse kinematics. So i searched and found 
on youtube easy basic to learn assets https://www.youtube.com/watch?v=kqbEoa7BGMY&amp;list=PLKlaUriRRX5MssOWHyJZcGf0L__QjtyXQ&amp;ab_channel=ProgramYourFace 
https://www.youtube.com/watch?v=EQ6UKCftHCE&amp;list=PLKlaUriRRX5MssOWHyJZcGf0L__QjtyXQ&amp;index=8&amp;ab_channel=ProgramYourFace . The problem when i worked in IK inside
of SCCoreSystems is that the unity3d Quaternion functions for rotating scene objects weren't available outside of unity3d. I tried to search for tutorials and forum posts
explanations on quaternions on how to rotate objects outside of unity3d and i thought i had found what i wanted inside of the delta engine quaternion functions
https://github.com/DeltaEngine/DeltaEngine/blob/master/Datatypes/Quaternion.cs. But i really hate using more dll's or references than i have to, although those quaternion
functions look great, so i decided to search more and finally found a post on the Unity3d forum from user Aldonaletto that explains how to get directions vectors out of
a quaternion inside of unity3d and the same thing worked outside of unity3d. I have been using that Aldonaletto equations to make rotations
(https://answers.unity.com/users/11109/aldonaletto.html). I also found at some point that the equations were also on pastebin but i cannot find either of those 2 posts
currently but i'm pretty certain i did a copy paste of the direct links to their forum posts somewhere inside of my backup projects (to find later). I think even ProgramYourFace letting his audience know where to find the equation on Wolfram's is what led me to go learn and understand how to developp a single equation on wolframs circlecircle intersection "solving for x", so you will notice that the inverse kinematics that i am using is a slightly modified version of ProgramYourFace. The equation is here circle circle intersection webpage here https://mathworld.wolfram.com/Circle-CircleIntersection.html .

There are a couple of scripts from unifywiki inside my projects like the meshcombine script slightly modified maybe and the scripts to make a plane/grid also are not from me, and the noise.cs either. I developped the cube fractures scripts on my own in 2016-2017 with simple minor changes this year but they aren't that great but it works. Also, i am doing instancing tests to see if i can make some form of an instancing queue for breakable voxels fracturing in parts so that the parts are instances instead of being instantiated, without using the kernel like how it was done in the KvantWall asset here https://github.com/keijiro/KvantWall... 

instead, i had an idea to make it work with the basic instancing methods unity3d provides because i don't want to learn something new again that would send me in a coding challenge of a couple of weeks or something. The idea that i had, is that since i am able to change the position and rotation of instanced objects already inside of unity3d to a satisfying level, nothing stops me from drawing as much instances as there will be blocks breaking and changing the position and rotation of the instances so that they match the position of single cubes breaking on the voxel planet. I beleive it to be entirely possible and that others have probably done it before me anyway. The only problem is that i only can think of instances being drawn linearly from index 0 to 100 if you would have 100 instances and they are drawn in order. 

using drawinstances of unity3d:
The idea that i had with using the instancing methods of unity is around a simple test of rendering 2 units of cubic voxels and 2 units of instances of those cubic voxels with time as the factor of when rendering of the instances should stop when one voxel is being broken after the other, without any other sort or form of factors like user manipulation after the voxel breaks and without physics. if we break voxel block 1 (at index 0 of the instantiated gameobjects list) and flip a switch to draw voxel block instance 1 (at index 0 of the instances list) where said voxel block 1 is broken, and then break voxel block 2 (at index 1 of the instantiated gameobjects list) and draw voxel block instance 2 (at index 1 of the instances list) where said voxel block 2 was broken (at index 1 of the instantiated gameobjects list). block 1 was broken before block 2 so block 1 should disappear from rendering before block 2 because it was broken before block 2. This causes an issue if the list of instances isn't sorted with time as a factor for drawing, because drawinstance draws the instances from the bottom of the list to the top of the list or however we arrange our lists of instances so in order to have instance 2 being drawn as the index 0 in drawinstances so that instance 1 isn't drawn anymore with darwinstances, you would have to rearrange your list of instances so that instance 1 isn't rendered and is swapped with instance 2 in order to limit the method drawinstances from drawing 1 instance or 2 instances at the right position so swapping the rotation and position should also be done if swapping the rendering position of the instance. I was almost there with my tests and you can find the tests in a folder named around instances and in the shader class of those instances scripts.

Thank you for reading me, steve chassé aka ninekorn

Please note that other programmers references to code/scripts/libraries that i am using have their own licenses. In this case, version sccsvrv2 is using grid shaders that aren't mine and they have their own licenses that i will be providing very soon also.

stackoverflow forums https://meta.stackexchange.com/questions/12527/do-i-have-to-worry-about-copyright-issues-for-code-posted-on-stack-overflow https://gamedev.stackexchange.com/help/licensing
unity 3d forums https://forum.unity.com/threads/license-on-code-in-forum-posts.714107/ https://unity3d.com/legal/terms-of-service/site-and-communities?_gl=1*1e9e77l*_ga*NTYzMjUzMDUwLjE2MjUwNjkxMjE.*_ga_1S78EFL1W5*MTYyNTI3NjY0NC4zLjAuMTYyNTI3NjY0NC42MA..&_ga=2.158021242.1379019786.1625276645-563253050.1625069121
unifywiki https://wiki.unity3d.com/index.php/Main_Page "Creative Commons Attribution Share Alike."





