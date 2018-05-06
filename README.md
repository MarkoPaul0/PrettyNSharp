# PrettyNSharp

![Author](https://img.shields.io/badge/author-MarkoPaul0-red.svg?style=flat-square)
[![License: GPL v3](https://img.shields.io/badge/License-GPL%20v3-blue.svg?style=flat-square)](https://www.gnu.org/licenses/old-licenses/gpl-3.0.en.html)
![GitHub last commit](https://img.shields.io/github/last-commit/MarkoPaul0/PrettyNSharp.svg?style=flat-square&maxAge=300)

Create beautiful and scalable custom UI controls for your c#/WPF application. **It's quick, easy, pretty, and always sharp!**

| ![](doc/pns_scalability.gif) |
|:--:| 
| *[PrettyNSharp] Scalable Desgin: no more blurry controls!* |

## How to get started
### To run the demo code with the PrettyNSharp solution
* Open the [PrettyNSharp solution](PrettyNSharp.sln) with VisualStudio
* Run 

### To use PrettyNSharp in your own solution
* Add [PrettyNSharp project file](prettynsharp/PrettyNSharp.csproj) to your solution
* Add reference to PrettyNSharp to the project(s) which will use PrettyNSharp
* (Optional) If you want to use my SVG designs, add a reference to the PrettyNSharp [SVG Dictionary](prettynsharp/SVGLibrary.xaml) in your App.xaml.
* You're ready to rock

## Examples:
### Create a PrettyNSharp button, aka. SharpButton

```xaml
<pns:SharpButton Height="25"  Width="50"  Vector="{StaticResource Gear}" VectorHeight="50%"/>
<pns:SharpButton Height="50"  Width="100" Vector="{StaticResource Mail}" VectorHeight="50%"/>
<pns:SharpButton Height="100" Width="200" Vector="{StaticResource Star}" VectorHeight="50%"/>
```
This gives you the following:

| ![](doc/how_to_star.png) |
|:--:| 
| *[PrettyNSharp] That's how you star a repo by the way ;)* |





