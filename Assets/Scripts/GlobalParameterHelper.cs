using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalParameterHelper
{
    public const float JUMP_XAXIS_BOOST = 10; // amount of force characters recieve when jumping forward/backwards
    public const int INPUT_BUFFER_SIZE = 10; // in fixed update frames, clears all inputs if the last input is not executed in time
    public const float AIR_ACCELERATION_LERP_FACTOR = 0.3f; // range is [1,0] , 1 is instant snap , 0 is no acceleration
}
