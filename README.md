<!-- wp:paragraph -->
<p><strong>English description/description anglaise: </strong><br>I have just uploaded 4 of my projects that i have developped on unity3d 2017.4.40f1 and they are part of my creations with the Oculus Rift cv1 in unity 2017.4.40f1. I have decided to call the "portefolio/series/suite" sccsvrunity and this is using the base vr sdk of unity 2017.4.40f1.</p>
<!-- /wp:paragraph -->

<!-- wp:paragraph -->
<p>download link: https://github.com/ninekorn/sccsvrunity-portefolio-draft</p>
<!-- /wp:paragraph -->

<!-- wp:paragraph -->
<p>sccsvrunity-v0 - after multiple tries with minecraft projects in unity, this is the one i deemed worthy of sharing and the ones that followed. I am using my fracture scripts (except 1 i think) i developped in 2016. The ik rig i learned how to do it by going on youtube and finding listening to ProgramYourFace which explained that the equations are on wolframs. I went there abd found another asset on github for inverse kinematics and also read and downloaded the scripts of user <a href="https://forum.unity.com/members/dogzerx2.13856/">dogzerx2</a> <a href="https://forum.unity.com/threads/free-inverse-kinematics-script.102765/">Free Inverse Kinematics script! - Unity Forum</a>. Even scripts as big as his are too complicated for me. The voxel terrain is based on what i have learned from the old and new minecraft tutorials of Craig Perko on youtube also.</p>
<!-- /wp:paragraph -->

<!-- wp:image {"id":622,"sizeSlug":"large","linkDestination":"none"} -->
<figure class="wp-block-image size-large"><img src="https://sccoresystems.files.wordpress.com/2021/07/sccsvrunityv0.png?w=1024" alt="" class="wp-image-622"/></figure>
<!-- /wp:image -->

<!-- wp:paragraph -->
<p>sccsvrunity-v1- I have incorporated the jitter physics engine that the user alexzzzz had migrated from monogame to unity. I had decided to use his migration of the jitter physics engine  and incorporate it to my project. You can find alexzzzz post/project on the complete basic systems of the jitter physics incorporated for the unity engine here <a href="https://forum.unity.com/threads/jitter-physics-engine-vs-built-in-physx.186325/">Jitter physics engine vs built-in Physx - Unity Forum</a> but i couldn't use the same functions i was using with version v0 for the ik collision with the floor so i developped something different. It's a little bit clunky but it works. And developping this jitter project helped me understand how i could make a raycast move forward per frame so that it's origin point moves in the direction it is aiming at and this technique i developped proved helpful and it inspired me to move forward and to try to break voxels using the same principles.</p>
<!-- /wp:paragraph -->

<!-- wp:image {"id":621,"sizeSlug":"large","linkDestination":"none"} -->
<figure class="wp-block-image size-large"><img src="https://sccoresystems.files.wordpress.com/2021/07/sccsvjitter0.png?w=1024" alt="" class="wp-image-621"/></figure>
<!-- /wp:image -->

<!-- wp:paragraph -->
<p>sccsvrunity-v2- I developped a different digging system than v0 to make voxels destroyable without using the collision and raycast system of unity. Also i am using here the same ik than v0. Here the IK is working with the unity physics engine but the voxel destruction is not. <em>The inverse kinematics system here in my repos isn't very complicated and barely approached in code in what i did. I am simply using the "solve for x" of wolfram's circle circle intersection everywhere in the limbs in this suite/series/portefolio and also in SCCoreSystems. </em></p>
<!-- /wp:paragraph -->

<!-- wp:image {"id":620,"sizeSlug":"large","linkDestination":"none"} -->
<figure class="wp-block-image size-large"><img src="https://sccoresystems.files.wordpress.com/2021/07/v2ik0.png?w=1024" alt="" class="wp-image-620"/></figure>
<!-- /wp:image -->

<!-- wp:paragraph -->
<p>sccsvrunity-v3. I succeeded in developping a couple of lines of code to miniaturize voxels and make voxels destroyable without using the collision and raycast system of unity. And after having developped and creating myself those assets (minus those that i referenced) for my brother Patrick's birthday, i wanted to increase the performance of this project series and once i would have understood and developped the whole of it in the editor of my choice unity3d (doing a migration/copy of the same thing in the ab3d.dxengine and monogame remains one of my goals), it was then going to be possible to use my earned knowledge for finally breaking voxels through the shader in my solution SCCoreSystems here  <a href="https://github.com/ninekorn/SCCoreSystems-rerelease">ninekorn/SCCoreSystems-rerelease (github.com)</a> but i didn't approach that issue yet in SCCoreSystems. I am still not using the shader for instancing and breaking voxels in this portefolio sccsvrunity at least not for the moment because i use the cpu for the calculations instead of the gpu (shader) and because i am barely started with instancing in unity. I developped these portefolio drafts starting end of april 2021 until June 2021 in my coding challenge but the planet generation/creation itself, i succeeded in 2017 and all of this in the goal of better understanding how voxels are working to upgrade my solution SCCoreSystems.</p>
<!-- /wp:paragraph -->

<!-- wp:image {"id":675,"sizeSlug":"large","linkDestination":"none"} -->
<figure class="wp-block-image size-large"><img src="https://sccoresystems.files.wordpress.com/2021/07/sccsvrunityv3.png?w=1024" alt="" class="wp-image-675"/></figure>
<!-- /wp:image -->

<!-- wp:paragraph -->
<p>My planet generation screenshot made on unity 5.5.2f1 in 2017 and my facebook page where i had posted it <a href="https://www.facebook.com/photo?fbid=271787936628041&amp;set=pcb.271788079961360">(20+) Facebook</a> with the cpu made voxels based on Craig Perko's old minecraft tutorial and i am still using the same way of creating voxels today and i have understood and applied the principles of creating voxels and instancing them with the cpu/gpu in low level programming under my SCCoreSystems solution here <a href="https://github.com/ninekorn/SCCoreSystems-rerelease">ninekorn/SCCoreSystems-rerelease (github.com)</a>:</p>
<!-- /wp:paragraph -->

<!-- wp:image {"id":680,"sizeSlug":"large","linkDestination":"none"} -->
<figure class="wp-block-image size-large"><img src="https://sccoresystems.files.wordpress.com/2021/07/18768330_271787936628041_4539584774789112718_o.jpg?w=1024" alt="" class="wp-image-680"/></figure>
<!-- /wp:image -->

<!-- wp:paragraph -->
<p>Here is a video of my fracture script in 2016 <a href="https://www.facebook.com/sccoresystems/videos/133258430480993/">(20+) Facebook</a>:<br><br>Those projects, i have developped them myself, in my free time with my creativity and i will provide the references of certain scripts which aren't mine but easily available on unify wiki or/and the stackoverflow forums or/and the unity engine forums.  To see the inverse kinematic in action in my projects, i have read/heard and understood the ik asset on github, with minor modifications and he has two assets here<br><br><a href="https://www.youtube.com/watch?v=EQ6UKCftHCE">https://www.youtube.com/watch?v=EQ6UKCftHCE</a>... and here</p>
<!-- /wp:paragraph -->

<!-- wp:embed {"url":"https://www.youtube.com/watch?v=kqbEoa7BGMY...","type":"rich","providerNameSlug":"youtube","responsive":true,"className":"wp-embed-aspect-16-9 wp-has-aspect-ratio"} -->
<figure class="wp-block-embed is-type-rich is-provider-youtube wp-block-embed-youtube wp-embed-aspect-16-9 wp-has-aspect-ratio"><div class="wp-block-embed__wrapper">
https://www.youtube.com/watch?v=kqbEoa7BGMY...
</div></figure>
<!-- /wp:embed -->

<!-- wp:paragraph -->
<p>and the chunk system i learned it from Craig Perko here</p>
<!-- /wp:paragraph -->

<!-- wp:embed {"url":"https://www.youtube.com/watch?v=YpHQ-Kykp_s...","type":"rich","providerNameSlug":"youtube","responsive":true,"className":"wp-embed-aspect-16-9 wp-has-aspect-ratio"} -->
<figure class="wp-block-embed is-type-rich is-provider-youtube wp-block-embed-youtube wp-embed-aspect-16-9 wp-has-aspect-ratio"><div class="wp-block-embed__wrapper">
https://www.youtube.com/watch?v=YpHQ-Kykp_s...
</div></figure>
<!-- /wp:embed -->

<!-- wp:paragraph -->
<p>Please be gentle, i don't have a diploma, but i'm not that stupid in programming, at least not that stupid in making basic scenes with ik and voxels and vr work in c# (virtual desktops outside of unity also) using the most simplest things as don't get me wrong, there are way better programmers than me around and i prefer keeping things simple although i want to learn more, but in python i'm not that good yet. I have shared a very basic Python scripts on my github repo here <a href="https://github.com/ninekorn/Python-2-way-local-network-communication-WIP">https://github.com/ninekorn/Python-2-way-local-network-communication-WIP</a> for my virtual reality headset i am also building and i needed that to send the capture screen of my project SCCoreSystems to my raspberry pi 4b kali os on my screen ST7735. But i already unveiled some python obj generators as mods for the game void expanse on the atomic torch studios so i'm not afraid to dive in with python. I have been able to display images to the screen ST7735 from the raspberri pi 4b kali os.<br><br>Be ready for a pc streaming vr headset, but not that fast anyway because i'm not even close to being done yet, but i wasn't kidding when i said i had learned to program in my free time, but it sure probably does show in my scripts that i look like i rushed some parts here and there and they lack comments hence why this is called a portefolio and not a triple a game article. it's wip and i will clean and comment them later on. I prefer how my solution SCCoreSystems looks here though.<br><br><a rel="noreferrer noopener" href="https://forums.frontier.co.uk/threads/virtual-desktop-program-with-embedded-physics-engine-at-the-press-of-a-button-coming-in-2020.542577/#post-9105870" target="_blank">https://forums.frontier.co.uk/.../virtual-desktop.../...</a></p>
<!-- /wp:paragraph -->

<!-- wp:paragraph -->
<p>I have always used unity3d for easy editing of voxel scenes or not and i almost always use unity for editing prior to entering low-level programming. <br><br>But i have a problem with the buffer from c# to python and i need to find a solution with the NamedPipeServerStream for sending my screen capture in byte array from c# to the python buffer in windows 10 under visual studio community edition 2017. Currently i am using the System.Text.Encoding.ASCII.GetString to decode the screen capture byte array in string and then i decode it with  System.Text.Encoding.ASCII.GetBytes to send it through the NamedServerPipe and to Python. If i succeed to send the image of my desktop screen capture from my solution SCCoreSystems C# through to Python and then to my raspberri pi 4b kali os, the goal is achieved for a streaming glasses of pc games (minus the virtual reality lenses) and i succeeded in making hand controllers of 3 buttons each + 1 thumbstick (2 triggers to come later). I would only need to understand the accelerometer module GY-521 for the hand controllers rotation and retry the module ESP8266 wifi for communication between the devices and tracking.</p>
<!-- /wp:paragraph -->

<!-- wp:paragraph -->
<p>So, my humongous project of not only doing a virtual reality headset but also voxel games, hopefully will come true after 4++ years of self imposed coding challenge to learn programming.<br><br>This post isn't to brag because there is nothing that extravagant here anyway, it is simply to share 3 voxel projects with inverse kinematics and vr in unity 2017.4.40f1 with the oculus rift cv1 that i have myself developped and that i will keep as a portefolio. I will soon  prepare a presentation video and a resume of those 4++ projects that i have myself completed because it took more than 1 tutorial to be able to get to this point.  😉 </p>
<!-- /wp:paragraph -->

<!-- wp:paragraph -->
<p>The problem when i worked in IK inside of SCCoreSystems is that the unity3d Quaternion functions for rotating scene objects weren't available outside of unity3d. I tried to search for tutorials and forum posts explanations on quaternions on how to rotate objects outside of unity3d and i thought i had found what i wanted inside of the delta engine quaternion functions&nbsp;<a href="https://github.com/DeltaEngine/DeltaEngine/blob/master/Datatypes/Quaternion.cs">https://github.com/DeltaEngine/DeltaEngine/blob/master/Datatypes/Quaternion.cs</a>. But i really hate using more dll's or references than i have to, although those quaternion functions look great, so i decided to search more and finally found a post on the Unity3d forum from user Aldonaletto that explains how to get directions vectors out of a quaternion inside of unity3d and the same thing worked outside of unity3d. I have been using those 3 equations of Aldonaletto to get the forward/up/right vectors from quaternions ever since (<a href="https://answers.unity.com/users/11109/aldonaletto.html">https://answers.unity.com/users/11109/aldonaletto.html</a>). I also found at some point that the equations were also on pastebin but i cannot find either of those 2 posts currently when i am trying to reference people i learned from, but i'm pretty certain i did a copy paste of the direct links to their forum posts somewhere inside of my backup projects (to find later) hopefully and if not that then i had probably set that in a bookmark in my browser(s) a long time ago. </p>
<!-- /wp:paragraph -->

<!-- wp:paragraph -->
<p>I think even ProgramYourFace letting his audience know where to find the equation on Wolfram's is what led me to go learn and understand how to developp a single equation on wolframs circlecircle intersection "solving for x", so you will notice that the inverse kinematics that i am using is a slightly modified version of the github repository where i found very simple ik project. The equation to circle circle intersection webpage is here <a href="https://mathworld.wolfram.com/Circle-CircleIntersection.html">https://mathworld.wolfram.com/Circle-CircleIntersection.html</a> .</p>
<!-- /wp:paragraph -->

<!-- wp:paragraph -->
<p>There are a couple of scripts from unifywiki inside my projects like the meshcombine script slightly modified maybe and the scripts to make a plane/grid also are not from me, and the noise/simplexnoise scripts either. I developped the cube fractures scripts on my own in 2016-2017 with simple minor changes this year but they aren't that great but it works. Also, i am doing instancing tests to see if i can make some form of an instancing queue for breakable voxels fracturing in parts so that the parts are instances instead of being instantiated, without using the kernel like how the instancing is done in the KvantWall asset here&nbsp;<a href="https://github.com/keijiro/KvantWall">https://github.com/keijiro/KvantWall</a>...</p>
<!-- /wp:paragraph -->

<!-- wp:paragraph -->
<p>Instead, since i couldn't modify anything in kvantwall and gave up years ago, i had an idea to make breakable voxel parts instances work with the basic instancing methods unity3d provides because i don't want to learn something new again that would send me in a coding challenge of a couple of weeks or something. The idea that i had, is that since i am able to change the position and rotation of instanced objects already inside of unity3d to a satisfying level, nothing stops me from drawing as much instances as there will be blocks breaking and changing the position and rotation of the instances so that they match the position of single cubes breaking on the voxel planet. I beleive it to be entirely possible and that others have probably done it before me anyway. The only problem is that i only can think of instances being drawn linearly from index 0 to 100 if you would have 100 instances and they are drawn in order. The idea that i had with using the instancing methods of unity is around a simple test of rendering 2 units of cubic voxels and 2 units of instances of those cubic voxels with time as the factor of when rendering of the instances should stop when one voxel is being broken after the other, without any other sort or form of factors like user manipulation after the voxel breaks and without physics. if we break voxel block 1 (at index 0 of the instantiated gameobjects list) and flip a switch to draw voxel block instance 1 (at index 0 of the instances list) where said voxel block 1 is broken, and then break voxel block 2 (at index 1 of the instantiated gameobjects list) and draw voxel block instance 2 (at index 1 of the instances list) where said voxel block 2 was broken (at index 1 of the instantiated gameobjects list). block 1 was broken before block 2 so block 1 should disappear from rendering before block 2 because it was broken before block 2. This causes an issue if the list of instances isn't sorted with time as a factor for drawing, because drawinstance draws the instances from the bottom of the list to the top of the list or however we arrange our lists of instances so in order to have instance 2 being drawn as the index 0 in drawinstances so that instance 1 isn't drawn anymore with darwinstances, you would have to rearrange your list of instances so that instance 1 isn't rendered and is swapped with instance 2 in order to limit the method drawinstances from drawing 1 instance or 2 instances at the right position so swapping the rotation and position should also be done if swapping the rendering position of the instance. I was almost there with my tests and you can find the tests in a folder named around instances and in the shader class of those instances scripts.<br><br>steve chassé aka ninekorn</p>
<!-- /wp:paragraph -->

<!-- wp:separator -->
<hr class="wp-block-separator"/>
<!-- /wp:separator -->

<!-- wp:heading {"fontSize":"normal"} -->
<h2 class="has-normal-font-size">French description/description française:</h2>
<!-- /wp:heading -->

<!-- wp:paragraph -->
<p>Je viens d'uploader 4 de mes projets que j'ai développé sur unity3d 2017.4.40f1 et qui font parti de mes créations avec le Oculus Rift cv1 dans unity 2017.4.40f1. J'ai décidé d'appeler le "portefolio/série/suite" sccsvrunity mais c'est le sdk de base de unity 2017.4.40f1.</p>
<!-- /wp:paragraph -->

<!-- wp:paragraph -->
<p>download link: https://github.com/ninekorn/sccsvrunity-portefolio-draft</p>
<!-- /wp:paragraph -->

<!-- wp:paragraph -->
<p>sccsvrunity-v0 -  Après de multiples essais avec des projets minecraft, celui-ci je le trouvais assez fonctionnel pour le partager ainsi que les autres qui suivent. J'utilise mes scripts de destruction (excepté un je crois) que j'ai developpé en 2016. J'ai appris comment faire le ik rig d'après les explications de ProgramYourFace sur son channel youtube ainsi que <a href="https://forum.unity.com/members/dogzerx2.13856/">dogzerx2</a> <a href="https://forum.unity.com/threads/free-inverse-kinematics-script.102765/">Free Inverse Kinematics script! - Unity Forum</a> et une autre référence sur github. Le terrain voxel est basé sur ce que j'ai appris du vieux tutoriel de Craig Perko aussi sur son channel youtube.</p>
<!-- /wp:paragraph -->

<!-- wp:image {"id":622,"sizeSlug":"large","linkDestination":"none"} -->
<figure class="wp-block-image size-large"><img src="https://sccoresystems.files.wordpress.com/2021/07/sccsvrunityv0.png?w=1024" alt="" class="wp-image-622"/></figure>
<!-- /wp:image -->

<!-- wp:paragraph -->
<p>sccsvrunity-v1-j'ai incorporé le jitter physics engine que le user alexzzzz avait migré de monogame à unity. J'ai donc décidé d'utiliser sa migration du système complet de l'engine de base jitter et de l'incorporer à mon projet. <a href="https://forum.unity.com/threads/jitter-physics-engine-vs-built-in-physx.186325/">Jitter physics engine vs built-in Physx - Unity Forum</a>  mais je n'ai pas été en mesure d'utiliser les mêmes fonctions que j'ai utilisé dans la version v0 pour la collision ik avec le sol alors j'ai développé quelque chose de différent. Mon implementation peut paraître un peu maladroite mais la base fonctionne. Développer ce projet avec l'incorporation du jitter physics engine m'a aider à comprendre comment je devais faire pour faire avancer un vecteur de direction en avant et par frame, pour que le point d'origine du vecteur/raycast et celui-ci bougent en même temps et cette technique a porté fruit et m'a inspiré à marcher de l'avant pour essayer de briser des voxels avec le même principe.</p>
<!-- /wp:paragraph -->

<!-- wp:image {"id":621,"sizeSlug":"large","linkDestination":"none"} -->
<figure class="wp-block-image size-large"><img src="https://sccoresystems.files.wordpress.com/2021/07/sccsvjitter0.png?w=1024" alt="" class="wp-image-621"/></figure>
<!-- /wp:image -->

<!-- wp:paragraph -->
<p>sccsvrunity-v2- système de digging différent de v0, qui n'utilise pas le système de collision du physics engine de unity ni les raycasts, que j'ai moi-même développé et ce sont les même équations de kinématique inverse que mes autres projets sur unity. <em>Mais on parle de kinématique inverse pas vraiment compliqué là, c'est à peine entamé comme sujet dans ce que j'ai fait. Je ne fait qu'utiliser le principe "solve for x" de wolfram's circle circle intersection partout <a href="https://mathworld.wolfram.com/Circle-CircleIntersection.html">Circle-Circle Intersection -- from Wolfram MathWorld</a> </em></p>
<!-- /wp:paragraph -->

<!-- wp:image {"id":620,"sizeSlug":"large","linkDestination":"none"} -->
<figure class="wp-block-image size-large"><img src="https://sccoresystems.files.wordpress.com/2021/07/v2ik0.png?w=1024" alt="" class="wp-image-620"/></figure>
<!-- /wp:image -->

<!-- wp:paragraph -->
<p>sccsvrunity-v3 - J'ai réussi à développer quelques lignes de codes pour miniaturizer le voxel et faire la destruction du voxel sans utiliser le système de collision de unity3d. Donc après avoir créé et développé ces assets moi-même en commençant par v0 pour la fête de mon frère Patrick, je voulais augmenter la performance et une fois que j'allais avoir compris et développé le tout dans l'éditeur actuel de mon choix unity3d (mais j'avais aussi comme choix le ab3d.dxengine ou monogame que j'ai toujours l'intention d'y faire aussi une migration de mes projets), j'allais pouvoir utiliser mes connaissances acquises pour finalement briser les voxels par le shader dans ma solution SCCoreSystems, ce qui n'est toujours pas le cas dans mon portefolio sccsvrunity du moins pour le moment car j'utilise le cpu pour les calculation et non le gpu (shader). J'ai développé ce portefolio-draft et ces projets commençant environ fin Avril 2021 à début Juin 2021 dans mon coding challenge mais la création de la planète elle même, je l'ai réussi en 2017 et tout ça dans le but de mieux comprendre comment ça fonctionne le  voxel pour améliorer entre autres ma solution SCCoreSystems.</p>
<!-- /wp:paragraph -->

<!-- wp:image {"id":675,"sizeSlug":"large","linkDestination":"none"} -->
<figure class="wp-block-image size-large"><img src="https://sccoresystems.files.wordpress.com/2021/07/sccsvrunityv3.png?w=1024" alt="" class="wp-image-675"/></figure>
<!-- /wp:image -->

<!-- wp:paragraph -->
<p>Une image de ma génération de voxel planet fait sur unity 5.5.2f1 2017 et ma page facebook ou je l'avais affichée <a href="https://www.facebook.com/photo.php?fbid=271787936628041&amp;set=pb.100013905109272.-2207520000..&amp;type=3">(20+) Facebook</a> avec la construction de voxel fait par cpu. Et j'utilise toujours le même principe aujourd'hui que celui démontré dans le vieux/nouveau tutoriel de Craig Perko sur youtube, mais j'ai déjà compris et appliqué le principe de créer des voxels et de les instancer avec le cpu/gpu dans le low level programming sous ma solution SCCoreSystems ici <a href="https://github.com/ninekorn/SCCoreSystems-rerelease">ninekorn/SCCoreSystems-rerelease (github.com)</a>.</p>
<!-- /wp:paragraph -->

<!-- wp:image {"id":680,"sizeSlug":"large","linkDestination":"none"} -->
<figure class="wp-block-image size-large"><img src="https://sccoresystems.files.wordpress.com/2021/07/18768330_271787936628041_4539584774789112718_o.jpg?w=1024" alt="" class="wp-image-680"/></figure>
<!-- /wp:image -->

<!-- wp:paragraph -->
<p>Voici un vidéo de mon script de fracture en 2016 <a href="https://www.facebook.com/sccoresystems/videos/133258430480993/">(20+) Facebook</a>:<br><br>Ces projets, je les ai moi-même développé, de mon temps et de ma créativité, et je vais fournir les références de certains scripts qui ne sont pas de moi, mais facilement disponibles sur unify wiki et les forums stackoverflow et unity forums. La kinématique inverse pour la faire fonctionner, j'ai utilisé et compris l'asset que j'ai trouvé sur github, avec de minimes modifications. Plusieurs assets sont dispos pour du ik et ceux de ProgramYourFace sont très attrayant et simple aussi là</p>
<!-- /wp:paragraph -->

<!-- wp:paragraph -->
<p><a href="https://www.youtube.com/watch?v=EQ6UKCftHCE">https://www.youtube.com/watch?v=EQ6UKCftHCE</a>... et là</p>
<!-- /wp:paragraph -->

<!-- wp:embed {"url":"https://www.youtube.com/watch?v=kqbEoa7BGMY...","type":"rich","providerNameSlug":"youtube","responsive":true,"className":"wp-embed-aspect-16-9 wp-has-aspect-ratio"} -->
<figure class="wp-block-embed is-type-rich is-provider-youtube wp-block-embed-youtube wp-embed-aspect-16-9 wp-has-aspect-ratio"><div class="wp-block-embed__wrapper">
https://www.youtube.com/watch?v=kqbEoa7BGMY...
</div></figure>
<!-- /wp:embed -->

<!-- wp:paragraph -->
<p>et le chunk system je l'ai appris en regardant et comprenant le tutoriel du youtubeur Craig Perko ici</p>
<!-- /wp:paragraph -->

<!-- wp:embed {"url":"https://www.youtube.com/watch?v=YpHQ-Kykp_s...","type":"rich","providerNameSlug":"youtube","responsive":true,"className":"wp-embed-aspect-16-9 wp-has-aspect-ratio"} -->
<figure class="wp-block-embed is-type-rich is-provider-youtube wp-block-embed-youtube wp-embed-aspect-16-9 wp-has-aspect-ratio"><div class="wp-block-embed__wrapper">
https://www.youtube.com/watch?v=YpHQ-Kykp_s...
</div></figure>
<!-- /wp:embed -->

<!-- wp:paragraph -->
<p>soyez gentils quand même, je n'ai pas de diplome, mais je ne suis pas con en programation, en tout cas, pas totalement con en c# mais je sais que je ne suis pas excellent non plus, et ça doit être encore plus visible en python, je suis pas encore très fort là. Le code python j'en ai juste besoin pour envoyer le capture screen de mon programme SCCoreSystems à mon raspberri pi 4b sur l'écran ST7735 qui fonctionne déjà sur mon installation kali os, mais j'ai déjà dévoilé quelques générateurs d'items codé sur python et posté sur le forum d'atomic torch en moddant le jeu void expanse.</p>
<!-- /wp:paragraph -->

<!-- wp:paragraph -->
<p>Attachez votre ceinture là, mais quand même pas trop vite parce que j'ai pas fini, mais moi je déconnais pas avec avoir apris à programmer depuis déjà 4++ années de mon temps libre et vouloir faire mon pc streaming virtual reality headset. mais je préfère toujours mes solutions SCCoreSystems que j'ai affiché sur le forum d'elite dangerous ici</p>
<!-- /wp:paragraph -->

<!-- wp:paragraph -->
<p>https://forums.frontier.co.uk/threads/virtual-desktop-program-with-embedded-physics-engine-at-the-press-of-a-button-coming-in-2020.542577/#post-9105870</p>
<!-- /wp:paragraph -->

<!-- wp:paragraph -->
<p>et j'ai toujours utilisé unity3d pour facilement comprendre comment ça fontionne le voxel avant de m'en aller dans le low-level programming.</p>
<!-- /wp:paragraph -->

<!-- wp:paragraph -->
<p>Par contre, j'ai un problème avec le buffer de c# à python et donc là faut que je trouve une solution avec le NamedPipeServerStream pour envoyer mon screen capture en byte array de c# au buffer de python dans windows 10 sous visual studio 2017 community edition. Présentement j'utilise le System.Text.Encoding.ASCII.GetString pour décoder mon byte array en string et ensuite le décoder en System.Text.Encoding.ASCII.GetBytes pour l'envoyer en ASCII bytes au travers du buffer NamedServerPipe à Python... Si je réussi à envoyer l'image de mon desktop screen capture de ma solution SCCoreSystems au travers de python et jusqu'à mon raspberri pi 4b kali os, le tour est joué pour le circuit codé pour une paire de lunette Streaming des jeux de pc (manquant les lentilles de réalité virtuelle) avec des manettes, car le circuit pour les handcontrollers simili Oculus Touch je l'ai réussi il y a déjà plus de 1 mois pour 6 boutons et 2 thumbsticks. Il ne me resterait qu'à comprendre le module accéléromètre GY-521 pour la rotation des deux hand controlleurs et aussi réessayer le module ESP8266 wifi pour la communication des manettes avec le headset ainsi que le module de tracking.</p>
<!-- /wp:paragraph -->

<!-- wp:paragraph -->
<p>Et donc, ça s'en vient mon gargantuan de projet de non seulement faire un virtual reality headset, mais aussi de faire des jeux voxels qui vont venir avec surtout quand ça fait 4 ans que je suis en self-imposed coding challenge. Je ne suis quand même plus une cloche en programmation après 12 heures par jours pendant 4++ années même si je ne suis pas allé à l'école en programmation et que je n'ai pas de diplôme.</p>
<!-- /wp:paragraph -->

<!-- wp:paragraph -->
<p>Ce post n'est pas pour me pêter les bretelles car il n'y a pas grand chose de si extravagant que ça là-dedans anyway, c'est simplement pour partager ces 3 projets de voxels chunk breaking et de kinématique inverse dans unity3d 2017 avec le oculus rift cv1 que j'ai moi-même développé et qui vont désormais aussi faire parti de mon porte-folio. Je vais bientôt faire un vidéo de présentation et résumé de ces 3 projets que j'ai complété moi-même car ce sont plus que un tutoriels que j'ai dû comprendre et apprendre pour en arriver là.&nbsp;😉</p>
<!-- /wp:paragraph -->

<!-- wp:paragraph -->
<p>Le problème quand je travaillais dans le ik dans ma solution SCCoreSystems c'est que les fonctions de unity quaternion pour faire la rotation d'un object dans la scene ne sont pas disponible de base et je pensais que j'avais trouvé la référence nécessaire dans le delta engine quaternion script  <a href="https://github.com/DeltaEngine/DeltaEngine/blob/master/Datatypes/Quaternion.cs">https://github.com/DeltaEngine/DeltaEngine/blob/master/Datatypes/Quaternion.cs</a>. Mais je déteste utiliser plus de référence que ce que j'ai besoin, malgré que ces functions de quaternion du delta engine sont très bien, j'ai décidé de chercher plus et finalement j'ai trouvé un post sur le forum de unity du user Aldonaletto qui expliquait comment retirer les vecteurs de directions d'un quaternion et la même chose a fonctionner en dehors de unity3d. Depuis, j'utilise les equations qu'Aldonaletto avait partagé  (<a href="https://answers.unity.com/users/11109/aldonaletto.html">https://answers.unity.com/users/11109/aldonaletto.html</a>). À un certain point j'avais trouvé les équations aussi sur pastebin mais je ne peux maintenant retrouver ces posts mais je suis certain d'avoir copié le lien comme référence dans un de mes scripts maths.cs. (à chercher plus tard) . Je crois aussi que c'est à cause du fait que le user ProgramYourFace a laisser son audience savoir comment comprendre la kinematique inversé en allant voir l'équation sur l'article intersection cercle-cercle et "solve for x", alors vous allez voir que le ik que j'utilise est celle que j'ai trouvé sur github légèrement modifiée. L'équation pour le cercle-cercle intersection est ici  <a href="https://mathworld.wolfram.com/Circle-CircleIntersection.html">https://mathworld.wolfram.com/Circle-CircleIntersection.html</a>  </p>
<!-- /wp:paragraph -->

<!-- wp:paragraph -->
<p><br>Il y a quelque scripts de unifywiki à l'intérieur de mes projets comme le meshcombine script légèrement modifié peut-être et les scripts pour faire une plane/grid aussi ne sont pas de moi et le noise/simplenoise non plus. J'ai developpé les scripts fracture par moi seul en 2017-2017 avec quelques simples changement cette année mais ils ne sont pas si parfait mais ça fonctionne. Aussi, je fais des tests d'instancing pour savoir si je peux faire une sorte de queue pour briser/fracturer des voxels et que les fractures soient des instances au lieu d'être instantiated, et sans utiliser le kernel comme le fait le magnifique asset KvantWall ici <a href="https://github.com/keijiro/KvantWall">https://github.com/keijiro/KvantWall</a>...</p>
<!-- /wp:paragraph -->

<!-- wp:paragraph -->
<p>À la place, j'avais une idée de faire la méthode de base du instancing que unity3d amène de base, car je ne veux pas partir tout de suite sous ce coding challenge de quelques semaines probablement ;) . L'idée que j'avais, est que vu que je suis capable de changer la position et rotation d'objects instancés déjà à l'intérieur de unity3d à un niveau satisfaisant, rien ne m'empêche de render à l'écran le plus d'instances possible équivalent au nombre de bloques qui brisent dans la scène et que les instances soient désignés la position et rotation à l'endroit au le bloque brise sur la planète voxel. Je crois que c'est entièrement possible et que d'autres programmeurs l'ont probablement déjà fait avant moi. Le seul problèeme est qu'en regardant vite comme ça le instancing de base dans unity, la function drawinstance dessine à l'écran les object en commençant à l'index 0 jusqu'à l'index 100 si on a 100 instances dans la scène mais quoi faire pour faire disparaître l'object 1 si l'object 0 disparait par le temps pour avoir été brisé avant le bloc 1? La méthode drawinstancing les dessines tous en commençant par le bas de la liste qu'on lui donne jusqu'au dernier item à moins qu'on arrête le rendering à un index maximum, mais alors on ferait le rendering 0 ou bien le rendering 0 et 1... ce qui ne marche pas si l'object 0 disparait avant l'object 1 car drawinstancing va le dessiner pareil... Alors il faut changer les objects de positions dans la liste avant de l'envoyer à la méthode drawinstancing.<br><br>thank you for reading me,<br>steve chassé aka ninekorn</p>
<!-- /wp:paragraph -->

<!-- wp:paragraph -->
<p></p>
<!-- /wp:paragraph -->

---------------------
LICENSES SECTION WIP
---------------------

Please note that other programmers references to code/scripts/libraries that i am using have their own licenses. In this case, version sccsvrv2 is using grid shaders that aren't mine and they have their own licenses that i will be providing very soon also and it is the same for craig perko's minecraft/voxel tutorial, ProgramYourFace ik tutorial, stackoverflow forums, unity forums and unifywiki and etc (in this case etc means the moment i developp my programs more i will provide more licenses if i used more licenses or if in any case i have forgotten to point to a reference of another programmer or another forum then i will update this wip section accordingly)...

stackoverflow forums https://meta.stackexchange.com/questions/12527/do-i-have-to-worry-about-copyright-issues-for-code-posted-on-stack-overflow https://gamedev.stackexchange.com/help/licensing
unity 3d forums https://forum.unity.com/threads/license-on-code-in-forum-posts.714107/ https://unity3d.com/legal/terms-of-service/site-and-communities?_gl=1*1e9e77l*_ga*NTYzMjUzMDUwLjE2MjUwNjkxMjE.*_ga_1S78EFL1W5*MTYyNTI3NjY0NC4zLjAuMTYyNTI3NjY0NC42MA..&_ga=2.158021242.1379019786.1625276645-563253050.1625069121
unifywiki https://wiki.unity3d.com/index.php/Main_Page "Creative Commons Attribution Share Alike."



