using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interface to unify MainScreenManagers. Functions
/// are to be implemented by children (to check screen switching
/// via inspector).
/// 
/// Trigger variables (boolean) will be added in the child for each
/// screen it handles. Each variable should also have a function that
/// calls HideScreens() at the beggining.
/// </summary>
public interface MainScreenManager {

    /// <summary>
    /// Variable to uncheck the trigger variables (implemented by children).
    /// </summary>
    void UncheckAll();
    /// <summary>
    /// Function that disables all screens.
    /// </summary>
    void HideScreens();

}
