Briefly describe overall implementation, esp. Unity terrain generation
"Several paragraphs of text are sufficient, and concise descrips preferred to long descrips"
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~


> Camera

    - The player flies around the landscape using FPS style yawing by default
    - Flight-sim controls are toggled via the "Flight Sim" options in "Main Camera"
    - The camera snaps back within the landscape along the offending axis when it exceeds bounds
    - A collision sphere around the camera prevents the player from flying it into the terrain
    - The camera is angled sharply by default to show the entire landscape

> Lighting

    - The lighting shader implements Gouraud shading on the Phong illumination model
    - The sun has customisable period and radius of orbit


> Water & Waves

    - The water plane is dynamically generated in run-time via the 'WaterGenerator' script
    - The waves effect is implemented by a custom unlit HLSL shader
    - Compound sine waves are used to displace vertexes in model space
    - Two different compound sine waves are used, with varying amplitude and wavelength
    - One sine wave is used for each of the x and z axis, respectively
    - The phases of the waves changes with respect to time
