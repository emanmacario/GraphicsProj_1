# Project Description

## Terrain

- The landscape is generated at runtime through the diamond square algorithm
- Seven iterations are used. Higher iterations generated more vertices than the mesh vertex limit, leading to artifacts
- The corners of the landscape are initialised to random values
- The mesh generated is linearly interpolated to be within a unit cube with the waterline at height 0
- Colours are assigned to vertices according to their height above the waterline
- Waterline height, the heights for different colours, and the colours themselves are all configurable values
- The code for TerrainGen was developed from a boilerplate adapted from a lab solution for procedurally generating meshes

## Camera

- The player flies around the landscape using FPS style yawing by default
- Flight-sim controls are toggled via the "Flight Sim" options in "Main Camera"
- The camera snaps back within the landscape along the offending axis when it exceeds bounds
- A collision sphere around the camera prevents the player from flying it into the terrain
- The camera is angled by default to show the landscape

## Lighting

- The lighting shader implements Gouraud shading on the Phong illumination model
- It is heavily derived from the given Lab 5 code
- Turned off specular lighting to avoid shiny terrain
- The sun has customisable period and radius of orbit
- Despite appearing close to the landscape, the sun is implemented as a directional light

## Water & Waves

- The water plane is dynamically generated in run-time via the 'WaterGenerator' script
- The waves effect is implemented by a custom unlit HLSL shader
- Compound sine waves are used to displace vertexes in model space
- Two different compound sine waves are used, with varying amplitude and wavelength
- One sine wave is used for each of the x and z axis, respectively
- The phases of the waves changes with respect to time
