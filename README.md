# Project: CARD

**Project: CARD** is a Unity Project which provides functionality to sort playing cards such as ***Shuffle, Straight, Same Kind***, and ***Smart*** in a very efficient way.

The project architecture is separated into three layers. The first layer is the **Core**, which contains the sorting algorithms and session management. The layer has been designed with ***pure C#***, and there isn't any Unity Engine library usage in the layer.

The second layer is the **Game** which contains the required gameplay implementation such as ***Level Management, Transition, Safe Area, TransformLayout, Managers, Asset Container***, and so on.

The last layer is the **Test** which contains unit tests with ***Unity Test Framework***.

### 🛑 Important! The project's entry point is MainScene.

### Optimization

The project also aims to achieve the **best possible optimization.** At this point;

```👉 Used Sprite Atlas to reduce batch counts.```

```👉 Used struct class as much as possible instead of class to use stack memory and avoid heap memory usage.```

```👉 Less new and Instantiate keywords usages for object creation to avoid Garbage Collection.```

```👉 Avoided complex nested algorithms and Linq usages for efficient CPU usage.```

```👉 Avoided object re-instantiation and destroy, used the instantiated objects efficiently.```

```👉 Used String Builder for string concatenations to avoid Garbage Collection.```

```👇 MainScene Batch Count : 4 👇```&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;```
👇 GameScene Batch Count : 5 👇```
<p float="left">
  <img src="https://user-images.githubusercontent.com/39636292/189323217-1228d39c-304a-4746-9bca-af774cc1b095.png" width="405">
  <img src="https://user-images.githubusercontent.com/39636292/189323207-7c1e86f1-49be-4d2b-99b2-ebca6d756288.png" width="405">
</p>

### Techs

```👉 Unity3D v2021.3.5f1``` ```👉 C# 9.0``` ```👉 DOTween v1.2.632``` ```👉 TestFramework v1.1.33```  
```👉 TextMeshPro v3.0.6``` ```👉 SafeAreaHelper v1.0.6``` ```👉 Memory Profiler v0.7.1-preview```

### Support Platforms

```👉 Unity Editor``` ```👉 IOS``` ```👉 Android``` ```👉 macOS``` ```👉 Windows```

## Features
### **Scene Transition**
![Scene Transition](https://user-images.githubusercontent.com/39636292/189327484-50645d00-1b5d-42be-ab70-db01e063be69.gif)

### **Deal Playing Cards**
![Deal Playing Cards](https://github.com/tunchasan/Project-Card/blob/756bf9e8ab1dbb07ac00f09d4f09b226b7968718/Recordings/Gifts/Deal%20Playing%20Cards.gif)

### **Dragging Cards**
![Dragging Cards](https://github.com/tunchasan/Project-Card/blob/756bf9e8ab1dbb07ac00f09d4f09b226b7968718/Recordings/Gifts/Dragging%20Cards.gif)

### **Sorting**
![*Sorting](https://github.com/tunchasan/Project-Card/blob/756bf9e8ab1dbb07ac00f09d4f09b226b7968718/Recordings/Gifts/Sort.gif)

### **Theme**
![Theme](https://github.com/tunchasan/Project-Card/blob/756bf9e8ab1dbb07ac00f09d4f09b226b7968718/Recordings/Gifts/Theme.gif)

### Architecture

![Project Card](https://user-images.githubusercontent.com/39636292/190354011-943bf0ae-5073-4c2e-bb7c-387e86b4418a.png)
