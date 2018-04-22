using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public interface IState {
    void Enter(); // What it wants to do when it's plugged in
    void Execute(); // What this cartridge continuely does when it's plugged it
    void Exit(); // What happens when this state gets kicked out
}
