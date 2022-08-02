# AnimalExplorer.App

This App is intended to explore how to create external User Controls for external assemblies while keeping everything decoupled.
The goal being to display custom User Control, in a WPF app, for a certain external assembly loaded.


## Intended Behaviors:

At boot up, the application is looking in the Animals folder (root/Animals) for any assemblies implementing IAnimal.

The main application then,  let the user create animals from a list of know animal type (from the Animals folder) and set the animal name.
The created animals are then listed and by selecting them, additional specific characteristics should be displayed and could be configured


## Problem to resolve: 

Developers can create any animal using the IAnimal interface and keeping them in class library (UI type agnostic).
However some animals have specific properties that are not cover by the interface and so how to display these properties in the main application without the application having each animals as a reference ?

The ideas would be for the developer to create the User Controls that goes with the Animal Class library so he can customize it to the settings it needs. Ultimately this should be done for all UI type we may encounter (WPF, WinForm, Web...)

Starting with WPF: what is the correct User Control architecture to link a piece of UI to a class library while using Caliburn Micro ? 


## First Trial:
The first trial was to create a WPF class library that provides a View & ViewModel where the Animal class is being injected in the ViewModel constructor. Then required properties are being created (current version does not take care of passing the value back to the class)

Dependency injection is heavily use in this case as it:

 - Discover and register all external assemblies of IAnimal, ViewModels and Views
 - Uses factories to resolve an Animal creation, the ViewModel and Views for the type of Animal created
 
 Naming convention is used to being able to link the right Settings to the Animal classes. 

**Issue:** *Feels very clunky ... Must be a better way*


**Must Use:** 
 - .Net Framework 4.8
 -  Caliburn Micro latest release
 -  Castle Windsor latest release
