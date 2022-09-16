# Parallax Effect using UnityEngine

![Parallax_Effect](https://user-images.githubusercontent.com/105242009/182032242-0a8eb5d1-fb1f-4cea-a148-94a06e9ce31e.gif)



## What is Parallax Effect?

- One could say it's something like an **optical illusion**. When driving a car through a highway, all the close trees/buildings seem to just _fly by really fast_, but the far mountains/forests are _barely moving_. That's basically what parallax effect is: ***moving objects with different speed based on their distance to the camera***.

## Usage of Parallax Effect

- A lot of **big companies** use this effect on their _websites/mobile apps_
- 2D video games (e.g. **_Terraria_**)
- Product advertisements

## Implementation

- First of all we need to get the starting position and the size of our image layer:
``` c#
  startPos = transform.position.x;
  length = GetComponent<SpriteRenderer>().bounds.size.x;
```

- Every frame the layer is moved to a new position:
``` c#
  float distance = mainCamera.transform.position.x * parallax;

  //  Setting position
  transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);
```

- The `parallax` variable determines how fast it's moving:
``` c#
  //  Defines how much of the
  //      effect is to be applied...
  //  - 0 --> biggest effect
  //  - 1 --> no effect
  public float parallax;
```

- At last we need to shift the layer if it went out of bounds:
```
  //  Updating bounds
  if (temp > startPos + length)
    startPos += length;
  if (temp < startPos-length)
    startPos -= length;
 ```
 
