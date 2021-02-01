using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{


    void Start()
    {
    }


    void ReceiveInput(A classA)
    {
        PrintType<A>();
    }

    void PrintType<T>()
    {
        Debug.Log(" :" + typeof(T));
    }

    bool TryConvert<T>(A input, T output) where T : A
    {
        if (input is T)
        {

            return true;
        }
        return false;
    }
}
public class A {}

public class B : A { }
public class C : A { }

