# How to use

Copy `Assets/Editor` and `Assets/Editor Default Resources` folders into your project and the built-in Mesh Inspector will be overridden with the updated one. Should work with Editor versions 2019.3 and up.

# What's new in the updated Mesh Inspector
## Mesh Inspector
The top part of the Inspector was updated to display information about mesh data format, memory usage, submeshes etc.

![DataPreview]

## Preview 3D Controls
Middle Mouse Button (Scroll Wheel) click and hold - <b>Pan</b>  
Scroll Wheel - <b>Zoom</b>  
Left Mouse Button - <b>Rotate</b>  
Right Mouse Button - <b>Rotate Directional lights</b>  
F - <b>Focus object</b> (resets pan and zoom, but not rotation)  

The Mesh Preview window allows meshes to be viewed in one of the following ways:

## Shaded

A simple shaded preview of the Mesh.

![ShadedPreview]

When the mesh has multiple sub-meshes, they are tinted with unique colors. The colors are listed in the top inspector part for each submesh:

![SubmeshPreview]

## UV Checker

Previews a Mesh with a checkerboard texture applied. You can use the slider next to UVChannel dropdown to adjust the tiling of the texture.

![CheckerPreview]

## UV Layout

Previews unwrapped UVs. Any UV Channel can be previewed, if available. The view can be zoomed and panned, and initially shows 0..1 UV range.

![LayoutPreview]

## Vertex Color

Previews Vertex Colors of a Mesh.

![VertColorPreview]

## Normals

Preview Mesh Normals. We've opted to not displays normals as tiny vectors poking out of vertices, since it feels like without full-on navigation inside the preview window, they would just be a mess for non-trivial meshes.

![NormalPreview]

## Tangents

Preview Mesh Tangents. We've opted to not displays tangents as tiny vectors poking out of vertices, since it feels like without full-on navigation inside the preview window, they would just be a mess for non-trivial meshes.

![TangentPreview]  

## Blendshapes  

Preview Mesh Blendshapes. An additional drop down is displayed when this option is selected where you can choose which blendshape to display.

![BlendShapePreview]

[DataPreview]: https://i.imgur.com/WYDqCZ2.png
[ShadedPreview]: https://i.imgur.com/fqyTnQd.png
[SubmeshPreview]: https://i.imgur.com/0k6E7R3.png
[CheckerPreview]: https://i.imgur.com/IYIpUBT.png
[LayoutPreview]: https://i.imgur.com/bb1LfuP.png
[VertColorPreview]: https://i.imgur.com/2dwvYLX.png
[NormalPreview]: https://i.imgur.com/0bnRfFs.png
[TangentPreview]: https://i.imgur.com/G30W4LL.png
[BlendShapePreview]: https://i.imgur.com/hsT9XG3.png?1

