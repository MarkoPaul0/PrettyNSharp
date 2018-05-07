# PrettyNSharp

![Author](https://img.shields.io/badge/author-MarkoPaul0-red.svg?style=flat-square)
[![License: GPL v3](https://img.shields.io/badge/License-GPL%20v3-blue.svg?style=flat-square)](https://www.gnu.org/licenses/old-licenses/gpl-3.0.en.html)
![GitHub last commit](https://img.shields.io/github/last-commit/MarkoPaul0/PrettyNSharp.svg?style=flat-square&maxAge=300)

Leverage the power of vector-graphics to create beautiful and scalable custom UI controls for your c#/WPF application. **It's quick, easy, pretty, and always sharp!**

| ![](doc/pns_scalability.gif) |
|:--:| 
| *[PrettyNSharp] Scalable Desgin: no more blurry controls!* |


## How to get started
### To run the demo code with the PrettyNSharp solution
* Open the [PrettyNSharp solution](PrettyNSharp.sln) with VisualStudio
* Run 

### To use PrettyNSharp in your own solution
* Add the PrettyNSharp project file [PrettyNSharp.csproj](prettynsharp/PrettyNSharp.csproj) to your solution (c.f. [Add existing project to solution](https://docs.microsoft.com/en-us/sql/ssms/solution/add-an-existing-project-to-a-solution?view=sql-server-2017))
* Add reference to PrettyNSharp to the project(s) in your solution which will use PrettyNSharp (c.f [Add project reference](https://msdn.microsoft.com/en-us/library/hh708954.aspx))
* (Optional) If you want to use my SVG designs, add a reference to the PrettyNSharp [SVG Dictionary](prettynsharp/SVGLibrary.xaml) in your App.xaml. (c.f. [Use SVG defined in PrettyNSharp](https://github.com/MarkoPaul0/PrettyNSharp/wiki/Use-SVG-designs-defined-in-PrettyNSharp))
* You're ready to rock :thumbsup:

## Usage
The PrettyNSharp contains 3 types of controls: the **SharpDisplay**, the **SharpButton**, and the **SharpCheckbox**.
### The SharpDisplay
The SharpDisplay is a WPF user control which allows you to display SVG data without headache. It does so by exposing the following dependency propeties:
* **Vector**: set an SVG graphic design as your button icon
* **VectorWidth** and **VectorHeight**: set the width and heigh of your button (wich can be a number, Auto, a percentage, or \*)
* **VectorBrush**: fill color of the Vector

### The SharpButton
The SharpButton is a user control deriving from the standard [C#/WPF Button class](https://msdn.microsoft.com/en-us/library/system.windows.controls.button(v=vs.110).aspx). It inherits all of its properties and features with a few added bonuses, which make *a wooOOoooOOoorld of difference, nothing less*. Such added properties include the ones introduced with SharpDisplay but also:
* **HighlightBrush**: fill color of the Vector when the mouse is over
* **BackgroundOnHover** and **BackgroundOnClick**: background color on hover and on click, respectively

### The SharpCheckbox
The SharpCheckbox is a user control deriving from the standard [C#/WPF Checkbox class](https://msdn.microsoft.com/en-us/library/system.windows.controls.checkbox(v=vs.110).aspx). It inherits all of its properties and features with - *guess what* - a few added bonuses. Such added properties include:
* **CheckMark** and **NullMark**: set an SVG graphic design as your mark when IsSet is true and null, repectively
* **MarkBrush** and **MarkHighligh**: set the fill color for the mark in normal conditions and when the mouse is over, respectively
* **BorderOnHover**: set the border color when the mouse is over
* **CornerRadius**: set the border corner radius

<!--
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
-->

## Customize your PrettyNSharp UI controls

PrettyNSharp controls are customizable just like any other WPF control. Here I will showcase a few things you could do to make your PrettyNSharp controls even prettier!

### Example of PrettyNSharp close button

Let's say you want your close button to look like this:

| ![](doc/close_button_demo.gif) |
|:--:| 
| *[PrettyNSharp] A pretty close-button* |

The xaml achieving this *level of prettiness* is as follows:

```xaml
<pns:SharpButton Width="100" BorderThickness="0"
                 Vector="{StaticResource Cross}" VectorHeight="50%" VectorBrush="White" 
                 Background="#E83140" BackgroundOnHover="#E87E87" BackgroundOnClick="#E83140"/>
```

Of course you can also define a style matching the results shown above, allowing you to reuse your close button design very quickly. Your xaml could become as simple as:

```xaml
<pns:SharpButton style={StaticResource CloseButtonStyle}/>
```

### Example of PrettyNSharp checkbox

Let's say you want your checkboxes to look like this:

| ![](doc/radio_checkbox_demo.gif) |
|:--:| 
| *[PrettyNSharp] A pretty checkbox* |

I actually do not recommend that design because it looks like a radio button when it is unchecked (and it is not a radio button, since it is checkbox. *I know, this was hard to follow..*)
In any case, the xaml achieving this *never-seen-before level of beauty* is as follows:

```xaml
<pns:SharpCheckbox BorderThickness="4" Height="50" CornerRadius="25" MarkMargin="5"/>
```

### A neat feature of SharpButton: ContentDisplayType

Since a picture is worth a 1000 words, let's check that one out.

| ![](doc/display_type_button_demo.gif) |
|:--:| 
| *[PrettyNSharp] ContentDisplay Type in SharpButton* |

This was actually a gif, and since it contains 52 frames, I guess it's worth 52,000 words. *How about that..*<br>
As you can see, buttons can be switched between 3 types of "display type":
* **IconOnly**: this is the default value, only the SVG is displayed
* **Both**: both the SVG and the content are diplayed
* **ContentOnly**: only the content is displayed

This is very cool (*I decided so*) if you have an app with a lot of menus and buttons. Once you are familiar with that app you might be ok with Icons only, but for a while you might want to have a look at what they mean without having to wait for the tooltip to show up.

This unparalleled level of refinement was achieved with the following xaml:

```xaml
<Grid >
    <Grid.ColumnDefinitions>
        <ColumnDefinition Width="*"/>
        <ColumnDefinition Width="*"/>
        <ColumnDefinition Width="*"/>
        <ColumnDefinition Width="*"/>
        <ColumnDefinition Width="*"/>
    </Grid.ColumnDefinitions>
    <pns:SharpButton Grid.Column="0" BorderThickness="0,0,1,0"
                     Content="Search" Vector="{StaticResource Magnifier}" 
                     VectorHeight="50%" ContentDisplay="{Binding DisplayType}"/>
    <pns:SharpButton Grid.Column="1" BorderThickness="0,0,1,0"
                     Content="Settings" Vector="{StaticResource Gear}" 
                     VectorHeight="50%" ContentDisplay="{Binding DisplayType}"/>
    <pns:SharpButton Grid.Column="2" BorderThickness="0,0,1,0" 
                     Content="Starred!" Vector="{StaticResource Star}" 
                     VectorHeight="50%" ContentDisplay="{Binding DisplayType}"/>
    <pns:SharpButton Grid.Column="3" BorderThickness="0,0,1,0"
                     Content="Messages" Vector="{StaticResource Mail}" 
                     VectorHeight="50%" ContentDisplay="{Binding DisplayType}"/>
    <pns:SharpButton Grid.Column="4" BorderThickness="0"
                     Vector="{StaticResource Cross}" VectorHeight="50%" 
                     BackgroundOnHover="#E83140" BackgroundOnClick="#E87E87" 
                     Content="Close" ContentDisplay="{Binding DisplayType}"/>
 </Grid>
```


