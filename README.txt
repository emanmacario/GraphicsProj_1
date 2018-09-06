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
