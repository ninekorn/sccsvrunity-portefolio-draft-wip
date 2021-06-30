# sccsUnity3DIKNVoxels
I finished building the first version of this in 2017 but where wasn't any VR IK first person controller, there was only a first person controller and a voxel planet. 
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
currently but i'm pretty certain i did a copy paste of the direct links to their forum posts somewhere inside of my backup projects (to find later). And of course i couldn't
move forward without looking every now and then at Wolfram's circle circle intersection https://mathworld.wolfram.com/Circle-CircleIntersection.html .

There are a couple of scripts from unifywiki inside of there and meshcombine also is not from me, and the noise.cs either. I developped the cube fractures scripts on my own in 2016-2017 with simple minor changes this year but they aren't that great but it works. Also, i am doing instancing tests to see if i can make some form of a instancing queue for breakable voxels fracturing in parts so that the parts are instances instead of being instantiated... 

Thank you for reading me, steve chassé aka ninekorn
