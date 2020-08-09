*****************************************
*                                       *
* Low Poly Water and Land Generator     *
* Daniel Fairley - poemdexter@gmail.com *
*                                       *
*****************************************

Thank you for choosing the Low Poly Water and Land Generator for your project.  This 
effect is achieved by dynamically creating a mesh where each triangle is a set of 3 
unique vertices.  This gives the triangle plane a normal not shared by any adjacent 
triangle allowing lighting to apply evenly across each triangle independently.

In the 'Low Poly' folder, you'll find 2 prefabs, Water and Land.

The Water prefab has the Mesh Generator script and the Perlin Waves script attached.  The Water prefab creates a 10x10 mesh.  The Perlin Waves script applies Perlin noise and sine waves to each vertex to give the water a subtle wave.  Both of these values can be tweaked to achieve the water look you want.

The Land prefab has just the Mesh Generator script.  The Land prefab creates a 5x5 mesh.
The 'Is Terrain' flag is checked and will run a single pass of perlin noise across all vertices EXCEPT those on the edges of the mesh creating a low poly hill.

For any GameObject with the Mesh Generator script, the 'Save Mesh' box can be checked.  This will save the mesh to your Assets/Models folder for later use.  Because meshes of size 20 or larger can take significant amounts of time to generate, it's a good idea to generate them once and then save them for use later.